using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace 远程通信控制系统.Utiles
{
    sealed class CommonDao
    {
        string connStr;

        public CommonDao(string connStr)
        {
            this.connStr = connStr;
        }

        public CommonDao()
        {
            this.connStr = IniUtil.Read("DB", "connStr");
        }

        public List<IDictionary<string, Object>> GetList(string sql, params string[] args)
        {
            List<IDictionary<string, Object>> list = new List<IDictionary<string, object>>();
            using (OleDbConnection conn = new OleDbConnection(this.connStr))
            {

                OleDbCommand cmd = new OleDbCommand(String.Format(sql, args), conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IDictionary<string, object> dic = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dic.Add(reader.GetName(i), reader.GetValue(i));
                    }
                }
            }
            return list;
        }

        public String GetStr(string sql, params string[] args)
        {
            String ret = "";
            using (OleDbConnection conn = new OleDbConnection(this.connStr))
            {
                OleDbCommand cmd = new OleDbCommand(String.Format(sql, args), conn);
                object obj = cmd.ExecuteScalar();
                ret = obj.ToString();
            }
            return ret;
        }

        public int Execute(string sql, params string[] args)
        {
            int count = 0;
            using (OleDbConnection conn = new OleDbConnection(this.connStr))
            {
                OleDbCommand cmd = new OleDbCommand(String.Format(sql, args), conn);
                count = cmd.ExecuteNonQuery();
            }
            return count;
        }
    }
}
