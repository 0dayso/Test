using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace KoDTicketing.DataAccessLayer
{
    sealed public class Connection
    {
        private Connection() { }
 
        public static int EXECommand(SqlCommand cmd, SqlConnection conn)
        {
            int i = 0;
            try
            {
                cmd.Connection = conn;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (SqlException _sqlException)
            {
                System.Text.StringBuilder err = new System.Text.StringBuilder();
                err.Append("Error executing [");
                err.Append(cmd.CommandText);
                //err.Append("]\nConnection String [");
                //err.Append(conn.ConnectionString);
                err.Append("]\nError [");
                err.Append(_sqlException.Message);
                err.Append("]");
                Logger.Write(err.ToString());
                return 0;
            }
            catch (InvalidOperationException _sqlException)
            {
                Logger.Write(_sqlException.Message);
            }
            finally
            {
                conn.Close();
            }
            return i;
        }
        public static int bulkinsert(DataTable source_table, SqlConnection conn,string detination_table)
        {
            int i = 1;
            
          using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
          {
              conn.Open();
              bulkCopy.DestinationTableName =detination_table;
                  //"[dbo].[ShowAudit_Table]";
              try
              {
                  bulkCopy.WriteToServer(source_table);
              }
              catch (Exception ex)
              {
                  i = 0;
                  Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.Message.ToString());
              }
              finally
              {
                  conn.Close();
              }
          }
          return i;
        }
           
        public static DataTable readTab(string str, SqlConnection conn)
        {
            //This implementation of the SqlDataAdapter opens and closes a SqlConnection if it is not already open. 
            //This can be useful in an application that must call the Fill method for two or more SqlDataAdapter objects. 
            //If the SqlConnection is already open, you must explicitly call Close or Dispose to close it.
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(str, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (SqlException _sqlException)
            {
                System.Text.StringBuilder err = new System.Text.StringBuilder();
                err.Append("Error creating DataTable for [");
                err.Append(str);
                //err.Append("]\nConnection String [");
                //err.Append(conn.ConnectionString);
                err.Append("]\nError [");
                err.Append(_sqlException.Message);
                err.Append("]");
                Logger.Write(err.ToString());
                return null;
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Trace.Assert(false, ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static DataTable readTab(SqlCommand command, SqlConnection conn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = command;
                cmd.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (SqlException _sqlException)
            {
                System.Text.StringBuilder err = new System.Text.StringBuilder();
                err.Append("Error creating dataAdapter for [");
                err.Append(command.CommandText);
                //err.Append("]\nConnection String [");
                //err.Append(conn.ConnectionString);
                err.Append("]\nError ["); 
                err.Append(_sqlException.Message);
                err.Append("]");
                Logger.Write(err.ToString());
                return null;
            }
        }

        public static DataSet readDataSet(string str, SqlConnection conn)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(str, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException _sqlException)
            {
                System.Text.StringBuilder err = new System.Text.StringBuilder();
                err.Append("Error retrieving data for [");
                err.Append(str);
                //err.Append("]\nConnection String [");
                //err.Append(conn.ConnectionString);
                err.Append("]\nError [");
                err.Append(_sqlException.Message);
                err.Append("]");
                Logger.Write(err.ToString());

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static DataSet readDataSet(SqlCommand command, SqlConnection conn)
        {
            try
            {
                command.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException _sqlException)
            {
                System.Text.StringBuilder err = new System.Text.StringBuilder();
                err.Append("Error creating DataSet for: [");
                err.Append(command.CommandText);
                //err.Append("]\nConnection String [");
                //err.Append(conn.ConnectionString);
                err.Append("]\nError [");
                err.Append(_sqlException.Message);
                err.Append("]");
                Logger.Write(err.ToString());
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public static DataSet APIreadDataSet(SqlCommand command, SqlConnection conn)
        {
            DataSet ds = new DataSet();
            try
            {
                command.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (SqlException _sqlException)
            {
                System.Text.StringBuilder err = new System.Text.StringBuilder();
                err.Append("Error creating DataSet for: [");
                err.Append(command.CommandText);
                //err.Append("]\nConnection String [");
                //err.Append(conn.ConnectionString);
                err.Append("]\nError [");
                err.Append(_sqlException.Message);
                err.Append("]");
                Logger.Write(err.ToString());
                ds = new DataSet();
                DataTable dt=new DataTable();
                dt.Columns.Add("Exception", typeof(string));
                dt.Rows.Add(_sqlException.Message.ToString());
                ds.Tables.Add(dt);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }

        public static DataTable APIreadTab(SqlCommand command, SqlConnection conn)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = command;
                cmd.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                
            }
            catch (SqlException _sqlException)
            {
                System.Text.StringBuilder err = new System.Text.StringBuilder();
                err.Append("Error creating dataAdapter for [");
                err.Append(command.CommandText);
                //err.Append("]\nConnection String [");
                //err.Append(conn.ConnectionString);
                err.Append("]\nError [");
                err.Append(_sqlException.Message);
                err.Append("]");
                Logger.Write(err.ToString());
                dt=new DataTable();
                dt.Columns.Add("Exception", typeof(string));
                dt.Rows.Add(_sqlException.Message.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        #region -- Transaction Status
        /// <summary>
        /// Fields Used in order  -- reference id, description, status, booking id
        /// </summary>
        /// <param name="Values"></param>
        public static void LogEntry(params string[] Values)
        {
            DBAccess.LogEntry(Values);
        }

        #endregion

        #region Logging
        public static void LogMessage(String message, System.Diagnostics.TraceEventType severity)
        {
            try
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry logEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry();
                logEntry.AppDomainName = "";
                logEntry.Severity = severity;
                logEntry.Message = message;
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntry);
            }
            catch (Exception ex)
            {
                //Logging not configured properly and message could not be written
                throw ex;
            }
        }

        #endregion
    }
}
