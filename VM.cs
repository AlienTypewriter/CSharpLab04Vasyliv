using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WpfApp1.Commands;
using static WpfApp1.Commands.TableModificationHelper;

namespace WpfApp1
{
    public class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string name="Name";
        string surname="Surname";
        string email="mail@somemail.com";
        string filterText;
        DateTime birthdate=DateTime.Now.AddYears(-18);
        AddModifyPersonCommand addPerson;
        SaveQueryCommand serialize;
        LoadQueryCommand deserialize;
        FilterCommand filterCommand;
        SortCommand sortCommand;
        DeletePersonCommand deletePerson;
        ShowAllItemsCommand showAll;
        private static int rowIndexSelected = 0;
        private static int columnIndexSelected = 0;
        private static int filterIndex = 0;
        private static bool isModify = false;
        private static string[] methodList = { "Save/Load", "Delete", "Add/Edit", "Sort","Filter" };
        public static bool filterBool = true;
        private ObservableCollection<Person> people = new ObservableCollection<Person>();
        private ObservableCollection<Person> peopleQuery;
        private string[] filterList = {"First name", "Last name", "Email", "Date of birth",
            "Is an adult", "Celebrates birthday today", "Sun zodiac", "Chinese zodiac" };
        public string Name { get => name; set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public string Surname { get => surname; set
            {
                surname = value;
                NotifyPropertyChanged("Surname");
            }
        }
        public string Email { get => email; set
            {
                email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public DateTime Birthdate
        {
            get => birthdate; set
            {
                birthdate = value;
                NotifyPropertyChanged("Birthdate");
            }
        }
        public static bool IsModify { get => isModify; set => isModify = value; }
        public static int RowIndexSelected { get => rowIndexSelected; set => rowIndexSelected = value; }
        public static int FilterIndex { get => filterIndex; set => filterIndex = value; }
        public string[] FilterList { get => filterList; set => filterList = value; }
        public string FilterText { get => filterText; set => filterText = value; }
        public FilterCommand FilterCommand { get => filterCommand; set => filterCommand = value; }
        public AddModifyPersonCommand AddModifyPerson { get => addPerson; set => addPerson = value; }
        public SaveQueryCommand Serialize { get => serialize; set => serialize = value; }
        public LoadQueryCommand Deserialize { get => deserialize; set => deserialize = value; }
        public SortCommand SortCommand { get => sortCommand; set => sortCommand = value; }
        public DeletePersonCommand DeletePerson { get => deletePerson; set => deletePerson = value; }
        public ShowAllItemsCommand ShowAll { get => showAll; set => showAll = value; }
        public ObservableCollection<Person> People
        {
            get => people; set
            {
                people = value;
                NotifyPropertyChanged("People");
            }
        }
        public ObservableCollection<Person> PeopleQuery { get => peopleQuery; set
            {
                peopleQuery=value;
                NotifyPropertyChanged("PeopleQuery");
            }
        }

        public static int ColumnIndexSelected { get => columnIndexSelected; set => columnIndexSelected = value; }
        public static string[] MethodList { get => methodList; set => methodList = value; }

        public VM()
        {
            if (!TablePresent()) GenerateAndSave();
            else
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("People.txt", FileMode.Open, FileAccess.Read);
                ObservableCollection<Person> people = (ObservableCollection<Person>)formatter.Deserialize(stream);
                People = people;
                PeopleQuery = people;
                stream.Close();
            }
            PeopleQuery = People;
            AddModifyPerson = new AddModifyPersonCommand(this);
            DeletePerson = new DeletePersonCommand(this);
            Serialize = new SaveQueryCommand(this);
            Deserialize = new LoadQueryCommand(this);
            FilterCommand = new FilterCommand(this);
            SortCommand = new SortCommand(this);
            ShowAll = new ShowAllItemsCommand(this);
        }
        private bool TablePresent()
        {
            try
            {
                File.OpenRead("People.txt");
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            return true;
        }
        private void GenerateAndSave()
        {
            string[] names = { "Christopher", "Delicia", "Keneth", "Cynthia", "Lloyd", "Vicky",
            "Columbus","Derrik", "Tony", "Clark", "Benjamin", "Erin", "Jeffrey"};
            string[] surnames = {"Tanner","Saylors","Lopez","Champion","Race","Lee",
            "Edwards","Rocha","Mejia","Johnson","Torres","Smith"};
            DateTime[] birthdates = {new DateTime(1987, 11, 1), new DateTime(1994, 11, 21),
            new DateTime(1939, 2, 11),new DateTime(1935, 9, 11), new DateTime(2002, 8, 31),
            new DateTime(2004, 1, 14),new DateTime(1987, 9, 11),new DateTime(2001, 8, 30),
            new DateTime(1969, 2, 19),new DateTime(1977, 2, 24),new DateTime(1967, 12, 28),
            new DateTime(2005, 5, 2),new DateTime(1987, 11, 1)};
            string[] emailSuffixes = { "@jourrapide.com", "@rhyta.com", "@dayrep.com",
            "@armyspy.com","@teleworm.us"};
            Random rand = new Random();
            for (int i=0;i<50;i++)
            {
                string name = names[rand.Next(0, 12)];
                string surname = surnames[rand.Next(0, 12)];
                People.Add(validatePerson(name, surname, birthdates[rand.Next(0, 12)],
                    name + surname + emailSuffixes[rand.Next(0, 4)]));
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("People.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream,People);
            stream.Close();
            PeopleQuery = People;
        }
    }
}
