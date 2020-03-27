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
        public int codigo, inicial, final, estado, codOp;
        public tokens a,b;
        public Arbol arbol;
        public tokens(string lexema, string tipo, int codigo)
        {
            this.lexema = lexema;
            this.tipo = tipo;
            this.codigo = codigo;
        }
        public tokens(tokens a, tokens b, int inicial, int final, int codOp)
        {
            this.a = a;
            this.b = b;
            this.codigo = 28;
            this.lexema = "auxiliar";
            this.inicial = inicial;
            this.final = final;
            this.codOp = codOp;
        }
        public tokens(Arbol a, int inicio, int final)
        {
            this.inicial = inicio;
            this.final = final;
            this.codigo = 28;
            this.arbol = a;
        }

    }
    
}
