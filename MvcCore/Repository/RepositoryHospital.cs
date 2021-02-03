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
    public class RepositoryHospital : IRepositoryHospital
    {
        HospitalContext context;
        private IMemoryCache cache;
        public RepositoryHospital(HospitalContext context,IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }

        #region Empleados
        public List<Empleado> GetEmpleados()
        {
            return this.context.Empleados.ToList();
        }
        public List<Empleado> BuscaEmpleadosDept(List<int> iddept)
        {
            var consulta = from datos in this.context.Empleados where iddept.Contains(datos.Departamento) select datos;
            return consulta.ToList();
        }


        #endregion

        #region Departamentos
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
            return this.context.Departamentos.ToList();
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

        public void InsertarDepartamento(int iddept, string nombre, string loc, string img)
        {
            Departamento dept = new Departamento();
            dept.Nombre = nombre;
            dept.Numero = iddept;
            dept.Localidad = loc;
            dept.Imagen = img;
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

        public void ModificarDepartamento(int iddept, string nombre, string loc, string img)
        {
            Departamento dept = this.BuscarDepartamento(iddept);
            dept.Localidad = loc;
            dept.Nombre = nombre;
            dept.Imagen = img;
            this.context.SaveChanges();
        }




        #endregion
    }
}
