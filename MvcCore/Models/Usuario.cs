using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    [Table("userhash")]
    public class Usuario
    {

        [Key]
        [Column("idusuario")]
        public int Id { get; set; }
        public String Nombre { get; set; }
        [Column("Usuario")]
        public String User { get; set; }
        [Column("pass")]
        public byte[] Password{get;set;}
        public String Salt { get; set; }

    }
}
