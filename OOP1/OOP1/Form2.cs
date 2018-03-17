using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OOP1
{
    public partial class Form2 : Form
    {
        List<int> Radius = new List<int>();
        Random a = new Random();
        public Form2()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            Radius.Sort();
            foreach (int chisls in Radius)
                textBox3.Text += "Число=" + Convert.ToString(chisls) + Environment.NewLine;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            int[] mas = new int[Convert.ToInt32(textBox1.Text)];
            for (int b = 0; b < Convert.ToInt32(textBox1.Text); b++)
                mas[b] = a.Next(20);
            for (int c = 0; c < mas.Length; c++)
                Radius.Add(mas[c]);
            foreach (int chisl in Radius)
                textBox2.Text += "Число=" + Convert.ToString(chisl) + Environment.NewLine;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            Radius.Sort();
            Radius.Reverse();
            foreach (int chisls in Radius)
                textBox3.Text += "Число=" + Convert.ToString(chisls) + Environment.NewLine;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            string max = Convert.ToString(Radius.Max());
            textBox4.Text = max;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            string min = Convert.ToString(Radius.Min());
            textBox4.Text = min;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            string sum = Convert.ToString(Radius.Sum());
            textBox4.Text = sum;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
