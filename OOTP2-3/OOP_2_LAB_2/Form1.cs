using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
        private readonly List<Student> _sortStdList = new List<Student>();
        private readonly List<Student> _searchStdList = new List<Student>();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var std = new Student(new Adress());
                var regex1 = new Regex(@"^[A-ZА-Я][a-zа-я]{1,20}$");
                if (!regex1.IsMatch(textBox4.Text)) { throw new Exception("Проверьте формат ввода названия города"); }
                else { std.Adress.City = textBox4.Text; }
                if (textBox5.Text.Length == 6)
                {
                    if (int.TryParse(textBox5.Text, out std.Adress.index) == false)
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
                if (String.Compare(comboBox1.Text, "ПОИТ", StringComparison.Ordinal)==0 || comboBox1.Text.CompareTo("ИСИТ") == 0 || comboBox1.Text.CompareTo("ПОИБМС") == 0 || comboBox1.Text.CompareTo("ДЭВИ") == 0){ }
                else { throw new Exception("Неверная Специальность"); }
                std.Kurs = trackBar1.Value;
                std.Average_mark = Convert.ToInt32(numericUpDown1.Value);
                switch (radioButton1.Checked)
                {
                    case true when radioButton2.Checked == false:
                        std.Sex = "Пол женский";
                        break;
                    case false when radioButton2.Checked == true:
                        std.Sex = "Пол мужской";
                        break;
                    case false when radioButton2.Checked == false:
                        throw new Exception("Необходимо выбрать пол");
                }
                var result = JsonConvert.SerializeObject(std_list);
                richTextBox1.Text = "";
                for (var p = 0; p < std_list.Count; p++)
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
                var json = new FileStream("C:/json.txt", FileMode.Open); //создаем файловый поток
                var reader = new StreamReader(json);
                if (json.Length != 0)
                {
                    var FILE = reader.ReadToEnd();
                    reader.Close();
                    std_list = JsonConvert.DeserializeObject<List<Student>>(FILE);
                    for (var p = 0; p < std_list.Count; p++)
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
            if (radioButton3.Checked != true) return;
            _searchStdList.Clear();
            var search_arg = textBox7.Text;
            var c = 0;
            for(var i = 0; i < std_list.Count; i++)
            {
                if (std_list.ElementAt(i).Surname != search_arg) continue;
                _searchStdList.Add(std_list.ElementAt(i));
                c++;
            }

            if(c == 0) { richTextBox1.Text = "Поиск не дал результатов.";}
            else
            {
                richTextBox1.Text = "";

                for (var p = 0; p < _searchStdList.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + _searchStdList.ElementAt(p).Surname + "\n" + "  Имя: " + _searchStdList.ElementAt(p).Name + "\n" + "  Отчество: " + _searchStdList.ElementAt(p).Second_name + "\n" + "  Пол: " + _searchStdList.ElementAt(p).Sex + "\n" + "  Дата рождения: " + _searchStdList.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + _searchStdList.ElementAt(p).Specialization + "\n" + "  Курс: " + _searchStdList.ElementAt(p).Kurs + "\n" + "  Средний бал: " + _searchStdList.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + _searchStdList.ElementAt(p).Adress.City + "\n" + "  Улица: " + _searchStdList.ElementAt(p).Adress.Street + "\n" + "  Дом: " + _searchStdList.ElementAt(p).Adress.House + "\n" + "  Квартира: " + _searchStdList.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + _searchStdList.ElementAt(p).Adress.index + "\n";
                }
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {//спецуха
            if (radioButton4.Checked != true) return;
            _searchStdList.Clear();
            var search_arg = textBox7.Text;
            var c = 0;
            for (var i = 0; i < std_list.Count; i++)
            {
                if (std_list.ElementAt(i).Specialization != search_arg) continue;
                _searchStdList.Add(std_list.ElementAt(i));
                c++;
            }
            if (c == 0) { richTextBox1.Text = "Поиск не дал результатов."; }
            else
            {
                richTextBox1.Text = "";

                for (var p = 0; p < _searchStdList.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + _searchStdList.ElementAt(p).Surname + "\n" + "  Имя: " + _searchStdList.ElementAt(p).Name + "\n" + "  Отчество: " + _searchStdList.ElementAt(p).Second_name + "\n" + "  Пол: " + _searchStdList.ElementAt(p).Sex + "\n" + "  Дата рождения: " + _searchStdList.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + _searchStdList.ElementAt(p).Specialization + "\n" + "  Курс: " + _searchStdList.ElementAt(p).Kurs + "\n" + "  Средний бал: " + _searchStdList.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + _searchStdList.ElementAt(p).Adress.City + "\n" + "  Улица: " + _searchStdList.ElementAt(p).Adress.Street + "\n" + "  Дом: " + _searchStdList.ElementAt(p).Adress.House + "\n" + "  Квартира: " + _searchStdList.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + _searchStdList.ElementAt(p).Adress.index + "\n";
                }
            }
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {//средний балл
            if (radioButton5.Checked != true) return;
            _searchStdList.Clear();
            var search_arg_is_valid = Int32.TryParse(textBox7.Text, out var search_arg);
            var c = 0;
            for (var i = 0; i < std_list.Count; i++)
            {
                if (std_list.ElementAt(i).Average_mark != search_arg) continue;
                _searchStdList.Add(std_list.ElementAt(i));
                c++;
            }
            if (c == 0) { richTextBox1.Text = "Поиск не дал результатов."; }
            else
            {
                richTextBox1.Text = "";

                for (var p = 0; p < _searchStdList.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + _searchStdList.ElementAt(p).Surname + "\n" + "  Имя: " + _searchStdList.ElementAt(p).Name + "\n" + "  Отчество: " + _searchStdList.ElementAt(p).Second_name + "\n" + "  Пол: " + _searchStdList.ElementAt(p).Sex + "\n" + "  Дата рождения: " + _searchStdList.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + _searchStdList.ElementAt(p).Specialization + "\n" + "  Курс: " + _searchStdList.ElementAt(p).Kurs + "\n" + "  Средний бал: " + _searchStdList.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + _searchStdList.ElementAt(p).Adress.City + "\n" + "  Улица: " + _searchStdList.ElementAt(p).Adress.Street + "\n" + "  Дом: " + _searchStdList.ElementAt(p).Adress.House + "\n" + "  Квартира: " + _searchStdList.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + _searchStdList.ElementAt(p).Adress.index + "\n";
                }
            }
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {//курс
            if (radioButton6.Checked != true) return;
            _searchStdList.Clear();
            var search_arg_is_valid = int.TryParse(textBox7.Text, out var search_arg);
            var c = 0;
            for (var i = 0; i < std_list.Count; i++)
            {
                if (std_list.ElementAt(i).Kurs != search_arg) continue;
                _searchStdList.Add(std_list.ElementAt(i));
                c++;
            }
            if (c == 0) { richTextBox1.Text = "Поиск не дал результатов."; }
            else
            {
                richTextBox1.Text = "";

                for (var p = 0; p < _searchStdList.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + _searchStdList.ElementAt(p).Surname + "\n" + "  Имя: " + _searchStdList.ElementAt(p).Name + "\n" + "  Отчество: " + _searchStdList.ElementAt(p).Second_name + "\n" + "  Пол: " + _searchStdList.ElementAt(p).Sex + "\n" + "  Дата рождения: " + _searchStdList.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + _searchStdList.ElementAt(p).Specialization + "\n" + "  Курс: " + _searchStdList.ElementAt(p).Kurs + "\n" + "  Средний бал: " + _searchStdList.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + _searchStdList.ElementAt(p).Adress.City + "\n" + "  Улица: " + _searchStdList.ElementAt(p).Adress.Street + "\n" + "  Дом: " + _searchStdList.ElementAt(p).Adress.House + "\n" + "  Квартира: " + _searchStdList.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + _searchStdList.ElementAt(p).Adress.index + "\n";
                }
            }
        }
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked != true) return;
            _sortStdList.Clear();
            var sort_variable = from i in std_list
                orderby i.Kurs descending
                select i;
            foreach (var s in sort_variable)
            {
                _sortStdList.Add(s);
            }
            richTextBox1.Text = "";
            for (var p = 0; p < _sortStdList.Count; p++)
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + _sortStdList.ElementAt(p).Surname + "\n" + "  Имя: " + _sortStdList.ElementAt(p).Name + "\n" + "  Отчество: " + _sortStdList.ElementAt(p).Second_name + "\n" + "  Пол: " + _sortStdList.ElementAt(p).Sex + "\n" + "  Дата рождения: " + _sortStdList.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + _sortStdList.ElementAt(p).Specialization + "\n" + "  Курс: " + _sortStdList.ElementAt(p).Kurs + "\n" + "  Средний бал: " + _sortStdList.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + _sortStdList.ElementAt(p).Adress.City + "\n" + "  Улица: " + _sortStdList.ElementAt(p).Adress.Street + "\n" + "  Дом: " + _sortStdList.ElementAt(p).Adress.House + "\n" + "  Квартира: " + _sortStdList.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + _sortStdList.ElementAt(p).Adress.index + "\n";
            }
        }
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked != true) return;
            _sortStdList.Clear();
            var sort_variable = from i in std_list
                orderby i.Average_mark descending
                select i;
                
            foreach (var s in sort_variable)
            {
                _sortStdList.Add(s);
            }
            richTextBox1.Text = "";
            for (var p = 0; p < _sortStdList.Count; p++)
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + _sortStdList.ElementAt(p).Surname + "\n" + "  Имя: " + _sortStdList.ElementAt(p).Name + "\n" + "  Отчество: " + _sortStdList.ElementAt(p).Second_name + "\n" + "  Пол: " + _sortStdList.ElementAt(p).Sex + "\n" + "  Дата рождения: " + _sortStdList.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + _sortStdList.ElementAt(p).Specialization + "\n" + "  Курс: " + _sortStdList.ElementAt(p).Kurs + "\n" + "  Средний бал: " + _sortStdList.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + _sortStdList.ElementAt(p).Adress.City + "\n" + "  Улица: " + _sortStdList.ElementAt(p).Adress.Street + "\n" + "  Дом: " + _sortStdList.ElementAt(p).Adress.House + "\n" + "  Квартира: " + _sortStdList.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + _sortStdList.ElementAt(p).Adress.index + "\n";
            }
        }
        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked != true) return;
            _sortStdList.Clear();
            var sort_variable = from i in std_list
                orderby i.Adress.index descending
                select i;
            foreach (var s in sort_variable)
            {
                _sortStdList.Add(s);
            }
            richTextBox1.Text = "";
            for (var p = 0; p < _sortStdList.Count; p++)
            {
                richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + _sortStdList.ElementAt(p).Surname + "\n" + "  Имя: " + _sortStdList.ElementAt(p).Name + "\n" + "  Отчество: " + _sortStdList.ElementAt(p).Second_name + "\n" + "  Пол: " + _sortStdList.ElementAt(p).Sex + "\n" + "  Дата рождения: " + _sortStdList.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + _sortStdList.ElementAt(p).Specialization + "\n" + "  Курс: " + _sortStdList.ElementAt(p).Kurs + "\n" + "  Средний бал: " + _sortStdList.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + _sortStdList.ElementAt(p).Adress.City + "\n" + "  Улица: " + _sortStdList.ElementAt(p).Adress.Street + "\n" + "  Дом: " + _sortStdList.ElementAt(p).Adress.House + "\n" + "  Квартира: " + _sortStdList.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + _sortStdList.ElementAt(p).Adress.index + "\n";
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

        #region Unused
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        #endregion

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                var std = new Student(new Adress());
                var regex1 = new Regex(@"^[A-ZА-Я][a-zа-я]{1,20}$");
                if (!regex1.IsMatch(textBox4.Text)) { throw new Exception("Проверьте формат ввода названия города"); }
                else { std.Adress.City = textBox4.Text; }
                if (textBox5.Text.Length == 6)
                {
                    if (int.TryParse(textBox5.Text, out std.Adress.index) == false)
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
                switch (radioButton1.Checked)
                {
                    case true when radioButton2.Checked == false:
                        std.Sex = "Пол женский";
                        break;
                    case false when radioButton2.Checked == true:
                        std.Sex = "Пол мужской";
                        break;
                    case false when radioButton2.Checked == false:
                        throw new Exception("Необходимо выбрать пол");
                }
                richTextBox1.Text = "";
                for (var p = 0; p < std_list.Count; p++)
                {
                    richTextBox1.Text = richTextBox1.Text + "\n" + "Общая информация: " + "\n" + "  Фамилия: " + std_list.ElementAt(p).Surname + "\n" + "  Имя: " + std_list.ElementAt(p).Name + "\n" + "  Отчество: " + std_list.ElementAt(p).Second_name + "\n" + "  Пол: " + std_list.ElementAt(p).Sex + "\n" + "  Дата рождения: " + std_list.ElementAt(p).Date_of_birth + "\n" + "  Специальность: " + std_list.ElementAt(p).Specialization + "\n" + "  Курс: " + std_list.ElementAt(p).Kurs + "\n" + "  Средний бал: " + std_list.ElementAt(p).Average_mark + "\n" + "Адрес:" + "\n" + "  Город: " + std_list.ElementAt(p).Adress.City + "\n" + "  Улица: " + std_list.ElementAt(p).Adress.Street + "\n" + "  Дом: " + std_list.ElementAt(p).Adress.House + "\n" + "  Квартира: " + std_list.ElementAt(p).Adress.Floor + "\n" + "  Индекс: " + std_list.ElementAt(p).Adress.index + "\n";
                }
                using (var fs = new FileStream("C:/Xml.xml", FileMode.OpenOrCreate))
                {
                    var xmlSerializer = new XmlSerializer(typeof(List<Student>));
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
                using (var fs = new FileStream("C:/Xml.xml", FileMode.Open))
                {
                    var xmlSerializer = new XmlSerializer(typeof(List<Student>));
                    var first = (List<Student>)xmlSerializer.Deserialize(fs);
                    if (first.Count != 0)
                    {
                        for (var p = 0; p < std_list.Count; p++)
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
            catch(Exception )
            {
                MessageBox.Show("XML файл отсутствует или пуст.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var std = new Student(new Adress());
                var regex1 = new Regex(@"^[A-ZА-Я][a-zа-я]{1,20}$");
                if (!regex1.IsMatch(textBox4.Text)) { throw new Exception("Проверьте формат ввода названия города"); }
                else { std.Adress.City = textBox4.Text; }
                if (textBox5.Text.Length == 6)
                {
                    if (int.TryParse(textBox5.Text, out std.Adress.index) == false)
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
                switch (radioButton1.Checked)
                {
                    case true when radioButton2.Checked == false:
                        std.Sex = "Пол женский";
                        break;
                    case false when radioButton2.Checked == true:
                        std.Sex = "Пол мужской";
                        break;
                    case false when radioButton2.Checked == false:
                        throw new Exception("Необходимо выбрать пол");
                }
                std_list.Add(std);
                for (var p = 0; p < std_list.Count; p++)
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
