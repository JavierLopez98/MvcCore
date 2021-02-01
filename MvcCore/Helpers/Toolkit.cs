using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class Toolkit
    {

        public static bool CompararArrayBytes(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i])) return false;

            }
            return true;
        }

        public static String NormalizarFilename(String filename)
        {
            String ending = '.' + filename.Split('.').Last();
            String cadena = "";
            for (int i = 0; i < filename.LastIndexOf('.'); i++)
            {
                if (Char.IsDigit(filename[i]) || Char.IsLetter(filename[i])) cadena += filename[i];
            }

            cadena += ending;
            return cadena;
        }

        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static Object ByteArrayToObject(byte[] arrBytes)
        {

            BinaryFormatter binForm = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream()) 
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return obj;
            }

        }

        //metodo que recibira un objeto y lo transformara a Json

        public static String SerializaJsonObject(object objeto)
        {
            String respuesta = JsonConvert.SerializeObject(objeto);
            return respuesta;
        }

        public static T JsonToObject<T>(String json)
        {

            return JsonConvert.DeserializeObject<T>(json);
            
        }
    }
}