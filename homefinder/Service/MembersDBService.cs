using homefinder.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace homefinder.Service
{
    public class MembersDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["homefinder"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);

        #region 註冊
        #region 新增會員
        public void Register(Members newMember)
        {
            newMember.password = HashPassword(newMember.password);
            string sql = string.Empty;
            //小帥改
            if (newMember.identity==0)
            {
                sql = $@"INSERT INTO Members (account,password,name,email,phone,authcode,identity,sorce) VALUES ('{newMember.account}','{newMember.password}','{newMember.name}','{newMember.email}','{newMember.phone}','{newMember.authcode}','{newMember.identity}','0')";
            }
            else if (newMember.identity==1)
            {
                sql = $@"INSERT INTO Members (account,password,name,email,phone,authcode,identity,sorce) VALUES ('{newMember.account}','{newMember.password}','{newMember.name}','{newMember.email}','{newMember.phone}','{newMember.authcode}','{newMember.identity}','1')";
            }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion
        #region Hash密碼
        public string HashPassword(string Password)
        {
            string saltkey = "kdsnkvnakeav123";
            string saltkeyAndPassword = String.Concat(Password, saltkey);
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            //取得密碼轉化成byte資料
            byte[] PassData = Encoding.UTF8.GetBytes(saltkeyAndPassword);
            //取得HASH後byte資料
            byte[] HashData = sha256.ComputeHash(PassData);
            string Hashresult = Convert.ToBase64String(HashData);
            return Hashresult;
        }
        #endregion
        #region 查詢一筆資料
        public Members GetDataByAccount(string Account)
        {
            Members Data = new Members();
            string sql = $@"SELECT * FROM MEMBERS WHERE account='{Account}' ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.account = dr["account"].ToString();
                Data.password = dr["password"].ToString();
                Data.name = dr["name"].ToString();
                Data.email = dr["email"].ToString();
                Data.phone = dr["phone"].ToString();
                Data.authcode = dr["authcode"].ToString();
                Data.identity = Convert.ToInt32(dr["identity"]);
                Data.sorce = Convert.ToInt32(dr["sorce"]);
            }
            catch(Exception)
            {
                Data = null;
            }
            finally
            {
                conn.Close();
            }
            return Data;
        }
        #endregion
        #region 帳號註冊重複確認
        public bool AccountCheck(string Account)
        {
            Members Data = GetDataByAccount(Account);
            bool result = (Data == null);
            return result;
        }
        #endregion
        #region 信箱驗證
        public string EmailValidate(string Account,string AuthCode)
        {
            Members ValidateMember = GetDataByAccount(Account);
            string ValidateStr = string.Empty;
            if (ValidateMember != null)
            {
                if (ValidateMember.authcode == AuthCode)
                {
                    string sql = $@"UPDATE MEMBERS SET authcode='{string.Empty}' WHERE account='{Account}' ";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception e)
                    {
                        throw new Exception(e.Message.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                    ValidateStr = "帳號信箱驗證成功，現在可以登入";
                }
                else
                {
                    ValidateStr = "驗證碼錯誤，請重新確認或在註冊";
                }
            }
            else
            {
                ValidateStr = "查無此帳號，請再重新註冊";
            }
            return ValidateStr;
        }
        #endregion
        #endregion
        #region 登入
        #region 登入確認
        public string LoginCheck(string Account,string Password)
        {
            Members LoginMember = GetDataByAccount(Account);
            if (LoginMember != null)
            {
                if (string.IsNullOrWhiteSpace(LoginMember.authcode))
                {
                    if (PasswordCheck(LoginMember, Password))
                    {
                        return "";
                    }
                    else
                    {
                        return "密碼錯誤";
                    }
                }
                else
                {
                    return "尚未驗證，請去EMAIL收驗證信";
                }
            }
            else
            {
                return "無此會員，請去註冊";
            }
        }
        #endregion
        #region 密碼確認
        public bool PasswordCheck(Members CheckMember,string Password)
        {
            bool result = CheckMember.password.Equals(HashPassword(Password));
            return result;
        }
        #endregion
        #region 取得角色
        public string GetRole(string Account)
        {
            string Role = "renter";
            Members LoginMember = GetDataByAccount(Account);
            if (LoginMember.identity == 2)
            {
                Role += ",lessor";
            }
            return Role;
        }
        #endregion
        #endregion
        #region 修改密碼
        public string ChangePassword(string Account,string Password,string newPassword)
        {
            Members LoginMember = GetDataByAccount(Account);
            if (PasswordCheck(LoginMember, Password))
            {
                LoginMember.password = HashPassword(newPassword);
                string sql = $@"UPDATE MEMBERS SET password = '{LoginMember.password}' WHERE account='{Account}' ";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message.ToString());
                }
                finally
                {
                    conn.Close();
                }
                return "密碼修改完成";
            }
            else
            {
                return "舊密碼輸入錯誤";
            }
        }
        #endregion

    }
}