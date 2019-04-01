using System;
using System.Windows.Input;

namespace WpfApp1.Commands
{
    public class TableModificationHelper
    {
        static private int[] sunSignChangeDays = new int[12]
            {
            21,21,21,21,21,22,23,24,23,24,23,22
            };
        static private string[] chineseSigns = new string[12]
        {
            "Rat", "Ox" , "Tiger", "Rabbit", "Dragon", "Snake",
        "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig"
        };
        static private string[] sunSigns = new string[12]
        {
            "Capricorn","Aquerius", "Pisces", "Aries", "Taurus" , "Gemini", "Cancer",
            "Leo", "Virgo","Libra", "Scoprio", "Sagittarius"
        };

        static private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static Person validatePerson(string name, string surname, DateTime selDate, string email)
        {
            DateTime now = DateTime.Now;
            TimeSpan dif = now - selDate;
            if (now.AddDays(dif.TotalDays) < now.AddYears(-120) || dif.Seconds < 0)
                throw new ArgumentOutOfRangeException("Date should not be more than 120 years in the past or in the future");
            if (!IsValidEmail(email)) throw new ArgumentException("Invalid email address");
            bool isBirthday = (now.Day == selDate.Day && now.Month == selDate.Month);
            bool isAdult = now > selDate.AddYears(18);
            int inEuropian = 0;
            for (int i = 0; i < 12; i++)
            {
                if (selDate.Month > i + 1 || (selDate.Month == i + 1 && selDate.Day >= sunSignChangeDays[i]))
                {
                    inEuropian = i + 1;
                }
            }
            inEuropian %= 12;
            int inChinese = (selDate.Year - 4 - ((selDate < new DateTime(selDate.Year, 2, 5)) ? 1 : 0)) % 12;
            string chineseSign = chineseSigns[inChinese];
            string sunSign = sunSigns[inEuropian];
            return (new Person(name, surname, email, selDate, isAdult, isBirthday, chineseSign, sunSign));
        }
        public class AddModifyPersonCommand : ICommand
        {
            VM parent;

            public AddModifyPersonCommand(VM parent)
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
                if (string.IsNullOrEmpty(parent.Name)) return false;
                if (string.IsNullOrEmpty(parent.Surname)) return false;
                if (string.IsNullOrEmpty(parent.Email)) return false;
                if (parent.Birthdate == null) return false;
                return true;
            }

            public void Execute(object parameter)
            {
                DateTime selDate = parent.Birthdate;
                try
                {
                    Person newPerson = validatePerson(parent.Name, parent.Surname,
                        parent.Birthdate, parent.Email);
                    if (!VM.IsModify) parent.People.Add(newPerson);
                    else parent.People[VM.RowIndexSelected] = newPerson;
                    parent.PeopleQuery = parent.People;
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message);
                }
            }
        }
        public class DeletePersonCommand : ICommand
        {
            VM parent;

            public DeletePersonCommand(VM parent)
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
                return VM.RowIndexSelected != -1;
            }

            public void Execute(object parameter)
            {
                parent.People.RemoveAt(VM.RowIndexSelected);
            }
        }
    } 
}
