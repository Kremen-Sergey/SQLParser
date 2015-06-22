namespace BLL.Interface
{
    public interface IParserMethods
    {
        string Output(string request, string parameters);
        string Format(string sourceString);
        string NumerateParams(string sourceString);
    }
}
