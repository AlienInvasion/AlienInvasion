using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AlienInvasion.Interfaces;

namespace AlienInvasion
{
    public abstract class SpaceObject : ISpaceObject
    {
        //new
        private int pointToScore;

        public int PointToScore
        {
            get { return pointToScore; }
            set { pointToScore = value; }
        }


        private int currentFrame;
        private int totalFrames;
        private int x;
        private int y;
        private bool isDestroyed;
        private bool isActive = false;


        private int windowClientBoundsRight;
        private int windowClientBoundsHeight;

        protected SpaceObject(Texture2D texture, Texture2D defaultTexture, int rows, int columns, int windowClientBoundsRight, int windowClientBoundsHeight)
        {
            this.Texture = texture;
            this.DefaultTexture = defaultTexture;
            this.Rows = rows;
            this.Columns = columns;
            this.currentFrame = 0;
            this.totalFrames = this.Rows * this.Columns;
            this.WindowClientBoundsRight = windowClientBoundsRight;
            this.WindowClientBoundsHeight = windowClientBoundsHeight;
        }

        public Texture2D Texture { get; set; }
        public Texture2D DefaultTexture { get; set; }

        public int Rows { get; set; }
        public int Columns { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public bool IsDestroyed { get; set; }

        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }
        public int CurrentFrame
        {
            get { return this.currentFrame; }
            set { this.currentFrame = value; }
        }

        public int TotalFrames
        {
            get { return totalFrames; }
            set { totalFrames = value; }
        }

        public int WindowClientBoundsRight
        {
            get { return windowClientBoundsRight; }
            set { windowClientBoundsRight = value; }
        }

        public int WindowClientBoundsHeight
        {
            get { return windowClientBoundsHeight; }
            set { windowClientBoundsHeight = value; }
        }

        public abstract void Update();


        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}