using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace BookCode
{
    public enum GameState { SignIn, SingedIn}
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        SpriteFont myFont;
        List<string> log = new List<string>();

        GameState currentGameState = GameState.SignIn;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Components.Add(new GamerServicesComponent(this));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            device = graphics.GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myFont = Content.Load<SpriteFont>("myFont");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (this.IsActive)
            {
                switch (currentGameState)
                {
                    case GameState.SignIn:
                        {
                            if (Gamer.SignedInGamers.Count < 1)
                            {
                                Guide.ShowSignIn(1, false);
                                log.Add("Opened User SignIn Interface");
                            }
                            else
                            {
                                currentGameState = GameState.SingedIn;
                                log.Add(Gamer.SignedInGamers[0].Gamertag + " logged in - proceed to SignedIn");
                            }                            
                        }
                        break;
                    case GameState.SingedIn:
                        {
                        }
                        break;
                }
            }

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1, 0);

            //Print each entry of the log to the screen
            spriteBatch.Begin();
            int i = 0;
            foreach (string entry in log)
            {
                spriteBatch.DrawString(myFont, entry, new Vector2(20, 20 * ++i), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
