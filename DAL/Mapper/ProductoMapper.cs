using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using BE;
using BE.SharedInterfaces;

namespace DAL.Mapper
{
    internal class ProductoMapper
    {

        public List<Producto> Map(DataTable table) 
        {

            List<Producto> lista = new List<Producto>();

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Producto producto = Map(row);
                    lista.Add(producto);
                }
            }
            return lista;
        }

        private Producto Map(DataRow row)
        {
            Producto producto = new Producto();

            producto.Id = Convert.ToInt32(row["Id"]);
            producto.Nombre = Convert.ToString(row["Nombre"]);
            producto.Precio = Convert.ToDecimal(row["Precio"]);
            producto.Stock = Convert.ToInt32(row["Stock"]);

            return producto;

        }
    }
}
