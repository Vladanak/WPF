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
            string first = textBox1.Text;
            int second = Convert.ToInt32(textBox2.Text);
            int index = first.Length - second;
            textBox3.Text = first.Remove(index);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = ">>";
            string first = textBox1.Text;
            int second = Convert.ToInt32(textBox2.Text);
            textBox3.Text = first.Remove(0, second);
        }


    }
}
