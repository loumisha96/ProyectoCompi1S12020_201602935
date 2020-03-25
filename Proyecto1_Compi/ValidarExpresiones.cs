using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Compi
{
    public class ValidarExpresiones
    {
        int i;
        List<tokens> tokens;
        tokens actual;
        List<signos> SIGNOS;
        List<Conjuntos> conjuntos;
        public ValidarExpresiones(int i, List<tokens> tokens, List<signos> signos, List<Conjuntos> conjuntos)
        {
            this.i = i;
            this.tokens = tokens;
            this.SIGNOS = signos;
            this.conjuntos = conjuntos;
        }
        public void Validar()
        {
            Console.WriteLine("HACER VALIDACIONES DE EXPRESIONES");
            
        }
    }
}
