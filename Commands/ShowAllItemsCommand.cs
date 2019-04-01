using System;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    public class ShowAllItemsCommand: ICommand
    {
        VM parent;

        public ShowAllItemsCommand(VM parent)
        {
            this.parent = parent;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            parent.PeopleQuery = parent.People;
        }
    }
}
