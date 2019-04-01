using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    public class LoadQueryCommand : ICommand
    {
        VM parent;
        public LoadQueryCommand(VM parent)
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
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select file";
            openDialog.Filter = "Text Files (*.txt)|*.txt";
            string file = "";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                file = openDialog.FileName;
            } else
            {
                return;
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            ObservableCollection<Person> people = (ObservableCollection<Person>)formatter.Deserialize(stream);
            parent.People = people;
            parent.PeopleQuery = people;
            stream.Close();
        }
    }
}
