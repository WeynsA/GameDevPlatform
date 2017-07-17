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

        Map activeLevel;
        Level1 level1;
        Level2 level2;
        //Map map;
        /*Player player;
        Texture2D healthTexture;
        Rectangle healthRectangle;*/
        Camera camera;
        Button btnPlay;
        Button btnQuit;
        /*Enemy enemy;
        Enemy enemy1;*/
        bool paused = false;
        Texture2D pausedTexture;
        Rectangle pausedRectangle;

        //Bullet bullet;

        enum GameState
        {
            MainMenu,
            Quit,
            Playing
        }

        GameState CurrentGameState = GameState.MainMenu;

        int screenWidth = 800, screenHeight = 600;

        

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
            camera = new Camera(new Viewport(400, 400, screenWidth, screenHeight));
            level1 = new Level1(Content, camera, graphics.GraphicsDevice);
            level2 = new Level2(Content, camera, graphics.GraphicsDevice);
            activeLevel = level1;
            //player = new Player(100, Content);
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

            pausedTexture = Content.Load<Texture2D>("Paused");

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            btnPlay = new Button(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 350));
            btnQuit = new Button(Content.Load<Texture2D>("BtnQuit"), graphics.GraphicsDevice);
            btnQuit.setPosition(new Vector2(350, 400));
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
                activeLevel.Update(gameTime);
            }

            else if (paused)
            {
                btnPlay.setPosition(new Vector2(screenWidth / 2-30, screenHeight / 2));
                btnQuit.setPosition(new Vector2(screenWidth / 2-30, screenHeight / 2+37));
                if (btnPlay.isClicked || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    paused = false;
                if (btnQuit.isClicked)
                    Exit();

                btnQuit.Update(mouse);
                btnPlay.Update(mouse);
            }
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
                    activeLevel.Draw(spriteBatch);
                    if (paused)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(pausedTexture, new Rectangle(0,0, screenWidth, screenHeight), Color.White);
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
