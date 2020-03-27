using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1_Compi
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ABRIRToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        string read;
        private void AbrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.ShowDialog();
            StreamReader leer = new StreamReader(open.FileName);
            area.Text = leer.ReadToEnd();
            read = open.FileName;
            leer.Close();
        }
        Analizador analizador;
        private void AnalizadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
             analizador =  new Analizador(area.Text);
             analizador.Analizar();
             Parser parser = new Parser();
             parser.Parsear(analizador.Token);
        }
        GenerarXML xml;
        private void SalidaDeTokensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml = new GenerarXML(analizador.Token);
            xml.SalidaTokens();
        }

        private void SalidaDeErroresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xml.SalidaDeErrores();
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(read != null)
                File.WriteAllText(read, area.Text);
            
        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                Title = " Seleccione Destino",
                Filter = "Archivo de Prueba |*.er",
                AddExtension = true,

            };
            save.ShowDialog();
            StreamWriter writer = new StreamWriter(save.FileName);
            writer.Write(area.Text);
            writer.Close();
        }
    }
}
