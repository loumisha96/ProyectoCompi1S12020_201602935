using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto1_Compi
{
    public class GenerarAFND
    {
        
        int i;
        List<tokens> tokens;
        tokens actual;
        List<signos> SIGNOS;
        List<Conjuntos> conjuntos;
        Pila pila = new Pila();
        string path;
        StreamWriter file;
        int estado = 0;
        int inicial, final;
        List<tokens> expr;
        tokens tokinicial, tokfinal;
        Arbol arbol;
        public GenerarAFND(int i, List<tokens> tokens, List<signos> signos, List<Conjuntos> conjuntos)
        {
            this.i = i;
            this.tokens = tokens;
            this.SIGNOS = signos;
            this.conjuntos = conjuntos;
        }
        public void Expresion()
        {
            actual = tokens.ElementAt(i);
            path = actual.lexema + ".dot";
            file = File.CreateText(path);
            file.WriteLine("digraph G{");
            file.WriteLine("node[shape=circle, style=filled, color= Gray70];");
            file.WriteLine("edge[color=black]");
            i++;
            actual = tokens.ElementAt(i);
            if (actual.codigo.Equals(27))
            {
                i++;
                actual = tokens.ElementAt(i);
                if (actual.codigo.Equals(26))
                {
                    //arbol = new Arbol(estado);
                    i++;
                    actual = tokens.ElementAt(i);
                    tokens Opaux = null;
                    while (actual.codigo != 16)
                    {
                        if (actual.tipo.Equals("Operacion"))
                        {
                            Opaux = actual;
                            pila.Push(actual);
                            
                            i++;
                            actual = tokens.ElementAt(i);
                        }
                        else if (actual.codigo.Equals(14) || actual.codigo.Equals(15))
                        {
                            i++;
                            actual = tokens.ElementAt(i);
                            
                        }
                        else
                        {
                            
                            Graph(Opaux);
                            Opaux = null;
                            i++;
                            
                            actual = tokens.ElementAt(i);

                        }
                        while (Verificar() == true)
                        {
                            nodoP auxiliar= pila.peek();
                            GraphnodoP(auxiliar);
                        }
                            
                    }
                    while (pila.primero != null)
                    {
                        nodoP aux = pila.peek();
                        GraphnodoP(aux);
                        
                    }
                    estado = 0;
                     file.WriteLine("}");
                    file.Close();
                    arbol.reporte(path);
                    i++;
                    actual = tokens.ElementAt(i);
                    while (actual.codigo.Equals(5) || actual.codigo.Equals(6))
                    {
                        i++;
                        actual = tokens.ElementAt(i);
                    }
                    if (actual.codigo.Equals(7))
                        Expresion();
                    while (actual.codigo.Equals(5) || actual.codigo.Equals(6))
                    {
                        i++;
                        actual = tokens.ElementAt(i);
                    }
                    ValidarExpresiones valExpr = new ValidarExpresiones(i,tokens,SIGNOS,conjuntos);
                    valExpr.Validar();
                    
                }
            }
        }
        public void GraficarDisyuncion(tokens a, tokens b)
        {
            if (a.final == 0 && b.final == 0)
            {
                inicial = estado;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=" + a.lexema + "]");
                estado++;
                file.WriteLine(inicial + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=" + b.lexema + "]");
                estado++;
                file.WriteLine(estado - 2 + "->{" + (estado + 1) + "}[label=\"E\"]");
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                final = estado;
                estado++;
            }
            else if (a.final != 0 && b.final != 0)
            {
                estado++;
                inicial = estado;
                file.WriteLine(estado + "->{" + a.inicial + "}[label=\"E\"]");
                file.WriteLine(estado + "->{" + b.inicial + "}[label=\"E\"]");
                estado++;
                file.WriteLine(a.final + "->{" + estado + "}[label=\"E\"]");
                file.WriteLine(b.final + "->{" + estado + "}[label=\"E\"]");
                final = estado;
                

            }
            else if (a.final != 0 && b.final == 0)
            {
                estado++;
                inicial = estado;
                file.WriteLine(estado + "->{" + a.inicial + "}[label=\"E\"]");
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(a.final + "->{" + (estado) + "}[label=\"E\"]");
                final = estado;
            }

        }
        public void GraficarConcatenacion(tokens a, tokens b)
        {
            if (a.final == 0 && b.inicial == 0)
            {
                inicial = estado;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=" + a.lexema + "]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"" + b.lexema + "\"]");
                estado++;
                final = estado;
            }else if(a.final==0 && b.final != 0&&a.inicial==0)
            {
                inicial = b.final + 1;
                file.WriteLine((estado) + "->{" + b.inicial + "}[label=" + a.lexema + "]");
                final = b.final;
            }else if (a.final != 0 && b.final == 0)
            {
                inicial = a.inicial;
                file.WriteLine(a.final + "->{" +estado + "}[label="+b.lexema+"]");
                final = b.final;
            }
            else if (a.inicial !=0 && b.final != 0)
            {
                inicial = a.inicial;
                file.WriteLine(a.final + "->{" + b.inicial + "}[label=\"E\"]");
                final = b.final;
            }
            
            else
            {
                inicial = a.inicial;
                file.WriteLine(a.final + "->{" + b.inicial + "}[label=\"E\"]");
                final = b.final;
            }
        }
        public void Graficar0oUnaVez(tokens a)
        {
            if (a.final == 0)
            {
                inicial = estado;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=" + a.lexema + "]");
                estado++;
                file.WriteLine((inicial) + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\" E\"]");
                estado++;
                file.WriteLine(estado - 2 + "->{" + (estado + 1) + "}[label=\"E\"]");
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                final = estado;
                estado++;
            }
            else
            {
                estado++;
                inicial =estado;
                file.WriteLine((inicial) + "->{" + a.inicial + "}[label=\"E\"]");
                file.WriteLine((inicial) + "->{" + (estado+1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado+1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(a.final + "->{" + (estado) + "}[label=\"E\"]");
                final = estado;
                estado++;
            }
        }
        public void Graficar1oMasVeces(tokens a)
        {
            if (a.final == 0)
            {
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=" + a.lexema + "]");
                estado++;
                inicial = estado;
                file.WriteLine(inicial + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=" + a.lexema + "]");
                estado++;
                file.WriteLine(estado + "->{" + (estado - 1) + "}[label=\"E\"]");
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(inicial + "->{" + estado + "}[label=\"E\"]");
                estado++;
                final = estado;
            }
            else
            {
                if (a.codOp.Equals(8))
                    GraficarDisyuncion(a.a, a.b);
                else if (a.codOp.Equals(9))
                    GraficarConcatenacion(a.a, a.b);
                else if (a.codOp.Equals(10))
                    Graficar0oUnaVez(a);
                else if (a.codOp.Equals(11))
                    Graficar1oMasVeces(a);
                else if (a.codOp.Equals(12))
                    GraficarKleene(a);
                int i = final+1;
                GraficarKleene(a);
                int f = a.final+1;
                file.WriteLine(i + "->{" + f + "}[label=\"E\"]");

            }

        }
        public void GraficarKleene(tokens a)
        {
            if (a.final == 0)
            {
                inicial = estado;
                file.WriteLine(inicial + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=" + a.lexema + "]");
                estado++;
                file.WriteLine(estado + "->{" + (estado - 1) + "}[label=\"E\"]");
                file.WriteLine(estado + "->{" + (estado + 1) + "}[label=\"E\"]");
                estado++;
                file.WriteLine(inicial + "->{" + estado + "}[label=\"E\"]");
                final = estado;
            }
            else
            {
                inicial = estado;
                file.WriteLine(a.final + "->{" + a.inicial + "}[label=\"E\"]");
                file.WriteLine((a.inicial-1) + "->{" + a.inicial + "}[label=\"E\"]");
                file.WriteLine((a.inicial-1) + "->{" + estado + "}[label=\"E\"]");
                file.WriteLine(a.final + "->{" + (estado ) + "}[label=\"E\"]");
                final = estado;
                estado++;
                
            }
        }
        public void Graph(tokens Opaux)
        {
            nodoP aux = pila.peek();
            if (Opaux == null)
            {
                pila.Push(actual);
            }
            else if (Opaux.codigo.Equals(8))
            {
                if (aux.actual.tipo != "Operacion")
                {
                    //arbol.Disyuncion(aux.actual, actual);
                    GraficarDisyuncion(aux.actual, actual);
                    pila.Pop();
                    pila.Pop();
                    tokens tokenP = new tokens(aux.actual, actual, inicial, final,8);

                    //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                    pila.Push(tokenP);
                }
                else
                    pila.Push(actual);
            }
            else if (Opaux.codigo.Equals(9))
            {

                if (aux.actual.tipo != "Operacion")
                {
                    //arbol.Concatenacion(aux.actual, actual);
                    GraficarConcatenacion(aux.actual, actual);
                    pila.Pop();
                    pila.Pop();
                     tokens tokenP = new tokens(aux.actual, actual, inicial, final,9);
                    //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                    pila.Push(tokenP);
                }
                else
                    pila.Push(actual);
            }
            else if (Opaux.codigo.Equals(10))
            {
                pila.Pop();
                
                Graficar0oUnaVez(actual);
                tokens tokenP = new tokens(aux.actual, actual, inicial, final,10);
                /*arbol.G0oUnaVez(actual);
                tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);*/
                pila.Push(tokenP);
                
            }
            else if (Opaux.codigo.Equals(11))
            {
                pila.Pop();
                //arbol.G1oMasVeces(actual);
                Graficar1oMasVeces(actual);
                tokens tokenP = new tokens(aux.actual, actual, inicial, final,11);
                //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                pila.Push(tokenP);
            }
            else if (Opaux.codigo.Equals(12))
            {
                pila.Pop();
                //arbol.kleene(actual);
                GraficarKleene(actual);
                 tokens tokenP = new tokens(aux.actual, actual, inicial, final,12);
                //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                pila.Push(tokenP);
            }
            
        }
        public void GraphnodoP(nodoP aux)
        {
            ElegirOp(aux);
        }
        public void Graph1( tokens a, tokens b)
        {
            nodoP Opaux = pila.peek();
            if (Opaux.actual.codigo.Equals(8))
            {
                //arbol.Disyuncion(a, b);
                   GraficarDisyuncion(a, b);
                    pila.Pop();
                tokens tokenP = new tokens(a, b, inicial, final,8);
                //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                pila.Push(tokenP);
            }
            else if (Opaux.actual.codigo.Equals(9))
            {
               // arbol.Concatenacion(a, b);
               GraficarConcatenacion(a,b);
                pila.Pop();
                tokens tokenP = new tokens(a, b, inicial, final,9);
                //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                pila.Push(tokenP);
            }
            else if (Opaux.actual.codigo.Equals(10))
            {
                pila.Pop();
                //arbol.G0oUnaVez(actual);
                Graficar0oUnaVez(actual);
                tokens tokenP = new tokens(a, b, inicial, final,10);
                //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                pila.Push(tokenP);
                
            }
            else if (Opaux.actual.codigo.Equals(11))
            {
                pila.Pop();
                //arbol.G1oMasVeces(actual);
                Graficar1oMasVeces(actual);
                tokens tokenP = new tokens(a, b, inicial, final,11);
                //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                pila.Push(tokenP);
            }
            else if (Opaux.actual.codigo.Equals(12))
            {
                pila.Pop();
                //arbol.kleene(actual);
                GraficarKleene(actual);
                tokens tokenP = new tokens(a, b, inicial, final,12);
                //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                pila.Push(tokenP);
            }
        }
        public Boolean Verificar()
        {
            nodoP auxb = pila.peek();
            pila.Pop();
            nodoP auxa = pila.peek();
            pila.Pop();
            if (auxb == null)
                return false;
            else if (auxb.actual.tipo != "Operacion" && (auxa.actual.codigo != 8 && auxa.actual.codigo != 9) && auxb != null && auxa != null)
            {
                pila.Push(auxa.actual);
                pila.Push(auxb.actual);
                return true;
            }
            else if (auxb != null && auxa != null && auxa.actual.tipo != "Operacion" && auxb.actual.tipo != "Operacion")
            {
                pila.Push(auxa.actual);
                pila.Push(auxb.actual);
                return true;
            }
            else
            {
                if (auxa != null)
                    pila.Push(auxa.actual);
                if (auxb != null)
                    pila.Push(auxb.actual);
                return false;
            }
                
        }
        public void ElegirOp(nodoP aux)
        {
            pila.Pop();
            nodoP b = aux;
            aux = pila.peek();
            if (aux.actual.codigo.Equals(8))
            {

                pila.Pop();
            }
            else if (aux.actual.codigo.Equals(9))
            {

            }
            else if (aux.actual.codigo.Equals(10))
            {
                //arbol.G0oUnaVez(b.actual);
                Graficar0oUnaVez(b.actual);
                if (pila.Pop() != false)
                {
                    tokens tokenP = new tokens(aux.actual, b.actual, inicial, final,10);
                    //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                    pila.Push(tokenP);
                }

            }
            else if (aux.actual.codigo.Equals(11))
            {
                //arbol.G1oMasVeces(b.actual);
                Graficar1oMasVeces(b.actual);
                if (pila.Pop() != false)
                {
                    tokens tokenP = new tokens(aux.actual, b.actual, inicial, final,11);
                    //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                    pila.Push(tokenP);
                }
            }
            else if (aux.actual.codigo.Equals(12))
            {
                //arbol.kleene(b.actual);
               GraficarKleene(b.actual);
                if (pila.Pop() != false)
                {
                    tokens tokenP = new tokens(aux.actual, b.actual, inicial, final,12);
                    //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                    pila.Push(tokenP);
                }
            }

            else if (aux.actual.codigo.Equals(28))
            {

                nodoP a = aux;
                pila.Pop();
                aux = pila.peek();
                if (aux.actual.codigo.Equals(8))
                {
                    //arbol.Disyuncion(a.actual, b.actual);
                    GraficarDisyuncion(a.actual, b.actual);
                    if (pila.Pop() != false)
                    {
                        tokens tokenP = new tokens(a.actual, b.actual, inicial, final,8);
                        //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                        pila.Push(tokenP);
                    }
                }
                else if (aux.actual.codigo.Equals(9))
                {
                    //arbol.Concatenacion(a.actual, b.actual);
                    GraficarConcatenacion(a.actual, b.actual);
                    if (pila.Pop() != false)
                    {
                        tokens tokenP = new tokens(a.actual, b.actual, inicial, final,9);
                        //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                        pila.Push(tokenP);

                    }
                }
                else if (aux.actual.codigo.Equals(11))
                {
                    //arbol.G1oMasVeces(a.actual);
                    Graficar1oMasVeces(a.actual);
                    if (pila.Pop() != false)
                    {
                        tokens tokenP = new tokens(a.actual, b.actual, inicial, final,11);
                        //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                        pila.Push(tokenP);
                    }
                }
                else if (aux.actual.codigo.Equals(10))
                {
                    //arbol.G0oUnaVez(a.actual);
                    Graficar0oUnaVez(a.actual);
                    if (pila.Pop() != false)
                    {
                        tokens tokenP = new tokens(aux.actual, a.actual, inicial, final,10);
                        //tokens tokenP = new tokens(arbol, arbol.eInicio, arbol.eFinal);
                        pila.Push(tokenP);
                        pila.Push(b.actual);
                    }
                }
            }
            else if (aux.actual.codigo.Equals(19))
            {
                pila.Pop();
                nodoP c = aux;
                aux = pila.peek();
                Graph1(c.actual, b.actual);
            }
            else if (aux.actual.codigo.Equals(7))
            {
                pila.Pop();
                nodoP c = aux;
                aux = pila.peek();
                Graph1(c.actual, b.actual);
            }
        }
    }
}
