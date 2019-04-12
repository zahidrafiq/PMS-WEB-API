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
        public static ResponseResult SaveOrder(OrderDTO dto)
        {
            using (DBHelper helper = new DBHelper())
            {
                String sqlQuery = "";
                if (dto.OrderId > 0)
                {
                    //sqlQuery = String.Format(@"Update dbo.Products Set Name=@name,Price=@price,PictureName=@picname,ModifiedOn=@modifiedon,ModifiedBy=@modifiedby Where ProductID=@pid");
                    //SqlCommand cmd = new SqlCommand(sqlQuery);

                    //SqlParameter parm = new SqlParameter();
                    //parm.ParameterName = "name";
                    //parm.SqlDbType = System.Data.SqlDbType.VarChar;
                    //parm.Value = dto.Name;
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
                    int rv = Convert.ToInt32(obj);
                    return ResponseResult.GetSuccessObject(rv);
                }
            }
        }


        public static ResponseResult GetAllOrders()
        {
            try
            {
                var query = "Select OrderId,OrderNum, OrderBy, CreatedOn,IsPaid, TotalAmount,IsActive,OrderStatus from dbo.Orders Where IsActive = 1";

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

        public static ResponseResult GetOrderById(int pid)
        {
            var sqlQuery = "";
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    sqlQuery = String.Format(@"Select * from dbo.Orders Where OrderId='{0}'", pid);

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

        private static OrderDTO FillDTO(SqlDataReader reader)
        {
            var dto = new OrderDTO();
            dto.OrderId = reader.GetInt32(0);
            dto.OrderNum = reader.GetString(1);
            dto.OrderBy = reader.GetInt32(2);
            dto.CreatedOn = reader.GetDateTime(3);
            dto.IsPaid = reader.GetBoolean(4);
            dto.TotalAmount = reader.GetFloat(5);
            dto.IsActive = reader.GetBoolean(6);
            dto.OrderStatus = reader.GetInt32(7);
            return dto;
        }

    }
}