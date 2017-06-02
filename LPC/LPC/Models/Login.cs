using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LPC.Models
{
    public class Login
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        MySqlConnection conn = new MySqlConnection(connStr);

        /**
         * User Login
         * param : LoginModel
         * return: LoginUser
        **/
        public LoginUser loginUser(LoginModel lm)
        {
            try
            {
                conn.Open();
                String query = "SELECT * FROM user WHERE email='" + lm.Email + "' AND password='" + lm.Password + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader mdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                LoginUser result =  null;

                if(mdr.HasRows)
                {
                    while (mdr.Read())
                    {
                        result = new LoginUser();
                        result.id = mdr.GetInt32(0);
                        result.name = mdr.GetString(1);
                        result.email = mdr.GetString(2);
                        result.password = mdr.GetString(3);
                        result.type = mdr.GetString(4);
                    }
                }
                conn.Close();
                
                //Update Seen Log
                Log log = new Log();
                LogModel logModel = new LogModel();
                logModel.user_id = result.id;
                logModel.seen_time = DateTime.Now;

                log.insertUpdateLog(logModel);

                return result;
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