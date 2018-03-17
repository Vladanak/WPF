using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace OOP_2_LAB_2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            
        }
        public List<Student> std_list = new List<Student>();
        List<Student> sort_std_list = new List<Student>();
        List<Student> search_std_list = new List<Student>();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Adress std_adr = new Adress();
                Student std = new Student(new Adress());
                Regex regex1 = new Regex(@"^[A-ZА-Я][a-zа-я]{1,20}$");

                if (!regex1.IsMatch(textBox4.Text)) { throw new Exception("Проверьте формат ввода названия города"); }
                else { std.Adress.City = textBox4.Text; }
                if (textBox5.Text.Length == 6)
                {
                    if (Int32.TryParse(textBox5.Text, out std.Adress.index) == false)
                    {
                        throw new Exception("Проверьте правильность символов индекса");
                    }
                }
                else
                {
                    throw new Exception("Проверьте количество символов индекса");
                }
                if (!regex1.IsMatch(textBox6.Text)) { throw new Exception("Проверьте формат ввода названия улицы"); }
                else { std.Adress.Street = textBox6.Text; }
                std.Adress.House = Convert.ToInt32(numericUpDown2.Value);
                std.Adress.Floor = Convert.ToInt32(numericUpDown3.Value);

                std.Specialization = comboBox1.Text;
                if (!regex1.IsMatch(textBox1.Text)) { throw new Exception("Проверьте формат ввода фамилии"); }
                else { std.Surname = textBox1.Text; }
                if (!regex1.IsMatch(textBox2.Text)) { throw new Exception("Проверьте формат ввода имени"); }
                else { std.Name = textBox2.Text; }
                if (!regex1.IsMatch(textBox3.Text)) { throw new Exception("Проверьте формат ввода отчества"); }
                else { std.Second_name = textBox3.Text; }
                std.Date_of_birth = dateTimePicker1.Text;
                if (comboBox1.Text == "") { throw new Exception("Выберите специальность"); }
                if (comboBox1.Text.CompareTo("ПОИТ")==0 || comboBox1.Text.CompareTo("ИСИТ") == 0 || comboBox1.Text.CompareTo("ПОИБМС") == 0 || comboBox1.Text.CompareTo("ДЭВИ") == 0){ }
                else { throw new Exception("Неверная Специальность"); }
                std.Kurs = trackBar1.Value;
                std.Average_mark = Convert.ToInt32(numericUpDown1.Value);
                if (radioButton1.Checked == true && radioButton2.Checked == false)
                {
                    std.Sex = "Пол женский";
                }
                else if ((radioButton1.Checked == false && radioButton2.Checked == true))
                {

                    std.Sex = "Пол мужской";
                }
                else if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    throw new Exception("Необходимо выбрать пол");
                }
                string result = JsonConvert.SerializeObject(std_list);
                //richTextBox1.Text = result;
                richTextBox1.Text = "";
                for (int p = 0; p < std_list.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + std_list.ElementAt(p).Surname + "\n" + "  Имя: " + std_list.ElementAt(p).Name + "\n" + "  Отчество: " + std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + std_list.ElementAt(p).Adress.index + "\n";
                }
                    File.WriteAllText("C:/json.txt", result);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try {
                FileStream json = new FileStream("C:/json.txt", FileMode.Open); //создаем файловый поток
                StreamReader reader = new StreamReader(json);
                if (json.Length != 0)
                {
                    string FILE = reader.ReadToEnd();
                    reader.Close();
                    std_list = JsonConvert.DeserializeObject<List<Student>>(FILE);
                    //richTextBox1.Text = Convert.ToString(FILE);
                    for (int p = 0; p < std_list.Count; p++)
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + std_list.ElementAt(p).Surname + "\n" + "  Имя: " + std_list.ElementAt(p).Name + "\n" + "  Отчество: " + std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + std_list.ElementAt(p).Adress.index + "\n";
                    }
                }
                else { throw new Exception("JSON файл отсутствует или пуст."); }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            }        
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {//фамилия  
            if(radioButton3.Checked == true)
            {
                search_std_list.Clear();
               string search_arg = textBox7.Text;
                int c = 0;
               for(int i = 0; i < std_list.Count; i++)
                {
                    if (std_list.ElementAt(i).Surname == search_arg)
                    {
                        search_std_list.Add(std_list.ElementAt(i));
                        c++;
                    }
                }

               if(c == 0) { richTextBox1.Text = "Поиск не дал результатов.";}
                else
                {
                    richTextBox1.Text = "";

                    for (int p = 0; p < search_std_list.Count; p++)
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + search_std_list.ElementAt(p).Surname + "\n" + "  Имя: " + search_std_list.ElementAt(p).Name + "\n" + "  Отчество: " + search_std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + search_std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + search_std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + search_std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + search_std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + search_std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + search_std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + search_std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + search_std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + search_std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + search_std_list.ElementAt(p).Adress.index + "\n";
                    }
                }
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {//спецуха
            if (radioButton4.Checked == true)
            {
                search_std_list.Clear();
                string search_arg = textBox7.Text;
                int c = 0;
                for (int i = 0; i < std_list.Count; i++)
                {
                    if (std_list.ElementAt(i).Specialization == search_arg)
                    {
                        search_std_list.Add(std_list.ElementAt(i));
                        c++;
                    }
                }
                if (c == 0) { richTextBox1.Text = "Поиск не дал результатов."; }
                else
                {
                    richTextBox1.Text = "";

                    for (int p = 0; p < search_std_list.Count; p++)
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + search_std_list.ElementAt(p).Surname + "\n" + "  Имя: " + search_std_list.ElementAt(p).Name + "\n" + "  Отчество: " + search_std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + search_std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + search_std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + search_std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + search_std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + search_std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + search_std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + search_std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + search_std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + search_std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + search_std_list.ElementAt(p).Adress.index + "\n";
                    }
                }
            }
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {//средний балл
            if (radioButton5.Checked == true)
            {
                search_std_list.Clear();
                bool search_arg_is_valid = Int32.TryParse(textBox7.Text, out int search_arg);
                int c = 0;
                for (int i = 0; i < std_list.Count; i++)
                {
                    if (std_list.ElementAt(i).Average_mark == search_arg)
                    {
                        search_std_list.Add(std_list.ElementAt(i));
                        c++;
                    }
                }
                if (c == 0) { richTextBox1.Text = "Поиск не дал результатов."; }
                else
                {
                    richTextBox1.Text = "";

                    for (int p = 0; p < search_std_list.Count; p++)
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + search_std_list.ElementAt(p).Surname + "\n" + "  Имя: " + search_std_list.ElementAt(p).Name + "\n" + "  Отчество: " + search_std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + search_std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + search_std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + search_std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + search_std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + search_std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + search_std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + search_std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + search_std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + search_std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + search_std_list.ElementAt(p).Adress.index + "\n";
                    }
                }
            }
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {//курс
            if (radioButton6.Checked == true)
            {
                search_std_list.Clear();
                bool search_arg_is_valid = Int32.TryParse(textBox7.Text, out int search_arg);
                int c = 0;
                for (int i = 0; i < std_list.Count; i++)
                {
                    if (std_list.ElementAt(i).Kurs == search_arg)
                    {
                        search_std_list.Add(std_list.ElementAt(i));
                        c++;
                    }
                }
                if (c == 0) { richTextBox1.Text = "Поиск не дал результатов."; }
                else
                {
                    richTextBox1.Text = "";

                    for (int p = 0; p < search_std_list.Count; p++)
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + search_std_list.ElementAt(p).Surname + "\n" + "  Имя: " + search_std_list.ElementAt(p).Name + "\n" + "  Отчество: " + search_std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + search_std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + search_std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + search_std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + search_std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + search_std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + search_std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + search_std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + search_std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + search_std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + search_std_list.ElementAt(p).Adress.index + "\n";
                    }
                }
            }
        }
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                sort_std_list.Clear();
                var sort_variable = from i in std_list
                                    orderby i.Kurs descending
                                    select i;
                
                foreach (Student s in sort_variable)
                {
                    sort_std_list.Add(s);
                }
                richTextBox1.Text = "";
                for (int p = 0; p < sort_std_list.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + sort_std_list.ElementAt(p).Surname + "\n" + "  Имя: " + sort_std_list.ElementAt(p).Name + "\n" + "  Отчество: " + sort_std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + sort_std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + sort_std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + sort_std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + sort_std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + sort_std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + sort_std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + sort_std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + sort_std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + sort_std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + sort_std_list.ElementAt(p).Adress.index + "\n";
                }
            }
        }
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                sort_std_list.Clear();
                var sort_variable = from i in std_list
                                    orderby i.Average_mark descending
                                    select i;
                
                foreach (Student s in sort_variable)
                {
                    sort_std_list.Add(s);
                }
                richTextBox1.Text = "";
                for (int p = 0; p < sort_std_list.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + sort_std_list.ElementAt(p).Surname + "\n" + "  Имя: " + sort_std_list.ElementAt(p).Name + "\n" + "  Отчество: " + sort_std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + sort_std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + sort_std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + sort_std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + sort_std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + sort_std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + sort_std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + sort_std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + sort_std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + sort_std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + sort_std_list.ElementAt(p).Adress.index + "\n";
                }
            }
        }
        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                sort_std_list.Clear();
                var sort_variable = from i in std_list
                                    orderby i.Adress.index descending
                                    select i;
                
                foreach (Student s in sort_variable)
                {
                    sort_std_list.Add(s);
                }
                richTextBox1.Text = "";
                for (int p = 0; p < sort_std_list.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + sort_std_list.ElementAt(p).Surname + "\n" + "  Имя: " + sort_std_list.ElementAt(p).Name + "\n" + "  Отчество: " + sort_std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + sort_std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + sort_std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + sort_std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + sort_std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + sort_std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + sort_std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + sort_std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + sort_std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + sort_std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + sort_std_list.ElementAt(p).Adress.index + "\n";
                }
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версия:Релиз" + "\n" + "Разработчик: " + "Муравейко Владислав Сергеевич");
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            if (System.IO.File.Exists("C:/json.txt"))
                File.WriteAllText("C:/json.txt", "");
            else
                MessageBox.Show("Файл отсутствует!");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                Adress std_adr = new Adress();
                Student std = new Student(new Adress());
                Regex regex1 = new Regex(@"^[A-ZА-Я][a-zа-я]{1,20}$");

                if (!regex1.IsMatch(textBox4.Text)) { throw new Exception("Проверьте формат ввода названия города"); }
                else { std.Adress.City = textBox4.Text; }
                if (textBox5.Text.Length == 6)
                {
                    if (Int32.TryParse(textBox5.Text, out std.Adress.index) == false)
                    {
                        throw new Exception("Проверьте правильность символов индекса");
                    }
                }
                else
                {
                    throw new Exception("Проверьте количество символов индекса");
                }
                if (!regex1.IsMatch(textBox6.Text)) { throw new Exception("Проверьте формат ввода названия улицы"); }
                else { std.Adress.Street = textBox6.Text; }
                std.Adress.House = Convert.ToInt32(numericUpDown2.Value);
                std.Adress.Floor = Convert.ToInt32(numericUpDown3.Value);

                std.Specialization = comboBox1.Text;
                if (!regex1.IsMatch(textBox1.Text)) { throw new Exception("Проверьте формат ввода фамилии"); }
                else { std.Surname = textBox1.Text; }
                if (!regex1.IsMatch(textBox2.Text)) { throw new Exception("Проверьте формат ввода имени"); }
                else { std.Name = textBox2.Text; }
                if (!regex1.IsMatch(textBox3.Text)) { throw new Exception("Проверьте формат ввода отчества"); }
                else { std.Second_name = textBox3.Text; }
                std.Date_of_birth = dateTimePicker1.Text;
                if (comboBox1.Text == "") { throw new Exception("Выберите специальность"); }
                if (comboBox1.Text.CompareTo("ПОИТ") == 0 || comboBox1.Text.CompareTo("ИСИТ") == 0 || comboBox1.Text.CompareTo("ПОИБМС") == 0 || comboBox1.Text.CompareTo("ДЭВИ") == 0) { }
                else { throw new Exception("Неверная Специальность"); }
                std.Kurs = trackBar1.Value;
                std.Average_mark = Convert.ToInt32(numericUpDown1.Value);
                if (radioButton1.Checked == true && radioButton2.Checked == false)
                {
                    std.Sex = "Пол женский";
                }
                else if ((radioButton1.Checked == false && radioButton2.Checked == true))
                {

                    std.Sex = "Пол мужской";
                }
                else if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    throw new Exception("Необходимо выбрать пол");
                }
                richTextBox1.Text = "";
                for (int p = 0; p < std_list.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + std_list.ElementAt(p).Surname + "\n" + "  Имя: " + std_list.ElementAt(p).Name + "\n" + "  Отчество: " + std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + std_list.ElementAt(p).Adress.index + "\n";
                }
                using (FileStream fs = new FileStream("C:/Xml.xml", FileMode.OpenOrCreate))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
                    xmlSerializer.Serialize(fs, std_list);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                List<Student> first;
                using (FileStream fs = new FileStream("C:/Xml.xml", FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
                    first = (List<Student>)xmlSerializer.Deserialize(fs);
                    if (first.Count != 0)
                    {
                        for (int p = 0; p < std_list.Count; p++)
                        {
                            richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + std_list.ElementAt(p).Surname + "\n" + "  Имя: " + std_list.ElementAt(p).Name + "\n" + "  Отчество: " + std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + std_list.ElementAt(p).Adress.index + "\n";
                        }
                    }
                    else
                    {
                        MessageBox.Show("collection is empty");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("XML файл отсутствует или пуст.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Adress std_adr = new Adress();
                Student std = new Student(new Adress());
                Regex regex1 = new Regex(@"^[A-ZА-Я][a-zа-я]{1,20}$");

                if (!regex1.IsMatch(textBox4.Text)) { throw new Exception("Проверьте формат ввода названия города"); }
                else { std.Adress.City = textBox4.Text; }
                if (textBox5.Text.Length == 6)
                {
                    if (Int32.TryParse(textBox5.Text, out std.Adress.index) == false)
                    {
                        throw new Exception("Проверьте правильность символов индекса");
                    }
                }
                else
                {
                    throw new Exception("Проверьте количество символов индекса");
                }
                if (!regex1.IsMatch(textBox6.Text)) { throw new Exception("Проверьте формат ввода названия улицы"); }
                else { std.Adress.Street = textBox6.Text; }
                std.Adress.House = Convert.ToInt32(numericUpDown2.Value);
                std.Adress.Floor = Convert.ToInt32(numericUpDown3.Value);

                std.Specialization = comboBox1.Text;
                if (!regex1.IsMatch(textBox1.Text)) { throw new Exception("Проверьте формат ввода фамилии"); }
                else { std.Surname = textBox1.Text; }
                if (!regex1.IsMatch(textBox2.Text)) { throw new Exception("Проверьте формат ввода имени"); }
                else { std.Name = textBox2.Text; }
                if (!regex1.IsMatch(textBox3.Text)) { throw new Exception("Проверьте формат ввода отчества"); }
                else { std.Second_name = textBox3.Text; }
                std.Date_of_birth = dateTimePicker1.Text;
                if (comboBox1.Text == "") { throw new Exception("Выберите специальность"); }
                if (comboBox1.Text.CompareTo("ПОИТ") == 0 || comboBox1.Text.CompareTo("ИСИТ") == 0 || comboBox1.Text.CompareTo("ПОИБМС") == 0 || comboBox1.Text.CompareTo("ДЭВИ") == 0) { }
                else { throw new Exception("Неверная Специальность"); }
                std.Kurs = trackBar1.Value;
                std.Average_mark = Convert.ToInt32(numericUpDown1.Value);
                if (radioButton1.Checked == true && radioButton2.Checked == false)
                {
                    std.Sex = "Пол женский";
                }
                else if ((radioButton1.Checked == false && radioButton2.Checked == true))
                {

                    std.Sex = "Пол мужской";
                }
                else if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    throw new Exception("Необходимо выбрать пол");
                }
                std_list.Add(std);
                for (int p = 0; p < std_list.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + std_list.ElementAt(p).Surname + "\n" + "  Имя: " + std_list.ElementAt(p).Name + "\n" + "  Отчество: " + std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + std_list.ElementAt(p).Adress.index + "\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
