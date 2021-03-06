using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsForm.Models;

namespace WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = tbxFullName.Text;

            SqlConnection connection = new SqlConnection("server=TANLUUTRONG2206\\SQLEXPRESS;database=Northwind;uid=sa;pwd=luutrongtan");

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Customers";

            var dataReader = command.ExecuteReader();
            var customers = DataReaderMapToList<Customer>(dataReader);

            cbxDepartment.DataSource = customers.Select(x => x.CustomerID).ToList();
            connection.Close();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Ten khong duoc bo trong");

                tbxFullName.Focus();
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
