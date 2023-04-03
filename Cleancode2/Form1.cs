using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Cleancode2
{
    public partial class Form1 : Form
    {
        public SqlConnection connection;
        public SqlCommand command;
        public string conString = ConfigurationManager.ConnectionStrings["CleanCode"].ConnectionString.ToString();
        private SqlDataAdapter adapter = new SqlDataAdapter();
        private DataTable table = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.studentTableAdapter.Fill(this.cleanCodeDataSet.Student);
                connection = new SqlConnection(conString);
                connection.Open();
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Display()
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Student";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                listStudent.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectCell;
                selectCell = listStudent.CurrentRow.Index;
                idStudent.Text = listStudent.Rows[selectCell].Cells[0].Value.ToString();
                nameStudent.Text = listStudent.Rows[selectCell].Cells[1].Value.ToString();
                birthdayStudent.Text = listStudent.Rows[selectCell].Cells[2].Value.ToString();
                gpaStudent.Text = listStudent.Rows[selectCell].Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {               
                command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Student (NameStudent, Birthday, GPA) VALUES (@NameStudent, @Birthday, @GPA)";
                command.Parameters.AddWithValue("@NameStudent", nameStudent.Text);
                command.Parameters.AddWithValue("@Birthday", birthdayStudent.Text);
                command.Parameters.AddWithValue("@GPA", gpaStudent.Text);
                command.ExecuteNonQuery();
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog;
                dialog = MessageBox.Show("Delete this student ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM Student WHERE ID = ( '" + idStudent.Text + "')";
                    command.ExecuteNonQuery();
                    Display();
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "UPDATE Student SET NameStudent = N'" + nameStudent.Text + "', Birthday = '" + birthdayStudent.Text + "', " +
                 "GPA = '" + gpaStudent.Text + "' WHERE ID = '" + idStudent.Text + "' ";
                command.ExecuteNonQuery();
                Display();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
