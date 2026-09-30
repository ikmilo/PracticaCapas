using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public List<Producto> Getall()
        {
            return _productoDAO.Getall();
        }

        public int Update(Producto entidad)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
