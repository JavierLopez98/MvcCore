using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    [Table("DEPT")]
    public class Departamento
    {
        [Key]
        [Column("Dept_no")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Numero { get; set; }
        [Column("Dnombre")]
        public String Nombre { get; set;}
        [Column("Loc")]
        public String Localidad { get; set; }



    }
}
