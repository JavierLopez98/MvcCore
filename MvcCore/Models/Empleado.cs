using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    
    [Table("Emp")]
    public class Empleado
    {
        [Key]
        [Column("Emp_no")]
        public int IdEmpleado { get; set; }
        public String Apellido { get; set;}
        public String Oficio { get; set; }
        public int Salario { get; set; }
        [Column("Dept_no")]
        public int Departamento { get; set; }
    }
}
