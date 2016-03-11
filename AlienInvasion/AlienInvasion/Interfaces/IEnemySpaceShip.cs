namespace AlienInvasion.Interfaces
{
    public interface IEnemySpaceShip : ISpaceShip
    {
        bool IsShootingBullet { get; set; }
        void EnemyShipInitialize();
    }
}