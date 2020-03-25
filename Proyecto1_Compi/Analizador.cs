using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Compi
{
    class Analizador
    {
         public List<tokens> Token = new List<tokens>();
        
        String texto;
        public Analizador(string texto)
        {
            this.texto = texto;
        }
        public void Analizar()
        {
            char[] caracteres = texto.ToCharArray();
            char caracter;
            String cadena = "";
            String error = "";
            int ascii;
            int estado = 0;
            for (int i = 0; i < caracteres.Length; i++)
            {
                caracter = caracteres[i];
                ascii = (int)caracter;
                switch (estado)
                {
                    case 0:
                        if (ascii == 47)
                        {//slash
                            cadena += (char)ascii;
                            estado = 1;
                        }
                        else if (ascii == 60)//<
                        {
                            cadena += (char)ascii;
                            estado = 8;
                        }
                        else if (ascii >= 65 && ascii <= 90 || (ascii >= 97 && ascii <= 122))
                        {
                            cadena += (char)ascii;
                            estado = 12;
                        }
                        else if (ascii == 124)//|
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Operacion", 8);
                            
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii == 126)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Virgulilla", 22);
                            cadena = "";
                            Token.Add(token);
                        }
                        else if (ascii == 95)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "guion bajo", 23);
                            cadena = "";
                            Token.Add(token);
                        }
                        else if (ascii == 45)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Guión", 27);
                            cadena = "";
                            Token.Add(token);
                        }
                        else if (ascii == 62)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Mayor que", 26);
                            cadena = "";
                            Token.Add(token);
                        }
                        else if (ascii == 37)
                        {
                            cadena += (char)ascii;
                            estado = 19;
                        }
                        else if (ascii == 44)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Coma", 24);
                            cadena = "";
                            Token.Add(token);
                        }
                        else if (ascii == 46)//.
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Operacion", 9);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii == 63)//?
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Operacion", 10);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii == 43)//+
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Operacion", 11);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii == 42)//*
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Operacion", 12);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii == 91)//[
                        {
                            cadena += (char)ascii;
                            estado = 13;
                        }
                        else if (ascii == 123)//{
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Llave abierta", 14);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii == 125)//}
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Llave cerrada", 15);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii == 59)//
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Punto y coma", 16);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii == 58)//
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Dos puntos", 17);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else if (ascii >= 48 && ascii <= 57)//
                        {
                            cadena += (char)ascii;
                            estado = 17;
                        }
                        else if (ascii == 34)//
                        {
                            cadena += (char)ascii;
                            estado = 18;
                        }
                        else if (ascii != 10 && ascii != 32 && ascii != 13)
                        {
                            cadena += (char)ascii;
                            tokens tk = new tokens(cadena, "Error",20);
                            Token.Add(tk);
                        }
                        break;
                    case 1:
                        if (ascii == 47)//
                        {
                            cadena += (char)ascii;
                            estado = 7;
                        }
                        break;
                    case 7:
                        if (ascii == 10)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Comentario", 5);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else
                            cadena += (char)ascii;
                        break;
                    case 8:
                        if (ascii == 33)//
                        {
                            cadena += (char)ascii;
                            estado = 9;
                        }
                        break;
                    case 9:
                        if (ascii == 33)
                        {
                            cadena += (char)ascii;
                            estado = 10;
                        }
                        else
                            cadena += (char)ascii;
                        break;
                    case 10:
                        if (ascii == 62)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Comentario Multilínea", 6);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else
                        {
                            error += (char)ascii;
                            tokens token = new tokens(error, "Error", 20);
                            error = "";
                            cadena = "";
                            Token.Add(token);
                        }
                        break;
                    case 12:
                        if ((ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || (ascii >= 48 && ascii <= 57))
                        {
                            cadena += (char)ascii;
                        }
                        else 
                        {
                            if (cadena == "CONJ")
                            {
                                tokens token = new tokens(cadena, "Reservada", 21);
                                cadena = "";
                                estado = 0;
                                Token.Add(token);
                                i--;
                            }
                            else
                            {
                                tokens token = new tokens(cadena, "Id", 7);
                                cadena = "";
                                estado = 0;
                                Token.Add(token);
                                i--;
                            }
                        }
                        
                        break;
                    case 13:
                        if (ascii == 58)
                        {
                            cadena += (char)ascii;
                            estado = 14;
                        }
                        /*else
                        {
                            error += (char)ascii;
                            tokens token = new tokens(error, "Error", 20);
                            error = "";
                            cadena = "";
                        }*/
                        break;
                    case 14:
                        if (ascii == 58)
                        {
                            cadena += (char)ascii;
                            estado = 15;
                        }
                        else
                        {
                            cadena += (char)ascii;
                        }
                        break;
                    case 15:
                        if (ascii == 93)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Cualquiera", 13);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        break;
                    case 17:
                        if (ascii >= 48 && ascii <= 57)
                            cadena += (char)ascii;
                        else if (ascii == 32 || ascii == 58 || ascii == 59 || ascii == 123 || ascii == 125 || ascii == 126)
                        {
                            tokens token = new tokens(cadena, "Digito", 18);
                            
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                            
                            i--;
                        }
                        break;
                    case 18:
                        if (ascii == 34)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Cadena", 19);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        else
                            cadena += (char)ascii;
                        break;

                    case 19:
                        if (ascii ==37)
                        {
                            cadena += (char)ascii;
                            tokens token = new tokens(cadena, "Porcentaje", 25);
                            cadena = "";
                            estado = 0;
                            Token.Add(token);
                        }
                        
                        break;
                }
            }
            foreach (tokens tok in Token)
            {
                Console.WriteLine(tok.codigo + "|" + tok.lexema + "|" + tok.tipo + "|" );


            }

        }
    }
    
}
