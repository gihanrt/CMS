using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ComMgtSystem.Views
{
    public partial class DisplayData : Form
    {
        SqlConnection sc = new SqlConnection("Data Source=LSF-PC;Initial Catalog=CMS_DB;Integrated Security=True; MultipleActiveResultSets=True");
        string tableName, columnName ;
        public DisplayData()
        {
            InitializeComponent();
        }

        private void DisplayData_Load(object sender, EventArgs e)
        {
            //load data()
            sc.Open();
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "dbo.MainView";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sc;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);

            //MessageBox.Show(ds.Tables[0].Columns.Count.ToString());
            dataGridView1.DataSource = ds;    
            dataGridView1.DataMember = ds.Tables[0].ToString();
            
            sc.Close();
        }

        #region Tab1
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hwllow");
        } 
        #endregion

        #region Tab2

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //check whether valid cell
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                //return the clicked cell column name
                columnName = dataGridView1.Columns[e.ColumnIndex].HeaderText.ToString();

                //execute query to update table @param column name
                string sql = "select F_Name from CMS_DB.dbo.Sub_Fields A, CMS_DB.dbo.Fields B where A.SF_Name = '" + columnName + "' and A.FID = B.FID";
                SqlCommand cmd = new SqlCommand(sql, sc);
                sc.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    tableName = reader.GetString(0);
                }

                sc.Close();
                //if table name were found begin edit
                //otherwise don't let user to edit

                if (!string.IsNullOrEmpty(tableName))
                {
                    //start editing 
                    dataGridView1.BeginEdit(true);

                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //let user to know editing begins
            //if exception occor cancel edit            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //let user to know value has been changed
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //execute update query to update cell new value to database
            //only update int value
            int value = (int)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            Console.WriteLine("value to enter is :" + value);

            string sql = "update CMS_DB.dbo." + tableName + " set " + columnName + " = " + value + " where EID = " + (e.RowIndex + 1) + "";
            Console.WriteLine(tableName + " " + columnName + " " + (e.RowIndex + 1));

            SqlCommand cmd = new SqlCommand(sql, sc);
            sc.Open();
            int result = cmd.ExecuteNonQuery();
            Console.WriteLine("number of rows affected " + result);
            dataGridView1.Refresh();
            sc.Close(); 
        #endregion
        }

        


    }
}
