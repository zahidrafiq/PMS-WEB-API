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


    public static List<ProductDTO> GetAllProducts()
        {
            var query = "Select ProductId,Name, Price, PictureName, CreatedOn,CreatedBy, ModifiedOn, ModifiedBy, IsActive from dbo.Products Where IsActive = 1";

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

                return list;
            }
        }

    public static ProductDTO GetProductById(int pid)
        {
            var sqlQuery = String.Format("Select * from dbo.Products Where ProductId=@pid");

            using (DBHelper helper = new DBHelper())
            {
                SqlCommand cmd = new SqlCommand(sqlQuery);

                SqlParameter parm = new SqlParameter();
                parm.ParameterName = "pid";
                parm.SqlDbType = System.Data.SqlDbType.Int;
                parm.Value = pid;
                cmd.Parameters.Add(parm);

                var reader = helper.ExecuteReader(sqlQuery);

                ProductDTO dto = null;

                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }

                return dto;
            }
        }

        public static int DeleteProduct(int id)
        {
            String sqlQuery = String.Format(@"Update dbo.Products Set IsActive=0 Where ProductId=@pid");

            using (DBHelper helper = new DBHelper())
            {
                SqlCommand cmd = new SqlCommand(sqlQuery);

                SqlParameter parm = new SqlParameter();
                parm.ParameterName = "pid";
                parm.SqlDbType = System.Data.SqlDbType.Int;
                parm.Value = id;
                cmd.Parameters.Add(parm);
                
                return helper.ExecuteQuery(sqlQuery);
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