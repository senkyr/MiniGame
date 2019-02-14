using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZakladniCtverecek
{
    class Ctverecek
    {
        public int Velikost { get; private set; }
        public int Rychlost { get; private set; }

        public byte RychlostZmenyBarvy { get; private set; } = 2;

        public Color Barva;
        public Vector2 Pozice;

        private Texture2D textura;

        public Keys SmerNahoru { get; private set; }
        public Keys SmerDolu { get; private set; }
        public Keys SmerDoleva { get; private set; }
        public Keys SmerDoprava { get; private set; }

        public Keys ZmenaCerveneBarvy { get; private set; }
        public Keys ZmenaZeleneBarvy { get; private set; }
        public Keys ZmenaModreBarvy { get; private set; }

        public Ctverecek(int velikost, int rychlost, Color barva, float poziceX, float poziceY, GraphicsDevice zobrazovaciZarizeni, Keys nahoru, Keys dolu, Keys doleva, Keys doprava, Keys cervena, Keys zelena, Keys modra)
        {
            Velikost = velikost;
            Rychlost = rychlost;

            Barva = barva;

            Pozice.X = poziceX;
            Pozice.Y = poziceY;

            #region Priprava textury
            textura = new Texture2D(zobrazovaciZarizeni, Velikost, Velikost);

            Color[] barevnaData = new Color[Velikost * Velikost];

            for (int i = 0; i < barevnaData.Length; i++)
                barevnaData[i] = Color.White;

            textura.SetData(barevnaData);
            #endregion

            SmerDoleva = doleva;
            SmerDoprava = doprava;
            SmerNahoru = nahoru;
            SmerDolu = dolu;

            ZmenaCerveneBarvy = cervena;
            ZmenaZeleneBarvy = zelena;
            ZmenaModreBarvy = modra;
        }

        public void aktualizovat(int sirkaOkna, int vyskaOkna)
        {
            if (Keyboard.GetState().IsKeyDown(SmerNahoru))
                Pozice.Y -= Rychlost;
            if (Keyboard.GetState().IsKeyDown(SmerDolu))
                Pozice.Y += Rychlost;
            if (Keyboard.GetState().IsKeyDown(SmerDoleva))
                Pozice.X -= Rychlost;
            if (Keyboard.GetState().IsKeyDown(SmerDoprava))
                Pozice.X += Rychlost;

            if (Pozice.X < 0)
                Pozice.X = 0;
            if (Pozice.X > sirkaOkna - Velikost)
                Pozice.X = sirkaOkna - Velikost;
            if (Pozice.Y < 0)
                Pozice.Y = 0;
            if (Pozice.Y > vyskaOkna - Velikost)
                Pozice.Y = vyskaOkna - Velikost;

            int cervenaPred = Barva.R;
            int zelenaPred = Barva.G;
            int modraPred = Barva.B;

            if (Keyboard.GetState().IsKeyDown(ZmenaCerveneBarvy))
                Barva.R += RychlostZmenyBarvy;
            if (Keyboard.GetState().IsKeyDown(ZmenaZeleneBarvy))
                Barva.G += RychlostZmenyBarvy;
            if (Keyboard.GetState().IsKeyDown(ZmenaModreBarvy))
                Barva.B += RychlostZmenyBarvy;

            if (Barva.R < cervenaPred)
                Barva.R = 255;
            if (Barva.G < zelenaPred)
                Barva.G = 255;
            if (Barva.B < modraPred)
                Barva.B = 255;
        }

        public void vykreslit(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, Pozice, Barva);
        }
    }
}
