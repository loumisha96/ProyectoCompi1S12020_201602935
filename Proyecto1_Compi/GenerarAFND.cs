﻿using System;
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
                            
                            graph(Opaux);
                            Opaux = null;
                            i++;
                            
                            actual = tokens.ElementAt(i);

                        }
                        while (verificar() == true)
                        {
                            nodoP auxiliar= pila.peek();
                            graphnodoP(auxiliar);
                        }
                            
                    }
                    while (pila.primero != null)
                    {
                        nodoP aux = pila.peek();
                        
                        graphnodoP(aux);
                        
                    }
                    estado = 0;
                    file.WriteLine("}");
                    file.Close();
                    i++;
                    actual = tokens.ElementAt(i);
                    while (actual.codigo.Equals(5) || actual.codigo.Equals(6))
                    {
                        i++;
                        actual = tokens.ElementAt(i);
                    }
                    if (actual.codigo.Equals(7))
                    {
                        Expresion();
                    }
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
        public void graficarDisyuncion(tokens a, tokens b)
        {
            if (a.inicial == 0 && b.inicial == 0)
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
            else if (a.inicial != 0 && b.inicial != 0)
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
            else if (a.inicial != 0 && b.inicial == 0)
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
        public void graficarConcatenacion(tokens a, tokens b)
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
        public void graficar0oUnaVez(tokens a)
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
        public void graficar1oMasVeces(tokens a)
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
        public void graficarKleene(tokens a)
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
        public void graph(tokens Opaux)
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
                    graficarDisyuncion(aux.actual, actual);
                    pila.Pop();
                    pila.Pop();
                    tokens tokenP = new tokens(aux.actual, actual, inicial, final);
                    pila.Push(tokenP);
                    

                }
                else
                {

                    pila.Push(actual);
                    
                }

            }
            else if (Opaux.codigo.Equals(9))
            {

                if (aux.actual.tipo != "Operacion")
                {
                    graficarConcatenacion(aux.actual, actual);
                    pila.Pop();
                    pila.Pop();
                    tokens tokenP = new tokens(aux.actual, actual, inicial, final);
                    pila.Push(tokenP);
                }
                else
                {
                    
                    pila.Push(actual);
                    
                }
                    

            }
            else if (Opaux.codigo.Equals(10))
            {
                pila.Pop();
                
                graficar0oUnaVez(actual);
                tokens tokenP = new tokens(aux.actual, actual, inicial, final);
                pila.Push(tokenP);
                
            }
            else if (Opaux.codigo.Equals(11))
            {
                pila.Pop();
                graficar1oMasVeces(actual);
                tokens tokenP = new tokens(aux.actual, actual, inicial, final);
                pila.Push(tokenP);
                

            }
            else if (Opaux.codigo.Equals(12))
            {
                pila.Pop();
                
                graficarKleene(actual);
                tokens tokenP = new tokens(aux.actual, actual, inicial, final);
                pila.Push(tokenP);
                

            }
            
        }
        public void graphnodoP(nodoP aux)
        {
            elegirOp(aux);
        }
       
        public void graph1( tokens a, tokens b)
        {
            nodoP Opaux = pila.peek();
            if (Opaux.actual.codigo.Equals(8))
            {
                    graficarDisyuncion(a, b);
                    pila.Pop();
                    tokens tokenP = new tokens(a, b, inicial, final);
                    pila.Push(tokenP);
                    


            }
            else if (Opaux.actual.codigo.Equals(9))
            {

                
                
                    graficarConcatenacion(a,b);
                    pila.Pop();
                    
                    tokens tokenP = new tokens(a, b, inicial, final);
                    pila.Push(tokenP);
                    


            }
            else if (Opaux.actual.codigo.Equals(10))
            {
                pila.Pop();
                
                graficar0oUnaVez(actual);
                tokens tokenP = new tokens(a, b, inicial, final);
                pila.Push(tokenP);
                
            }
            else if (Opaux.actual.codigo.Equals(11))
            {
                pila.Pop();
                graficar1oMasVeces(actual);
                tokens tokenP = new tokens(a, b, inicial, final);
                pila.Push(tokenP);
                

            }
            else if (Opaux.actual.codigo.Equals(12))
            {
                pila.Pop();
                
                graficarKleene(actual);
                tokens tokenP = new tokens(a, b, inicial, final);
                pila.Push(tokenP);
                

            }

        }
        public Boolean verificar()
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
        public void elegirOp(nodoP aux)
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


                graficar0oUnaVez(b.actual);
                if (pila.Pop() != false)
                {
                    tokens tokenP = new tokens(aux.actual, b.actual, inicial, final);
                    pila.Push(tokenP);
                }

            }
            else if (aux.actual.codigo.Equals(11))
            {
                graficar1oMasVeces(b.actual);
                if (pila.Pop() != false)
                {
                    tokens tokenP = new tokens(aux.actual, b.actual, inicial, final);
                    pila.Push(tokenP);
                }
            }
            else if (aux.actual.codigo.Equals(12))
            {
                graficarKleene(b.actual);
                if (pila.Pop() != false)
                {
                    tokens tokenP = new tokens(aux.actual, b.actual, inicial, final);
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
                    graficarDisyuncion(a.actual, b.actual);
                    if (pila.Pop() != false)
                    {
                        tokens tokenP = new tokens(a.actual, b.actual, inicial, final);
                        pila.Push(tokenP);
                    }
                }
                else if (aux.actual.codigo.Equals(9))
                {
                    graficarConcatenacion(a.actual, b.actual);
                    if (pila.Pop() != false)
                    {
                        tokens tokenP = new tokens(a.actual, b.actual, inicial, final);
                        pila.Push(tokenP);
                        
                    }
                }
                else if (aux.actual.codigo.Equals(11))
                {
                    graficar1oMasVeces(a.actual);
                    if (pila.Pop() != false)
                    {
                        tokens tokenP = new tokens(a.actual, b.actual, inicial, final);
                        pila.Push(tokenP);
                    }
                }
                else if (aux.actual.codigo.Equals(10))
                {

                    graficar0oUnaVez(a.actual);
                    if (pila.Pop() != false)
                    {
                        tokens tokenP = new tokens(aux.actual, a.actual, inicial, final);
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
                graph1(c.actual, b.actual);


            }
            else if (aux.actual.codigo.Equals(7))
            {
                pila.Pop();
                nodoP c = aux;
                aux = pila.peek();
                graph1(c.actual, b.actual);
            }
        }
    }
}
