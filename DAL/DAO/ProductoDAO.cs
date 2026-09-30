using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BE;
using BE.SharedInterfaces;
using DAL.Mapper;

namespace DAL.DAO
{
    public class ProductoDAO : ICrud<Producto>
    {
        private ProductoMapper _mapper = new ProductoMapper();

        public int Add(Producto entidad)
        {
            int filasAfectadas = 0;

            using (SqlConnection conexion = new SqlConnection(ConexionDAO.ConnectionString))
            {
                string query = "INSERT INTO dbo.Productos (Nombre, Precio, Stock) VALUES (@Nombre, @Precio, @Stock);";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                    comando.Parameters.AddWithValue("@Precio", entidad.Precio);
                    comando.Parameters.AddWithValue("@Stock", entidad.Stock);

                    conexion.Open();
                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }

            return filasAfectadas;
        }

        public List<Producto> Getall()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = new SqlConnection())
            {
                conexion.ConnectionString = ConexionDAO.ConnectionString;

                using (SqlCommand comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "SELECT Id, Nombre, Precio, Stock FROM dbo.Productos ORDER BY Id;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = comando;
                        adapter.Fill(tabla);
                    }
                }
            }

            return _mapper.Map(tabla);
        }

        public int Update(Producto entidad)
        {
            int filasAfectadas = 0;

            using (SqlConnection conexion = new SqlConnection(ConexionDAO.ConnectionString))
            {
                string query = "UPDATE dbo.Productos SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock WHERE Id = @Id;";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", entidad.Id);
                    comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                    comando.Parameters.AddWithValue("@Precio", entidad.Precio);
                    comando.Parameters.AddWithValue("@Stock", entidad.Stock);

                    conexion.Open();
                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }

            return filasAfectadas;
        }

        public int Delete(int id)
        {
            int filasAfectadas = 0;

            using (SqlConnection conexion = new SqlConnection(ConexionDAO.ConnectionString))
            {
                string query = "DELETE FROM dbo.Productos WHERE Id = @Id;";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);

                    conexion.Open();
                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }

            return filasAfectadas;
        }
    }
}
