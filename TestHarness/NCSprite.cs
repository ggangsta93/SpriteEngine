using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness
{
    class NCSprite
    {
        //Atributos

        //Posicion del sprite
        private int posX;
        private int posY;

        //Dimensiones del sprite
        private int ancho;               //Solo dimensiones de un cuadro de animacion
        private int alto;

        //Imagen con los cuadros
        private Bitmap imagen;          //Imagen que contiene los cuadros de animacion

        //Informacion de animacion
        private int cuadros;            //Cantidad de cuadros en las animaciones
        private int cuadroActual;       //Animacion actual que se está mostrando "Columna"
        private int animaciones;        //Cantidad de animaciones que tiene el sprite
        private int animacionActual;    //Animacion actua que se muestra "Filas"

        private bool activo;            //Indica si el sprite hace el ciclo de animacion
        private bool visible;           //Indica si el sprite se dibuja

        //Imagen para dibujo
        private Bitmap canvas;          //Bitmap donde se va dibujar
        private Bitmap recorte;         //Bitmap del recorte del fondo

        //Constructor
        public NCSprite(int pX, int pY, int pAncho, int pAlto, string pImagen,
                        int pCuadros, int pAnimaciones, bool pActivo, bool pVisible)
        {
            //Asignamos a los atributos
            posX = pX;
            posY = pY;
            ancho = pAncho;
            alto = pAlto;
            cuadros = pCuadros;
            animaciones = pAnimaciones;
            activo = pActivo;
            visible = pVisible;

            //Inicializamos los que no se pasam al constructor
            animacionActual = 0;
            cuadroActual = 0;

            //Nuevo
            //Cargamos la imagen del bitmap con los cuadros de animacion
            imagen = new Bitmap(pImagen);
        }

        //Creamos las propiedades
        public int X { get => posX; set => posX = value; }
        public int Y { get => posY; set => posY = value; }

        public int Ancho { get => ancho; }
        public int Alto { get => alto; }

        public int CuadroActual { get => cuadroActual; set => cuadroActual = value; }
        public int Animaciones { get => animaciones; }
        public int AnimacionActual { get => animacionActual; set => animacionActual = value; }

        public bool Activo { get => activo; set => activo = value; }
        public bool Visible { get => visible; set => visible = value; }

        public void ColocaCanvas(Bitmap pCanvas)
        {
            canvas = pCanvas;
        }

        public void ColocaImagen(Bitmap pImagen)
        {
            imagen = pImagen;
        }

        public void DibujarSprite()
        {
            //Aquí guardamos el color obtenido de imagen
            Color colorImagen = new Color();

            //Calculamos la posicion desde donde copiamos hasta donde
            int x = posX;
            int y = posY;

            int inicioX = cuadroActual * ancho;
            int inicioY = animacionActual * alto;
            int finalX = inicioX + ancho;
            int finalY = inicioY + alto;

            int xRecorrido = 0;
            int yRecorrido = 0;

            //Recorremos la el sector a copiar de la imagen
            for (xRecorrido = inicioX; xRecorrido < finalX; xRecorrido++, x++)
            {
                for (yRecorrido = inicioY, y=posY; yRecorrido < finalY; yRecorrido++, y++)
                {
                    //Obtenemos el color del pixel en la pimagen
                    colorImagen = imagen.GetPixel(xRecorrido, yRecorrido);

                    //Lo colocamos en el canvas
                    canvas.SetPixel(x, y, colorImagen);
                }
            }  
        }

        public string Version
        {
            get => "1.0.0.2";
        }
    }
}
