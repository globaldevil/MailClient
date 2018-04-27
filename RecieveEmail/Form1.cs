using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Limilabs.Client.IMAP;
using Limilabs.Mail;

namespace RecieveEmail
{
    public partial class Form1 : Form
    {
        private Imap imap;
        private IMail imail;
        private DataTable table;
         public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            imap = new Imap();
            imap.ConnectSSL("imap.gmail.com", 993);
            try
            {
                imap.Login(textBox1.Text, textBox2.Text);
                table = new DataTable();
                table.Columns.Add("IDMail", typeof(string));
                table.Columns.Add("Subject", typeof(string));
                MessageBox.Show("Connect Successful");
            }
            catch
            {
                MessageBox.Show("Connect Successful");
            }
        }

        private void btnInbox_Click(object sender, EventArgs e)
        {
            imap.SelectInbox();
            List<long> uids = imap.Search(Flag.Unseen);
            int i = int.Parse(textBox3.Text);
            if (i > uids.Count) i = uids.Count;
            int j = 0;
            foreach(long uid in uids)
            {
                if(j<i)
                {
                    imail = new MailBuilder().CreateFromEml(imap.GetHeadersByUID(uid));
                    DataRow row = table.NewRow();
                    row["IDMail"] = uid.ToString();
                    row["Subject"] = imail.Subject;
                    table.Rows.Add(row);
                    table.AcceptChanges();
                    j++;
                }
                else
                    break;
            }
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value!=null)
            {
                if(e.ColumnIndex==0)
                {
                    string id = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    imail = new MailBuilder().CreateFromEml(imap.GetMessageByUID(long.Parse(id)));
                    richTextBox1.Text = imail.Text;
                }
            }
        }
    }
}
