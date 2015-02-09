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
    class titScene : Scene
    {
    public static bool isDead { set; get; }

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

        public titScene()
        {
            counter = 0;
            sw.Start();
            TextureStatic.Load("logo3", @"Art/titleScreen");
            TextureStatic.Load("bg", @"Art/titleScreen");
            TextureStatic.Load("mark", @"Art/titleScreen");
            TextureStatic.Load("pic1", @"Art/titleScreen");
            TextureStatic.Load("pic2", @"Art/titleScreen");
            TextureStatic.Load("pic3", @"Art/titleScreen");

            Song bgm = Cloud.content.Load<Song>("The Whip Theme");
            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;

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
            if ((GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed | GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
            {
                MediaPlayer.Stop();
                Cloud.sceneDriver.Push(new BasketballScene());
                //Cloud.sceneDriver.
            }
            counter++;
            if (sw.ElapsedMilliseconds / 1000 > 2)
                s1 = true;
            if (sw.ElapsedMilliseconds / 1000 > 3)
                s2 = true;
            if (sw.ElapsedMilliseconds / 1000 > 4 && counter % 300 == 0)
                s3 = true;

        }

        public void Draw()
        {
            int halfwid = (int)(Cloud.screenWidth * Math.Pow(Camera2D.cam.Zoom, -1)) / 2;
            int halfhei = (int)(Cloud.screenHeight * Math.Pow(Camera2D.cam.Zoom, -1)) / 2;
            Drawer.Draw(
                    TextureStatic.Get("bg"),
                    new Rectangle(-halfwid, -halfhei, 2 * halfwid, 2 * halfhei),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .1f);

            if (s1)
            {
                Drawer.Draw(
                    TextureStatic.Get("logo3"),
                    new Rectangle(-halfwid, 0, 2 * halfwid, 300),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, (int)((sw.ElapsedMilliseconds - 3000) / 5)),
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9999999f);
            }
            if (s2)
            {
                Drawer.Draw(
                    TextureStatic.Get("mark"),
                    new Rectangle(75, -250, (int)(710*.7), (int)(677*.7)),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, (int)((sw.ElapsedMilliseconds - 4000) / 5)),
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9f);
            }
            if (s3)
            {
                if (counter % 900 < 300)
                {
                    int a = 256;
                    int x = (int)(-300 + (1000 - 1000*(counter % 300)/60));
                    if (x < -400)
                        x = -400;
                    
                    if(counter % 300 < 60)
                    {
                        //a = (int)(((sw.ElapsedMilliseconds % 3000) / 1000) * 256);
                        a = (int)(((counter % 60) / 60) * 256);
                    }
                    if (counter % 300 > 240)
                    {
                        a = (int)(256 - (((counter % 60) / 60) * 256));
                    }

                    Drawer.Draw(
                        TextureStatic.Get("pic1"),
                        new Rectangle(x, -400, (int)(892 * .7), (int)(619 * .7)),
                        null,
                        Color.FromNonPremultiplied(256, 256, 256, a),
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        .8f);
                }
                else if (counter % 900 < 600)
                {
                    int a = 256;
                    int x = (int)(-300 + (1000 - 1000 * (counter % 300) / 60));
                    if (x < -400)
                        x = -400;

                    if (counter % 300 < 60)
                    {
                        //a = (int)(((sw.ElapsedMilliseconds % 3000) / 1000) * 256);
                        a = (int)(((counter % 60) / 60) * 256);
                    }
                    if (counter % 300 > 240)
                    {
                        a = (int)(256 - (((counter % 60) / 60) * 256));
                    }

                    Drawer.Draw(
                        TextureStatic.Get("pic2"),
                        new Rectangle(x, -400, (int)(892 * .7), (int)(619 * .7)),
                        null,
                        Color.FromNonPremultiplied(256, 256, 256, a),
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        .8f);
                }
                else
                {
                    int a = 256;
                    int x = (int)(-300 + (1000 - 1000 * (counter % 300) / 60));
                    if (x < -400)
                        x = -400;

                    if (counter % 300 < 60)
                    {
                        //a = (int)(((sw.ElapsedMilliseconds % 3000) / 1000) * 256);
                        a = (int)(((counter % 60) / 60) * 256);
                    }
                    if (counter % 300 > 240)
                    {
                        a = (int)(256 - (((counter % 60) / 60) * 256));
                    }

                    Drawer.Draw(
                        TextureStatic.Get("pic3"),
                        new Rectangle(x, -400, (int)(892 * .7), (int)(619 * .7)),
                        null,
                        Color.FromNonPremultiplied(256, 256, 256, a),
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        .8f);
                }
            }




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