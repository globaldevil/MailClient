using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace SendEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            GuiEmail(textBox1.Text,textBox3.Text,textBox4.Text,textBox5.Text);
        }
        public void GuiEmail(string from, string to, string subject, string message)
        {
            MailMessage mess= new MailMessage(from,to,subject,message);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(textBox1.Text, textBox2.Text);
            client.Send(mess);
            MessageBox.Show("Sent Successful");
        }
    }
}
