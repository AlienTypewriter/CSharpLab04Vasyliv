using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    public class SaveQueryCommand : ICommand
    {
        VM parent;
        public SaveQueryCommand(VM parent)
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
            return !(parent.People == null || parent.People.Count == 0);
        }

        public void Execute(object parameter)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Select file";
            save.Filter = "Text Files (*.txt)|*.txt";
            string file = "";
            if (save.ShowDialog() == DialogResult.OK)
            {
                file = save.FileName;
            }
            else
            {
                return;
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            formatter.Serialize(stream, parent.PeopleQuery);
            stream.Close();
        }
    }
}
