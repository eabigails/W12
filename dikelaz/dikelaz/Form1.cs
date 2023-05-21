using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace dikelaz
{
    public partial class Form1 : Form
    {
        MySqlConnection mysqlconnection;

        MySqlCommand mysqlcommand;

        MySqlDataAdapter mysqldataadapter;

        MySqlDataReader mysqlreader;

        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
            string connection = "server=localhost;uid=root;pwd=;database=premier_league";
            mysqlconnection = new MySqlConnection(connection);
        }

        private void addPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Dock = DockStyle.Fill;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.TopLevel = false;
            panel1.Controls.Clear();
            form2.Show();
            this.panel1.Controls.Add(form2);
        }

        private void editManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Dock = DockStyle.Fill;
            form3.FormBorderStyle = FormBorderStyle.None;
            form3.TopLevel = false;
            panel1.Controls.Clear();
            form3.Show();
            this.panel1.Controls.Add(form3);
        }

        private void removePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form4 form4 = new Form4();
            form4.Dock = DockStyle.Fill;
            form4.FormBorderStyle = FormBorderStyle.None;
            form4.TopLevel = false;
            panel1.Controls.Clear();
            form4.Show();
            this.panel1.Controls.Add(form4);
        }
    }
}
