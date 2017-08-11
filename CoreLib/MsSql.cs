using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace CoreLib
{
    public class MsSql
    {


    }

    #region SqlExecute

    public class SqlExecute : MsSql {

        private string connectionString;
        public SqlExecute(string connectionString)
        {
            this.connectionString = connectionString;
        }

     

        /// <summary>
        ///  執行命令
        /// </summary>
        /// <param name="execString"></param>
        /// <returns></returns>
        public Boolean Execute(string execString)
        {
            
            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(execString, conn);
            conn.Open();
            int n = cmd.ExecuteNonQuery();
            conn.Close();


            return n > 0;
        }

        public Boolean Execute(string execString, List<SqlParameter> paramName)
        {

            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(execString, conn);
            foreach (SqlParameter s in paramName)
            {
                cmd.Parameters.Add(s);
            }

            conn.Open();
            int n = cmd.ExecuteNonQuery();
            conn.Close();


            return n > 0;
        }


        public DataSet GetDataset(string sqlQuery)
        {

            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, this.connectionString);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;

        }


        public DataSet GetDataset(string sqlQuery, CommandType type, List<SqlParameter> param)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.CommandType = type;
            foreach (SqlParameter s in param)
            {
                cmd.Parameters.Add(s);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;
        }

        public DataSet GetDataset(string sqlQuery, List<SqlParameter> paramName)
        {


            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            foreach (SqlParameter s in paramName)
            {
                cmd.Parameters.Add(s);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;
        }

        /*
        public Boolean Execute(string execString, SqlParameterCollection sqlParameters) {

            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(execString, conn);
            cmd.Parameters.Add();

            conn.Open();
            int n = cmd.ExecuteNonQuery();
            conn.Close();
           

            return false;
        }
        */


        /// <summary>
        /// 回傳單一值的欄位
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public object ExecuteScalar(string queryString)
        {

            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(queryString, conn);
            conn.Open();
            object result = cmd.ExecuteScalar();
            conn.Close();
            return result;
        }


        public object ExecuteScalar(string queryString, List<SqlParameter> paramName)
        {

            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(queryString, conn);
            foreach (SqlParameter s in paramName)
            {
                cmd.Parameters.Add(s);
            }
            conn.Open();
            object result = cmd.ExecuteScalar();
            conn.Close();
            return result;


        }


    }

    #endregion


    #region SqlDatareader

    public class SqlReader : MsSql
    {

        SqlDataReader sr;
        string connectionString;
        SqlConnection connection;

        public SqlReader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlDataReader ExecuteReader(string queryString)
        {

            this.connection = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader sr = cmd.ExecuteReader();

            return sr;
        }



        public void Close()
        {
            if (!sr.IsClosed)
            {
                sr.Close();
            }

            if (this.connection.State != System.Data.ConnectionState.Closed)
            {
                this.connection.Close();
            }

        }


    }

    #endregion



}
