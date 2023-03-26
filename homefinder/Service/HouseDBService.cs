using homefinder.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace homefinder.Service
{
    public class HouseDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["homefinder"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);

        #region 取得單一房屋資料
        public Rental GetRentalById(Guid Id)
        {
            Rental Data = new Rental();
            string sql = $@"select * from RENTAL where rental_id='{Id}'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.rental_id = Guid.Parse((string)dr["rental_id"]);//不確定是否這樣
                Data.publisher = dr["publisher"].ToString();
                Data.title = dr["title"].ToString();
                Data.content = dr["content"].ToString();
                Data.address = dr["address"].ToString();
                Data.area= Convert.ToInt32(dr["area"]);
                Data.rent= Convert.ToInt32(dr["rent"]);
                Data.type = Convert.ToString(dr["type"]);
                Data.adminfee = Convert.ToInt32(dr["adminfee"]);
                Data.waterfee = Convert.ToInt32(dr["waterfee"]);
                Data.electricitybill = Convert.ToInt32(dr["electricitybill"]);
                Data.check = Convert.ToBoolean(dr["check"]);
                Data.tenant = Convert.ToBoolean(dr["tenant"]);
                Data.uploadtime = Convert.ToDateTime(dr["uploadtime"]);
                Data.Member.members_id= Guid.Parse((string)dr["members_id"]);//不確定是否這樣
            }
            catch (Exception)
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
        #region 取得房屋編號陣列

        #endregion
        #region 設定最大頁數方法
        #endregion
        #region 新增房屋
        #region 新增 Rental
        
        public void InsertHouse_Rental(Rental newData)
        {
            string sql = $@"INSERT INTO RENTAL(title,content,address,area,rent,type,adminfee,waterfee,electricitybill,check,tenant,uploadtime) VALUES 
                        ('{newData.title}','{newData.content}','{newData.address}','{newData.area}','{newData.rent}','{newData.type}','{newData.adminfee}','{newData.waterfee}','{newData.electricitybill}','{newData.tenant}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}') ";
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
        #region 新增 Equipment
        
        public void InsertHouse_Equipment(Equipment newData)
        {
            string sql = $@"INSERT INTO EQUIPMENT(equipmentname,img1,img2,img3,img4,img5,img6,img7,img8) VALUES 
                        ('{newData.equipmentname}','{newData.img1}','{newData.img2}','{newData.img3}','{newData.img4}','{newData.img5}','{newData.img6}','{newData.img7}','{newData.img8}') ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
        #endregion

        #region 修改房屋=>還要再改
        public void UpdateRental(Rental newData)
        {
            string sql = $@"update RENTAL(title,content,address,area,rent,type,adminfee,waterfee,electricitybill,check,tenant,uploadtime) set 
                        ('{newData.title}','{newData.content}','{newData.address}','{newData.area}','{newData.rent}','{newData.type}','{newData.adminfee}','{newData.waterfee}','{newData.electricitybill}','{newData.tenant}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}') ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
        #region 刪除房屋
        public void DeleteRental(Guid Id)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($@" DELETE FROM RENTAL WHERE rental_id = {Id}; ");
            sql.AppendLine($@" DELETE FROM EQUIPMENT WHERE euipment_id = {Id}; ");
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion
        #region 審核房屋
        public bool CheckUpload(Guid Id)
        {
            string sql = $@"update RENTAL set check='1' where rental_id='{Id}'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return true;
        }
        #endregion
    }
}