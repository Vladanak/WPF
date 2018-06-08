using System;

namespace OOP_2_LAB_2
{
    [Serializable]
    public class Adress
    {
        private string city;
        public int index;
        private string street;
        private int house;
        private int floor;

        public string City { get => city; set => city = value; }
        public string Street { get => street; set => street = value; }
        public int House { get => house; set => house = value; }
        public int Floor { get => floor; set => floor = value; }
    }

    [Serializable]
    public class Student
    {
        private string name;
        private string second_name;
        private string surname;
        private string date_of_birth;
        private string specialization;
        private int kurs;
        private double average_mark;
        private string sex;
        private Adress adress;

        public string Name { get => name; set => name = value; }
        public string Second_name { get => second_name; set => second_name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Date_of_birth { get => date_of_birth; set => date_of_birth = value; }
        public string Specialization { get => specialization; set => specialization = value; }
        public int Kurs { get => kurs; set => kurs = value; }
        public double Average_mark { get => average_mark; set => average_mark = value; }
        public string Sex { get => sex; set => sex = value; }
        public Adress Adress { get => adress; set => adress = value; }

        public Student()
        { }
        public Student(Adress adr)
        {
        Adress = adr;
        }
    }
}