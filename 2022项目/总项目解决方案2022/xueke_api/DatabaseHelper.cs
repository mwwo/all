using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace xueke_api
{
    public class DatabaseHelper
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;

        // 根据ID查询用户
        public DataTable GetKey(string key)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string MySql = "SELECT * FROM mykeys WHERE mykey = @key";
                MySqlCommand cmd = new MySqlCommand(MySql, conn);
                cmd.Parameters.AddWithValue("@key", key);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Dispose();
                conn.Close();

            }
            return dt;
        }

        // 查询所有用户
        public DataTable GetAllKeys()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string MySql = "SELECT * FROM mykeys";
                MySqlCommand cmd = new MySqlCommand(MySql, conn);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Dispose();
                conn.Close();
            }
            
            return dt;
        }

        #region  修改次数

        public bool editecishu(string key)
        {

            MySqlConnection mycon = new MySqlConnection(connectionString);
            mycon.Open();
            string sql = "update mykeys SET cishu = cishu - 1  where mykey='" + key + " ' ";
            MySqlCommand cmd = new MySqlCommand(sql, mycon);         //SQL语句读取textbox的值'"+skinTextBox1.Text+"'

            int count = cmd.ExecuteNonQuery();  //count就是受影响的行数,如果count>0说明执行成功,如果=0说明没有成功.
            mycon.Close();
            cmd.Dispose();
            if (count>0)
            {
                return true;    
            }
            else { return false; }  
        }
        #endregion-


        #region  修改时间

        public  void editetime(string key, string time)
        {

            MySqlConnection mycon = new MySqlConnection(connectionString);
            mycon.Open();


            MySqlCommand cmd = new MySqlCommand("update mykeys SET extime = '" + time + " '  where mykey='" + key + " ' ", mycon);         //SQL语句读取textbox的值'"+skinTextBox1.Text+"'

            int count = cmd.ExecuteNonQuery();  //count就是受影响的行数,如果count>0说明执行成功,如果=0说明没有成功.
            mycon.Close();
        }
        #endregion

        #region  添加下载记录

        public bool  adddownlog(string key, string link, string isvip, string cishu, string day, string price, string commercialLevel,  string stageId)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            MySqlConnection mycon = new MySqlConnection(connectionString);
            mycon.Open();

            string sql = "INSERT INTO downlogs (mykey,link,isvip,downtime,cishu,day,price,commercialLevel, stageId)VALUES(@key, @link, @isvip, @downtime,@cishu,@day,@price,@commercialLevel, @stageId)";


            MySqlCommand cmd = new MySqlCommand(sql, mycon);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@link", link);
            cmd.Parameters.AddWithValue("@isvip", isvip);
            cmd.Parameters.AddWithValue("@downtime", time);
            cmd.Parameters.AddWithValue("@cishu", cishu);
            cmd.Parameters.AddWithValue("@day", day);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@commercialLevel", commercialLevel);
            cmd.Parameters.AddWithValue("@stageId", stageId);
            int count = cmd.ExecuteNonQuery();  //count就是受影响的行数,如果count>0说明执行成功,如果=0说明没有成功.


            mycon.Close();
            cmd.Dispose();
            if (count > 0)
            {
                return true;
            }
            else { return false; }
        }
        #endregion




    }
}