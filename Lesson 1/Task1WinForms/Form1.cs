using Task2;
using System;
using System.Windows.Forms;

namespace Task1WinForms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var hello = new HelloLibrary();
        MessageBox.Show(hello.GetMessage(textBox1.Text));
    }
}
