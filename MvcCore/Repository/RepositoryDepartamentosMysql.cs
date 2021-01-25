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
        HospitalContext context;
        public RepositoryDepartamentosMysql(HospitalContext context)
        {
            this.context = context;
        }

        public Departamento BuscarDepartamento(int iddept)
        {
            throw new NotImplementedException();
        }

        //public Departamento BuscarDepartamento(int iddept)
        //{
        //    return this.context.Departamentos
        //    //var consulta = from datos in this.tabledept.AsEnumerable()
        //    //               where datos.Field<int>("Dept_no") == iddept
        //    //               select new Departamento
        //    //               {
        //    //                   Numero = datos.Field<int>("Dept_no"),
        //    //                   Nombre = datos.Field<String>("dNombre"),
        //    //                   Localidad = datos.Field<String>("loc")
        //    //               };
        //    //return consulta.FirstOrDefault();
        //}

        public void EliminarDepartamento(int iddept)
        {
            Departamento dept = this.BuscarDepartamento(iddept);
            this.context.Departamentos.Remove(dept);
            this.context.SaveChanges();
            
        }

        public List<Departamento> GetDepartamentos()
        {
            //var consulta = from datos in this.tabledept.AsEnumerable()
            //               select new Departamento
            //{
            //                   Numero=datos.Field<int>("Dept_no"),
            //                   Nombre=datos.Field<String>("dNombre"),
            //                   Localidad=datos.Field<String>("loc")
            //};
            //return consulta.ToList();

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

            //DataRow row = this.tabledept.NewRow();
            //row["Dept_no"] = iddept;
            //row["dNombre"] = nombre;
            //row["Loc"] = loc;
            //this.tabledept.Rows.Add(row);
            //this.addept.Update(this.tabledept);
            //this.tabledept.AcceptChanges();
        }

        

        public void ModificarDepartamento(int iddept, string nombre, string loc)
        {
            Departamento dept = this.BuscarDepartamento(iddept);
            dept.Nombre = nombre;
            dept.Localidad = loc;
            this.context.SaveChanges();
        }
    }
}
