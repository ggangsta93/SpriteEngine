using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness
{
    class NCEngine
    {
        //Bitmap donde se dibuja todo
        private Bitmap lienzo;

        private int ancho;
        private int alto;

        //Lista donde se guardan los sprites
        private List<NCSprite> lstSprites = new List<NCSprite>();

        public NCEngine(int pAncho, int pAlto)
        {
            //Asignamos los valores
            ancho= pAncho;
            alto = pAlto;

            //Creamos el canvas
            lienzo = new Bitmap(ancho, alto);
            InitPruebas();
        }

        //Propiedades
        public Bitmap Canvas { get => lienzo; }

        //Metodo temporal para pruebas y experimetacion
        private void InitPruebas()
        {

            for (int x = 0; x < lienzo.Width; x++)
                for (int y = 0; y < lienzo.Height; y++)
                    lienzo.SetPixel(x, y, Color.DarkGreen);
        }

        public void AdicionaSprite(NCSprite pSprite)
        {
            if(pSprite != null)
            {
                pSprite.ColocaCanvas(lienzo);
                lstSprites.Add(pSprite);
            }
        }

        public void CicloJuego()
        {
            foreach (NCSprite sprite in lstSprites) {
                sprite.DibujarSprite();
            }
        }

    }
}
