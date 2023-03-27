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
            //要改成這樣吧
            //string sql = $@" SELECT m.*,d.Name,d.Image FROM Article m INNER JOIN Members d ON m.Account = d.Account WHERE m.A_Id = {A_Id}; ";
            //string sql = $@"select * from RENTAL where rental_id='{Id}'";
            string sql = $@"SELECT m.*,d.name FROM RENTAL m INNER JOIN MEMBERS d ON m.publisher = d.Account WHERE m.rental_id = {Id}; ";
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

                Data.equipmentname = dr["equipmentname"].ToString();
                Data.img1 = dr["img1"].ToString();
                Data.img2 = dr["img2"].ToString();
                Data.img3 = dr["img3"].ToString();
                Data.img4 = dr["img4"].ToString();
                Data.img5 = dr["img5"].ToString();
                Data.img6 = dr["img6"].ToString();
                Data.img7 = dr["img7"].ToString();
                Data.img8 = dr["img8"].ToString();

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
        public List<Rental> GetDataList(string Search,string Account)
        {
            List<Rental> DataList = new List<Rental>();
            if (!string.IsNullOrWhiteSpace(Search))
            {
                DataList = GetAllDataList(Search, Account);
            }
            else
            {
                DataList = GetAllDataList(Account);
            }
            return DataList;
        }
        public List<Rental> GetAllDataList(string Search,string Account)
        {
            List<Rental> DataList = new List<Rental>();
            string sql = $@"SELECT m.*,d.name FROM(SELECT row_number() OVER(ORDER BY A_Id) AS sort,* FROM RENTAL WHERE ( equipmentname LIKE '%{Search}%' ) AND publisher = '{Account}' ) m INNER JOIN MEMBERS d ON m.publisher = d.Account WHERE m.sort ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Rental Data = new Rental();
                    Data.rental_id = Guid.Parse((string)dr["rental_id"]);//不確定是否這樣
                    Data.publisher = dr["publisher"].ToString();
                    Data.title = dr["title"].ToString();
                    Data.content = dr["content"].ToString();
                    Data.address = dr["address"].ToString();
                    Data.area = Convert.ToInt32(dr["area"]);
                    Data.rent = Convert.ToInt32(dr["rent"]);
                    Data.type = Convert.ToString(dr["type"]);
                    Data.adminfee = Convert.ToInt32(dr["adminfee"]);
                    Data.waterfee = Convert.ToInt32(dr["waterfee"]);
                    Data.electricitybill = Convert.ToInt32(dr["electricitybill"]);
                    Data.check = Convert.ToBoolean(dr["check"]);
                    Data.tenant = Convert.ToBoolean(dr["tenant"]);
                    Data.uploadtime = Convert.ToDateTime(dr["uploadtime"]);

                    Data.equipmentname = dr["equipmentname"].ToString();
                    Data.img1 = dr["img1"].ToString();
                    Data.img2 = dr["img2"].ToString();
                    Data.img3 = dr["img3"].ToString();
                    Data.img4 = dr["img4"].ToString();
                    Data.img5 = dr["img5"].ToString();
                    Data.img6 = dr["img6"].ToString();
                    Data.img7 = dr["img7"].ToString();
                    Data.img8 = dr["img8"].ToString();

                    Data.Member.members_id = Guid.Parse((string)dr["members_id"]);//不確定是否這樣
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }
        public List<Rental> GetAllDataList(string Account)
        {
            List<Rental> DataList = new List<Rental>();
            string sql = $@"SELECT m.*,d.name FROM(SELECT row_number() OVER(ORDER BY A_Id) AS sort,* FROM RENTAL WHERE publisher = '{Account}' ) m INNER JOIN MEMBERS d ON m.publisher = d.Account WHERE m.sort ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Rental Data = new Rental();
                    Data.rental_id = Guid.Parse((string)dr["rental_id"]);//不確定是否這樣
                    Data.publisher = dr["publisher"].ToString();
                    Data.title = dr["title"].ToString();
                    Data.content = dr["content"].ToString();
                    Data.address = dr["address"].ToString();
                    Data.area = Convert.ToInt32(dr["area"]);
                    Data.rent = Convert.ToInt32(dr["rent"]);
                    Data.type = Convert.ToString(dr["type"]);
                    Data.adminfee = Convert.ToInt32(dr["adminfee"]);
                    Data.waterfee = Convert.ToInt32(dr["waterfee"]);
                    Data.electricitybill = Convert.ToInt32(dr["electricitybill"]);
                    Data.check = Convert.ToBoolean(dr["check"]);
                    Data.tenant = Convert.ToBoolean(dr["tenant"]);
                    Data.uploadtime = Convert.ToDateTime(dr["uploadtime"]);

                    Data.equipmentname = dr["equipmentname"].ToString();
                    Data.img1 = dr["img1"].ToString();
                    Data.img2 = dr["img2"].ToString();
                    Data.img3 = dr["img3"].ToString();
                    Data.img4 = dr["img4"].ToString();
                    Data.img5 = dr["img5"].ToString();
                    Data.img6 = dr["img6"].ToString();
                    Data.img7 = dr["img7"].ToString();
                    Data.img8 = dr["img8"].ToString();

                    Data.Member.members_id = Guid.Parse((string)dr["members_id"]);//不確定是否這樣
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;
        }
        #endregion
        #region 新增 Rental

        public void InsertHouse_Rental(Rental newData)
        {
            string sql = $@"INSERT INTO RENTAL(title,content,address,area,rent,type,adminfee,waterfee,electricitybill,check,tenant,uploadtime,equipmentname,img1,img2,img3,img4,img5,img6,img7,img8) VALUES 
                        ('{newData.title}','{newData.content}','{newData.address}','{newData.area}','{newData.rent}','{newData.type}','{newData.adminfee}','{newData.waterfee}','{newData.electricitybill}','0','0','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{newData.equipmentname}','{newData.img1}','{newData.img2}','{newData.img3}','{newData.img4}','{newData.img5}','{newData.img6}','{newData.img7}','{newData.img8}') ";
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

        #region 修改房屋
        public void UpdateRental(Rental UpdateData)
        {
            string sql = $@"update RENTAL set title='{UpdateData.title}',content='{UpdateData.content}',address='{UpdateData.address}',area='{UpdateData.area}',rent='{UpdateData.rent}',type='{UpdateData.type}',adminfee='{UpdateData.adminfee}',waterfee='{UpdateData.waterfee}',electricitybill='{UpdateData.electricitybill}',
                        equipmentname='{UpdateData.equipmentname}',img1='{UpdateData.img1}',img2='{UpdateData.img2}',img3='{UpdateData.img3}',img4='{UpdateData.img4}',img5='{UpdateData.img5}',img6='{UpdateData.img6}',img7='{UpdateData.img7}',img8='{UpdateData.img8}',) ";
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
            string sql = $@" DELETE FROM RENTAL WHERE rental_id = {Id}; ";
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
        #region 審核房屋
        public void CheckUpload(Guid Id)
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

            //這裡感覺怪怪的
            //return true;
        }
        #endregion
    }
}