using System;
using System.IO;
using System.Windows;
using BLL.Interface;
using Microsoft.Win32;
using MVVM.ViewModels;


using System.Windows;
using BLL.Interface;
using DI;
using MVVM.Models;
using Ninject;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        //private readonly ParserMethods methods;
        private readonly Controller methods;
       //public DependencyResolver dependencyResolver;
        private ParseViewModel viewmodel ;
        public MainWindow(IParserMethods methods)
        {
            InitializeComponent();
            //methods = new ParserMethods();
            this.methods = new Controller(methods);
            //viewmodel=new ParseViewModel(new Result(""));
            //ParseViewModel parseVM = new ParseViewModel(new Result("bla-bla"));
            //this.DataContext = parseVM;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.Request.Clear();
            this.Parameters.Clear();
            Output1.Text = "";
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            string textForCopy = Output1.Text;
            Clipboard.SetData(DataFormats.Text, (Object)textForCopy);

        }

        private void LoadRequest_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog=new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|SQL request (*.sql)|*.sql|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                Request.Text = File.ReadAllText(openFileDialog.FileName);
            }

        }

        private void Load_Params_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|SQL request (*.sql)|*.sql|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                Parameters.Text = File.ReadAllText(openFileDialog.FileName);
            }

        }

        private void Output_Click(object sender, RoutedEventArgs e)
        {
            string request = this.Request.Text;
            string parameters = this.Parameters.Text;
            string result = request;
            bool formatFlag = false;
            bool numerateParamsFlag = false;
            if (this.Format_CheckBox.IsChecked == true)
                formatFlag = true;
            if (this.NumerateParamsCheckBox.IsChecked == true)
                numerateParamsFlag = true;
            result = methods.Output(request, parameters, numerateParamsFlag, formatFlag);

            
            //Output1.Text = result;
            if (this.Copy_CheckBox.IsChecked == true)
            {
                string textForCopy = Output1.Text;
                Clipboard.SetData(DataFormats.UnicodeText, (Object)textForCopy);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Copy_CheckBox.IsChecked = Properties.Settings.Default.CheckBoxChecked;
            Format_CheckBox.IsChecked = Properties.Settings.Default.FormatCheckboxChecked;
            NumerateParamsCheckBox.IsChecked = Properties.Settings.Default.NumerateParamsCheckBoxChecked;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.CheckBoxChecked = (bool)Copy_CheckBox.IsChecked;
            Properties.Settings.Default.FormatCheckboxChecked = (bool)Format_CheckBox.IsChecked;
            Properties.Settings.Default.NumerateParamsCheckBoxChecked = (bool)NumerateParamsCheckBox.IsChecked;
            Properties.Settings.Default.Save();        
        }

        private void Format_CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
