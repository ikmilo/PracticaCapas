using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.SharedInterfaces
{
    public interface ICrud<T>
    {
        int Add(T entidad);
        List<T> Getall();
        int Update(T entidad);
        int Delete(int id);
    }
}
