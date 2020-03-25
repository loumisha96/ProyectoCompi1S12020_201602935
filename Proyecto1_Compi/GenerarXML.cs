using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Proyecto1_Compi
{
    class GenerarXML
    {
        
        List<tokens> tokens;
        tokens actual;
        int i;
        public GenerarXML( List<tokens> tokens)
        {
            this.tokens = tokens;
        }
        public void SalidaTokens()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            XmlElement element1 = doc.CreateElement(string.Empty, "ListaTokens", string.Empty);
            doc.AppendChild(element1);
            

            foreach(tokens tok in tokens)
            {
                if(tok.codigo != 20)
                {
                    XmlElement element2 = doc.CreateElement(string.Empty, "Token", string.Empty);
                    element1.AppendChild(element2);
                    XmlElement element3 = doc.CreateElement(string.Empty, "Nombre", string.Empty);
                    XmlText text1 = doc.CreateTextNode(tok.lexema);
                    element3.AppendChild(text1);
                    element2.AppendChild(element3);
                    XmlElement element4 = doc.CreateElement(string.Empty, "Tipo", string.Empty);
                    XmlText text2 = doc.CreateTextNode(tok.tipo);
                    element4.AppendChild(text2);
                    element2.AppendChild(element4);
                }
                
            }
            doc.Save("SalidaTokens.xml");
        

        }
        public void SalidaDeErrores()
        {

            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);
            XmlElement element1 = doc.CreateElement(string.Empty, "ListaTokens", string.Empty);
            doc.AppendChild(element1);


            foreach (tokens tok in tokens)
            {
                if (tok.codigo == 20)
                {
                    XmlElement element2 = doc.CreateElement(string.Empty, "Token", string.Empty);
                    element1.AppendChild(element2);
                    XmlElement element3 = doc.CreateElement(string.Empty, "Nombre", string.Empty);
                    XmlText text1 = doc.CreateTextNode(tok.lexema);
                    element3.AppendChild(text1);
                    element2.AppendChild(element3);
                    XmlElement element4 = doc.CreateElement(string.Empty, "Tipo", string.Empty);
                    XmlText text2 = doc.CreateTextNode(tok.tipo);
                    element4.AppendChild(text2);
                    element2.AppendChild(element4);
                }

            }
            doc.Save("SalidaTokens.xml");
        }
    }
}
