using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Repository
{
    public class RepositoryAlumno
    {

        PathProvider provider;
        private XDocument docxml;
        private string path; 
        public RepositoryAlumno(PathProvider provider)
        {
            this.provider = provider;
            this.path = this.provider.MapPath("alumnos.xml", Folders.Documents);
            this.docxml = XDocument.Load(path);
        }
        public List<Alumno> GetAlumnos()
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           select new Alumno
                           {
                               IdAlumno = Convert.ToInt32(datos.Element("idalumno").Value),
                               Nombre = datos.Element("nombre").Value,
                               Apellido = datos.Element("apellidos").Value,
                               Nota = Convert.ToInt32(datos.Element("nota").Value)
                           };
            return consulta.ToList();
        }

        public Alumno BuscarAlumno(int idAlumno)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idAlumno.ToString()
                           select new Alumno
                           {
                               IdAlumno = Convert.ToInt32(datos.Element("idalumno").Value),
                               Nombre = datos.Element("nombre").Value,
                               Apellido = datos.Element("apellidos").Value,
                               Nota = Convert.ToInt32(datos.Element("nota").Value)
                           };
            return consulta.FirstOrDefault();
        }
        public void EliminarAlumno(int idAlumno)
        {
            var consulta = from datos in this.docxml.Descendants("alumno")
                           where datos.Element("idalumno").Value == idAlumno.ToString()
                           select datos;
            XElement elementalumno = consulta.FirstOrDefault();
            elementalumno.Remove();
            this.docxml.Save(this.path);
        }
        public void InsertarAlumno(int idalumno,String nombre,String apellido, int nota)
        {
            XElement elementalumno = new XElement("alumno");
            XElement elementidalumno = new XElement("idalumno", idalumno);
            XElement elementnombre = new XElement("nombre", nombre);
            XElement elementapellido = new XElement("apellidos", apellido);
            XElement elementnota = new XElement("nota", nota);
            elementalumno.Add(elementidalumno);
            elementalumno.Add(elementnombre);
            elementalumno.Add(elementapellido);
            elementalumno.Add(elementnota);

            this.docxml.Element("alumnos").Add(elementalumno);
            this.docxml.Save(this.path);
        }
        public void ModificarAlumno(int idalumno, String nombre, String apellido, int nota)
        {
            var consulta = from datos in this.docxml.Descendants("alumno") where datos.Element("idalumno").Value == idalumno.ToString() select datos;

            XElement element = consulta.FirstOrDefault();
            element.Element("nombre").Value = nombre;
            element.Element("apellidos").Value = apellido;
            element.Element("nota").Value = nota.ToString();
            this.docxml.Save(this.path);
        }
    }
}
