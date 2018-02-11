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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            label1.Text = ">";
            if (textBox1.Text.Length > textBox2.Text.Length)
                textBox3.Text = "True";
            if (textBox1.Text.Length < textBox2.Text.Length)
                textBox3.Text = "False";
            if (textBox1.Text.Length == textBox2.Text.Length)
                textBox3.Text = "False";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            label1.Text = "<";
            if (textBox1.Text.Length > textBox2.Text.Length)
                textBox3.Text = "False";
            if (textBox1.Text.Length < textBox2.Text.Length)
                textBox3.Text = "True";
            if (textBox1.Text.Length == textBox2.Text.Length)
                textBox3.Text = "False";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "==";
            if (textBox1.Text.Length > textBox2.Text.Length)
                textBox3.Text = "False";
            if (textBox1.Text.Length < textBox2.Text.Length)
                textBox3.Text = "False";
            if (textBox1.Text.Length == textBox2.Text.Length)
                textBox3.Text = "True";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "!=";
            if (textBox1.Text.Length > textBox2.Text.Length)
                textBox3.Text = "True";
            if (textBox1.Text.Length < textBox2.Text.Length)
                textBox3.Text = "True";
            if (textBox1.Text.Length == textBox2.Text.Length)
                textBox3.Text = "False";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            label1.Text = "<<";
            char size = Convert.ToChar(textBox2.Text.Remove(1));
            string first = textBox2.Text.Remove(0,1);
            string second = textBox1.Text.PadRight(textBox2.Text.Length + 1, size);
            textBox3.Text = second + " " + first;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = ">>";
            char size = Convert.ToChar(textBox1.Text.Remove(0, textBox1.Text.Length - 1));
            string first = textBox2.Text.PadLeft(textBox2.Text.Length+1, size);
            string second = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
            textBox3.Text = second + " " + first;
        }
    }
}
