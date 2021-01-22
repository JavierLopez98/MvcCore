using MvcCore.Helpers;
using MvcCore.Interfaces;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Repository
{
    public class RepositoryDepartamentosXml:IRepositoryDepartamentos
    {

        PathProvider provider;
        private XDocument docxml;
        private String path;

        public RepositoryDepartamentosXml(PathProvider provider)
        {
            this.provider = provider;
            this.path = this.provider.MapPath("departamentos.xml", Folders.Documents);
            this.docxml = XDocument.Load(this.path);
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO") select new Departamento {
                Numero = Convert.ToInt32(datos.Attribute("NUMERO").Value),
                Nombre = datos.Element("NOMBRE").Value,
                Localidad=datos.Element("LOCALIDAD").Value
            };
            return consulta.ToList();
        }

        public Departamento BuscarDepartamento(int iddept)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == iddept.ToString()
                           select new Departamento
                           {
                               Numero = Convert.ToInt32(datos.Attribute("NUMERO").Value),
                               Nombre = datos.Element("NOMBRE").Value,
                               Localidad = datos.Element("LOCALIDAD").Value
                           };
            return consulta.FirstOrDefault();
        }

        public void ModificarDepartamento(int iddept,String nombre,String loc)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO") where datos.Attribute("NUMERO").Value == iddept.ToString() select datos;
            XElement element = consulta.FirstOrDefault();
            element.Element("NOMBRE").Value = nombre;
            element.Element("LOCALIDAD").Value = loc;
            this.docxml.Save(this.path);
        }
        public void EliminarDepartamento(int iddept)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                         where datos.Attribute("NUMERO").Value == iddept.ToString()
                         select datos;
            XElement elementDept = consulta.FirstOrDefault();
            elementDept.Remove();
            this.docxml.Save(this.path);
        }

        public void InsertarDepartamento(int iddept, String nombre, String loc)
        {
            XElement elementDept = new XElement("DEPARTAMENTO");
            XAttribute elementnumero = new XAttribute("NUMERO", iddept);
            XElement elementnombre = new XElement("NOMBRE", nombre);
            XElement elementloc = new XElement("LOCALIDAD", loc);
            elementDept.Add(elementnumero);
            elementDept.Add(elementnombre);
            elementDept.Add(elementloc);
            this.docxml.Element("DEPARTAMENTOS").Add(elementDept);
            this.docxml.Save(this.path);
        }
    }
}
