using System;
using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        Parser methods=new Parser();
        [TestMethod]
        public void OutputTestMethod1()
        {
            // arrange
            string request = "Select o.owner_id, o.delflag , d.vc, d.is_freezed, d.is_file_locked, d.is_prop_locked, d.is_old_version, d.version_nr from dox_docs d inner join dox_obj o on d.doc_id = o.obj_id where doc_id = @doc_id1 qqq";
            string parameters = "doc_id1(Int64): '137'";
            string expected = "SELECT o.owner_id, o.delflag , d.vc, d.is_freezed, d.is_file_locked, d.is_prop_locked, d.is_old_version, d.version_nr FROM dox_docs d INNER JOIN dox_obj o ON d.doc_id = o.obj_id WHERE doc_id = 137 qqq";
            // act
            String actual=methods.Output(request, parameters);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutputTestMethod2()
        {
            // arrange
            string request = "Select distinct g.prop_group_id, n1.name, n2.name, g.vc, g.tabular, g.dependent_cat, o.orig_obj_id, t.prop_id, n3.name, n4.name, t.value_type, t.max_len, t.input_mask, mg.flag_required, mg.flag_unique, mg.flag_main_value, mg.ref_table_id, t.sel_list_type, ot.orig_obj_id, mg.lfd, g.is_system, t.multi_val_possible, t.is_status, mg.multi_val, mg.filename_value, mg.docname_value,  mg.flag_hidden, mg.flag_readonly, mg.flag_statistic, mg.flag_search_value, mg.default_value,  mg.flag_new_version_on_change  from dox_prop_groups g left join dox_prop_group_members mg on mg.prop_group_id = g.prop_group_id left join dox_prop_types t on mg.prop_id = t.prop_id inner join dox_obj o on o.obj_id = g.prop_group_id and o.delflag = @o_delflag12 and o.subscriber_id = @o_subscriber_id13 left join dox_obj ot on ot.obj_id = t.prop_id inner join dox_names n1 on n1.obj_id = g.prop_group_id and n1.lang_id = @n1_lang_id4  inner join dox_names n2 on n2.obj_id = g.prop_group_id and n2.lang_id = @n2_lang_id5 left join dox_names n3 on n3.obj_id = t.prop_id and n3.lang_id = @n3_lang_id6 left join dox_names n4 on n4.obj_id = t.prop_id and n4.lang_id = @n4_lang_id7  where  g.dependent_cat = @g_dependent_cat1  order by g.is_system, n1.name, mg.lfd";
            string parameters = "g_dependent_cat1(Int32): '0', o_delflag12(Int32): '0', o_subscriber_id13(Int64): '2', n1_lang_id4(String): 'en_EN', n2_lang_id5(String): 'Sql', n3_lang_id6(String): 'en_EN', n4_lang_id7(String): 'Sql'";
            string expected = "SELECT DISTINCT g.prop_group_id, n1.name, n2.name, g.vc, g.tabular, g.dependent_cat, o.orig_obj_id, t.prop_id, n3.name, n4.name, t.value_type, t.max_len, t.input_mask, mg.flag_required, mg.flag_unique, mg.flag_main_value, mg.ref_table_id, t.sel_list_type, ot.orig_obj_id, mg.lfd, g.is_system, t.multi_val_possible, t.is_status, mg.multi_val, mg.filename_value, mg.docname_value,  mg.flag_hidden, mg.flag_readonly, mg.flag_statistic, mg.flag_search_value, mg.default_value,  mg.flag_new_version_on_change  FROM dox_prop_groups g LEFT JOIN dox_prop_group_members mg ON mg.prop_group_id = g.prop_group_id LEFT JOIN dox_prop_types t ON mg.prop_id = t.prop_id INNER JOIN dox_obj o ON o.obj_id = g.prop_group_id AND o.delflag = 0 AND o.subscriber_id = 2 LEFT JOIN dox_obj ot ON ot.obj_id = t.prop_id INNER JOIN dox_names n1 ON n1.obj_id = g.prop_group_id AND n1.lang_id = 'en_EN'  INNER JOIN dox_names n2 ON n2.obj_id = g.prop_group_id AND n2.lang_id = 'Sql' LEFT JOIN dox_names n3 ON n3.obj_id = t.prop_id AND n3.lang_id = 'en_EN' LEFT JOIN dox_names n4 ON n4.obj_id = t.prop_id AND n4.lang_id = 'Sql'  WHERE  g.dependent_cat = 0  ORDER BY g.is_system, n1.name, mg.lfd";
            // act
            String actual = methods.Output(request, parameters);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutputTestMethod3()
        {
            // arrange
            string request = "Select  distinct no.name, f.folder_id   from dox_folders f  INNER JOIN dox_folder_members fm ON f.folder_id = fm.folder_id INNER JOIN dox_obj om ON om.obj_id = fm.obj_id and om.obj_type <> @om_obj_type11 left join dox_names no on no.obj_id = f.folder_id and no.lang_id = @no_lang_id12 inner join dox_obj o on o.obj_id = f.folder_id and o.delflag = @o_delflag13 and o.subscriber_id = @o_subscriber_id14 and f.main_folder = @f_main_folder5  order by no.name";
            string parameters = "om_obj_type11(Int32): '13', no_lang_id12(String): 'en_EN', o_delflag13(Int32): '0', o_subscriber_id14(Int64): '2', f_main_folder5(Int32): '0',";
            string expected = "SELECT  DISTINCT no.name, f.folder_id   FROM dox_folders f  INNER JOIN dox_folder_members fm ON f.folder_id = fm.folder_id INNER JOIN dox_obj om ON om.obj_id = fm.obj_id AND om.obj_type <> 13 LEFT JOIN dox_names no ON no.obj_id = f.folder_id AND no.lang_id = 'en_EN' INNER JOIN dox_obj o ON o.obj_id = f.folder_id AND o.delflag = 0 AND o.subscriber_id = 2 AND f.main_folder = 0  ORDER BY no.name";
            // act
            String actual = methods.Output(request, parameters);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutputTestMethod4()
        {
            // arrange
            string request = "insert into dox_obj (intern_system_id, subscriber_id, creator_id, mod_id, obj_type, owner_id, orig_obj_id, creation_date_str, mod_date, mod_date_str, delflag, obj_group_id, export_state) values (@intern_system_id, @subscriber_id, @creator_id, @mod_id, @obj_type, @owner_id, @orig_obj_id, @creation_date_str, @mod_date, @mod_date_str, @delflag, @obj_group_id, @export_state)";
            string parameters ="intern_system_id(Int32): '1',\n\nsubscriber_id(Int64): '2', creator_id(Int64): '22', mod_id(Int64): '22', mod_date(DateTime): '21.04.2015 1:22:40', obj_type(Int32): '22', owner_id(Int64): '22', orig_obj_id: DBNull, creation_date_str(String): '20150421', mod_date_str(String): '20150421', delflag(Int32): '0', obj_group_id(Int64): '0', export_state(Int32): '0', ";
            string expected = "INSERT INTO dox_obj (intern_system_id, subscriber_id, creator_id, mod_id, obj_type, owner_id, orig_obj_id, creation_date_str, mod_date, mod_date_str, delflag, obj_group_id, export_state) VALUES (1, 2, 22, 22, 22, 22, DBNull, '20150421', '21.04.2015 1:22:40', '20150421', 0, 0, 0)";
            // act
            String actual = methods.Output(request, parameters);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutputTestMethod5()
        {
            // arrange
            string request = "update dox_prop_group_members set flag_hidden = @flag_hidden1 where prop_id in  (select obj_id from dox_obj where orig_obj_id = @orig_obj_id2)";
            string parameters ="flag_hidden1(Int32): '1', orig_obj_id2(String): 'aaaabbbb-ffff-0000-ffff-ccccddddeeee/411', ";
            string expected = "UPDATE dox_prop_group_members SET flag_hidden = 1 WHERE prop_id IN  (SELECT obj_id FROM dox_obj WHERE orig_obj_id = 'aaaabbbb-ffff-0000-ffff-ccccddddeeee/411')";
            // act
            String actual = methods.Output(request, parameters);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutputTestMethod6()
        {
            // arrange
            string request = "SELECT DISTINCT dox_obj.obj_id,dox_obj.obj_type,dox_obj.creation_date,dox_obj.creator_id,dox_obj.mod_date,dox_obj.mod_id,dox_obj.owner_id,dox_obj.export_state,dox_folder_members.folder_id,dox_docs.is_freezed,dox_docs.is_file_locked,dox_docs.is_prop_locked,dox_docs.is_old_version,dox_docs.need_new_version,dox_docs.version_nr,dox_docs.vc,dox_docs.file_size,dox_docs.mime_type_id,dox_docs.compound_name,dox_docs.frame_count,dox_docs.has_pdf_preview,dox_docs.doc_ident,dox_docs.extern_doc_ident,dox_docs.orig_version  FROM dox_obj INNER JOIN dox_folder_members  ON dox_obj.obj_id = dox_folder_members.obj_id INNER JOIN dox_docs  ON dox_obj.obj_id = dox_docs.doc_id INNER JOIN dox_doc_ft_all  ON dox_obj.obj_id = dox_doc_ft_all.doc_id  WHERE  ((dox_folder_members.is_link = @p1_dox_fo) AND (dox_docs.is_old_version = @p2_dox_do) AND (dox_obj.obj_type = @p3_dox_ob) AND (dox_obj.subscriber_id = @p4_dox_ob) AND (dox_obj.delflag = @p5_dox_ob))  AND ( CONTAINS (dox_doc_ft_all.ft_content, '\"testsearch*\"')) ORDER BY dox_obj.creation_date DESC ";
            string parameters = "@p1_dox_fo:(Int32): 0, @p2_dox_do:(Int32): 0, @p3_dox_ob:(Int32): 12, @p4_dox_ob:(Int64): 2, @p5_dox_ob:(Int32): 0, ";
            string expected = "Error occured while processing request. Values for next parameters were not found: \n@p1_dox_fo\n@p2_dox_do\n@p3_dox_ob\n@p4_dox_ob\n@p5_dox_ob\n";
            // act
            string actual = "";
            try
            {
                String result = methods.Output(request, parameters);
            }
            catch (ArgumentException ex)
            {
                actual = ex.Message;
            }
            // assert
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void DeleteSpaceTestMethod1()
        //{
        //    // arrange
        //    string[] keyWordList = { "from    ", "    where", "inner    join", " outer  join ", "left   join", "  order", "and          " };
        //    string[] expected = { "from", "where", "inner join", "outer join", "left join", "order", "and" };
        //    // act
        //    String[] actual = methods.DeleteSpace(keyWordList);
        //    // assert
        //    for (int i = 0; i < keyWordList.Length; i++)
        //    {
        //        Assert.AreEqual(expected[i], actual[i]);
        //    }
        //}

        [TestMethod]
        public void FormatTestMethod1()
        {
            string newLine = String.Format("{0}", Environment.NewLine);
            // arrange
            string[] keyWordList = { "SELECT", "FROM", "WHERE", "INNER JOIN", "OUTER JOIN", "LEFT JOIN", "RIGHT JOIN", "ORDER BY", "HAVING", "GROUP BY" };
            string request = "Select o.owner_id, o.delflag , d.vc, d.is_freezed, d.is_file_locked, d.is_prop_locked, d.is_old_version, d.version_nr "+newLine+newLine+" from dox_docs d inner join dox_obj o on d.doc_id = o.obj_id  where doc_id = 137";
            string expected = "Select o.owner_id, o.delflag , d.vc, d.is_freezed, d.is_file_locked, d.is_prop_locked, d.is_old_version, d.version_nr " + newLine + "FROM dox_docs d" + newLine + "INNER JOIN dox_obj o on d.doc_id = o.obj_id " + newLine + "WHERE doc_id = 137";
            // act
            String actual = methods.Format(request);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddFieldNumberSelectTestMethod1()
        {
            // arrange
            string request = "select distinct o.owner_id, id , d.vc, d.is_freezed, d.is_file_locked, d.is_prop_locked, d.is_old_version, d.version_nr from dox_docs d inner join dox_obj o on d.doc_id = o.obj_id where doc_id = @doc_id1";
            string expected = "select distinct o.owner_id/*0*/, id/*1*/ , d.vc/*2*/, d.is_freezed/*3*/, d.is_file_locked/*4*/, d.is_prop_locked/*5*/, d.is_old_version/*6*/, d.version_nr/*7*/ from dox_docs d inner join dox_obj o on d.doc_id = o.obj_id where doc_id = @doc_id1";
            // act
            String actual = methods.NumerateParamsForSelectKeyword(request);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddFieldNumberInsertTestMethod1()
        {
            // arrange
            string request = "Insert into dox_obj (intern_system_id, id, creator_id, mod_id, obj_type, owner_id, orig_obj_id, creation_date_str, mod_date, mod_date_str, delflag, obj_group_id, export_state) values (@intern_system_id, @subscriber_id, @creator_id, @mod_id, @obj_type, @owner_id, @orig_obj_id, @creation_date_str, @mod_date, @mod_date_str, @delflag, @obj_group_id, @export_state)";
            string expected = "Insert into dox_obj (intern_system_id/*0*/, id/*1*/, creator_id/*2*/, mod_id/*3*/, obj_type/*4*/, owner_id/*5*/, orig_obj_id/*6*/, creation_date_str/*7*/, mod_date/*8*/, mod_date_str/*9*/, delflag/*10*/, obj_group_id/*11*/, export_state/*12*/) values (@intern_system_id, @subscriber_id, @creator_id, @mod_id, @obj_type, @owner_id, @orig_obj_id, @creation_date_str, @mod_date, @mod_date_str, @delflag, @obj_group_id, @export_state)";
            // act
            String actual = methods.NumerateParamsForInsertKeyword(request);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        //[DeploymentItem("DataSourceForOutoutMethodDDT.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        "|DataDirectory|\\DataSourceForOutoutMethodDDT.xml", "testData", DataAccessMethod.Sequential)]
        public void OutputDDTestMethod()
        {
        string request = TestContext.DataRow["requestValue"].ToString();
        string parameters = TestContext.DataRow["parametersValue"].ToString();
        string expected = TestContext.DataRow["expectedValue"].ToString();
        string actual = methods.Output(request, parameters);
        Assert.AreEqual(expected, actual);
    }

        [TestMethod]
        public void KeyWordsToUpperTestMethod()
        {
            // arrange
            string request = "Insert into dox_obj (intern_system_id, id, creator_id, mod_id, obj_type, owner_id, orig_obj_id, creation_date_str, mod_date, mod_date_str, delflag, obj_group_id, export_state) values (@intern_system_id, @subscriber_id, @creator_id, @mod_id, @obj_type, @owner_id, @orig_obj_id, @creation_date_str, @mod_date, @mod_date_str, @delflag, @obj_group_id, @export_state)";
            string expected ="INSERT INTO dox_obj (intern_system_id, id, creator_id, mod_id, obj_type, owner_id, orig_obj_id, creation_date_str, mod_date, mod_date_str, delflag, obj_group_id, export_state) VALUES (@intern_system_id, @subscriber_id, @creator_id, @mod_id, @obj_type, @owner_id, @orig_obj_id, @creation_date_str, @mod_date, @mod_date_str, @delflag, @obj_group_id, @export_state)";
            // act
            String actual = methods.KeyWordsToUpper(request);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutputTestMethodRequestNull()
        {
            // arrange
            string request = null;
            string parameters = "@p1_dox_fo:(Int32): 0, @p2_dox_do:(Int32): 0, @p3_dox_ob:(Int32): 12, @p4_dox_ob:(Int64): 2, @p5_dox_ob:(Int32): 0, ";
            string expected = "Request field is Empty";
            // act
            string actual = "";
            try
            {
                String result = methods.Output(request, parameters);
            }
            catch (ArgumentException ex)
            {
                actual = ex.Message;
            }
            // assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void OutputTestMethodRequestIsEmpty()
        {
            // arrange
            string request = "";
            string parameters = "@p1_dox_fo:(Int32): 0, @p2_dox_do:(Int32): 0, @p3_dox_ob:(Int32): 12, @p4_dox_ob:(Int64): 2, @p5_dox_ob:(Int32): 0, ";
            string expected = "Request field is Empty";
            // act
            string actual = "";
            try
            {
                String result = methods.Output(request, parameters);
            }
            catch (ArgumentException ex)
            {
                actual = ex.Message;
            }
            // assert
            Assert.AreEqual(expected, actual);
        }


       [TestMethod]
        public void OutputTestMethodParametersIsEmpty()
        {
            // arrange
            string request = "SELECT DISTINCT dox_obj.obj_id,dox_obj.obj_type,dox_obj.creation_date,dox_obj.creator_id,dox_obj.mod_date,dox_obj.mod_id,dox_obj.owner_id,dox_obj.export_state,dox_folder_members.folder_id,dox_docs.is_freezed,dox_docs.is_file_locked,dox_docs.is_prop_locked,dox_docs.is_old_version,dox_docs.need_new_version,dox_docs.version_nr,dox_docs.vc,dox_docs.file_size,dox_docs.mime_type_id,dox_docs.compound_name,dox_docs.frame_count,dox_docs.has_pdf_preview,dox_docs.doc_ident,dox_docs.extern_doc_ident,dox_docs.orig_version  FROM dox_obj INNER JOIN dox_folder_members  ON dox_obj.obj_id = dox_folder_members.obj_id INNER JOIN dox_docs  ON dox_obj.obj_id = dox_docs.doc_id INNER JOIN dox_doc_ft_all  ON dox_obj.obj_id = dox_doc_ft_all.doc_id  WHERE  ((dox_folder_members.is_link = @p1_dox_fo) AND (dox_docs.is_old_version = @p2_dox_do) AND (dox_obj.obj_type = @p3_dox_ob) AND (dox_obj.subscriber_id = @p4_dox_ob) AND (dox_obj.delflag = @p5_dox_ob))  AND ( CONTAINS (dox_doc_ft_all.ft_content, '\"testsearch*\"')) ORDER BY dox_obj.creation_date DESC ";
            string expected = "Parameters field is empty";
            // act
            string actual = "";
            try
            {
                String result = methods.Output(request, "");
            }
            catch (ArgumentException ex)
            {
                actual = ex.Message;
            }
            // assert
            Assert.AreEqual(expected, actual);
        }

       [TestMethod]
       public void OutputTestMethodParametersIsNull()
       {
           // arrange
           string request = "SELECT DISTINCT dox_obj.obj_id,dox_obj.obj_type,dox_obj.creation_date,dox_obj.creator_id,dox_obj.mod_date,dox_obj.mod_id,dox_obj.owner_id,dox_obj.export_state,dox_folder_members.folder_id,dox_docs.is_freezed,dox_docs.is_file_locked,dox_docs.is_prop_locked,dox_docs.is_old_version,dox_docs.need_new_version,dox_docs.version_nr,dox_docs.vc,dox_docs.file_size,dox_docs.mime_type_id,dox_docs.compound_name,dox_docs.frame_count,dox_docs.has_pdf_preview,dox_docs.doc_ident,dox_docs.extern_doc_ident,dox_docs.orig_version  FROM dox_obj INNER JOIN dox_folder_members  ON dox_obj.obj_id = dox_folder_members.obj_id INNER JOIN dox_docs  ON dox_obj.obj_id = dox_docs.doc_id INNER JOIN dox_doc_ft_all  ON dox_obj.obj_id = dox_doc_ft_all.doc_id  WHERE  ((dox_folder_members.is_link = @p1_dox_fo) AND (dox_docs.is_old_version = @p2_dox_do) AND (dox_obj.obj_type = @p3_dox_ob) AND (dox_obj.subscriber_id = @p4_dox_ob) AND (dox_obj.delflag = @p5_dox_ob))  AND ( CONTAINS (dox_doc_ft_all.ft_content, '\"testsearch*\"')) ORDER BY dox_obj.creation_date DESC ";
           string expected = "Parameters field is empty";
           // act
           string actual = "";
           try
           {
               String result = methods.Output(request, null);
           }
           catch (ArgumentException ex)
           {
               actual = ex.Message;
           }
           // assert
           Assert.AreEqual(expected, actual);
       }


       [TestMethod]
       public void OutputTestMethodParametersNoExist()
       {
           // arrange
           string request = "SELECT DISTINCT dox_obj.obj_id,dox_obj.obj_type,dox_obj.creation_date,dox_obj.creator_id,dox_obj.mod_date,dox_obj.mod_id,dox_obj.owner_id,dox_obj.export_state,dox_folder_members.folder_id,dox_docs.is_freezed,dox_docs.is_file_locked,dox_docs.is_prop_locked,dox_docs.is_old_version,dox_docs.need_new_version,dox_docs.version_nr,dox_docs.vc,dox_docs.file_size,dox_docs.mime_type_id,dox_docs.compound_name,dox_docs.frame_count,dox_docs.has_pdf_preview,dox_docs.doc_ident,dox_docs.extern_doc_ident,dox_docs.orig_version  FROM dox_obj INNER JOIN dox_folder_members  ON dox_obj.obj_id = dox_folder_members.obj_id INNER JOIN dox_docs  ON dox_obj.obj_id = dox_docs.doc_id INNER JOIN dox_doc_ft_all  ON dox_obj.obj_id = dox_doc_ft_all.doc_id  WHERE  ((dox_folder_members.is_link = @p1_dox_fo) AND (dox_docs.is_old_version = @p2_dox_do) AND (dox_obj.obj_type = @p3_dox_ob) AND (dox_obj.subscriber_id = @p4_dox_ob) AND (dox_obj.delflag = @p5_dox_ob))  AND ( CONTAINS (dox_doc_ft_all.ft_content, '\"testsearch*\"')) ORDER BY dox_obj.creation_date DESC ";
           string parameters = "p1_dox_fo=0, p2_dox_do:(Int32): 0, @p3_dox_ob";
           string expected = "Parameters field is empty";
           // act
           string actual = "";
           try
           {
               String result = methods.Output(request, null);
           }
           catch (ArgumentException ex)
           {
               actual = ex.Message;
           }
           // assert
           Assert.AreEqual(expected, actual);
       }
    }
}
