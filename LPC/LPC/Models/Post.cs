using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LPC.Models
{
    public class Post
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        Utils utils = new Utils();

        /**
         * Create new Post
         * param : PostModel
         * return: int 
        **/
        public int CreatePost(PostModel pm)
        {
            string query = "INSERT INTO post(title, content, author, date) VALUES('" +
                pm.title + "','" +
                pm.content + "'," +
                pm.author + ",'" +
                utils.getSQLDateString(pm.date) + "');" +
                "select last_insert_id();";

            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}