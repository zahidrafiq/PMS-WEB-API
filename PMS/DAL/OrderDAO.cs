using PMS.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PMS.DAL
{
    public class OrderDAO
    {
        public static ResponseResult SaveOrder(OrderDetails dto)
        {
            using (DBHelper helper = new DBHelper())
            {
                String sqlQuery = "";
                if (dto.OrderId > 0)
                {
                    //sqlQuery = String.Format(@"Update dbo.Products Set Name=@name,Price=@price,PictureName=@picname,ModifiedOn=@modifiedon,ModifiedBy=@modifiedby Where ProductId=@pid");
                    //SqlCommand cmd = new SqlCommand(sqlQuery);

                    //SqlParameter parm = new SqlParameter();
                    //parm.ParameterName = "name";
                    //parm.SqlDbType = System.Data.SqlDbType.VarChar;
                    //parm.Value = dto.;
                    //cmd.Parameters.Add(parm);

                    //parm = new SqlParameter();
                    //parm.ParameterName = "price";
                    //parm.SqlDbType = System.Data.SqlDbType.Int;
                    //parm.Value = dto.Price;
                    //cmd.Parameters.Add(parm);

                    //parm = new SqlParameter();
                    //parm.ParameterName = "picname";
                    //parm.SqlDbType = System.Data.SqlDbType.VarChar;
                    //parm.Value = dto.PictureName;
                    //cmd.Parameters.Add(parm);

                    //parm = new SqlParameter();
                    //parm.ParameterName = "modifiedon";
                    //parm.SqlDbType = System.Data.SqlDbType.DateTime;
                    //parm.Value = dto.ModifiedOn;
                    //cmd.Parameters.Add(parm);

                    //parm = new SqlParameter();
                    //parm.ParameterName = "modifiedby";
                    //parm.SqlDbType = System.Data.SqlDbType.Int;
                    //parm.Value = dto.ModifiedBy;
                    //cmd.Parameters.Add(parm);

                    //parm = new SqlParameter();
                    //parm.ParameterName = "pid";
                    //parm.SqlDbType = System.Data.SqlDbType.Int;
                    //parm.Value = dto.ProductId;
                    //cmd.Parameters.Add(parm);

                    //int a = helper.ExecuteQueryParm(cmd);
                    //return dto.ProductId;
                    return ResponseResult.GetSuccessObject();
                }
                else
                {
                    sqlQuery = String.Format(@"INSERT INTO dbo.Orders(OrderNum, IsPaid ,TotalAmount,OrderBy, CreatedOn, IsActive,OrderStatus) 
                        VALUES (@oNum,@pStatus,@tAmount,@orderBy,@createdon,@isactive,@oStatus); Select @@IDENTITY");

                    SqlCommand cmd = new SqlCommand(sqlQuery);

                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = "oNum";
                    parm.SqlDbType = System.Data.SqlDbType.VarChar;
                    parm.Value = dto.OrderNum;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "pStatus";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Value = dto.IsPaid;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "tAmount";
                    parm.SqlDbType = System.Data.SqlDbType.Float;
                    parm.Value = dto.TotalAmount;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "createdon";
                    parm.SqlDbType = System.Data.SqlDbType.DateTime;
                    parm.Value = dto.CreatedOn;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "orderBy";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Value = dto.OrderBy;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter(); 
                    parm.ParameterName = "isactive";
                    parm.SqlDbType = System.Data.SqlDbType.Bit;
                    parm.Value = true;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter(); 
                    parm.ParameterName = "oStatus";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Value = 0;
                    cmd.Parameters.Add(parm);
                    
                    Object obj = helper.ExecuteScalarParm(cmd);
                    int orderId = Convert.ToInt32(obj);
                    if (dto.productList != null)
                    {
                        for (int i = 0; i < dto.productList.Count; i++)
                        {
                            int prodId = ProductDAO.Save(dto.productList[i]);
                            sqlQuery = String.Format(@"INSERT INTO dbo.ProductOrderMapping(OrderId,ProductId) 
                            VALUES ({0},{1});", orderId, prodId);
                            int a = helper.ExecuteQuery(sqlQuery);
                        }
                    }
                    return ResponseResult.GetSuccessObject();
                }
            }
        }


        public static ResponseResult GetAllOrders()
        {
            try
            {
                var query = "Select ord.OrderId,ord.OrderNum, ord.OrderBy,u.Name, ord.CreatedOn,ord.IsPaid, ord.TotalAmount,ord.IsActive,ord.OrderStatus from dbo.Orders ord,dbo.Users u Where ord.OrderBy=u.UserId AND ord.IsActive = 1";

                using (DBHelper helper = new DBHelper())
                {
                    var reader = helper.ExecuteReader(query);
                    List<OrderDTO> list = new List<OrderDTO>();

                    while (reader.Read())
                    {
                        var dto = FillDTO(reader);
                        if (dto != null)
                        {
                            list.Add(dto);
                        }
                    }

                    return ResponseResult.GetSuccessObject(list);
                }
            }
            catch (Exception exp)
            {
                return ResponseResult.GetErrorObject("Some Error has occured! " + exp);
            }
        }

        public static ResponseResult GetOrderById(int OrderId)
        {
            var sqlQuery = "";
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    sqlQuery = String.Format(@"Select ord.OrderId,ord.OrderNum, ord.OrderBy,u.Name, ord.CreatedOn,ord.IsPaid, ord.TotalAmount,ord.IsActive,ord.OrderStatus from dbo.Orders ord,dbo.Users u Where ord.OrderBy=u.UserId AND ord.IsActive = 1 AND OrderId='{0}'", OrderId);

                    SqlCommand cmd = new SqlCommand(sqlQuery);
                    
                    var reader = helper.ExecuteReader(sqlQuery);

                    OrderDTO dto = null;

                    if (reader.Read())
                    {
                        dto = FillDTO(reader);
                    }

                    return ResponseResult.GetSuccessObject(dto);
                }
            }
            catch (Exception exp)
            {
                return ResponseResult.GetErrorObject("Some error has occured!" + exp);
            }
        }

        public static ResponseResult DeleteOrder(int id)
        {
            String sqlQuery = String.Format(@"Update dbo.Orders Set IsActive=0 Where OrderId={0}", id);
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    SqlCommand cmd = new SqlCommand(sqlQuery);
                    var rv = helper.ExecuteQuery(sqlQuery);
                    return ResponseResult.GetSuccessObject(rv);
                }
            }
            catch(Exception exp)
            {
                return ResponseResult.GetErrorObject("Some error has occured!" + exp);
            }
        }

        public static ResponseResult UpdateOrderStatus(String status,int id)
        {
            String sqlQuery = String.Format(@"Update dbo.Orders Set OrderStatus={0} Where OrderId={1}",status, id);
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    SqlCommand cmd = new SqlCommand(sqlQuery);
                    var rv = helper.ExecuteQuery(sqlQuery);
                    return ResponseResult.GetSuccessObject(rv);
                }
            }
            catch (Exception exp)
            {
                return ResponseResult.GetErrorObject("Some error has occured!" + exp);
            }
        }
        private static OrderDTO FillDTO(SqlDataReader reader)
        {
            var dto = new OrderDTO();
            dto.OrderId = reader.GetInt32(0);
            dto.OrderNum = reader.GetString(1);
            dto.OrderBy = reader.GetInt32(2);
            dto.OrderByName = reader.GetString(3);
            dto.CreatedOn = reader.GetDateTime(4);
            dto.IsPaid = reader.GetBoolean(5);
            dto.TotalAmount = reader.GetFloat(6);
            dto.IsActive = reader.GetBoolean(7);
            dto.OrderStatus = reader.GetString(8);
            return dto;
        }
    }
}