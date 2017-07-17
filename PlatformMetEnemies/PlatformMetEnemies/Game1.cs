using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PlatformMetEnemies
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D collisionBox;


        Map map;
        Player player;
        Texture2D healthTexture;
        Rectangle healthRectangle;
        Camera camera;
        Button btnPlay;
        Button btnQuit;
        Enemy enemy;
        Enemy enemy1;
        bool paused = false;
        Texture2D pausedTexture;
        Rectangle pausedRectangle;
        Button btnPausePlay;
        Button btnPauseQuit;
        Bullet bullet;

        enum GameState
        {
            MainMenu,
            Quit,
            Playing
        }

        GameState CurrentGameState = GameState.MainMenu;

        int screenWidth = 800, screenHeight = 600;
        int canvasWidth, canvasHeight;
        

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
            map = new Map();
            player = new Player(100, Content);
            //player = new Player(Content.Load<Texture2D>("hero"), new Vector2(200, 70), 47, 44, 100);
            //player = new Player(100, 47,44, new Vector2(200,200));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            collisionBox = new Texture2D(GraphicsDevice,1,1);
            collisionBox.SetData(new Color[] { Color.Red });

            Tiles.Content = Content;

            pausedTexture = Content.Load<Texture2D>("Paused");
           
            camera = new Camera(GraphicsDevice.Viewport);

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            healthTexture = Content.Load<Texture2D>("Health");
            
            IsMouseVisible = true;
            btnPlay = new Button(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 350));
            btnQuit = new Button(Content.Load<Texture2D>("BtnQuit"), graphics.GraphicsDevice);
            btnQuit.setPosition(new Vector2(350, 400));
            
            map.Generate(new int[,]{
                { 0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1},
                { 0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2},
                { 0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2,2,2},
                { 0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,1,1,2,2,2,2,2,2,2,2},
                { 0,2,2,1,1,1,0,0,0,0,1,1,1,2,2,2,2,2,2,2,1,0,0,0,0,0,2,2,2,2,2,2,2,2},
                { 0,2,2,0,0,0,0,0,0,1,2,2,2,2,2,2,2,2,2,2,2,2,1,0,0,0,2,2,2,2,2,2,2,2},
                { 0,2,0,0,0,0,0,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2},
                { 0,2,0,0,0,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                }, 64);

            player.Load(Content);
            enemy = new Enemy(Content.Load<Texture2D>("Enemy"), new Vector2(300, 400), 100);
            enemy1 = new Enemy(Content.Load<Texture2D>("Enemy"), new Vector2(650, 150), 100);

            //player = new Player(Content.Load<Texture2D>("Hero"), new Vector2(200, 200), 44, 47, 100);


            canvasWidth = GraphicsDevice.Viewport.Bounds.Width;
            canvasHeight = GraphicsDevice.Viewport.Bounds.Height;
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
            MouseState mouse = Mouse.GetState();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    if (btnQuit.isClicked == true) this.Exit();
                    btnQuit.Update(mouse);
                    break;
                case GameState.Quit:
                    break;
                case GameState.Playing:
                    break;
                default:
                    break;
            }

            if (!paused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    paused = true;
                    btnPlay.isClicked = false;
                }
                Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

                enemy.Update(player);
                enemy1.Update(player);
                //bullet.Update(gameTime);

                player.Update(gameTime);
                foreach (CollisionTiles tile in map.CollisionTiles)
                {
                    player.Collision(tile.Rectangle, map.Width, map.Height);
                    enemy.Collision(tile.Rectangle, map.Width, map.Height);
                    enemy1.Collision(tile.Rectangle, map.Width, map.Height);
                    camera.Update(player.Position, map.Width, map.Height);
                }

                if (enemy.rectangle.Intersects(player.rectangle)) player.Hit();
                if (enemy1.rectangle.Intersects(player.rectangle))
                {
                    player.Hit();
                }
                //if (enemy.rectangle.Intersects(bullet.colrectangle)) enemy.Death();
            }

            else if (paused)
            {
                if (btnPlay.isClicked || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    paused = false;
                if (btnQuit.isClicked)
                    Exit();

                btnQuit.Update(mouse);
                btnPlay.Update(mouse);
            }

            healthRectangle = new Rectangle((int)camera.centre.X - (canvasWidth / 2) + 80, (int)camera.centre.Y - (canvasHeight / 2) + 80, player.health, 20);
            pausedRectangle = new Rectangle((int)camera.centre.X - (canvasWidth / 2), (int)camera.centre.Y - (canvasHeight / 2), screenWidth, screenHeight);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    btnQuit.Draw(spriteBatch);
                    spriteBatch.End();
                    break; 
                case GameState.Quit:
                    break;
                case GameState.Playing:

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
                    player.Draw(spriteBatch);
                    enemy.Draw(spriteBatch);
                    enemy1.Draw(spriteBatch);
                    map.Draw(spriteBatch, collisionBox);
                    spriteBatch.Draw(healthTexture, healthRectangle, Color.White);

                    if (paused)
                    {
                        spriteBatch.Draw(pausedTexture, pausedRectangle, Color.White);
                        btnPlay.Draw(spriteBatch);
                        btnQuit.Draw(spriteBatch);
                    }
                    spriteBatch.End();

                    break;
                default:
                    break;
            }     
            base.Draw(gameTime);
        }
    }
}
