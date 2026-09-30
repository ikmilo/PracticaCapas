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
            using (SqlConnection conexion = new SqlConnection(ConexionDAO.ConnectionString))
            {
                string query = "INSERT INTO dbo.Productos (Nombre, Precio, Stock) VALUES (@Nombre, @Precio, @Stock);";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                    comando.Parameters.AddWithValue("@Precio", entidad.Precio);
                    comando.Parameters.AddWithValue("@Stock", entidad.Stock);

                }
            }

            return 0;
        }

        public List<Producto> Getall()
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDAO.ConnectionString))
            {
                string query = "SELECT Id, Nombre, Precio, Stock FROM dbo.Productos ORDER BY Id;";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                    }
                }
            }

            return new List<Producto>();
        }

        public int Update(Producto entidad)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDAO.ConnectionString))
            {
                string query = "UPDATE dbo.Productos SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock WHERE Id = @Id;";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", entidad.Id);
                    comando.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                    comando.Parameters.AddWithValue("@Precio", entidad.Precio);
                    comando.Parameters.AddWithValue("@Stock", entidad.Stock);

                }
            }

            return 0;
        }

        public int Delete(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDAO.ConnectionString))
            {
                string query = "DELETE FROM dbo.Productos WHERE Id = @Id;";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@Id", id);

                }
            }

            return 0;
        }
    }
}
