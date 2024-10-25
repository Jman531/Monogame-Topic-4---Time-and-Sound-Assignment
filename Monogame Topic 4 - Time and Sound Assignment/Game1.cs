using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Monogame_Topic_4___Time_and_Sound_Assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        float seconds = 15;
        bool bombExploded = false;

        Rectangle window;

        Texture2D bombTexture, explosionTexture;
        Rectangle bombRect, explosionRect, mouseRect;

        SpriteFont timeFont;
        SoundEffect explode;

        SoundEffectInstance explodeSound;

        MouseState mouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bombRect = new Rectangle(50, 50, 700, 400);
            explosionRect = new Rectangle(50, 50, 700, 400);
            mouseRect = new Rectangle(0, 0, 1, 1);
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            this.Window.Title = "Bomb";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            explosionTexture = Content.Load<Texture2D>("bomb explosion");
            timeFont = Content.Load<SpriteFont>("time");
            explode = Content.Load<SoundEffect>("explosion");

            explodeSound = explode.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mouseRect.X = mouseState.X;
            mouseRect.Y = mouseState.Y;

            seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds <= 0)
            {
                if (!bombExploded)
                    explodeSound.Play();

                bombExploded = true;

                if (explodeSound.State != SoundState.Playing)
                    this.Exit();
            }

            mouseState = Mouse.GetState();
            if (bombRect.Intersects(mouseRect) && mouseState.LeftButton == ButtonState.Pressed && !bombExploded)
                seconds = 15f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (!bombExploded)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            else if (bombExploded)
            {
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
