using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlienInvasion.Interfaces
{
    public interface IEnemyBullet
    {
        int X { get; set; }

        int Y { get; set; }

        Texture2D Texture { get; set; }

        Vector2 Posituon { get; set; }

        Vector2 Velocity { get; set; }

        Vector2 Origin { get; set; }

        bool IsVisible { get; set; }

        void Draw(SpriteBatch spriteBatch);
    }
}