using System.Windows;
using BLL.Interface;
using DI;
using MVVM.Models;
using Ninject;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IKernel kernel = new StandardKernel(new DIModule());
            IParserMethods methods = kernel.Get<IParserMethods>();
    
            var view = kernel.Get<MainWindow>();
            //view.DataContext = view;
            //view.Show();
            ParseViewModel parseVM = new ParseViewModel(new Result("bla-bla"));
            view.DataContext = parseVM;
            view.Show();
        }
    }
}
