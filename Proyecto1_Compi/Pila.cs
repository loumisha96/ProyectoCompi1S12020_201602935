using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Compi
{
    public class nodoP
    {
        public tokens actual;
        public nodoP sig;
        public nodoP(tokens actual)
        {
            this.actual = actual;

        }
    }
    public class Pila
    {
        public nodoP primero;
        public nodoP ultimo;
        public Pila()
        {
            this.primero = null;
            this.ultimo = null;
        }

        public void Push(tokens actual)
        {
            nodoP nuevo = new nodoP(actual);
            nodoP aux = ultimo;
            if(ultimo == null)
            {
                primero = nuevo;
                ultimo = nuevo;
                ultimo.sig = primero;
                primero.sig = null;
            }
            else
            {
                primero.sig = nuevo;
                primero = nuevo;
                
                
            }
        }
        public Boolean Pop()
        {
            
            nodoP aux = ultimo;
            if (aux == null)
                return false;
            else
            {
                while (aux != null && aux.sig!=primero)
                {
                    aux = aux.sig;
                }
                if (aux == null)
                {
                    primero = null;
                    ultimo = null;
                    return false;
                }
                    
                else
                {
                    aux.sig = null;
                    primero = aux;
                    return true;
                }
                
            }
            
        }
        public nodoP peek()
        {
            return primero;
        }
        public void  print()
        {
            nodoP aux = ultimo;
            while(aux.sig !=null)
            {
                Console.WriteLine(aux.actual.lexema + " | " + aux.actual.codigo);
                aux = aux.sig;
            }
            Console.WriteLine(aux.actual.lexema + " | " + aux.actual.codigo);
            Console.WriteLine("***************FIN**************");
        }
    }
}
