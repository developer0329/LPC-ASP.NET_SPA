using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LPC.Models
{
    public class Log
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        Utils utils = new Utils();

        /**
         * Create new Seen Log
         * param : LogModel
         * return: int 
        **/
        public int insertSeenLog(LogModel lm)
        {
            string query = "INSERT INTO log(user_id, post_id, seen_time) VALUES(" +
                lm.user_id + "," +
                lm.post_id + ",'" +
                utils.getSQLDateString(lm.seen_time) + "');" +
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

        /**
         * Get All Seen Log
         * param : Post ID
         * return: List 
        **/
        public List<string> getAllSeenList(int post_id)
        {
            try
            {
                conn.Open();
                String query = "SELECT user.name FROM log " +
                    "INNER JOIN user On log.user_id=user.id WHERE log.post_id='" + post_id + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader mdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<string> seen_list = new List<string>();

                while (mdr.Read())
                {
                    seen_list.Add(mdr.GetString(0));
                }
                conn.Close();
                

                return seen_list;
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

        /**
         * Check Exist Seen log
         * param : LogModel(Real Check Value: User ID and Post ID)
         * return:  
        **/
        public void insertUpdateLog(LogModel lm)
        {
            try
            {
                conn.Open();
                String query = "SELECT id FROM post";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader mdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<int> posts = new List<int>();

                while (mdr.Read())
                {
                    posts.Add(mdr.GetInt32(0));
                }
                conn.Close();

                foreach (int post_id in posts)
                {
                    conn.Open();
                    query = "SELECT * FROM log WHERE post_id=" + post_id + " AND user_id=" + lm.user_id +";";
                    cmd = new MySqlCommand(query, conn);
                    mdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (!mdr.HasRows)
                    {
                        lm.post_id = post_id;
                        conn.Close();
                        insertSeenLog(lm);
                    }
                    conn.Close();
                }
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