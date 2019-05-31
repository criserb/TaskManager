using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager.Model
{
    public class TaskRepo
    {
        public List<Task> TaskRepository { get; set; }

        public TaskRepo()
        {
            TaskRepository = GetTaskRepo();
        }

        //Returns all the records in table  
        public List<Task> GetTaskRepo()
        {
            List<Task> listOfTasks = new List<Task>();

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.TaskManagerConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in TaskCatalog->Properties-?Settings.settings");
                }

                SqlCommand query = new SqlCommand("SELECT * from Tasks", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Task t = new Task();
                    t.Id = (int)row["Id"];
                    t.Status = row["Status"].ToString();
                    t.Priority = row["Priority"].ToString();
                    t.Term = row["Term"].ToString();
                    t.Content = row["Content"].ToString();

                    listOfTasks.Add(t);
                }

                return listOfTasks;
            }
        }
        
        //Adding new record to the database
        //with the help of stored procedure    
        public void AddNewRecord(Task TaskRecord)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.TaskManagerConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in TaskCatalog->Properties-?Settings.settings");
                }
                else if (TaskRecord == null)
                    throw new Exception("The passed argument 'TaskRecord' is null");

                SqlCommand query = new SqlCommand("addRecord", conn);
                conn.Open();
                query.CommandType = CommandType.StoredProcedure;
                SqlParameter param0 = new SqlParameter("pId", SqlDbType.Int);
                SqlParameter param1 = new SqlParameter("pStatus", SqlDbType.VarChar);
                SqlParameter param2 = new SqlParameter("pPriority", SqlDbType.VarChar);
                SqlParameter param3 = new SqlParameter("pTerm", SqlDbType.VarChar);
                SqlParameter param4 = new SqlParameter("pContent", SqlDbType.VarChar);

                param0.Value = TaskRecord.Id;
                param1.Value = TaskRecord.Status;
                param2.Value = TaskRecord.Priority;
                param3.Value = TaskRecord.Term;
                param4.Value = TaskRecord.Content;


                query.Parameters.Add(param0);
                query.Parameters.Add(param1);
                query.Parameters.Add(param2);
                query.Parameters.Add(param3);
                query.Parameters.Add(param4);

                query.ExecuteNonQuery();
            }
        }

        
        //Deleting the record with reference to supplied id
        //with the help of stored procedure
        
        public void DelRecord(int id)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.TaskManagerConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in TaskCatalog->Properties-?Settings.settings");
                }

                SqlCommand query = new SqlCommand("deleteRecord", conn);
                conn.Open();
                query.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("pId", SqlDbType.Int);
                param1.Value = id;
                query.Parameters.Add(param1);

                query.ExecuteNonQuery();
            }
        }

         //Updating the Task Record with reference to supplied id
         //with the help of stored procedure
         
        public void UpdateRecord(Task TaskRecord)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.TaskManagerConnectionString))
            {
                if (conn == null)
                {
                    throw new Exception("Connection String is Null. Set the value of Connection String in TaskCatalog->Properties-?Settings.settings");
                }

                SqlCommand query = new SqlCommand("updateRecord", conn);
                conn.Open();
                query.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("pId", SqlDbType.Int);
                SqlParameter param2 = new SqlParameter("pStatus", SqlDbType.VarChar);
                SqlParameter param3 = new SqlParameter("pPriority", SqlDbType.VarChar);
                SqlParameter param4 = new SqlParameter("pTerm", SqlDbType.VarChar);
                SqlParameter param5 = new SqlParameter("pContent", SqlDbType.VarChar);

                param1.Value = TaskRecord.Id;
                param2.Value = TaskRecord.Status;
                param3.Value = TaskRecord.Priority;
                param4.Value = TaskRecord.Term;
                param5.Value = TaskRecord.Content;

                query.Parameters.Add(param1);
                query.Parameters.Add(param2);
                query.Parameters.Add(param3);
                query.Parameters.Add(param4);
                query.Parameters.Add(param5);

                query.ExecuteNonQuery();
            }
        }
    }
}
