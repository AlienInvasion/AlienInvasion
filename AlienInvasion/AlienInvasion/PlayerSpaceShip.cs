using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlienInvasion
{
    public class PlayerSpaceShip : SpaceObject
    {

        //private int currentFrame;
        //private int totalFrames;
        //private int x;
        //private int y;

        public PlayerSpaceShip(Texture2D texture, Texture2D defaultTexture, int rows, int columns, int windowClientBoundsRight, int windowClientBoundsHeight)
            : base(texture, defaultTexture, rows, columns, windowClientBoundsRight, windowClientBoundsHeight)
        {
        }

        public override void Update()
        {
            this.CurrentFrame++;
            if (CurrentFrame == TotalFrames)
                CurrentFrame = 0;
            KeepPlayerSpaceShipInBorders();
        }

        public void Controls()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                X += 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                X -= 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Y -= 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Y += 2;
            }
        }

        private void KeepPlayerSpaceShipInBorders()
        {
            if (this.X < 0)
            {
                this.X = 0;
            }

            if (this.X > WindowClientBoundsRight - 60)
            {
                this.X = WindowClientBoundsRight - 60;
            }

            if (this.Y < 0)
            {
                this.Y = 0;
            }

            if (this.Y > WindowClientBoundsHeight - 60)
            {
                this.Y = WindowClientBoundsHeight - 60;
            }
        }
    }
}