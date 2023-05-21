using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.SqlServer.Server;
using System.Data.Common;

namespace dikelaz
{
    public partial class Form4 : Form
    {

        MySqlConnection sqlconnect;
        MySqlCommand sqlcommand;
        MySqlDataAdapter SqlAdapter;
        MySqlDataReader sqlDataReader;
        DataTable players = new DataTable();
        string connectionString;
        string sqlquery;

        public Form4()
        {
            InitializeComponent();
            connectionString = "server=localhost;uid=root;pwd=ujinujin;database=premier_league";
            sqlconnect = new MySqlConnection(connectionString);
            dataGridView1.DataSource = players;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            sqlquery = "select team_name 'Team Name', team_id from team;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(players);
            comboBox1.DataSource = players;
            comboBox1.DisplayMember = "Team Name";
            comboBox1.ValueMember = "team_id";
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            players = new DataTable();
            delete();
        }
        private void delete()
        {
            players = new DataTable();
            sqlquery = "select p.player_id, p.team_number, p.player_name, n.nation, p.playing_pos, p.height, p.weight, p.birthdate, t.team_name FROM player p, nationality n, team t where p.nationality_id = n.nationality_id and t.team_id = p.team_id and p.team_id = '" + comboBox1.SelectedValue + "' and p.status = 1; ";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(players);
            dataGridView1.DataSource = players;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count >= 12)
            {
                string player = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string hore = $"update player set status = 0 where player_id = '{player}'";
                try
                {
                    sqlconnect.Open();
                    sqlcommand = new MySqlCommand(hore, sqlconnect);
                    SqlAdapter = new MySqlDataAdapter(sqlcommand);
                    sqlDataReader = sqlcommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlconnect.Close();
                    delete();
                }
            }
            else
            {
                MessageBox.Show("Player harus > 11");
            }
        }
    }
}
