using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication6.Models
{
    public class ImageContext
    {
        private const string _connectionString = @"Data Source=DESKTOP-U7FBHUA\SQLEXPRESS;Initial Catalog=UploadImageDB;Integrated Security=True;Pooling=False;MultipleActiveResultSets=True;Application Name=EntityFramework";

        public int UploadImage(byte[] image)
        {
            try
            {
                var result = 0;
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand()
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "UploadImage",
                        Connection = connection
                    };
                    command.Parameters.AddWithValue("@imageContent", image);
                    connection.Open();
                    result = (int)command.ExecuteScalar();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TheImage> GetImages()
        {
            try
            {
                var result = new List<TheImage>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand()
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "GetImages",
                        Connection = connection
                    };
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            result.Add(new TheImage
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ImageContent = (byte[])reader["TheImage"]
                            });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteImage(int id)
        {
            try
            {
                var result = 0;
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand()
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "DeleteImage",
                        Connection = connection
                    };
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    result = command.ExecuteNonQuery();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GetImageById(int id)
        {
            try
            {
                byte[] result = null;
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand()
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "GetImage",
                        Connection = connection
                    };
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    result = (byte[])command.ExecuteScalar();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}