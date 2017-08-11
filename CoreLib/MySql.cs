using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using MySql.Data.MySqlClient;

namespace CoreLib
{
    public class MySql
    {
        private string _connectionString;

        public MySql(string connstring)
        {
            //this._connectionString = "server=127.0.0.1;port=3306;database=nc;uid=nc_user;password=!qaz2wsx";
            this._connectionString = connstring;
        }


        public string getConnectionString()
        {
            return _connectionString;
        }


        /// <summary>
        /// 測試資料庫
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public Boolean TestConnection(out string err)
        {

            try
            {
                MySqlConnection cn = new MySqlConnection(getConnectionString());
                cn.Open();

                cn.Close();
                err = "";
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                err = ex.ToString();
                return false;
            }

        }


        public DataSet GetDataset(string sqlQuery)
        {

            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlQuery, getConnectionString());
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;

        }

        public bool Execute(string sql)
        {

            MySqlConnection conn = new MySqlConnection(_connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Connection.Open();
            Boolean result = cmd.ExecuteNonQuery() > 0;
            cmd.Connection.Close();
            return result;

        }

        public bool Execute(string sql, List<MySqlParameter> parameters)
        {

            MySqlConnection conn = new MySqlConnection(_connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            foreach (MySqlParameter s in parameters)
            {
                cmd.Parameters.Add(s);
            }
            cmd.Connection.Open();
            Boolean result = cmd.ExecuteNonQuery() > 0;
            cmd.Connection.Close();
            return result;
        }

        public DataSet GetDataset(string sqlQuery, List<MySqlParameter> parameters)
        {

            MySqlConnection conn = new MySqlConnection(_connectionString);
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            foreach (MySqlParameter s in parameters)
            {
                cmd.Parameters.Add(s);
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;
        }



        public object ExecuteScalar(string sql, List<MySqlParameter> parameters)
        {

            MySqlConnection conn = new MySqlConnection(_connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            foreach (MySqlParameter s in parameters)
            {
                cmd.Parameters.Add(s);
            }

            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return obj;


        }

    }
}
