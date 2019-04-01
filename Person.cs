using System;
using System.Windows;

namespace WpfApp1
{
    [Serializable]
    public class Person 
    {
        private string name;
        private string surname;
        private string email;
        private DateTime birthdate;
        private readonly bool isAdult;
        private readonly bool isBirthday;
        private readonly string chineseSign;
        private readonly string sunSign;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        public bool IsAdult => isAdult;
        public bool IsBirthday => isBirthday;
        public string ChineseSign => chineseSign;
        public string SunSign => sunSign;

        public Person(string name, string surname, string email, DateTime birthdate,
            bool isAdult, bool isBirthday, string chineseSign, string sunSign)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Birthdate = birthdate;
            this.isAdult = isAdult;
            this.isBirthday = isBirthday;
            this.chineseSign = chineseSign;
            this.sunSign = sunSign;
        }
        public override string ToString()
        {
            return "Name -- " + Name + "\nSurname -- " + Surname + "\nEmail -- " + Email + "\nDate of birth -- " + Birthdate +
                "\nIs an adult? -- " + IsAdult + "\nSelebrates birthday today? -- " + IsBirthday +
                "\nChinese Zodiac sign -- " + ChineseSign + "\nSun Zodiac sign -- " + SunSign;
        }
    }
}
