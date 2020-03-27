using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Compi
{
    public class nodo
    {
        public nodo izq;
        public nodo der;
        public int estado;
        public string lexema;
        public nodo(int estado, string lexema)
        {
            this.lexema = lexema;
            this.estado = estado;
        }
        public nodo(int estado)
        {
            this.lexema = null;
            this.estado = estado;
        }
    }
    public class Arbol
    {
        public nodo inicio;
        public nodo final;
        public int eInicio;
        public int eFinal;
        public tokens a;
        public tokens b;
        public StreamWriter report;
        public int estado;
        public Arbol(int eInicio)
        {
            
            this.eInicio = 0;
            this.eFinal = 0;
            this.a = null;
            this.b = null;
        }
        public void setInicio(int eInicio)
        {
            this.eInicio = eInicio;
        }
        public int getInicio()
        {
            return this.eInicio;
        }
        public void setFinal(int eFinal)
        {
            this.eFinal = eFinal;
        }
        public int getFinal()
        {
            return this.eFinal;
        }
        public void Concatenacion(tokens a, tokens b)
        {

            if (a.arbol == null && b.arbol == null)
            {
                inicio.lexema = a.lexema;
                setInicio(estado);

                estado++;
                nodo nuevo = new nodo(estado, b.lexema);
                inicio.der = nuevo;
                estado++;
                nodo nuevo1 = new nodo(estado);
                nuevo.der = nuevo1;
                setFinal(estado);
                if (final != null)
                    final.der = nuevo1;
                final = nuevo1;
                if (inicio == null)
                    inicio = nuevo;


            }
            else if (a.arbol != null && b.arbol == null)
            {
                if (inicio == null)
                    inicio = a.arbol.inicio;
                inicio.lexema = a.lexema;
                setInicio(a.arbol.eInicio);
                setFinal(a.arbol.eFinal + 1);
                a.arbol.final.lexema = b.lexema;
                nodo nuevo = new nodo(eFinal);
                a.arbol.final.der = nuevo;
                if (final != null)
                    final.der = inicio;
                final = nuevo;

            }
            else if (a.arbol == null && b.arbol != null)
            {

                setInicio(estado);
                nodo nuevo = new nodo(estado, a.lexema);
                nuevo.der = b.arbol.inicio;
                setFinal(b.arbol.eFinal);
                if (inicio == null)
                    inicio = nuevo;
                if (final != null)
                    final.der = nuevo;
                final = b.arbol.final;
            }
            else
            {
                if (inicio == null)
                    inicio = a.arbol.inicio;
                setInicio(estado);
                setFinal(b.arbol.eFinal);
                a.arbol.eFinal = b.arbol.eInicio;
                if (final != null)
                    final.der = inicio;
                final = b.arbol.final;
            }
        }
        public void Disyuncion(tokens a, tokens b)
        {

            if (a.arbol == null && b.arbol == null)
            {
                setInicio(estado);
                nodo nuevo1 = new nodo(estado, "E");
                if (final != null)
                    final.der = nuevo1;
                estado++;
                nodo nuevo2 = new nodo(estado, a.lexema);
                estado++;
                nodo nuevo3 = new nodo(estado, b.lexema);
                nuevo1.der = nuevo2;
                nuevo1.izq = nuevo3;
                estado++;
                nodo nuevo4 = new nodo(estado, "E");
                estado++;
                nodo nuevo5 = new nodo(estado, "E");
                nuevo2.der = nuevo4;
                nuevo3.der = nuevo5;
                estado++;
                nodo nuevo6 = new nodo(estado);
                nuevo4.der = nuevo6;
                nuevo5.der = nuevo6;
                setFinal(estado);
                if (inicio == null)
                    inicio = nuevo1;
                if (final != null)
                    final.der = nuevo1;
                final = nuevo6;
            }
            else if (a.arbol != null && b.arbol == null)
            {

                setInicio(a.arbol.eInicio - 1);
                nodo nuevo1 = new nodo(estado, "E");
                nuevo1.der = a.arbol.inicio;
                estado++;
                nodo nuevo2 = new nodo(estado, b.lexema);
                nuevo1.der = nuevo2;
                estado++;
                nodo nuevo3 = new nodo(estado, "E");
                nuevo2.der = nuevo3;
                nodo nuevo4 = new nodo(eInicio);
                a.arbol.final.lexema = "E";
                a.arbol.final = nuevo4;
                nuevo3.der = nuevo4;
                if (inicio == null)
                    inicio = nuevo1;
                if (final != null)
                    final.der = nuevo1;
                final = nuevo4;


            }
            else if (a.arbol == null && b.arbol != null)
            {
                setInicio(estado);
                nodo nuevo = new nodo(estado, "E");
                estado++;
                nodo nuevo1 = new nodo(estado, a.lexema);
                nuevo.der = nuevo1;
                nuevo.izq = b.arbol.inicio;
                estado++;
                nodo nuevo2 = new nodo(estado, "E");
                nuevo1.der = nuevo2;
                b.arbol.final.lexema = "E";
                estado++;
                nodo nuevo3 = new nodo(estado);
                nuevo2.der = nuevo3;
                b.arbol.final.der = nuevo3;
                if (inicio == null)
                    inicio = nuevo;
                if (final != null)
                    final.der = nuevo;
                final = nuevo3;

            }
            else
            {
                setInicio(estado);
                nodo nuevo = new nodo(estado, "E");
                if (inicio == null)
                    inicio = nuevo;
                nuevo.der = a.arbol.inicio;
                nuevo.izq = b.arbol.inicio;
                estado++;
                a.arbol.final.lexema = "E";
                b.arbol.final.lexema = "E";
                nodo nuevo1 = new nodo(estado);
                a.arbol.final.der = nuevo1;
                b.arbol.final.der = nuevo1;
                if (final != null)
                    final.der = nuevo;
                final = nuevo1;
            }
        }
        public void kleene(tokens a)
        {
            if (a.arbol == null)
            {
                setInicio(estado);
                nodo nuevo = new nodo(estado, "E");
                estado++;
                nodo nuevo1 = new nodo(estado, a.lexema);
                estado++;
                nodo nuevo2 = new nodo(estado, "E");
                estado++;
                nodo nuevo3 = new nodo(estado);
                nuevo.der = nuevo1;
                nuevo.izq = nuevo3;
                nuevo1.der = nuevo2;
                nuevo2.der = nuevo3;
                nuevo2.izq = nuevo1;
                if (inicio == null)
                    inicio = nuevo;
                if (final != null)
                    final.der = nuevo;
                final = nuevo3;
            }
            else
            {
                setInicio(estado);
                nodo nuevo = new nodo(estado, "E");
                estado++;
                nodo nuevo1 = new nodo(estado);
                nuevo.der = a.arbol.inicio;
                nuevo.izq = nuevo1;
                a.arbol.final.izq = a.arbol.inicio;
                a.arbol.final.lexema = "E";
                a.arbol.final.der = nuevo1;
                if (inicio == null)
                    inicio = nuevo;
                if (final != null)
                    final.der = nuevo;
                final = nuevo1;
            }
        }
        public void G0oUnaVez(tokens a)
        {
            if (a.arbol == null)
            {

                setInicio(estado);
                
                nodo nuevo1 = new nodo(estado, "E");

                estado++;
                nodo nuevo2 = new nodo(estado, a.lexema);
                estado++;
                nodo nuevo3 = new nodo(estado, "E");
                nuevo1.der = nuevo2;
                nuevo1.izq = nuevo3;
                estado++;
                nodo nuevo4 = new nodo(estado, "E");
                estado++;
                nodo nuevo5 = new nodo(estado, "E");
                nuevo2.der = nuevo4;
                nuevo3.der = nuevo5;
                estado++;
                nodo nuevo6 = new nodo(estado);
                nuevo4.der = nuevo6;
                nuevo5.der = nuevo6;
                setFinal(estado);
                if (inicio == null)
                    inicio = nuevo1;
                if (final != null)
                    final.der = nuevo1;
                final = nuevo6;
            }
            else
            {
                nodo aux = buscar(a.inicial);
                setInicio(estado);
                estado++;
                nodo nuevo2 = new nodo(estado, "E");
                aux.izq = nuevo2;
                estado++;
                nodo nuevo3 = new nodo(estado, "E");
                nuevo2.der = nuevo3;
                nodo nuevo4 = new nodo(estado);
                a.arbol.final.lexema = "E";
                a.arbol.final = nuevo4;
                nuevo3.der = nuevo4;
                if (inicio == null)
                    inicio = aux;
                
                final = nuevo4;
            }
        }
        public void G1oMasVeces(tokens a)
        {
            if (a.arbol == null)
            {
                setInicio(estado);
                nodo nuevo = new nodo(estado, a.lexema);
                estado++;
                nodo nuevo1 = new nodo(estado, "E");
                estado++;
                nodo nuevo2 = new nodo(estado, a.lexema);
                estado++;
                nodo nuevo3 = new nodo(estado, "E");
                estado++;
                nodo nuevo4 = new nodo(estado);
                nuevo.der = nuevo1;
                nuevo1.der = nuevo2;
                nuevo1.izq = nuevo4;
                nuevo2.der = nuevo3;
                nuevo3.der = nuevo4;
                nuevo3.izq = nuevo2;
                if (inicio == null)
                    inicio = nuevo;
                if (final != null)
                    final.der = nuevo;
                final = nuevo4;
            }
            else
            {

            }
        }

        public void reporte(string path)
        {
            report = File.CreateText(path);
            report.WriteLine("digraph G{");
            report.WriteLine("node[shape=circle, style=filled, color = Gray95];");
            report.WriteLine("edge[color =black]");
            nodo actual = inicio;
            reporte(actual);

            report.WriteLine("}");
            report.Close();
            // system("dot -Tpng RepJugadores.dot -o RepJugadores.png");
            //system("RepJugadores.png &");

        }
        public nodo reporte(nodo actual)
        {
            if (actual.izq != null)
            {
                report.WriteLine(actual.estado + "->{" + (actual.izq.estado) + "}[label=" + actual.lexema + "]");
                reporte(actual.izq);
            }
            if (actual.der != null)
            {
                report.WriteLine(actual.estado + "->{" + (actual.der.estado) + "}[label=" + actual.lexema + "]");
                reporte(actual.der);
            }
            return actual;

        }
        public nodo buscar(int estado)
        {
            return buscar(inicio, estado);
        }
        public bool encontrado = false;
        nodo buscar(nodo  actual, int estado)
        {
            if(encontrado != true)
            {
                if (actual == null)
                {
                    return null;
                }
                else
                {
                    if (actual.estado == estado)
                    {
                        encontrado = true;
                        return actual;
                    }
                    else
                    {
                        
                       buscar(actual.izq, estado);
                       buscar(actual.der, estado);
                    }
                    return actual;
                }
            }return actual;
            
        }
    }
}
