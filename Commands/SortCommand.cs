using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    public class SortCommand: ICommand
    {
        VM parent;

        public SortCommand(VM parent)
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
            IEnumerable<Person> sortedQuery;
            switch (VM.FilterIndex)
            {
                case 0:
                    sortedQuery =
                                from person in parent.PeopleQuery
                                orderby person.Name
                                select person;
                    break;
                case 1:
                    sortedQuery =
                                from person in parent.PeopleQuery
                                orderby person.Surname
                                select person;
                    break;
                case 2:
                    sortedQuery =
                                from person in parent.PeopleQuery
                                orderby person.Email
                                select person;
                    break;
                case 3:
                    sortedQuery =
                                from person in parent.PeopleQuery
                                orderby person.Birthdate
                                select person;
                    break;
                case 4:
                    sortedQuery =
                                from person in parent.PeopleQuery
                                orderby person.IsAdult
                                select person;
                    break;
                case 5:
                    sortedQuery =
                                from person in parent.PeopleQuery
                                orderby person.IsBirthday
                                select person;
                    break;
                case 6:
                    sortedQuery =
                                from person in parent.PeopleQuery
                                orderby person.SunSign
                                select person;
                    break;
                case 7:
                    sortedQuery =
                                from person in parent.PeopleQuery
                                orderby person.ChineseSign
                                select person;
                    break;
                default:
                    sortedQuery = parent.PeopleQuery;
                    break;
            }
            ObservableCollection<Person> people = new ObservableCollection<Person>();
            foreach (Person p in sortedQuery)
            {
                people.Add(p);
            }
            parent.PeopleQuery = people;
        }
    }
}
