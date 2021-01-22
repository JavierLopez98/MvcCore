using MvcCore.Data;
using MvcCore.Interfaces;
using MvcCore.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repository
{
    public class RepositoryDepartamentosMysql : IRepositoryDepartamentos
    {
        private MySqlDataAdapter addept;
        private DataTable tabledept;
        public RepositoryDepartamentosMysql(String cadena )
        {
            this.addept = new MySqlDataAdapter("select * from dept", cadena);
            this.tabledept = new DataTable();
            this.addept.Fill(this.tabledept);
        }
        public Departamento BuscarDepartamento(int iddept)
        {
            var consulta = from datos in this.tabledept.AsEnumerable()
                           where datos.Field<int>("Dept_no") == iddept
                           select new Departamento
                           {
                               Numero = datos.Field<int>("Dept_no"),
                               Nombre = datos.Field<String>("dNombre"),
                               Localidad = datos.Field<String>("loc")
                           };
            return consulta.FirstOrDefault();
        }

        public void EliminarDepartamento(int iddept)
        {
            throw new NotImplementedException();
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.tabledept.AsEnumerable()
                           select new Departamento
            {
                               Numero=datos.Field<int>("Dept_no"),
                               Nombre=datos.Field<String>("dNombre"),
                               Localidad=datos.Field<String>("loc")
            };
            return consulta.ToList();
        }

        public void InsertarDepartamento(int iddept, string nombre, string loc)
        {
            
        }

        public void ModificarDepartamento(int iddept, string nombre, string loc)
        {
            throw new NotImplementedException();
        }
    }
}
