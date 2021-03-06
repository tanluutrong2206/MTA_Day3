using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class DataGridView : Form
    {
        public DataGridView()
        {
            InitializeComponent();

            SqlConnection connection = new SqlConnection("server=TANLUUTRONG2206\\SQLEXPRESS;database=Northwind;uid=sa;pwd=luutrongtan");

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Customers";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            DataSet data = new DataSet();
            dataAdapter.Fill(data, "Customers");
            dgvCustomer.DataSource = data.Tables["Customers"];
        }

        private void dgvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dgvCustomer.Rows.Count; i++)
            {
                if (dgvCustomer.Rows[i].Selected)
                {
                    dgvCustomer.Rows[i].Selected = false;
                }
            }
            dgvCustomer.Rows[e.RowIndex].Selected = true;

            var customerId = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();

            MessageBox.Show("Selected customer with id: " + customerId);
        }
    }
}
