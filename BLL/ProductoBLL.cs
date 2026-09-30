using System;
using System.Collections.Generic;
using BE;
using BE.SharedInterfaces;
using DAL.DAO;

namespace BLL
{
    public class ProductoBLL : ICrud<Producto>
    {
        private ICrud<Producto> _productoDAO = new ProductoDAO();

        public int Add(Producto entidad)
        {
            Validar(entidad);
            return _productoDAO.Add(entidad);
        }

        public List<Producto> Getall()
        {
            return _productoDAO.Getall();
        }

        public int Update(Producto entidad)
        {
            Validar(entidad);

            if (entidad.Id <= 0)
            {
                throw new ArgumentException("El Id debe ser un número positivo mayor a cero.");
            }

            return _productoDAO.Update(entidad);
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El Id debe ser un número positivo mayor a cero.");
            }

            return _productoDAO.Delete(id);
        }

        private void Validar(Producto entidad)
        {
            if (entidad == null)
            {
                throw new ArgumentNullException(nameof(entidad), "El producto no puede ser nulo.");
            }

            if (entidad.Nombre == null)
            {
                throw new ArgumentException("El nombre del producto no puede ser nulo.");
            }

            entidad.Nombre = entidad.Nombre.Trim();

            if (entidad.Nombre.Length < 1 || entidad.Nombre.Length > 100)
            {
                throw new ArgumentException("El nombre debe tener entre 1 y 100 caracteres.");
            }

            if (entidad.Precio <= 0 || entidad.Precio > 99999999.99m)
            {
                throw new ArgumentException("El precio debe ser mayor a 0 y hasta 99999999.99.");
            }

            if (decimal.Round(entidad.Precio, 2) != entidad.Precio)
            {
                throw new ArgumentException("El precio no puede tener más de dos decimales.");
            }

            if (entidad.Stock < 0)
            {
                throw new ArgumentException("El stock no puede ser negativo.");
            }
        }
    }
}
