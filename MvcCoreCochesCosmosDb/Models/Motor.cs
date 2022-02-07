using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCochesCosmosDb.Models
{
    public class Motor
    {
        public string Tipo { get; set; }
        public bool Turbo { get; set; }
        public int Cilindrada { get; set; }
        public int Caballos { get; set; }
    }
}
