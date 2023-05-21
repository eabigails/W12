using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data.SqlClient;
//using Org.BouncyCastle.Utilities.Collections;
using Microsoft.SqlServer.Server;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace dikelaz
{
    public partial class Form3 : Form
    {

        MySqlConnection sqlconnect;
        MySqlCommand sqlcommand;
        MySqlDataAdapter SqlAdapter;
        MySqlDataReader DataReader;
        string sqlquery;
        string connectionString;
        DataTable manager = new DataTable();
        DataTable managers = new DataTable();
        DataTable dtteam = new DataTable();
        

        public Form3()
        {
            InitializeComponent();
            connectionString = "server=localhost;uid=root;pwd=ujinujin;database=premier_league";
            sqlconnect = new MySqlConnection(connectionString);
            dataGridView1.DataSource = manager;
            dataGridView2.DataSource = managers;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            sqlquery = $"select team_name 'Team Name', team_id from team;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(dtteam);
            comboBox_team.DataSource = dtteam;
            comboBox_team.ValueMember = "team_id";
            comboBox_team.DisplayMember = "Team Name";


            manager = new DataTable();
            sqlquery = $"select m.manager_id, m.manager_name, n.nation, m.birthdate from manager m, team t, nationality n where m.manager_id = t.manager_id  and m.nationality_id = n.nationality_id and t.team_id = '{comboBox_team.SelectedValue}';";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(manager);
            dataGridView1.DataSource = manager;

            managers = new DataTable();
            sqlquery = $"select m.manager_id, m.manager_name, n.nation, m.birthdate from manager m, nationality n where m.nationality_id = n.nationality_id and m.working = 0;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(managers);
            dataGridView2.DataSource = managers;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtteam = new DataTable();
            update();

        }
        private void update()
        {
            manager = new DataTable();
            sqlquery = $"select m.manager_id, m.manager_name, n.nation, m.birthdate from manager m, team t, nationality n where m.manager_id = t.manager_id  and m.nationality_id = n.nationality_id and t.team_id = '{comboBox_team.SelectedValue}';";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(manager);
            dataGridView1.DataSource = manager;


            update2();

        }
        private void update2()
        {
            managers = new DataTable();
            sqlquery = $"select m.manager_id, m.manager_name, n.nation, m.birthdate from manager m, nationality n where m.nationality_id = n.nationality_id and m.working = 0;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(managers);
            dataGridView2.DataSource = managers;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                string manager2 = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                string Comdd = $"update team set manager_id = '" + manager2 + "' where team_id = '" + comboBox_team.SelectedValue + "';";
                try
                {
                    sqlconnect.Open();
                    sqlcommand = new MySqlCommand(Comdd, sqlconnect);
                    DataReader = sqlcommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlconnect.Close();
                }

                string manager1 = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string Comd = $"update manager set working = 0 where manager_id = '" + manager1 + "';";
                try
                {
                    sqlconnect.Open();
                    sqlcommand = new MySqlCommand(Comd, sqlconnect);
                    DataReader = sqlcommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlconnect.Close();
                }



                string commands = $"update manager set working = 1 where manager_id =  '" + manager2 + "'; ";
                try
                {
                    sqlconnect.Open();
                    sqlcommand = new MySqlCommand(commands, sqlconnect);
                    DataReader = sqlcommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlconnect.Close();
                    update();
                }
            }
            else
            {
                MessageBox.Show("Please select Manager!");
            }
        }
    }
}
