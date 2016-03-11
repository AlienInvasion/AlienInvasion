using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AlienInvasion.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienInvasion
{
    public class EnemyBullet : IEnemyBullet
    {
        private Texture2D texture;
        private Vector2 posituon;
        private Vector2 velocity;
        private Vector2 origin;
        private int x;
        private int y;
        private bool isVisible;

        public EnemyBullet(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = true;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 Posituon
        {
            get { return posituon; }
            set { posituon = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, posituon, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
            X = (int)Posituon.X;
            Y = (int)Posituon.Y;
        }
    }
}
