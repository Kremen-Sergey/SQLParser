using BLL.Interface;
using MVVM.Models;
using MVVM.ViewModels;

namespace WpfApplication1
{
    class ParseViewModel:ViewModelBase
    {
        public Result Res { get; set; }
        public ParseViewModel(Result res)
        {
            Res = res;
        }
    }
}
