using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LPC.Models
{
    public class Comment
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        Utils utils = new Utils();

        /**
         * Create new Comment
         * param : CommentModel
         * return: int 
        **/
        public int insertComment(CommentModel cm)
        {
            string query = "INSERT INTO comment(post_id, content, author, date) VALUES(" +
                cm.post_id + ",'" +
                cm.content + "'," +
                cm.author + ",'" +
                utils.getSQLDateString(cm.date) + "');" +
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
         * Get All Comments
         * param : 
         * return: List 
        **/
        public List<CommentViewModel> getAllComments()
        {
            try
            {
                conn.Open();
                String query = "SELECT post.id, post.title, post.content, user.name, post.date FROM post " + 
                    "INNER JOIN user On post.author=user.id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader mdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                List<CommentViewModel> comments = new List<CommentViewModel>();
                
                while (mdr.Read())
                {
                    CommentViewModel item = new CommentViewModel();
                    item.post_id = mdr.GetInt32(0);
                    item.post_title = mdr.GetString(1);
                    item.post_content = mdr.GetString(2);
                    item.post_author = mdr.GetString(3);
                    item.post_date = mdr.GetMySqlDateTime(4).ToString();
                    item.comments = new List<CommentViewItemModel>();
                    comments.Add(item);
                }
                conn.Close();
                
                foreach (CommentViewModel cvm in comments)
                {
                    conn.Open();
                    query = "SELECT comment.content, user.name, comment.date FROM comment " +
                        "INNER JOIN user On comment.author=user.id " +
                        "WHERE post_id=" + cvm.post_id + ";";
                    cmd = new MySqlCommand(query, conn);
                    mdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (mdr.HasRows)
                    {
                        while (mdr.Read())
                        {
                            CommentViewItemModel item = new CommentViewItemModel();

                            item.comment_content = mdr.GetString(0);
                            item.comment_author = mdr.GetString(1);
                            item.comment_date = mdr.GetMySqlDateTime(2).ToString();

                            cvm.comments.Add(item);
                        }
                    }
                    conn.Close();
                }

                return comments;
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