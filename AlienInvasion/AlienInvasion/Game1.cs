﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlienInvasion
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EnemyManager enemyManager;

        // menu
        enum GameState
        {
            MainMenu,
            Winners,
            PlayingEasy,
            PlayingMedium,
            PlayingHard
        }
        GameState CurrentGameState = GameState.MainMenu;

        private cButton btnPlayEasy;
        private cButton btnPlayMedium;
        private cButton btnPlayHard;
        private cButton btnBack;

        private Texture2D background;
        //private Texture2D shuttle;
        private Texture2D earth;
        //private Texture2D arrow;
        private float angle = 0;

        private int borderRight = 800;
        private int borderDown = 480;

        private SpriteFont font;
        private int score = 0;

        private PlayerSpaceShip playerSpaceShip;

        private IList<EnemySpaceShip> enemySpaceShips = new List<EnemySpaceShip>();

        //Bullets
        readonly List<Bullets> bullets = new List<Bullets>();
        private KeyboardState pastKey;

        private Random randomNumber = new Random();

        //Colision detection variables
        // Point playerSpaceShipPoint = new Point(200, 200);
        // Point enemySpaceShipPoint = new Point(200, 300);
        int playerSpaceShipCollisionRectOffset = 10;
        int enemySpaceShipCollisionRectOffset = 10;

        public Game1()
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

            // menu
            graphics.PreferredBackBufferWidth = this.borderRight;
            graphics.PreferredBackBufferHeight = this.borderDown;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            this.btnPlayEasy = new cButton(Content.Load<Texture2D>("Button_easy"), this.graphics.GraphicsDevice);
            this.btnPlayEasy.setPosition(new Vector2(35, 130));
            this.btnPlayMedium = new cButton(Content.Load<Texture2D>("Button_medium"), this.graphics.GraphicsDevice);
            this.btnPlayMedium.setPosition(new Vector2(35, 180));
            this.btnPlayHard = new cButton(Content.Load<Texture2D>("Button_hard"), this.graphics.GraphicsDevice);
            this.btnPlayHard.setPosition(new Vector2(35, 230));
            this.btnBack = new cButton(Content.Load<Texture2D>("Button_back"), this.graphics.GraphicsDevice);
            this.btnBack.setPosition(new Vector2(685, 430));

            // TODO: use this.Content to load your game content here
            enemyManager = new EnemyManager(Window.ClientBounds.Right, Window.ClientBounds.Height);
            background = Content.Load<Texture2D>("stars");
            //shuttle = Content.Load<Texture2D>("shuttle");
            earth = Content.Load<Texture2D>("earth");
            //arrow = Content.Load<Texture2D>("arrow");
            font = Content.Load<SpriteFont>("MainFont");

            Texture2D texturePlayerSpaceShip = Content.Load<Texture2D>("PlayerSpriteShipBlinkingLamp");
            Texture2D textureEnemySpaceShip = Content.Load<Texture2D>("EnemySpriteShipClear");

            playerSpaceShip = new PlayerSpaceShip(texturePlayerSpaceShip, texturePlayerSpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            playerSpaceShip.X = 400;
            playerSpaceShip.Y = 360;

            EnemySpaceShip enemySpaceShip1 = new EnemySpaceShip(textureEnemySpaceShip, textureEnemySpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
            EnemySpaceShip enemySpaceShip2 = new EnemySpaceShip(textureEnemySpaceShip, textureEnemySpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);

            enemySpaceShips.Add(enemySpaceShip1);
            enemySpaceShips.Add(enemySpaceShip2);


            for (int i = 0; i < enemySpaceShips.Count; i++)
            {
                //enemySpaceShips[i] = new EnemySpaceShip(textureEnemySpaceShip, textureEnemySpaceShip, 4, 4, Window.ClientBounds.Right, Window.ClientBounds.Height);
                int randomPosition = randomNumber.Next(0, Window.ClientBounds.Right - 100);
                enemySpaceShips[i].X = randomPosition;
                randomPosition = randomNumber.Next(-175, -45);
                enemySpaceShips[i].Y = randomPosition;
            }
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // menu
            MouseState mouse = Mouse.GetState();

            string gameDifficulty = String.Empty;

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (this.btnPlayEasy.isClicked == true)
                    {
                        this.CurrentGameState = GameState.PlayingEasy;
                    }
                    this.btnPlayEasy.Update(mouse);
                    if (this.btnPlayMedium.isClicked == true)
                    {
                        this.CurrentGameState = GameState.PlayingMedium;
                    }
                    this.btnPlayMedium.Update(mouse);
                    if (this.btnPlayHard.isClicked == true)
                    {
                        this.CurrentGameState = GameState.PlayingHard;
                    }
                    this.btnPlayHard.Update(mouse);
                    break;
                case GameState.Winners:
                    break;
                case GameState.PlayingEasy:
                    gameDifficulty = "easy";
                    if (this.btnBack.isClicked == true)
                    {
                        this.CurrentGameState = GameState.MainMenu;
                    }
                    this.btnBack.Update(mouse);
                    break;
                case GameState.PlayingMedium:
                    gameDifficulty = "medium";
                    if (this.btnBack.isClicked == true)
                    {
                        this.CurrentGameState = GameState.MainMenu;
                    }
                    this.btnBack.Update(mouse);
                    break;
                case GameState.PlayingHard:
                    gameDifficulty = "hard";
                    if (this.btnBack.isClicked == true)
                    {
                        this.CurrentGameState = GameState.MainMenu;
                    }
                    this.btnBack.Update(mouse);
                    break;
            }

            // TODO: Add your update logic here
                    //angle += 0.01f;

                    //PlayerSpaceShip controls management
                    playerSpaceShip.Update();
            playerSpaceShip.Controls();
            enemyManager.Update(enemySpaceShips);


            foreach (var enemyShip in enemySpaceShips)
            {
                enemyShip.Update();
            }

            //collision between player ship and enemy1 ship
            foreach (var enemyShip in enemySpaceShips)
            {
                if (CollideShips(playerSpaceShip, enemyShip))
                {
                    playerSpaceShip.Texture = Content.Load<Texture2D>("explosion");
                    playerSpaceShip.IsDestroyed = true;
                    //enemyShip.Texture = Content.Load<Texture2D>("explosion2");
                    enemyShip.Texture = Content.Load<Texture2D>("explosion");
                    enemyShip.IsDestroyed = true;
                    score++;
                }

            }

            if (playerSpaceShip.IsDestroyed == true && playerSpaceShip.CurrentFrame > 14)
            {
                playerSpaceShip.Texture = Content.Load<Texture2D>("emptySprite");
            }

            // Bullets shooting
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
            {
                Shoot();
            }
            pastKey = Keyboard.GetState();

            UpdateBullets();

            // Bullets collide
            foreach (var enemyShip in enemySpaceShips)
            {
                foreach (var bullet in bullets)
                {
                    if (CollideBullets(bullet, enemyShip))
                    {
                        //enemyShip.Texture = Content.Load<Texture2D>("explosion2");
                        enemyShip.CurrentFrame = randomNumber.Next(1, 4);
                        enemyShip.Texture = Content.Load<Texture2D>("explosion");
                        enemyShip.IsDestroyed = true;
                        enemyShip.Y = -101;
                        bullet.Posituon += new Vector2(1000, -500);
                        score++;
                    }
                }
            }

            base.Update(gameTime);
        }

        public void UpdateBullets()
        {
            foreach (Bullets bullet in bullets)
            {
                bullet.Posituon += new Vector2(0, -5);
                //bullet.Velocity += new Vector2(100,200);
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

        public void Shoot()
        {
            Bullets newBullet = new Bullets(Content.Load<Texture2D>("bullet01_1f_8p"));
            //newBullet.Velocity = new Vector2(180, 1000);
            newBullet.Posituon = new Vector2(playerSpaceShip.X + 30, playerSpaceShip.Y + 10);
            newBullet.IsVisible = true;

            if (bullets.Count < 20)
            {
                bullets.Add(newBullet);
            }
        }

        protected bool CollideShips(SpaceObject obj1, SpaceObject obj2)
        {
            Rectangle colilisionRectForShip1 = new Rectangle(
               (int)obj1.X,
               (int)obj1.Y,
               50,
               50
               );

            Rectangle colilisionRectForShip2 = new Rectangle(
                (int)obj2.X,
                (int)obj2.Y,
                50,
                50
                );

            return colilisionRectForShip1.Intersects(colilisionRectForShip2);

        }

        public bool CollideBullets(Bullets bullletLokal, SpaceObject obj)
        {
            bool result = false;

            Rectangle colilisionRectForBullets = new Rectangle(
            (int)bullletLokal.X,
            (int)bullletLokal.Y,
            20,
            15
            );

            Rectangle colilisionRectForSpaceObject = new Rectangle(
            (int)obj.X,
            (int)obj.Y,
            50,
            50
            );
            result = colilisionRectForBullets.Intersects(colilisionRectForSpaceObject);

            return result;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // menu
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
                case GameState.Winners:
                    break;
                case GameState.PlayingEasy:
                case GameState.PlayingMedium:
                case GameState.PlayingHard:
                    // TODO: Add your drawing code here

                    spriteBatch.Draw(background, new Rectangle(0, 0, borderRight, borderDown), Color.White);
                    spriteBatch.Draw(earth, new Vector2(400, 240), Color.White);
                    //spriteBatch.Draw(shuttle, new Vector2(450, 240), Color.White);
                    this.btnBack.Draw(this.spriteBatch);

                    spriteBatch.DrawString(font, "Score " + score, new Vector2(30, 410), Color.White);
                    spriteBatch.DrawString(font, "PlayerSpaceShip X,Y= " + playerSpaceShip.X + ", " + playerSpaceShip.Y, new Vector2(30, 430), Color.White);
                    spriteBatch.DrawString(font, "EnemySpaceShip1 X,Y= " + enemySpaceShips[0].X + ", " + enemySpaceShips[0].Y, new Vector2(30, 450), Color.White);
                    spriteBatch.DrawString(font, "EnemySpaceShip2 X,Y= " + enemySpaceShips[1].X + ", " + enemySpaceShips[1].Y, new Vector2(30, 465), Color.White);

                    //spriteBatch.DrawString(font, "ColisionRectForPlayer X,Y= " + co + ", " + enemySpaceShip1.Y, new Vector2(30, 465), Color.White);


                    Vector2 location = new Vector2(400, 240);
                    //Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
                    //Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height);
                    //spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);

                    playerSpaceShip.Draw(spriteBatch, new Vector2(playerSpaceShip.X, playerSpaceShip.Y));
                    //PlayerSpaceShip.Draw(spriteBatch, new Vector2   (300, 200));

                    foreach (var enemyShip in enemySpaceShips)
                    {
                        enemyShip.Draw(spriteBatch, new Vector2(enemyShip.X, enemyShip.Y));
                    }

                    // enemySpaceShip1.Draw(spriteBatch, new Vector2(enemySpaceShip1.X, enemySpaceShip1.Y));
                    // enemySpaceShip2.Draw(spriteBatch, new Vector2(enemySpaceShip2.X, enemySpaceShip2.Y));


                    foreach (Bullets bullet in bullets)
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
