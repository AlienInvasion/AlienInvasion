using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienInvasion.Interfaces
{
    public interface ISpaceObject
    {
        Texture2D Texture { get; set; }
        Texture2D DefaultTexture { get; set; }
        int Rows { get; set; }
        int Columns { get; set; }
        int X { get; set; }
        int Y { get; set; }
        bool IsDestroyed { get; set; }
        bool IsActive { get; set; }
        int CurrentFrame { get; set; }
        int TotalFrames { get; set; }
        int WindowClientBoundsRight { get; set; }
        int WindowClientBoundsHeight { get; set; }

        void Update();

        void Draw(SpriteBatch spriteBatch, Vector2 location);

    }
}