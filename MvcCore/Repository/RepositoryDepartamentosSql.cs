using Microsoft.Extensions.Caching.Memory;
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

        HospitalContext context;
        private IMemoryCache cache;

        public RepositoryDepartamentosSql(HospitalContext context,IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
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
            List<Departamento> departamentos;
            if (this.cache.Get("departamentos") == null)
            {
                var consulta = from datos in this.context.Departamentos select datos;
                departamentos = consulta.ToList();
                this.cache.Set("departamentos", departamentos);
            }
            else
            {
                departamentos = this.cache.Get("departamentos") as List<Departamento>;
            }
            return departamentos;
            
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
