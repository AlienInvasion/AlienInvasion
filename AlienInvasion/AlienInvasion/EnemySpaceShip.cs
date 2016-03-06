using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AlienInvasion
{
    public class EnemySpaceShip : SpaceObject
    {

        Random randomNumber = new Random();

        public EnemySpaceShip(Texture2D texture, Texture2D defaultTexture, int rows, int columns, int windowClientBoundsRight, int windowClientBoundsHeight)
            : base(texture, defaultTexture, rows, columns, windowClientBoundsRight, windowClientBoundsHeight)
        {
        }

        public void EnemyShipInitialize()
        {
            this.Y = -100;
            int randomPosition = randomNumber.Next(0, WindowClientBoundsRight - 100);
            this.X = randomPosition;
        }

        public override void Update()
        {
            this.CurrentFrame++;
            if (CurrentFrame == TotalFrames)
                CurrentFrame = 0;

            this.Y++;
            //ReturnEnemySpaceShipInFieldIfGoOutOfBorders();
            if (this.IsDestroyed == true && this.CurrentFrame > 14)
            {
                this.IsDestroyed = false;
                this.Y = randomNumber.Next(-200, -65);
                this.X = randomNumber.Next(75, WindowClientBoundsRight - 75);
                this.Texture = this.DefaultTexture;

            }
        }

        
    }
}