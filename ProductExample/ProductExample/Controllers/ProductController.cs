using Microsoft.AspNetCore.Mvc;
using ProductExample.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {


        [HttpPost("/addProduct")]
        public async Task<IActionResult> addProduct(ProductModel product)
        {

            if(product.Price < 0)
            {
                return BadRequest("price must higher than 0");
            }
            else if (product.Price < 999)
            {
                return BadRequest("price must higher than 999");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO Products (Code, Name, Photo, Price, LastUpdated) VALUES (@Code, @Name, @Photo, @Price, @LastUpdated)";


                    await using (SqlConnection con = new SqlConnection("Server=DESKTOP-GQ5T1F6;Database=Products;Trusted_Connection=True;"))
                    {
                        using (SqlCommand command = new SqlCommand(query))
                        {
                            con.Open();

                            command.Connection = con;
                            command.CommandText = query;


                            command.Parameters.AddWithValue("@Code", product.Code);
                            command.Parameters.AddWithValue("@Name", product.Name);
                            command.Parameters.AddWithValue("@Photo", product.Photo);
                            command.Parameters.AddWithValue("@Price", product.Price);
                            command.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

                            command.ExecuteNonQuery();


                            con.Close();

                        }

                    }
                    return Ok("1");
                }
                catch (SqlException ex)
                {
                    return NotFound(ex);
                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
            }           
            
        }


        [HttpPut("/editProduct")]
        public async Task<IActionResult> editProduct(ProductModel product)
        {

            if (product.Price < 0)
            {
                return BadRequest("price must higher than 0");
            }
            else if (product.Price < 999)
            {
                return BadRequest("price must higher than 999");
            }
            else
            {
                try
                {
                    string query = "UPDATE Products SET Name = @Name, Photo = @Photo, Price = @Price, LastUpdated = @LastUpdated WHERE Code = @Code";


                    await using (SqlConnection con = new SqlConnection("Server=DESKTOP-GQ5T1F6;Database=Products;Trusted_Connection=True;"))
                    {
                        using (SqlCommand command = new SqlCommand(query))
                        {
                            con.Open();

                            command.Connection = con;
                            command.CommandText = query;


                            command.Parameters.AddWithValue("@Code", product.Code);
                            command.Parameters.AddWithValue("@Name", product.Name);
                            command.Parameters.AddWithValue("@Photo", product.Photo);
                            command.Parameters.AddWithValue("@Price", product.Price);
                            command.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

                            command.ExecuteNonQuery();

                            con.Close();


                        }

                    }
                    return Ok("1");
                }
                catch (SqlException ex)
                {
                    return NotFound(ex);
                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
            }

        }


        [HttpDelete("/removeProduct")]
        public async Task<IActionResult> removeProduct(int Code)
        {           
            
                try
                {
                    string query = "DELETE FROM Products WHERE Code = @Code";


                    await using (SqlConnection con = new SqlConnection("Server=DESKTOP-GQ5T1F6;Database=Products;Trusted_Connection=True;"))
                    {
                        using (SqlCommand command = new SqlCommand(query))
                        {
                            con.Open();

                            command.Connection = con;
                            command.CommandText = query;


                            command.Parameters.AddWithValue("@Code", Code);

                            command.ExecuteNonQuery();


                            con.Close();

                    }

                }
                    return Ok("1");
                }
                catch (SqlException ex)
                {
                    return NotFound(ex);
                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
        }



        [HttpGet("/viewProduct")]
        public async Task<IActionResult> viewProduct(int Code)
        {

            try
            {
                string query = "SELECT * FROM Products WHERE Code = @Code";


                await using (SqlConnection con = new SqlConnection("Server=DESKTOP-GQ5T1F6;Database=Products;Trusted_Connection=True;"))
                {
                    using (SqlCommand command = new SqlCommand(query))
                    {
                        con.Open();

                        command.Connection = con;
                        command.CommandText = query;


                        command.Parameters.AddWithValue("@Code", Code);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                StringBuilder sb = new StringBuilder(reader["Code"] + " " + reader["Price"]);

                                return Ok(sb.ToString());
                            }
                        }

                        con.Close();

                    }

                }
                return Ok("1");
            }
            catch (SqlException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }


        [HttpPost("/selectProduct")]
        public async Task<IActionResult> selectProduct(ProductModel product)
        {

            try
            {
                string query = "SELECT * FROM Products WHERE Name  = @Name OR Code = @Code";


                await using (SqlConnection con = new SqlConnection("Server=DESKTOP-GQ5T1F6;Database=Products;Trusted_Connection=True;"))
                {
                    using (SqlCommand command = new SqlCommand(query))
                    {
                        con.Open();

                        command.Connection = con;
                        command.CommandText = query;

                        command.Parameters.AddWithValue("@Code", product.Code);                      
                        command.Parameters.AddWithValue("@Name", product.Name);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                StringBuilder sb = new StringBuilder(reader["Code"] + " "  + reader["Name"] +  " " + reader["Price"]);

                                return Ok(sb.ToString());
                            }
                        }

                        con.Close();

                    }

                }
                return Ok("1");
            }
            catch (SqlException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




    }
}
