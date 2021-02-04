using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    [Serializable]
    public class CarritoEmpleados
    {

        public int idempleado { get; set; }
        public int cantidad { get; set; }
    }
}
