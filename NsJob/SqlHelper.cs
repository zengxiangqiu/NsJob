using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace NsJob
{
    internal static class SqlHelper
    {
        public static DataTable GetDataTable(SqlConnection connection, string sqlSmt)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlSmt, connection);
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int Excute(SqlConnection connection, string sqlSmt)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            try
            {
                SqlCommand command = new SqlCommand(sqlSmt, connection);
                var result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
