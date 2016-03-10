using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AlienInvasion.Interfaces;

namespace AlienInvasion
{
    /// <summary>
    /// This is the main engine of game.
    /// </summary>
    public class Engine : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EnemyManager enemyManager;

        GameState CurrentGameState = GameState.MainMenu;
        private string gameDifficulty = String.Empty;
        private Button btnPlayEasy;
        private Button btnPlayMedium;
        private Button btnPlayHard;
        private Button btnBack;
        public static Boolean exitgame = false;

        private Texture2D background;
        private Texture2D earth;
        private SpriteFont font;
        private int borderRight = 800;
        private int borderDown = 480;
        private int score = 0;
        private bool isDifficultyLevelSet = false;

        private PlayerSpaceShip playerSpaceShip;
        readonly IList<IEnemySpaceShip> enemySpaceShips = new List<IEnemySpaceShip>();
        readonly List<IBullet> bullets = new List<IBullet>();
        private KeyboardState pastKey;
        private Random randomNumber = new Random();

        int playerSpaceShipCollisionRectOffset = 10;
        int enemySpaceShipCollisionRectOffset = 10;
        bool isRestarting = false;

        Song musicBackgrownd;
        SoundEffect shootSoundEffect1;
        SoundEffect boomSoundEffect1;
        SoundEffect boomSoundEffect2;
        SoundEffect boomSoundEffect3;

        MouseState mouse = Mouse.GetState();

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = this.borderRight;
            graphics.PreferredBackBufferHeight = this.borderDown;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            this.btnPlayEasy = new Button(this.Content.Load<Texture2D>("Button_easy"), this.graphics.GraphicsDevice);
            this.btnPlayEasy.setPosition(new Vector2(35, 130));
            this.btnPlayMedium = new Button(this.Content.Load<Texture2D>("Button_medium"), this.graphics.GraphicsDevice);
            this.btnPlayMedium.setPosition(new Vector2(35, 180));
            this.btnPlayHard = new Button(this.Content.Load<Texture2D>("Button_hard"), this.graphics.GraphicsDevice);
            this.btnPlayHard.setPosition(new Vector2(35, 230));
            this.btnBack = new Button(this.Content.Load<Texture2D>("Button_back"), this.graphics.GraphicsDevice);
            this.btnBack.setPosition(new Vector2(685, 430));

            enemyManager = new EnemyManager(Window.ClientBounds.Right, Window.ClientBounds.Height);
            background = this.Content.Load<Texture2D>("stars");
            earth = this.Content.Load<Texture2D>("earth");
            font = this.Content.Load<SpriteFont>("MainFont");

            Texture2D texturePlayerSpaceShip = this.Content.Load<Texture2D>("PlayerSpriteShipBlinkingLamp");
            Texture2D textureEnemySpaceShip = this.Content.Load<Texture2D>("EnemySpriteShipClear");
            Texture2D textureEnemySpaceShipExtraFast = this.Content.Load<Texture2D>("EnemySpriteShipGreenClear");

            this.playerSpaceShip = new PlayerSpaceShip(texturePlayerSpaceShip, texturePlayerSpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            this.playerSpaceShip.X = 400;
            this.playerSpaceShip.Y = 360;
            IEnemySpaceShip enemySpaceShip1 = new EnemySpaceShipStandart(textureEnemySpaceShip, textureEnemySpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip2 = new EnemySpaceShipStandart(textureEnemySpaceShip, textureEnemySpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip3 = new EnemySpaceShipStandart(textureEnemySpaceShip, textureEnemySpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip4 = new EnemySpaceShipExtraFast(textureEnemySpaceShipExtraFast, textureEnemySpaceShipExtraFast, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip5 = new EnemySpaceShipStandart(textureEnemySpaceShip, textureEnemySpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip6 = new EnemySpaceShipExtraFast(textureEnemySpaceShipExtraFast, textureEnemySpaceShipExtraFast, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip7 = new EnemySpaceShipExtraFast(textureEnemySpaceShipExtraFast, textureEnemySpaceShipExtraFast, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip8 = new EnemySpaceShipExtraFast(textureEnemySpaceShipExtraFast, textureEnemySpaceShipExtraFast, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip9 = new EnemySpaceShipStandart(textureEnemySpaceShip, textureEnemySpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            IEnemySpaceShip enemySpaceShip10 = new EnemySpaceShipExtraFast(textureEnemySpaceShipExtraFast, textureEnemySpaceShipExtraFast, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);

            enemySpaceShips.Add(enemySpaceShip1);
            enemySpaceShips.Add(enemySpaceShip2);
            enemySpaceShips.Add(enemySpaceShip3);
            enemySpaceShips.Add(enemySpaceShip4);
            enemySpaceShips.Add(enemySpaceShip5);
            enemySpaceShips.Add(enemySpaceShip6);
            enemySpaceShips.Add(enemySpaceShip7);

            for (int i = 0; i < enemySpaceShips.Count; i++)
            {

                enemySpaceShips[i].EnemyShipInitialize();
                int randomPosition = randomNumber.Next(0, Window.ClientBounds.Right - 100);
                enemySpaceShips[i].X = randomPosition;
                randomPosition = randomNumber.Next(-175, -75);
                enemySpaceShips[i].Y = randomPosition;
            }

            //music
            this.musicBackgrownd = Content.Load<Song>("musicBackgrownd");
            MediaPlayer.Play(musicBackgrownd);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            this.shootSoundEffect1 = Content.Load<SoundEffect>("shoot2");
            this.boomSoundEffect1 = Content.Load<SoundEffect>("boom1");
            this.boomSoundEffect2 = Content.Load<SoundEffect>("boom2");
            this.boomSoundEffect3 = Content.Load<SoundEffect>("boom3");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            CheckForCurrentGameState();
            MusicVolumeControl();
            setEnemyShipsDependOnDifficulty();
            playerSpaceShip.Update();
            playerSpaceShip.Controls();
            enemyManager.Update(enemySpaceShips);
            UpdateEnemyShips();
            CheckForCollisionBetweenPlayerAndEnemyShips();
            BulletsShooting();
            UpdateBullets();
            CheckForCollisionBetweenBulletsAndEnemyShips();

            base.Update(gameTime);
        }

        private void CheckForCurrentGameState()
        {
            mouse = Mouse.GetState();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (this.btnPlayEasy.isClicked == true)
                    {
                        this.CurrentGameState = GameState.PlayingEasy;
                        isRestarting = false;
                    }
                    this.btnPlayEasy.Update(mouse);

                    if (this.btnPlayMedium.isClicked == true)
                    {
                        this.CurrentGameState = GameState.PlayingMedium;
                        isRestarting = false;
                    }
                    this.btnPlayMedium.Update(mouse);

                    if (this.btnPlayHard.isClicked == true)
                    {
                        this.CurrentGameState = GameState.PlayingHard;
                        isRestarting = false;
                    }
                    this.btnPlayHard.Update(mouse);
                    break;

                case GameState.PlayingEasy:
                    gameDifficulty = "easy";
                    if (this.btnBack.isClicked == true)
                    {
                        if (!isRestarting)
                        {
                            RestartGame();
                            isRestarting = true;
                        }
                    }
                    this.btnBack.Update(mouse);
                    break;

                case GameState.PlayingMedium:
                    gameDifficulty = "medium";
                    if (this.btnBack.isClicked == true)
                    {
                        if (!isRestarting)
                        {
                            RestartGame();
                            isRestarting = true;
                        }
                    }
                    this.btnBack.Update(mouse);
                    break;

                case GameState.PlayingHard:
                    gameDifficulty = "hard";
                    if (this.btnBack.isClicked == true)
                    {
                        if (!isRestarting)
                        {
                            RestartGame();
                            isRestarting = true;
                        }
                    }
                    this.btnBack.Update(mouse);
                    break;
            }
        }

        private void RestartGame()
        {
            this.btnBack.isClicked = false;
            this.btnPlayEasy.isClicked = false;
            this.btnPlayMedium.isClicked = false;
            this.btnPlayHard.isClicked = false;

            this.CurrentGameState = GameState.MainMenu;
            isDifficultyLevelSet = false;
            gameDifficulty = string.Empty;
            score = 0;
            enemySpaceShips.Clear();
            bullets.Clear();
            ResetElapsedTime();
            MediaPlayer.Stop();
            LoadContent();

            this.musicBackgrownd = Content.Load<Song>("musicBackgrownd");
            MediaPlayer.Play(musicBackgrownd);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            this.shootSoundEffect1 = Content.Load<SoundEffect>("shoot2");
            this.boomSoundEffect1 = Content.Load<SoundEffect>("boom1");
            this.boomSoundEffect2 = Content.Load<SoundEffect>("boom2");
            this.boomSoundEffect3 = Content.Load<SoundEffect>("boom3");
        }

        private void MusicVolumeControl()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                MediaPlayer.Volume += 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                MediaPlayer.Volume -= 0.01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                MediaPlayer.Stop();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                MediaPlayer.Play(musicBackgrownd);
            }
        }

        public void UpdateBullets()
        {
            foreach (IBullet bullet in bullets)
            {
                bullet.Posituon += new Vector2(0, -5);
                if (Vector2.Distance(bullet.Posituon, new Vector2(1, 1)) > 1070)
                {
                    bullet.IsVisible = false;
                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].IsVisible)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        private void UpdateEnemyShips()
        {
            foreach (var enemyShip in enemySpaceShips)
            {
                enemyShip.Update();
            }
        }

        public void Shoot()
        {
            IBullet newBullet = new Bullet(Content.Load<Texture2D>("bullet01_1f_8p"));
            newBullet.Posituon = new Vector2(playerSpaceShip.X + 30, playerSpaceShip.Y + 10);
            newBullet.IsVisible = true;

            if (bullets.Count < 20)
            {
                shootSoundEffect1.Play();
                bullets.Add(newBullet);
            }
        }

        private void BulletsShooting()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
            {
                if (!playerSpaceShip.IsDestroyed)
                {
                    Shoot();
                }
            }

            pastKey = Keyboard.GetState();
        }

        private void CheckForCollisionBetweenPlayerAndEnemyShips()
        {
            foreach (var enemyShip in enemySpaceShips)
            {
                if (CollideShips(playerSpaceShip, enemyShip))
                {
                    playerSpaceShip.Texture = Content.Load<Texture2D>("explosion");
                    playerSpaceShip.IsDestroyed = true;
                    enemyShip.Texture = Content.Load<Texture2D>("explosion");
                    if (enemyShip.IsDestroyed == false)
                    {
                        score += enemyShip.PointToScore;
                    }
                    enemyShip.IsDestroyed = true;
                }
            }
        }

        private void CheckForCollisionBetweenBulletsAndEnemyShips()
        {
            foreach (var enemyShip in enemySpaceShips)
            {
                foreach (var bullet in bullets)
                {
                    if (CollideBullets(bullet, enemyShip))
                    {
                        if (!enemyShip.IsDestroyed)
                        {
                            enemyShip.CurrentFrame = randomNumber.Next(1, 4);
                            enemyShip.Texture = Content.Load<Texture2D>("explosion");
                        }
                        enemyShip.IsDestroyed = true;
                        bullet.Posituon += new Vector2(1000, -500);
                        score += enemyShip.PointToScore;
                    }
                }
            }
        }

        protected bool CollideShips(SpaceShip spaceShip1, IEnemySpaceShip spaceShip2)
        {
            bool result = false;
            Rectangle colilisionRectForShip1 = new Rectangle(
               (int)spaceShip1.X,
               (int)spaceShip1.Y,
               50,
               50
               );

            Rectangle colilisionRectForShip2 = new Rectangle(
                (int)spaceShip2.X,
                (int)spaceShip2.Y,
                50,
                50
                );

            result = colilisionRectForShip1.Intersects(colilisionRectForShip2);

            if (result == true)
            {
                SFXRandomExplosionPlay();
            }

            return result;
        }

        public bool CollideBullets(IBullet bullletLokal, IEnemySpaceShip enemySpaceShip)
        {
            bool result = false;

            Rectangle colilisionRectForBullets = new Rectangle(
            (int)bullletLokal.X,
            (int)bullletLokal.Y,
            20,
            15
            );

            Rectangle colilisionRectForSpaceObject = new Rectangle(
            (int)enemySpaceShip.X,
            (int)enemySpaceShip.Y,
            50,
            50
            );
            result = colilisionRectForBullets.Intersects(colilisionRectForSpaceObject);

            //If object is incaming and is over the screen he can`t be shooted
            if (enemySpaceShip.Y < -50)
            {
                return false;
            }

            if (result == true)
            {
                SFXRandomExplosionPlay();
            }

            return result;
        }

        private void setEnemyShipsDependOnDifficulty()
        {
            if (!isDifficultyLevelSet)
            {
                int numberOfEnemyShips = 0;
                switch (gameDifficulty)
                {
                    case "easy":
                        numberOfEnemyShips = 3;
                        break;

                    case "medium":
                        numberOfEnemyShips = 5;
                        break;

                    case "hard":
                        numberOfEnemyShips = 7;
                        break;

                    default:
                        return;
                }

                for (int i = 0; i < numberOfEnemyShips; i++)
                {
                    enemySpaceShips[i].IsActive = true;
                }

                isDifficultyLevelSet = true;
            }
        }

        void SFXRandomExplosionPlay()
        {
            int randomNumberSFX = randomNumber.Next(1, 4);

            switch (randomNumberSFX)
            {
                case 1:
                    boomSoundEffect1.Play();
                    break;
                case 2:
                    boomSoundEffect2.Play();
                    break;
                case 3:
                    boomSoundEffect3.Play();
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    this.spriteBatch.Draw(
                        Content.Load<Texture2D>("MainMenu"),
                        new Rectangle(0, 0, this.borderRight, this.borderDown),
                        Color.White);
                    this.btnPlayEasy.Draw(this.spriteBatch);
                    this.btnPlayMedium.Draw(this.spriteBatch);
                    this.btnPlayHard.Draw(this.spriteBatch);
                    break;

                case GameState.PlayingEasy:
                case GameState.PlayingMedium:
                case GameState.PlayingHard:


                    spriteBatch.Draw(background, new Rectangle(0, 0, borderRight, borderDown), Color.White);
                    spriteBatch.Draw(earth, new Vector2(400, 240), Color.White);
                    this.btnBack.Draw(this.spriteBatch);

                    if (playerSpaceShip.IsDestroyed)
                    {
                        spriteBatch.DrawString(font, "GAME OVER", new Vector2(Window.ClientBounds.Width / 2 - 50, Window.ClientBounds.Height / 2), Color.AntiqueWhite);
                        exitgame = true;
                    }

                    spriteBatch.DrawString(font, "Score " + score, new Vector2(30, 450), Color.AntiqueWhite);

                    Vector2 location = new Vector2(400, 240);
                    playerSpaceShip.Draw(spriteBatch, new Vector2(playerSpaceShip.X, playerSpaceShip.Y));
                    
                    foreach (var enemyShip in enemySpaceShips)
                    {
                        enemyShip.Draw(spriteBatch, new Vector2(enemyShip.X, enemyShip.Y));
                    }

                    foreach (Bullet bullet in bullets)
                    {
                        bullet.Draw(spriteBatch);
                    }
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
