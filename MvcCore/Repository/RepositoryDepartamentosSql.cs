using MvcCore.Data;
using MvcCore.Interfaces;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repository
{
    public class RepositoryDepartamentosSql : IRepositoryDepartamentos
    {

        DepartamentosContext context;

        public RepositoryDepartamentosSql(DepartamentosContext context)
        {
            this.context = context;
        }
        public Departamento BuscarDepartamento(int iddept)
        {
            
            return this.context.Departamentos.Where(z => z.Numero == iddept).FirstOrDefault();
        }

        public void EliminarDepartamento(int iddept)
        {
            Departamento dept = this.BuscarDepartamento(iddept);
            this.context.Departamentos.Remove(dept);
            this.context.SaveChanges();
        }

        public List<Departamento> GetDepartamentos()
        {
            //var consulta = from datos in this.context.Departamentos select datos;
            return this.context.Departamentos.ToList() ;
        }

        public void InsertarDepartamento(int iddept, string nombre, string loc)
        {
            Departamento dept = new Departamento();
            dept.Numero = iddept;
            dept.Nombre = nombre;
            dept.Localidad = loc;
            this.context.Departamentos.Add(dept);
            this.context.SaveChanges();
        }

        public void ModificarDepartamento(int iddept, string nombre, string loc)
        {
            Departamento dept = this.BuscarDepartamento(iddept);
            dept.Localidad = loc;
            dept.Nombre = nombre;
            this.context.SaveChanges();
        }
    }
}
