using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestHarness
{
    public partial class Form1 : Form
    {
        private Bitmap resultante;    //Aquí se muestra 
        private int anchoVentana, altoVentana;

        private NCEngine motor;

        //Variables para el double buffer y evitar el flicker
        private Bitmap dBufferBMP;
        private Graphics dBufferDC;     //Es una superficie de dibujo del GDI+

        //Variables para el ejemplo
        private int cx = 100, cy = 0;
        private NCSprite uno = new NCSprite(100, 100, 80, 60, "Sprite0.png", 0, 0, true, true);
        private NCSprite dos = new NCSprite(250, 200, 80, 60, "Sprite0.png", 0, 0, true, true);
        public Form1()
        {
            InitializeComponent();

            //Creamos un bitmap y obtenemos su superficie de dibujo
            dBufferBMP = new Bitmap(this.Width, this.Height);
            dBufferDC = Graphics.FromImage(dBufferBMP);

            //Creamos el bitmap resultante del cuadro
            resultante = new Bitmap(800, 600);

            //Colocamos los valores para el dibujo con scrolls
            anchoVentana = 800;
            altoVentana = 600;

            //Informacion de la version de sprite
            this.Text += " " + uno.Version.ToString();

            //Creamos la instancia del motor
            motor = new NCEngine(anchoVentana, altoVentana);

            motor.AdicionaSprite(uno);
            motor.AdicionaSprite(dos);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simulaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (simulaToolStripMenuItem.Checked == true)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Por XAVI","ggangsta93.dll@gmail.com");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /////////
            //Actualizar valores
            cx++;
            cy++;
            ////////
            //Codigo de dibujo

            uno.X = cx;
            dos.Y = cy;
            motor.CicloJuego();

            resultante = motor.Canvas;

            ////////
            //Codigo de copia al buffer
            Graphics clientDC = this.CreateGraphics();

            //Verificamos que se tenga un bitmap instanciado
            if (resultante != null)
            {
                //Calculamos el scroll
                AutoScrollMinSize = new Size(anchoVentana, altoVentana);

                //Copiamos el bitmap resultante al buffer
                clientDC.DrawImage(resultante, 
                                    new Rectangle(this.AutoScrollPosition.X,
                                                  this.AutoScrollPosition.Y + 30,
                                                  anchoVentana, altoVentana));

                //Mandamos a dibujar al buffer
                clientDC.DrawImage(dBufferBMP,0,0);
            }
        }

        private void procesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////
            //////Procesamos y colocamos los pixeles necesarios
            //////////////////////////////////////////////////////////////////
            ///

            uno.CuadroActual = 2;
            uno.AnimacionActual = 2;

            motor.CicloJuego();
            resultante = motor.Canvas;

            //Redibujar la ventana
            this.Invalidate();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Verificamos que se tenga un bitmap instanciado
            if (resultante != null)
            {
                //Obtenemos el objeto graphics
                Graphics g = e.Graphics;

                //Calculamos el scroll
                AutoScrollMinSize = new Size(anchoVentana, altoVentana);

                //Copiamos del bitmap a la ventana
                g.DrawImage(resultante, new Rectangle(this.AutoScrollPosition.X,
                                                        this.AutoScrollPosition.Y+30,
                                                        anchoVentana, altoVentana));
                //Liberamos el recurso
                g.Dispose();
            }

        }
    }
}
