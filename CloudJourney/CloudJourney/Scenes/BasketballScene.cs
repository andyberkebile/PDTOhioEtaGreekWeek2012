using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Scenes;
using CloudJourney.Framework.Renderables;
using CloudJourney.Actors;
using Microsoft.Xna.Framework;
using CloudJourney.Framework;
using CloudJourney.Framework.Collision;
using CloudJourney.Framework.Rendering;
using Microsoft.Xna.Framework.Graphics;
using CloudJourney.Actors.Obstacles;
using CloudJourney.Framework.Input;
using CloudJourney.Framework.BoneAnimation;
using CloudJourney.Framework.BoneAnimator.Stances;
using CloudJourney.Particles;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
namespace CloudJourney.Scenes
{
    class BasketballScene : Scene
    {
    public static bool isDead { set; get; }

        //public BoneAnimator animator;
        //public BoneAnimator animator2;
        //public BoneAnimator animator3;
        //public BoneAnimator animator4;
        //public BoneAnimator animator5;

        private RenderableCollection renderables;
        private ball Ball;
        private List<Renderable> toAdd;
        private List<Renderable> toRemove;

        public static float roadHeight { set; get; }

        public static float counter { set; get; }

        public static float vanSpeed;
        public static float vanSpeedA;
        public static float worldSpeed;
        public static float edgeLine { get; private set;}
        public static float voidLine { get; private set; }

        private int particleTimer { get; set; }

        private float oscillator;
        //private Background voidbg;
        private Background farbg;
        private Background midbg;
        private Background forebg;
        private Background courtbg;
        public static Background mainbg;

        public static long timer;

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        bool s1 = false; //Player One is Up - 100 seconds
        bool s2 = false; //Ready
        public bool s3 = false; //Go
        public bool gameover = false; //Go
        public bool reset = false; //Set Ball at random location

        /*      String tString = question;
                Vector2 textSize = Cloud.fontFixed.MeasureString(WrapText(Cloud.fontFixed, tString, 900));
                Drawer.DrawString(Cloud.fontFixed,
                    WrapText(Cloud.fontFixed, tString, 900),
                    new Vector2(0 * (int)scale, -250 * (int)scale),
                    Color.WhiteSmoke,
                    0.0f,
                    new Vector2(textSize.X/2, textSize.Y/2),
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.99f
                    );*/
        public int score1 = 0;
        public int score2 = 0;

        public BasketballScene()
        {
            


            reset = false;

            Song bgm = Cloud.content.Load<Song>("Thats a Wrap");
            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;

            sw.Start();

            renderables = new RenderableCollection();
            Physics.collisions = new CollisionHandler(renderables);

            toAdd = new List<Renderable>();
            toRemove = new List<Renderable>();

            Ball = new ball(new Vector2(120, 150), this);
            renderables.Add(Ball);

            TextureStatic.Load("hoopA", @"Art");
            TextureStatic.Load("hoopB", @"Art");
            TextureStatic.Load("courtBG", @"Art");
            TextureStatic.Load("net", @"Art");
            TextureStatic.Load("net2", @"Art");
            //courtbg = new Background("court", @"Art\Backgrounds", new Vector2(-360, 110), new Vector2(-10, 0), 0.11f, true, this);
            //renderables.Add(courtbg);

            //tentacle tn = new tentacle(new Vector2(0, 0), this);
            //renderables.Add(tn);
        }

        public void Load()
        {

        }

        public void Unload()
        {

        }

        public void Focus()
        {

        }

        public void Unfocus()
        {

        }

        public void Update()
        {
            if (gameover && (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed | GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
            {
                MediaPlayer.Stop();
                Cloud.sceneDriver.Push(new TriviaScene());
                //Cloud.sceneDriver.
            }

            

            if (sw.ElapsedMilliseconds / 1000 > 3)
                s1 = true;
            if (sw.ElapsedMilliseconds / 1000 > 6)
                s2 = true;
            if (sw.ElapsedMilliseconds / 1000 > 8)
                s3 = true;
            if (sw.ElapsedMilliseconds / 1000 > 128)
            {
                s1 = false;
                s2 = false;
                s3 = false;
                if (reset)
                    gameover = true;
                reset = true;
                sw.Restart();
            }
            timer++;
            counter++;

            //Camera2D.cam.Zoom = Math.Max(0.2f, 1 - (voidLine / -4000))-.2f;

            //mainbg.velocity= new Vector2(worldSpeed, 0);

            //Cloud.controller.Rumble(
            foreach (Renderable r in renderables)
            {
                if (r is Actor)
                    (r as Actor).Update();
            }

            foreach (Renderable r in toAdd)
            {
                renderables.Add(r);
            }
            toAdd.Clear();

            foreach (Renderable r in toRemove)
            {
                r.Unload();
                renderables.Remove(r);
            }
            toRemove.Clear();
        }

        public void Draw()
        {
            int halfwid = (int)(Cloud.screenWidth * Math.Pow(Camera2D.cam.Zoom, -1)) / 2;
            int halfhei = (int)(Cloud.screenHeight * Math.Pow(Camera2D.cam.Zoom, -1)) / 2;
            
            if (gameover)
            {
                Drawer.Draw(
                    TextureStatic.Get("black"),
                    new Rectangle(-halfwid, -halfhei, 2 * halfwid, 2 * halfhei),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9999991f);
                string result = "";
                if (score1 > score2)
                {
                    result = "Player 1 Wins";
                }
                else if (score1 < score2)
                {
                    result = "Player 2 Wins";
                }
                else
                {
                    result = "Tie Game";
                }

                Drawer.DrawString(
                    Cloud.fontCollege,
                    result,
                    new Vector2(-350, -50),
                    Color.FromNonPremultiplied((int)sw.ElapsedMilliseconds % 256, 256 - (int)sw.ElapsedMilliseconds % 256, 256, (int)sw.ElapsedMilliseconds % 1024 / 4),
                    -.1f * (float)Math.PI,
                    Vector2.Zero,
                    1.5f,
                    SpriteEffects.None,
                    .99999999999f);

                Drawer.DrawString(
                    Cloud.fontCollege,
                    "Press 'A' to Continue",
                    new Vector2(-300, 400),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    .99999999999f);
            }

            Drawer.DrawString(
                    Cloud.fontCollege,
                    "Player 1 Score: " + score1,
                    new Vector2(-500, 200),
                    Color.Goldenrod,
                    0f,
                    Vector2.Zero,
                    .5f,
                    SpriteEffects.None,
                    .999999f);
            Drawer.DrawString(
                Cloud.fontCollege,
                "Player 1 Score: " + score1,
                new Vector2(-498, 202),
                Color.Black,
                0f,
                Vector2.Zero,
                .5f,
                SpriteEffects.None,
                .999998f);
            Drawer.DrawString(
                Cloud.fontCollege,
                "Player 2 Score: " + score2,
                new Vector2(300, 200),
                Color.Goldenrod,
                0f,
                Vector2.Zero,
                .5f,
                SpriteEffects.None,
                .999999f);
            Drawer.DrawString(
                Cloud.fontCollege,
                "Player 2 Score: " + score2,
                new Vector2(302, 202),
                Color.Black,
                0f,
                Vector2.Zero,
                .5f,
                SpriteEffects.None,
                .999998f);

            if (s1 && !s2)
            {
                if (!reset)
                {
                    Drawer.DrawString(
                        Cloud.fontCollege,
                        "Player 1 Get Ready",
                        new Vector2(-200, -200),
                        Color.Goldenrod,
                        0f,
                        Vector2.Zero,
                        .7f,
                        SpriteEffects.None,
                        .999999f);
                    Drawer.DrawString(
                        Cloud.fontCollege,
                        "Player 1 Get Ready",
                        new Vector2(-198, -198),
                        Color.Black,
                        0f,
                        Vector2.Zero,
                        .7f,
                        SpriteEffects.None,
                        .999998f);
                }
                else
                {
                    Drawer.DrawString(
                        Cloud.fontCollege,
                        "Player 2 Get Ready",
                        new Vector2(-200, -200),
                        Color.Goldenrod,
                        0f,
                        Vector2.Zero,
                        .7f,
                        SpriteEffects.None,
                        .999999f);
                    Drawer.DrawString(
                        Cloud.fontCollege,
                        "Player 2 Get Ready",
                        new Vector2(-198, -198),
                        Color.Black,
                        0f,
                        Vector2.Zero,
                        .7f,
                        SpriteEffects.None,
                        .999998f);
                }

            }
            if (s2 && !s3)
            {
                Drawer.DrawString(
                    Cloud.fontCollege,
                    "READY",
                    new Vector2(-50, -200),
                    Color.Goldenrod,
                    0f,
                    Vector2.Zero,
                    .7f,
                    SpriteEffects.None,
                    .999999f);
                Drawer.DrawString(
                    Cloud.fontCollege,
                    "READY",
                    new Vector2(-48, -198),
                    Color.Black,
                    0f,
                    Vector2.Zero,
                    .7f,
                    SpriteEffects.None,
                    .999998f);
            }
            if (s3 && sw.ElapsedMilliseconds/1000 < 10)
            {
                Drawer.DrawString(
                    Cloud.fontCollege,
                    "GO",
                    new Vector2(-50, -200),
                    Color.Goldenrod,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    .999999f);
                Drawer.DrawString(
                    Cloud.fontCollege,
                    "GO",
                    new Vector2(-48, -198),
                    Color.Black,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    .999998f);
            }
            if (s3)
            {
                Drawer.DrawString(
                    Cloud.fontCollege,
                    (120 - (sw.ElapsedMilliseconds/1000 - 8)).ToString(),
                    new Vector2(-60, -250),
                    Color.Goldenrod,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    .999999f);
                Drawer.DrawString(
                    Cloud.fontCollege,
                    (120 - (sw.ElapsedMilliseconds / 1000 - 8)).ToString(),
                    new Vector2(-58, -248),
                    Color.Black,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    .999998f);
            }
            Drawer.Draw(TextureStatic.Get("court"),
                new Rectangle(-halfwid - 10, 0, halfwid * 2 + 10, halfhei + 10),
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.05f);
            Drawer.Draw(TextureStatic.Get("courtBG"),
                new Rectangle(-halfwid - 10, -400, halfwid * 2 + 10, 600),
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.0501f);
            Drawer.Draw(TextureStatic.Get("hoopA"),
                new Rectangle(-700, -400, 324, 384),
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.051f);
            Drawer.Draw(TextureStatic.Get("net"),
                new Rectangle(-466, -94, 89, 70),
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.512f);
            Drawer.Draw(TextureStatic.Get("net2"),
                new Rectangle(-466, -102, 89, 70),
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.0512f);
            Drawer.Draw(TextureStatic.Get("hoopB"),
                new Rectangle(-466, -94, 89, 18),
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.51f);
            //Drawer.DrawLine(new Vector2(-250, 275), new Vector2(250, 275), 1, 1, Color.Fuchsia);
            //Drawer.DrawLine(new Vector2(-473, -50), new Vector2(-473, -205), 1, 1, Color.Fuchsia);
            
            //Drawer.DrawLine(new Vector2(-473, -90), new Vector2(-447, -90), 1, 1, Color.Fuchsia);
            //Drawer.DrawLine(new Vector2(-447, -90), new Vector2(-447, -80), 1, 1, Color.Fuchsia);


            /*Points of Rim
             * 
             *-380, -87
             *-380, -92
             *-387, -87
             *-397, -92
             * 
             */
            /*Drawer.DrawLine(new Vector2(-387, -92), new Vector2(-387, -87), 1, 1, Color.Fuchsia);
            Drawer.DrawLine(new Vector2(-387, -92), new Vector2(-380, -92), 1, 1, Color.Fuchsia);
            Drawer.DrawLine(new Vector2(-380, -92), new Vector2(-380, -87), 1, 1, Color.Fuchsia);
            Drawer.DrawLine(new Vector2(-387, -87), new Vector2(-380, -87), 1, 1, Color.Fuchsia);
             */
            /*Drawer.DrawString(Cloud.fontFixed,
                String.Format("{0:00}:{1:00}:{2:000}", Math.Floor(timer / 3600.0), Math.Floor(timer / 60.0) % 60, Math.Floor(((timer % 60) / 60.0f) * 100.0f)),
                new Vector2(-580, -340),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.99f
                );
            */
            renderables.Draw();
        }

        public void AddRenderable(Renderable r)
        {
            toAdd.Add(r);
        }

        public void RemoveRenderable(Renderable r)
        {
            toRemove.Add(r);
        }
    }
}