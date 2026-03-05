namespace Task1WinForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button1;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        textBox1 = new();
        button1 = new();

        textBox1.Location = new System.Drawing.Point(50, 50);
        textBox1.Size = new System.Drawing.Size(200, 30);  

        button1.Location = new System.Drawing.Point(50, 100);
        button1.Size = new System.Drawing.Size(100, 40);
        button1.Text = "Greetings!";
        button1.Click += button1_Click;
        
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(textBox1);
        Controls.Add(button1);
        Text = "Form1";
    }

    #endregion
}
