using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace dikelaz
{
    public partial class Form2 : Form
    {

        MySqlConnection sqlconnect;
        MySqlCommand sqlcommand;
        MySqlDataAdapter SqlAdapter;
        MySqlDataReader sqlDataReader;
        string connectionString;
        string sqlquery;
        DataTable player = new DataTable();
        DataTable dtteam = new DataTable();
        DataTable dtnationality = new DataTable();
        DataTable playpos = new DataTable();
  
        public Form2()
        {

            InitializeComponent();
            connectionString = "server=localhost;uid=root;pwd=ujinujin;database=premier_league";
            sqlconnect = new MySqlConnection(connectionString);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

        }
       

        private void Form2_Load(object sender, EventArgs e)
        {

            sqlquery = "select team_name 'Team Name', team_id from team;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(dtteam);
            comboBox_team.DataSource = dtteam;
            comboBox_team.ValueMember = "team_id";
            comboBox_team.DisplayMember = "Team Name";

            sqlquery = "select nation, nationality_id from nationality;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(dtnationality);
            comboBox_nationality.DataSource = dtnationality;
            comboBox_nationality.ValueMember = "nationality_id";
            comboBox_nationality.DisplayMember = "nation";

            sqlquery = "select playing_pos from player group by playing_pos;";
            sqlcommand = new MySqlCommand(sqlquery, sqlconnect);
            SqlAdapter = new MySqlDataAdapter(sqlcommand);
            SqlAdapter.Fill(playpos);
            comboBox_position.DataSource = playpos;
            comboBox_position.ValueMember = "playing_pos";
            comboBox_position.DisplayMember = "playing_pos";


        }

        private void comboBox_nationality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            string nama = textBox_nama.Text;
            string id = textBox_id.Text;
            string height = textBox_height.Text;
            string num = textBox5.Text;
            string weight = textBox_weight.Text;
            string nationality = comboBox_nationality.SelectedValue.ToString();
            string pos = comboBox_position.SelectedValue.ToString();
            string team = comboBox_team.SelectedValue.ToString();
            string bday = dateTimePicker1.Value.Date.ToString("yyyyMMdd");

            string commad = $"insert into player values ('{id}','{num}','{nama}','{nationality}', '{pos}', '{height}', '{weight}', '{bday}', '{team}', 1, 0);";

            try
            {
                sqlconnect.Open();
                sqlcommand = new MySqlCommand(commad, sqlconnect);
                sqlDataReader = sqlcommand.ExecuteReader();

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
            
            textBox5.Clear();
            textBox_height.Clear();
            textBox_height.Clear();
            textBox_id.Clear();
            textBox_nama.Clear();
            textBox_weight.Clear();

        }
        private void update()
        {
            player.Clear();
            try
            {
                string comb2 = "select * from player;";
                sqlcommand = new MySqlCommand(comb2, sqlconnect);
                SqlAdapter = new MySqlDataAdapter(sqlcommand);
                SqlAdapter.Fill(player);
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}