using homefinder.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace homefinder.Service
{
    public class HouseDBService
    {
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["homefinder"].ConnectionString;
        private readonly SqlConnection conn = new SqlConnection(cnstr);

        #region 取得單一房屋資料
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

        #region 修改房屋
        #endregion
        #region 刪除房屋
        #endregion
        #region 
        #endregion
    }
}