using System.ComponentModel;
using System.Windows.Input;

namespace MVVM.ViewModels
{
    /// <summary>
    /// Provides common functionality for ViewModel classes
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public bool CanExecute(object parameter)
        {
            throw new System.NotImplementedException();
        }

        public event System.EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
