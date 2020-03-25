using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Compi
{
    public class tokens
    {
       public string lexema, tipo;
       public int codigo;
        public tokens a;
        public tokens b;
        public int inicial;
        public int final;
        public tokens(string lexema, string tipo, int codigo)
        {
            this.lexema = lexema;
            this.tipo = tipo;
            this.codigo = codigo;
        }
        public tokens(tokens a, tokens b, int inicial, int final)
        {
            this.a = a;
            this.b = b;
            this.codigo = 28;
            this.lexema = "auxiliar";
            this.inicial = inicial;
            this.final = final;
        }
    }
    
}
