using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OOP1
{
    public partial class Form2 : Form
    {
        private readonly List<int> _radius = new List<int>();
        private readonly Random _a = new Random();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            _radius.Sort();
            foreach (int chisls in _radius)
                textBox3.Text += "Число=" + Convert.ToString(chisls) + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
                return;
            textBox2.Clear();
            int[] mas = new int[Convert.ToInt32(textBox1.Text)];
            for (int b = 0; b < Convert.ToInt32(textBox1.Text); b++)
                mas[b] = _a.Next(20);
            foreach (var t in mas)
                _radius.Add(t);
            foreach (var chisl in _radius)
                textBox2.Text += "Число=" + Convert.ToString(chisl) + Environment.NewLine;
        }

        #region Unused
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void Form2_Load(object sender, EventArgs e)
        {
        }
        private void label2_Click(object sender, EventArgs e)
        {
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            _radius.Sort();
            _radius.Reverse();
            foreach (int chisls in _radius)
                textBox3.Text += "Число=" + Convert.ToString(chisls) + Environment.NewLine;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            string max = Convert.ToString(_radius.Max());
            textBox4.Text = max;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            string min = Convert.ToString(_radius.Min());
            textBox4.Text = min;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            string sum = Convert.ToString(_radius.Sum());
            textBox4.Text = sum;
        }
    }
}