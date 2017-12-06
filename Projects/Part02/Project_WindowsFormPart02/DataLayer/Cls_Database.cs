using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer
{
    public class Cls_Database : IDisposable
    {
        //field
       private SqlConnection cnn;
       private SqlCommand cmd;
      
        Cls_ReadConnectionString readconnect;

        public void Dispose()
        {
            if (cnn != null)
            {
                cnn.Dispose();
                cnn = null;
            }
        }
        //properties
        //contructor
        public Cls_Database(string path,bool winNT)
       {
           readconnect = new Cls_ReadConnectionString(path, winNT);
          
           cnn = new SqlConnection(readconnect.connectionstring);
           cmd = cnn.CreateCommand();
           
       }
        //mothod
        public bool KiemTraKetNoi(ref string err)
        {
            try
            {
                if (!string.IsNullOrEmpty(cnn.ConnectionString))
                {
                    cnn.Open();
                    return true;
                }
                else
                {
                    err = "Chuỗi kết nối rỗng";
                    return false;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return false;
        }
        public int MyExcuteNonQuery(string sql,CommandType ct)
        {
            int sodong = 0;
            cnn.Open();
            cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = cnn;
            cmd.CommandTimeout = 600;
            cmd.CommandType = ct;
            sodong=cmd.ExecuteNonQuery();
            cnn.Close();
            return sodong;
        }
    }
}
