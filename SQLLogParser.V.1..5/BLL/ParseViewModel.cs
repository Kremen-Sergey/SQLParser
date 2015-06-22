using System;
using BLL.Interface;
using System.ComponentModel;

namespace Controllers
{
    public class ParseViewModel : INotifyPropertyChanged
    {
        private IParserMethods methods;
        private string resultString;
        public string ResultString
        {
            get { return resultString; }
            set
            {
                resultString = value;
                //OnPropertyChanged("resultString");
            }
        }
        public ParseViewModel(IParserMethods methods)
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
