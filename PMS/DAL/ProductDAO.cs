using PMS.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PMS.DAL
{
    public class ProductDAO
    {
        
        public static int Save(ProductDTO dto)
        {
            using (DBHelper helper = new DBHelper())
            {
                String sqlQuery = "";
                if (dto.ProductId > 0)
                {
                    sqlQuery = String.Format(@"Update dbo.Products Set Name=@name,Price=@price,PictureName=@picname,ModifiedOn=@modifiedon,ModifiedBy=@modifiedby Where ProductID=@pid");
                    SqlCommand cmd = new SqlCommand(sqlQuery);
           
                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = "name";
                    parm.SqlDbType = System.Data.SqlDbType.VarChar;
                    parm.Value = dto.Name;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "price";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Value = dto.Price;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "picname";
                    parm.SqlDbType = System.Data.SqlDbType.VarChar;
                    parm.Value = dto.PictureName;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "modifiedon";
                    parm.SqlDbType = System.Data.SqlDbType.DateTime;
                    parm.Value = dto.ModifiedOn;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "modifiedby";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Value = dto.ModifiedBy;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "pid";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Value = dto.ProductId;
                    cmd.Parameters.Add(parm);

                    int a = helper.ExecuteQueryParm(cmd);
                    return dto.ProductId;
                }
                else
                {
                    sqlQuery = String.Format(@"INSERT INTO dbo.Products(Name, Price, PictureName, CreatedOn, CreatedBy,IsActive) VALUES
                            (@name,@price,@picname,@createdon,@createdby,@isactive); Select @@IDENTITY");
                 
                    SqlCommand cmd = new SqlCommand(sqlQuery);

                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = "name";
                    parm.SqlDbType = System.Data.SqlDbType.VarChar;
                    parm.Value = dto.Name;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "price";
                    parm.SqlDbType = System.Data.SqlDbType.Float;
                    parm.Value = dto.Price;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "picname";
                    parm.SqlDbType = System.Data.SqlDbType.VarChar;
                    parm.Value = dto.PictureName;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "createdon";
                    parm.SqlDbType = System.Data.SqlDbType.DateTime;
                    parm.Value = dto.CreatedOn;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "createdby";
                    parm.SqlDbType = System.Data.SqlDbType.Int;
                    parm.Value = dto.CreatedBy;
                    cmd.Parameters.Add(parm);

                    parm = new SqlParameter();
                    parm.ParameterName = "isactive";
                    parm.SqlDbType = System.Data.SqlDbType.Bit;
                    parm.Value = 1;
                    cmd.Parameters.Add(parm);

                    //int a = Convert.ToInt32 ( cmd.ExecuteScalar () );
                    Object obj = helper.ExecuteScalarParm(cmd);
                    int a = Convert.ToInt32(obj);
                    return a;
                }
            }
        }


        public static ResponseResult GetAllProducts()
        {
            var query = "Select ProductId,Name, Price, PictureName, CreatedOn,CreatedBy, ModifiedOn, ModifiedBy, IsActive from dbo.Products Where IsActive = 1";
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    var reader = helper.ExecuteReader(query);
                    List<ProductDTO> list = new List<ProductDTO>();

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
            catch(Exception exp)
            {
                return ResponseResult.GetErrorObject("Some Error has occured! " +exp);
            }
        }

        public static ResponseResult GetProductById(int pid)
        {
            var sqlQuery = "" ;
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    sqlQuery = String.Format(@"Select * from dbo.Products Where ProductId='{0}'", pid);

                    SqlCommand cmd = new SqlCommand(sqlQuery);

                    var reader = helper.ExecuteReader(sqlQuery);

                    ProductDTO dto = null;

                    if (reader.Read())
                    {
                        dto = FillDTO(reader);
                    }

                    return ResponseResult.GetSuccessObject(dto);
                }
            }
            catch(Exception exp)
            {
                return ResponseResult.GetErrorObject();
            }
        }


        public static ResponseResult GetProductsByOrderId(int OrderId)
        {
            var sqlQuery = "";
            try
            {
                using (DBHelper helper = new DBHelper())
                {
                    sqlQuery = String.Format(@"select p.ProductId,p.Name, p.Price, p.PictureName, p.CreatedOn,p.CreatedBy, p.ModifiedOn, p.ModifiedBy, p.IsActive from dbo.Products p,dbo.Orders ord, dbo.ProductOrderMapping map
WHERE map.ProductId=p.ProductId AND ord.OrderId=map.OrderId AND ord.OrderId={0}", OrderId);

                    SqlCommand cmd = new SqlCommand(sqlQuery);

                    var reader = helper.ExecuteReader(sqlQuery);
                  
                    List<ProductDTO> list = new List<ProductDTO>();

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
                return ResponseResult.GetErrorObject();
            }
        }

        public static ResponseResult DeleteProduct(int id)
        {
            try
            {
                String sqlQuery = String.Format(@"Update dbo.Products Set IsActive=0 Where ProductId={0}", id);

                using (DBHelper helper = new DBHelper())
                {
                    SqlCommand cmd = new SqlCommand(sqlQuery);
                    return ResponseResult.GetSuccessObject(helper.ExecuteQuery(sqlQuery));
                }
            }
            catch(Exception exp)
            {
                return ResponseResult.GetErrorObject("Some Error has occured "+exp);
            }
        }

        private static ProductDTO FillDTO(SqlDataReader reader)
        {
            var dto = new ProductDTO();
            dto.ProductId = reader.GetInt32(0);
            dto.Name = reader.GetString(1);
            dto.Price = reader.GetDouble(2);
            dto.PictureName = reader.GetString(3);
            dto.CreatedOn = reader.GetDateTime(4);
            dto.CreatedBy = reader.GetInt32(5);
            if (reader.GetValue(6) != DBNull.Value)
                dto.ModifiedOn = reader.GetDateTime(6);
            if (reader.GetValue(7) != DBNull.Value)
                dto.ModifiedBy = reader.GetInt32(7);
            dto.IsActive = reader.GetBoolean(8);
            return dto;
        }
        
    }
}