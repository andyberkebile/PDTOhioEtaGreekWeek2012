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
using CloudJourney.Framework.Scenes;
using CloudJourney.Framework.Rendering;
using CloudJourney.Scenes;
using CloudJourney.Framework.Input;
using CloudJourney.Framework;

namespace CloudJourney
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Cloud : Microsoft.Xna.Framework.Game
    {
        public GameTime gt;
        public static Random randCloud { get; set; }
        public static GraphicsDevice graphics { get; private set; }
        public static GraphicsDeviceManager graphicsman { get; private set; }
        public static ContentManager content { get; private set; }
        public static SpriteBatch spriteBatch { get; private set; }
        public static SceneDriver sceneDriver { get; private set; }
        public static Controller controller { get; private set; }
        public static Controller controller2 { get; private set; }
        public static SpriteFont fontMain { get; private set; }
        public static SpriteFont fontFixed { get; private set; }
        public static SpriteFont fontCollege { get; private set; }
        public static SpriteFont fontAnswer { get; private set; }
        public static int screenWidth { get { return 1280; } }
        public static int screenHeight { get { return (int)(0.5625f * screenWidth); } }
        public float ellapse;

        public Cloud()
            : base()
        {
            ellapse = 0;
            gt = new GameTime();
            randCloud = new Random();
            graphicsman = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphicsman.PreferredBackBufferWidth
                = screenWidth;
            graphicsman.PreferredBackBufferHeight
                = screenHeight;
            //graphicsman.IsFullScreen = true;
            sceneDriver = new SceneDriver();
            controller = new Controller(PlayerIndex.One);
            controller2 = new Controller(PlayerIndex.Two);
            
            graphicsman.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            content = Content;
            graphics = GraphicsDevice;

            graphics.PresentationParameters.BackBufferWidth
                = screenWidth; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PresentationParameters.BackBufferHeight
                = screenHeight;

#if !XBOX
            graphics.PresentationParameters.IsFullScreen = false;
#endif

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureStatic.Load("Blank", @"Art");
            TextureStatic.Load("court", @"Art");
            TextureStatic.Load("Strosacker", @"Art");
            TextureStatic.Load("Abutton", @"Art");
            TextureStatic.Load("Xbutton", @"Art");
            TextureStatic.Load("Bbutton", @"Art");
            TextureStatic.Load("Ybutton", @"Art");
            TextureStatic.Load("black", @"Art");

            fontMain = content.Load<SpriteFont>("AgencyFB");
            fontFixed = content.Load<SpriteFont>("Fixed");
            fontAnswer = content.Load<SpriteFont>("Answer");
            fontCollege = content.Load<SpriteFont>("College");
            //fontFixed = content.Load<SpriteFont>("Courier New");

            //Song bgm = content.Load<Song>("music");
            //MediaPlayer.Play(bgm);
            //MediaPlayer.IsRepeating = true;

            Window.Title = "Mark Starr's Greek Week 12";

            
            //sceneDriver.Push(new TriviaScene());
            //sceneDriver.Push(new BasketballScene());
            sceneDriver.Push(new titScene());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            controller.SetPlayerIndex(PlayerIndex.One);
            controller2.SetPlayerIndex(PlayerIndex.Two);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //if (GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed)
               // this.Exit();
            Console.WriteLine(controller.playerIndex);
            Console.WriteLine(controller2.playerIndex);

            controller.Update();
            controller2.Update();
            sceneDriver.Update();

            base.Update(gameTime);

            if (sceneDriver.empty) this.Exit();
            ellapse += (float)gt.ElapsedGameTime.TotalMilliseconds;
            //Console.WriteLine(gt.TotalGameTime.Seconds);
                       
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); // ;)
            Cloud.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Camera2D.cam.get_transformation(Cloud.graphics));

            sceneDriver.Draw();

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
