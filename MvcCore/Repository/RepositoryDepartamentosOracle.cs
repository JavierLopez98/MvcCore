using MvcCore.Interfaces;
using MvcCore.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repository
{
    public class RepositoryDepartamentosOracle : IRepositoryDepartamentos
    {
        DataTable tabladept;
        OracleDataAdapter addept;
        OracleCommandBuilder builder;
        public RepositoryDepartamentosOracle(String cadenaoracle)
        {
            this.addept = new OracleDataAdapter("select * from dept", cadenaoracle);
            this.builder = new OracleCommandBuilder(this.addept);
            this.tabladept = new DataTable();
            this.addept.Fill(this.tabladept);
        }

        public Departamento BuscarDepartamento(int iddept)
        {
            var consulta = from datos in this.tabladept.AsEnumerable()
                           where datos.Field<int>("Dept_no") == iddept
                           select new Departamento
                           {
                               Numero = datos.Field<int>("DEPT_NO"),
                               Nombre = datos.Field<String>("DNOMBRE"),
                               Localidad = datos.Field<String>("LOC")
                           };
            return consulta.FirstOrDefault();
        }

        private DataRow GetDataRowId(int iddept)
        {
            DataRow fila = this.tabladept.AsEnumerable().Where(z => z.Field<int>("dept_no") == iddept).FirstOrDefault();
            return fila;
        }

        public void EliminarDepartamento(int iddept)
        {
            //para eliminar, debemos hacerlo sobre el objeto datatable, debemos buscar
            //la fila que corresponta con el id
            //la fila tiene un metodo delete que marcará en la table el valor para eliminar

            DataRow row = this.GetDataRowId(iddept);
            row.Delete();
            this.addept.Update(this.tabladept);
            this.tabladept.AcceptChanges();

        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.tabladept.AsEnumerable()
                           select new Departamento
                           {
                               Numero = datos.Field<int>("DEPT_NO"),
                               Nombre=datos.Field<String>("DNOMBRE"),
                               Localidad=datos.Field<String>("LOC")
                           };
            return consulta.ToList();
        }

        public void InsertarDepartamento(int iddept, string nombre, string loc)
        {
            DataRow row = this.tabladept.NewRow();
            row["Dept_no"] = iddept;
            row["dNombre"] = nombre;
            row["Loc"] = loc;
            this.tabladept.Rows.Add(row);
            this.addept.Update(this.tabladept);
            this.tabladept.AcceptChanges();
        }

        public void ModificarDepartamento(int iddept, string nombre, string loc)
        {
            DataRow row = this.GetDataRowId(iddept);
            row["dNombre"] = nombre;
            row["loc"] = loc;
            this.addept.Update(this.tabladept);
            this.tabladept.AcceptChanges();
        }
    }
}
