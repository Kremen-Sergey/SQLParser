using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BLL.Interface;

namespace BLL
{
    public class Parser : IParserMethods
    {
        //private class Param
        //{
        //    public String ParamName { get; set; }
        //    public String ParamType { get; set; }
        //    private string paramValue;
        //    public String ParamValue { get
        //    {
        //        if (ParamType.Equals("String") || (ParamType.Equals("DateTime")))
        //            return "'" + paramValue + "'";
        //        else
        //            return paramValue;
        //    } set { paramValue = value; } }
        //}

        //private List<Param> paramsList;

        //private void ParseParams(string paramsString)
        //{
        //    string DBNullPattern=@"((?<paramName>[a-zA-Z0-9_]+)(?<equal>:\s+)(?<paramValue>DBNull)(?<comma>,*))";
        //    string paramNamePpattern = @"((?<paramName>[a-zA-Z0-9_]+)(?<openBracket>\()";
        //    string paramTypePattern = @"(?<openBracket>\()(?<paramType>[a-zA-Z0-9]+)(?<closeBracket>\))";
        //    string paramValuePattern =@"(?<equal>:\s+)(?<openQuotes>')(?<paramValue>[^\']+)(?<closeQuotes>')(?<comma>,*))";
        //    Regex DBNullRegexRegex = new Regex(DBNullPattern);
        //    Regex paramNameRegex = new Regex(paramNamePpattern);
        //    Regex paramTypeRegex = new Regex(paramTypePattern);
        //    Regex paramValueRegex = new Regex(paramValuePattern);
        //    string[] paramsArray=paramsString.Split(',');
        //    foreach (string param in paramsArray)
        //    {
        //        //Match match in paramNameRegex.Matches(param)
        //    }
        //}
        //All reserved Keywords (Transact-SQL)
        private readonly string[] allKeyWordList = { "ADD", "ALL", "ALTER", "AND", "ANY", "AS", "ASC", "AUTHORIZATION", "BACKUP", 
        "BEGIN", "BETWEEN", "BREAK", "BROWSE", "BULK", "BY", "CASCADE", "CASE", "CHECK", "CHECKPOINT", "CLOSE", "CLUSTERED", 
        "COALESCE", "COLLATE", "COLUMN", "COMMIT", "COMPUTE", "CONSTRAINT", "CONTAINS", "CONTAINSTABLE", "CONTINUE", "CONVERT", 
        "CREATE", "CROSS", "CURRENT", "CURRENT_DATE", "CURRENT_TIME", "CURRENT_TIMESTAMP", "CURRENT_USER", "CURSOR", "DATABASE", 
        "DBCC", "DEALLOCATE", "DECLARE", "DEFAULT", "DELETE", "DENY", "DESC", "DISK", "DISTINCT", "DISTRIBUTED", "DOUBLE", "DROP", 
        "DUMP", "ELSE", "END", "ERRLVL", "ESCAPE", "EXCEPT", "EXEC", "EXECUTE", "EXISTS", "EXIT", "EXTERNAL", "FETCH", "FILE", 
        "FILLFACTOR", "FOR", "FOREIGN", "FREETEXT", "FREETEXTTABLE", "FROM", "FULL", "FUNCTION", "GOTO", "GRANT", "GROUP", "HAVING", 
        "HOLDLOCK", "IDENTITY", "IDENTITY_INSERT", "IDENTITYCOL", "IF", "IN", "INDEX", "INNER", "INSERT", "INTERSECT", "INTO", "IS", 
        "JOIN", "KEY", "KILL", "LEFT", "LIKE", "LINENO", "LOAD", "MERGE", "NATIONAL", "NOCHECK", "NONCLUSTERED", "NOT", "NULL", 
        "NULLIF", "OF", "OFF", "OFFSETS", "ON", "OPEN", "OPENDATASOURCE", "OPENQUERY", "OPENROWSET", "OPENXML", "OPTION", "OR", 
        "ORDER", "OUTER", "OVER", "PERCENT", "PIVOT", "PLAN", "PRECISION", "PRIMARY", "PRINT", "PROC", "PROCEDURE", "PUBLIC", 
        "RAISERROR", "READ", "READTEXT", "RECONFIGURE", "REFERENCES", "REPLICATION", "RESTORE", "RESTRICT", "RETURN", "REVERT", 
        "REVOKE", "RIGHT", "ROLLBACK", "ROWCOUNT", "ROWGUIDCOL", "RULE", "SAVE", "SCHEMA", "SECURITYAUDIT", "SELECT", 
        "SEMANTICKEYPHRASETABLE", "SEMANTICSIMILARITYDETAILSTABLE", "SEMANTICSIMILARITYTABLE", "SESSION_USER", "SET", "SETUSER", 
        "SHUTDOWN", "SOME", "STATISTICS", "SYSTEM_USER", "TABLE", "TABLESAMPLE", "TEXTSIZE", "THEN", "TO", "TOP", "TRAN", 
        "TRANSACTION", "TRIGGER", "TRUNCATE", "TRY_CONVERT", "TSEQUAL", "UNION", "UNIQUE", "UNPIVOT", "UPDATE", "UPDATETEXT", "USE", 
        "USER", "VALUES", "VARYING", "VIEW", "WAITFOR", "WHEN", "WHERE", "WHILE", "WITH", "WITHIN GROUP", "WRITETEXT" };

        //Keywords must be written from new line
        private readonly string[] newLineKeyWordList ={"SELECT","FROM","WHERE","INNER JOIN", "OUTER JOIN","LEFT JOIN", "RIGHT JOIN", "ORDER BY", "HAVING","GROUP BY"};

        public string Output(string request, string parameters)
        {
            //Error if request string is empty
            if (String.IsNullOrEmpty(request))
            {
                throw new ArgumentException("Request field is Empty");
            }
            //Eror if parameters string is empty and parameters in request are needed
            string testResultPattern = @"\B@[a-zA-Z0-9_]+\b";//pattern for parameters in request string
            Regex testResultRegex = new Regex(testResultPattern);
            if ((String.IsNullOrEmpty(parameters)) && (testResultRegex.Matches(request).Count != 0))
            {
                throw new ArgumentException("Parameters field is empty");
            }
            //Key words to upper
            request = KeyWordsToUpper(request);
            //Logic of replace
            string pattern = @"((?<paramName>[a-zA-Z0-9_]+)(?<openBracket>\()(?<paramType>[a-zA-Z0-9]+)(?<closeBracket>\))(?<equal>:\s+)(?<openQuotes>')(?<paramValue>[^\']+)(?<closeQuotes>')(?<comma>,*))"+
                @"|((?<paramName>[a-zA-Z0-9_]+)(?<equal>:\s+)(?<paramValue>DBNull)(?<comma>,*))";
            Regex regex = new Regex(pattern);
            foreach (Match match in regex.Matches(parameters))
            {
                if ((match.Groups["paramType"].Value.Equals("String")) || (match.Groups["paramType"].Value.Equals("DateTime")))
                    request = Regex.Replace(request, @"\B@" + match.Groups["paramName"].Value + @"\b","'"+ match.Groups["paramValue"].Value+"'");
                else
                    request = Regex.Replace(request, @"\B@" + match.Groups["paramName"].Value + @"\b", match.Groups["paramValue"].Value); 
            }
            //Error if for some parameters in request were not found values in parameters
            if (testResultRegex.Matches(request).Count != 0)
            {
                StringBuilder errorMessage =new StringBuilder("Error occured while processing request. Values for next parameters were not found: \n");
                foreach (Match match in testResultRegex.Matches(request))
                {
                    errorMessage.Append(match.Groups[0].Value + "\n");
                }
                throw new ArgumentException(errorMessage.ToString());
            }
            
            return request;
        }

        public string Format(string sourceString)
        {
            if (String.IsNullOrEmpty(sourceString))
            {
                throw new ArgumentException("Sorce string is empty");
            }
            string newLine = String.Format("{0}", Environment.NewLine);
            foreach (var item in newLineKeyWordList)
            {      
               // string newLineStr = String.Format("{0}{1}{2}", newLine, item.ToUpper(), " ");
                string newLineStr = String.Format("{0}{1}", newLine, item.ToUpper());
                sourceString = Regex.Replace(sourceString, @"[^\^]\b"+item+@"\b", newLineStr,RegexOptions.IgnoreCase);
            }
            sourceString = Regex.Replace(sourceString, newLine+@"\s*"+newLine, newLine);
            return sourceString;
        }

        //private string[] DeleteSpace(params string[] keyWordsList)
        //{
        //    for (var i = 0; i < keyWordsList.Length; i++)
        //    {
        //        keyWordsList[i] = keyWordsList[i].Trim();
        //        keyWordsList[i]=Regex.Replace(keyWordsList[i], "\\s+", " ");
        //    }
        //    return keyWordsList;
        //}

        public string NumerateParamsForSelectKeyword(string sourceString)
        {
            string newLine = String.Format("{0}", Environment.NewLine);
            return NumerateParams(sourceString, @"(select)(.+)([" + newLine + "]*from)");
        }

        public string NumerateParamsForInsertKeyword(string sourceString)
        {
            return NumerateParams(sourceString, @"(insert\s+into\s+\w+\s+\()(.+)(\)\s+values)");
        }

        public string NumerateParams(string sourceString, string pattern)
        {
            Regex regex = new Regex(pattern,RegexOptions.IgnoreCase);
            Match match = regex.Match(sourceString);
            string res = match.Groups[2].Value;
            string patternWords = @"(?<words>[\w.]+)";
            Regex regexWords = new Regex(patternWords);
            int i = 0;
            foreach (Match match1 in regexWords.Matches(res))
            {
                if (!InKeyWords(match1.Groups["words"].ToString()))
                {
                    res = Regex.Replace(res, @"\b" + match1.Groups["words"].ToString() + @"\b",
                        match1.Groups["words"].ToString() + "/*" + i + "*/");
                    i++;
                }
            }
            sourceString = Regex.Replace(sourceString, match.Groups[2].Value, res);
            return sourceString;
        }

        public string NumerateParams(string sourceString)
        {
            sourceString = NumerateParamsForInsertKeyword(sourceString);
            sourceString = NumerateParamsForSelectKeyword(sourceString);
            return sourceString;

        }
        public bool InKeyWords(string word)
        {
            bool isKeyWord = false;
            return allKeyWordList.Contains(word.ToUpper());

        }

        public string KeyWordsToUpper(string request)
        {
            string pattern = @"([\w.]+)"; //@"(^|\b)([A-Za-z_]+)($|\b)";
            Regex regex = new Regex(pattern);
            foreach (Match match in regex.Matches(request))
            {
                if (InKeyWords(match.Groups[1].Value))
                {
                    request = Regex.Replace(request, @"\b" + match.Groups[1].Value + @"\b", match.Groups[1].Value.ToUpper()); 
                }
                //foreach (var item in allKeyWordList)
                //{
                //    if (item.ToUpper() == match.Groups[1].Value.ToUpper())
                //    {
                //        request = Regex.Replace(request, @"\b"+match.Groups[1].Value+@"\b", match.Groups[1].Value.ToUpper());
                //      //  request = Regex.Replace(request, @"\b" + match.Groups[2].Value + @"\b", match.Groups[2].Value.ToUpper());
                //    }
                //}
            }
            return request;
        }
    }  
}
