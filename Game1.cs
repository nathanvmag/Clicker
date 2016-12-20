using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clicker
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int ScreenWidth;
        public static int ScreenHeight;
        Monsters[] monters = new Monsters[6];
        int[] velochange = new int[2] { 1, -1 };
        float speed = 4;
        Random rnd = new Random();
        int Score = 0;
        float Timer = 30;
        Texture2D scorebar,timerbar,score,playAgain;
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
            for (int i = 0; i < monters.Length;i++ )
            {
                monters[i] = new Monsters();
            }
            ScreenWidth = GraphicsDevice.Viewport.Width;
            ScreenHeight = GraphicsDevice.Viewport.Height;
            Score = 0;
            Timer = 30;
            speed = 4;
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
            scorebar = Content.Load<Texture2D>("Life");
            timerbar = Content.Load<Texture2D>("timer");
            score = Content.Load<Texture2D>("Score");
            playAgain = Content.Load<Texture2D>("PlayAgain");
            for (int i = 0; i < monters.Length; i++)
            {int color=0;
                if (i<2)
                {
                    color=1;
                }
                else if (i<4)
                {
                    color=2;
                }
                else color=3;

                monters[i].Texture = Content.Load<Texture2D>("monster "+color.ToString());
               // Console.WriteLine(rnd.Next(monters[i].w, ScreenWidth - monters[i].w) + " " + rnd.Next(monters[i].h, ScreenHeight - monters[i].h));
                monters[i].Position = new Vector2(rnd.Next(monters[i].w, ScreenWidth - monters[i].w -70), rnd.Next(monters[i].h, ScreenHeight - monters[i].h-70));
                monters[i].Launch(speed* velochange[rnd.Next(0,2)]);

            }

            // TODO: use this.Content to load your game content here
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
            Timer-= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (Timer > 0)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                ScreenWidth = GraphicsDevice.Viewport.Width;
                ScreenHeight = GraphicsDevice.Viewport.Height;
                for (int i = 0; i < monters.Length; i++)
                {
                    monters[i].Move(monters[i].Velocity);
                    if (monters[i].CheckMouse())
                    {
                        Score++;                        
                        monters[i].Launch(speed * velochange[rnd.Next(0, 2)]);
                        monters[i].Position = new Vector2(rnd.Next(monters[i].w, ScreenWidth - monters[i].w - 70), rnd.Next(monters[i].h, ScreenHeight - monters[i].h - 70));
                    }
                }
            }
            else {
                Timer = -1;
                Rectangle PAgainarea = new Rectangle(ScreenWidth / 2 - 150 / 2, ScreenHeight / 2 - 100 / 2 + 170, 150, 100);
                var mouseState = Mouse.GetState();
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if(PAgainarea.Contains(mousePosition))
                    {
                        Score = 0;
                        Timer = 30;
                        speed = 4;
                    }
                }
            
            };
            // TODO: Add your update logic here
            IsMouseVisible = true;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.LightGreen);
            spriteBatch.Begin();
           
            if (Timer > 0)
            {
                for (int i = 0; i < monters.Length; i++)
                {
                    monters[i].Draw(spriteBatch);
                }
                for (int i = 0; i < Score; i++)
                {
                    spriteBatch.Draw(scorebar, new Rectangle(10 + i * 4, 10, 3, 10), Color.White);
                }
                int tempsize = Convert.ToInt32((140 * Timer) / 30);
                spriteBatch.Draw(timerbar, new Rectangle(ScreenWidth - 160, 10, tempsize, 20), Color.White);
            }
            else {
                spriteBatch.Draw(score, new Rectangle(ScreenWidth/2 -300/2, ScreenHeight/2-150/2-100,300, 150), Color.White);
                for (int i = 0; i < Score; i++)
                {
                    spriteBatch.Draw(scorebar, new Rectangle( ScreenWidth / 2 - 300 / 2+ 7*i -220, ScreenHeight / 2 +100 / 2,5,30), Color.White);
                }
                spriteBatch.Draw(playAgain, new Rectangle(ScreenWidth / 2 - 150 / 2, ScreenHeight / 2 - 100 / 2+170, 150, 100), Color.White);
            }           
            
             
                spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
