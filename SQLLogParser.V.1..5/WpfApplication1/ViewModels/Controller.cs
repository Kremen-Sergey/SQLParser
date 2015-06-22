using System;
using BLL.Interface;

namespace MVVM.ViewModels

{
    class Controller
    {
       private IParserMethods methods;

        public Controller(IParserMethods methods)
        {
          
            this.methods = methods;
        }
        public string Output(string request, string parameters, bool numerateParamsFlag, bool formatFlag)
        {
            string result = request;
            try
            {
                result = methods.Output(request, parameters);
            }
            catch (ArgumentException ex)
            {
                result = ex.Message;
            }
            if (numerateParamsFlag)
            {
                result = methods.NumerateParams(result);
            }

            if (formatFlag)
                result = methods.Format(result);
            return result;
        }
    }
}
