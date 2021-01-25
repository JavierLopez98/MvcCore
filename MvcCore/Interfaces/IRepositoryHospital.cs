using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Interfaces
{
    public interface IRepositoryHospital
    {
        List<Departamento> GetDepartamentos();

        Departamento BuscarDepartamento(int iddept);

        void ModificarDepartamento(int iddept, String nombre, String loc);

        void EliminarDepartamento(int iddept);

        void InsertarDepartamento(int iddept, String nombre, String loc);
        List<Empleado> GetEmpleados();
        List<Empleado> BuscaEmpleadosDept(List<int>iddept);
    }
}
