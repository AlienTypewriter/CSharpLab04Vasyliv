using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    public class FilterCommand : ICommand
    {
        VM parent;

        public FilterCommand(VM parent)
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
            if (VM.FilterIndex!=4&&VM.FilterIndex!=5&&
                string.IsNullOrEmpty(parent.FilterText)) return false;
            if (VM.FilterIndex == 3) {
                DateTime empty;
                return DateTime.TryParse(parent.FilterText, out empty);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            IEnumerable<Person> peopleQuery;
            switch (VM.FilterIndex)
            {
                case 0:
                    peopleQuery =
                                from person in parent.People
                                where person.Name == parent.FilterText
                                select person;
                    break;
                case 1:
                    peopleQuery =
                                from person in parent.People
                                where person.Surname == parent.FilterText
                                select person;
                    break;
                case 2:
                    peopleQuery =
                                from person in parent.People
                                where person.Email == parent.FilterText
                                select person;
                    break;
                case 3:
                    peopleQuery =
                                from person in parent.People
                                where person.Birthdate == DateTime.Parse(parent.FilterText)
                                select person;
                    break;
                case 4:
                    peopleQuery =
                                from person in parent.People
                                where person.IsAdult == VM.filterBool
                                select person;
                    break;
                case 5:
                    peopleQuery =
                                from person in parent.People
                                where person.IsBirthday == VM.filterBool
                                select person;
                    break;
                case 6:
                    peopleQuery =
                                from person in parent.People
                                where person.SunSign == parent.FilterText
                                select person;
                    break;
                case 7:
                    peopleQuery =
                                from person in parent.People
                                where person.ChineseSign == parent.FilterText
                                select person;
                    break;
                default:
                    peopleQuery = parent.People;
                    break;
            }
            ObservableCollection<Person> people = new ObservableCollection<Person>();
            foreach (Person p in peopleQuery)
            {
                people.Add(p);
            }
            parent.PeopleQuery = people;
        }
    }
}
