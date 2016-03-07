using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AlienInvasion
{
    public class EnemyManager
    {
        int windowClientBoundsRight;
        int windowClientBoundsHeight;
        Random randomNumber = new Random();

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

        public EnemyManager(int windowClientBoundsRight, int windowClientBoundsHeight)
        {
            WindowClientBoundsRight = windowClientBoundsRight;
            WindowClientBoundsHeight = windowClientBoundsHeight;
        }

        public void Update(IList<EnemySpaceShip> enemySpaceShips)
        {
            ReturnEnemySpaceShipInFieldIfGoOutOfBorders(enemySpaceShips);
            RandomizeEnemySpaceShips(enemySpaceShips);
        }

        internal void ReturnEnemySpaceShipInFieldIfGoOutOfBorders(IList<EnemySpaceShip> enemySpaceShips)
        {
            foreach (var enemyShip in enemySpaceShips)
            {
                if (enemyShip.Y > WindowClientBoundsHeight + 50)
                {
                    enemyShip.Y = randomNumber.Next(-200, -75);
                    enemyShip.X = randomNumber.Next(75, WindowClientBoundsRight - 75);
                }
            }
        }

        internal void RandomizeEnemySpaceShips(IList<EnemySpaceShip> enemySpaceShips)
        {
            foreach (var enemyShip in enemySpaceShips)
            {
                if (enemyShip.Y >= -70 && enemyShip.Y <= -68)
                {
                    //enemyShip.Y = randomNumber.Next(-200, -65);
                    enemyShip.X = randomNumber.Next(55, WindowClientBoundsRight - 55);
                }
            }
        }


    }
}
