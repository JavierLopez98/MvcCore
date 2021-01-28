using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repository
{
    public class RepositoryUsuarios
    {
        HospitalContext context;
        public RepositoryUsuarios(HospitalContext context)
        {
            this.context = context;
        }

        public void InsertarUsuario(int idusuario,String nombre,
            String username,String password)
        {
            Usuario user = new Usuario();
            user.Id = idusuario;
            user.Nombre = nombre;
            user.User = username;
            user.Salt = CypherService.GenerateSalt();
            user.Password = CypherService.CifrarContenido(password, user.Salt);
            this.context.Usuarios.Add(user);
            this.context.SaveChanges();
        }

        public Usuario UserLogIn(String username,String password)
        {
            Usuario user = this.context.Usuarios
                .Where(z => z.User == username).FirstOrDefault();
            if (user == null) return null;
            else
            {
                String salt = user.Salt;
                byte[] passbbdd = user.Password;
                byte[] passtemporal = CypherService.CifrarContenido(password, salt);
                if (Toolkit.CompararArrayBytes(passbbdd,passtemporal)) return user;
                else return null;
            }
            
        }
    }
}
