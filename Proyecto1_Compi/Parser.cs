using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Compi
{
    class Parser
    {
        List<tokens> tokens;
        List<Conjuntos> conjuntos = new List<Conjuntos>();
        List<signos> SIGNOS = new List<signos>();
        List<Expresion> expresiones = new List<Expresion>();
        tokens actual;
        GenerarAFND AFND;
        int i;
        
        public void Parsear(List<tokens> tokens)
        {
            this.tokens = tokens;
            for (i = 0; i < tokens.Count; i++)
            {
                actual = tokens.ElementAt(i);
                if (actual.codigo.Equals(14))
                {
                    i++;
                    actual = tokens.ElementAt(i);
                    if(actual.codigo.Equals(5) || actual.codigo.Equals(6))
                    {
                        
                        i++;
                        actual = tokens.ElementAt(i);
                        while (actual.codigo.Equals(5) || actual.codigo.Equals(6))
                        {
                            i++;
                            actual = tokens.ElementAt(i);
                        }
                        if (actual.codigo.Equals(21))
                        {
                            i++;
                            Conjuntos();
                        }else if(actual.codigo.Equals(5) || actual.codigo.Equals(6))
                        {
                            i++;
                            actual = tokens.ElementAt(i);
                            while (actual.codigo.Equals(5) || actual.codigo.Equals(6))
                            {
                                i++;
                                actual = tokens.ElementAt(i);
                            }
                            if (actual.codigo.Equals(21))
                            {
                                i++;
                                Conjuntos();
                            }
                        }
                    }
                    else if (actual.codigo.Equals(21))
                    {
                        i++;
                        
                        Conjuntos();
                    }
                    else if (!actual.codigo.Equals(5) || !actual.codigo.Equals(6))
                    {
                        Error(actual);
                    }
                }
                else if(!actual.codigo.Equals(5) || !actual.codigo.Equals(6))
                {
                    Error(actual);
                    break;
                }
            }
        }
        public void Conjuntos()
        {
            Conjuntos conj = new Conjuntos(); 
            actual = tokens.ElementAt(i);
            if (actual.codigo.Equals(17))
            {
                i++;
                ID(conj);
                actual = tokens.ElementAt(i);
                if (actual.codigo.Equals(27))
                {
                    i++;
                    actual = tokens.ElementAt(i);
                    if (actual.codigo.Equals(26))
                    {
                        i++;
                        RANGO();
                        actual = tokens.ElementAt(i);
                        if (actual.codigo.Equals(16))
                        {
                            conjuntos.Add(conj);
                            rango = "";
                            i++;
                            actual = tokens.ElementAt(i);
                            if (actual.codigo.Equals(21))
                            {
                                i++;
                                Conjuntos();
                            }else if(actual.codigo.Equals(5) || actual.codigo.Equals(6))
                            {
                                i++;
                                actual = tokens.ElementAt(i);
                                while (actual.codigo.Equals(5) || actual.codigo.Equals(6))
                                {
                                    i++;
                                    actual = tokens.ElementAt(i);
                                }
                                if (actual.codigo.Equals(21))
                                {
                                    i++;
                                    Conjuntos();
                                }else if (actual.codigo.Equals(7))
                                {

                                   AFND = new GenerarAFND(i, tokens, SIGNOS, conjuntos);
                                   AFND.Expresion();
                                    i = tokens.Count;
                                    
                                }
                            }
                            else if (actual.codigo.Equals(25))
                            {
                                i++;
                                actual = tokens.ElementAt(i);
                                if (actual.codigo.Equals(25))
                                {
                                    i++;
                                    AFND.Expresion();//4 porcentajes

                                }
                                else
                                    Error(actual);
                            }
                            else
                                Error(actual);
                        }
                        else
                            Error(actual);
                    }
                    else
                        Error(actual);
                }
                else
                    Error(actual);
            }
            else
                Error(actual);
                  
        }
        public void ID(Conjuntos conj)
        {
            actual = tokens.ElementAt(i);
            if (actual.codigo.Equals(7) || actual.codigo.Equals(18))
            {
                conj.nombre = actual.lexema;
                i++;
            }
            else
                Error(actual);
        }
        string rango;
        public void RANGO()
        {
            actual = tokens.ElementAt(i);
            if (actual.codigo.Equals(7))
            {
                rango += actual.lexema;
                i++;
                actual = tokens.ElementAt(i);
                if (actual.codigo.Equals(22))
                {
                    rango += actual.lexema;
                    i++;
                    actual = tokens.ElementAt(i);
                    if (actual.codigo.Equals(7))
                    {
                        rango += actual.lexema;
                        i++;
                    }
                    else
                        Error(actual);
                }
                else if (actual.codigo.Equals(24))
                {
                    rango += actual.lexema;
                    i++;
                    RANGO();
                }
            }
            else if (actual.codigo.Equals(18))
            {
                rango += actual.lexema;
                i++;
                actual = tokens.ElementAt(i);
                if (actual.codigo.Equals(22))
                {
                    rango += actual.lexema;
                    i++;
                    actual = tokens.ElementAt(i);
                    if (actual.codigo.Equals(18))
                    {
                        rango += actual.lexema;
                        i++;
                    }
                    else if (actual.codigo.Equals(24))
                    {
                        rango += actual.lexema;
                        i++;
                        RANGO();
                    }
                    else
                        Error(actual);
                }
                else
                    Error(actual);
            }
            else
                Error(actual);
        }

        public void Error(tokens tok)
        {
            tokens token = new tokens(tok.lexema, "Error sintáctico", 20);
            tokens.Add(token);
        }
        Boolean buscar(tokens tok)
        {
            foreach(Conjuntos conj in conjuntos)
            {
                if (conj.nombre.Equals(tok.lexema))
                {
                    return true;
                }
            }
            return false;
        }
        
        public void printConjuntos()
        {
            foreach (Conjuntos conj in conjuntos)
            {
              Console.WriteLine(conj.cod + "|" + conj.nombre + "|" + conj.rango);
                
            }
        }
    }
}
