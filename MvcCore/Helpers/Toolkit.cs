using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class Toolkit
    {

        public static bool CompararArrayBytes(byte[]a,byte[]b)
        {
            if (a.Length!=b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if(!a[i].Equals(b[i])) return false;

            }
            return true;
        }
    }
}
