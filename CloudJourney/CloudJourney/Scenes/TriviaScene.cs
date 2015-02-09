using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using Microsoft.Xna.Framework.Input;
using CloudJourney.Framework.BoneAnimation;
using CloudJourney.Framework.BoneAnimator.Stances;
using CloudJourney.Particles;
using Microsoft.Xna.Framework.Media;
namespace CloudJourney.Scenes

{
    class TriviaScene : Scene
    {
    public static bool isDead { set; get; }

        //public BoneAnimator animator;
        //public BoneAnimator animator2;
        //public BoneAnimator animator3;
        //public BoneAnimator animator4;
        //public BoneAnimator animator5;

        private RenderableCollection renderables;
        private List<Renderable> toAdd;
        private List<Renderable> toRemove;

        public static float roadHeight { set; get; }

        public static float counter { set; get; }


        public static float edgeLine { get; private set;}
        public static float voidLine { get; private set; }

        private int particleTimer { get; set; }

        private long swTime;

        public static long timer;

        private int answered1 = -1;
        private int answered2 = -1;

        private Boolean readyToStart = false;
        private Boolean readyToAsk = false;
        private Boolean asked = false;
        private Boolean startTimer = false;
        private Boolean timeUp = false;
        private Boolean reveal = false;

        private int questionsAsked = 0;
        private int questions = 10;

        private TextReader tr;

        private String question;
        private String right;
        private String wrong1;
        private String wrong2;
        private String wrong3;

        private int rightNum = 0;
        private LinkedList<String> answers = new LinkedList<string>();

        public BoneAnimator animator;
        public BoneAnimator animator2;

        private int p1Score = 0;
        private int p2Score = 0;

        private int temp1Score = 0;
        private int temp2Score = 0;

        private Boolean gameOver = false;
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        GameTime gt = new GameTime();

        public TriviaScene()
        {
            Song bgm = Cloud.content.Load<Song>("MilitaireElectronic");
            MediaPlayer.Play(bgm);
            MediaPlayer.IsRepeating = true;

            Camera2D.cam.Zoom = .5f;
            animator = new BoneAnimator(null, StaticStanceplayer.GetBoneSet(), StaticStanceplayer.Get(playerStance.Idle), 0, 0);
            animator.Depth = .6f;
            //animator.SetAnimation(StaticStanceplayer.GetAnimation(playerAnimation.Idle));
            animator.Update();


            animator2 = new BoneAnimator(null, StaticStanceplayer2.GetBoneSet(), StaticStanceplayer2.Get(player2Stance.Idle), 0, 0);
            animator2.Depth = .6f;
            //animator2.SetAnimation(StaticStanceplayer2.GetAnimation(player2Animation.Idle));
            animator2.Update();


            answers.Clear();
            tr = new StreamReader("../../trivia.txt");

            renderables = new RenderableCollection();
            Physics.collisions = new CollisionHandler(renderables);

            toAdd = new List<Renderable>();
            toRemove = new List<Renderable>();

            TextureStatic.Load("questionBG", @"Art");
            TextureStatic.Load("playerAnswerBG", @"Art");
            TextureStatic.Load("unknown", @"Art");
            TextureStatic.Load("pod", @"Art");
            

            //Ball = new ball(new Vector2(120, 150), this);
            //renderables.Add(Ball);
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
            if (gameOver && (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed | GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
            {
            }

            animator.SetOrigin(new Vector2(400, 0));
            animator.Update();

            animator2.SetOrigin(new Vector2(800, 0));
            animator2.Update();
            


            timer++;
            counter++;
            swTime = sw.ElapsedMilliseconds;
            //Console.WriteLine(swTime/1000);

            if (answered1 == -1)
            {
                animator.SetAnimation(StaticStanceplayer.GetAnimation(playerAnimation.Idle));
            }
            else
            {
                animator.SetAnimation(StaticStanceplayer.GetAnimation(playerAnimation.Answer));
            }
            if (answered2 == -1)
            {
                animator2.SetAnimation(StaticStanceplayer2.GetAnimation(player2Animation.Idle));
            }
            else
            {
                animator2.SetAnimation(StaticStanceplayer2.GetAnimation(player2Animation.Answer));
            }
            sw.Start();
            if (questionsAsked < questions)
            {
                if (!readyToStart)
                {
                    
                    if (sw.ElapsedMilliseconds / 1000 > 3)
                    {
                        readyToStart = true;
                        sw.Restart();
                        //Pick Question
                        tr = new StreamReader("../../trivia.txt");
                        //String allText = tr.ReadToEnd();
                        int q = Cloud.randCloud.Next(1, 72);
                        //q = 27;
                        String qNum = "<" + q + ">";
                        String textLine = tr.ReadLine();
                        while (!textLine.StartsWith(qNum))
                        {
                            textLine = tr.ReadLine();
                        }
                        question = textLine.Substring(qNum.Length, textLine.Length-(2*qNum.Length+1));
                        String temp = tr.ReadLine();
                        right = temp.Substring(9, temp.Length - 19);
                        temp = tr.ReadLine();
                        wrong1 = temp.Substring(7, temp.Length - 15);
                        temp = tr.ReadLine();
                        wrong2 = temp.Substring(7, temp.Length - 15);
                        temp = tr.ReadLine();
                        wrong3 = temp.Substring(7, temp.Length - 15);

                        answers.Clear();

                        LinkedList<int> taken = new LinkedList<int>();

                        for (int i = 0; i < 4; i++)
                        {
                            int next = Cloud.randCloud.Next(0, 4);
                            while (taken.Contains(next))
                            {
                                next = Cloud.randCloud.Next(0, 4);
                            }
                            taken.AddLast(next);

                            if (next == 0)
                            {
                                rightNum = i;
                                answers.AddLast(right);
                            }
                            if (next == 1)
                            {
                                answers.AddLast(wrong1);
                            }
                            if (next == 2)
                            {
                                answers.AddLast(wrong2);
                            }
                            if (next == 3)
                            {
                                answers.AddLast(wrong3);
                            }
                        }
                    }
                }
                else if(!readyToAsk)
                {

                    if (sw.ElapsedMilliseconds / 1000 > 2)
                    {
                        sw.Restart();
                        readyToAsk = true;
                    }
                }
                else if(!asked && readyToAsk)
                {
                    if (sw.ElapsedMilliseconds / 1000 > 3)
                    {
                        sw.Restart();
                        asked = true;
                    }
                }
                else if(asked && !startTimer)
                {

                    if (sw.ElapsedMilliseconds / 1000 > 2)
                    {
                        sw.Restart();
                        startTimer = true;
                    }
                }


                Console.WriteLine(Cloud.controller.playerIndex + "\t" + Cloud.controller2.playerIndex);
                if (startTimer && answered1 == -1)
                {

                    if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                    {
                        answered1 = 0;
                        temp1Score = 10 - (int)sw.ElapsedMilliseconds / 1000;
                        if (temp1Score < 0)
                        {
                            temp1Score = 0;
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
                    {
                        answered1 = 1;
                        temp1Score = 10 - (int)sw.ElapsedMilliseconds / 1000;
                        if (temp1Score < 0)
                        {
                            temp1Score = 0;
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                    {
                        answered1 = 2;
                        temp1Score = 10 - (int)sw.ElapsedMilliseconds / 1000;
                        if (temp1Score < 0)
                        {
                            temp1Score = 0;
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                    {
                        answered1 = 3;
                        temp1Score = 10 - (int)sw.ElapsedMilliseconds / 1000;
                        if (temp1Score < 0)
                        {
                            temp1Score = 0;
                        }
                    }
                }
                Console.WriteLine(Cloud.controller.playerIndex + "\t" + Cloud.controller2.playerIndex);
                if (startTimer && answered2 == -1)
                {
                    
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.X == ButtonState.Pressed)
                    {
                        answered2 = 0;
                        temp2Score = 10 - (int)sw.ElapsedMilliseconds / 1000;
                        if (temp2Score < 0)
                        {
                            temp2Score = 0;
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.Y == ButtonState.Pressed)
                    {
                        answered2 = 1;
                        temp2Score = 10 - (int)sw.ElapsedMilliseconds / 1000;
                        if (temp2Score < 0)
                        {
                            temp2Score = 0;
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed)
                    {
                        answered2 = 2;
                        temp2Score = 10 - (int)sw.ElapsedMilliseconds / 1000;
                        if (temp2Score < 0)
                        {
                            temp2Score = 0;
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.B == ButtonState.Pressed)
                    {
                        answered2 = 3;
                        temp2Score = 10 - (int)sw.ElapsedMilliseconds / 1000;
                        if (temp2Score < 0)
                        {
                            temp2Score = 0;
                        }
                    }
                }
                Console.WriteLine(Cloud.controller.playerIndex + "\t" + Cloud.controller2.playerIndex);
                if (startTimer && sw.ElapsedMilliseconds / 1000 > 10)
                {
                    timeUp = true;
                }
                if (timeUp && sw.ElapsedMilliseconds / 1000 > 13)
                {
                    reveal = true;
                }
                if (reveal && sw.ElapsedMilliseconds / 1000 > 18)
                {
                    questionsAsked++;
                    readyToStart = false;
                    readyToAsk = false;
                    asked = false;
                    startTimer = false;
                    timeUp = false;
                    reveal = false;
                    

                    //XYAB
                    //AYXB

                    if ((answered1 == 1 && rightNum == 1) | (answered1 == 3 && rightNum == 3) | (answered1 == 0 && rightNum == 2) | (answered1 == 2 && rightNum == 0))
                        p1Score = p1Score + temp1Score;
                    if ((answered2 == 1 && rightNum == 1) | (answered2 == 3 && rightNum == 3) | (answered2 == 0 && rightNum == 2) | (answered2 == 2 && rightNum == 0))
                        p2Score = p2Score + temp2Score;

                    temp1Score = 0;
                    temp2Score = 0;
                    answered1 = -1;
                    answered2 = -1;

                }
            }
            else
            {
                gameOver = true;
            }



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
            if (gameOver)
            {
                Drawer.Draw(
                    TextureStatic.Get("black"),
                    new Rectangle(-halfwid, -halfhei, 2 * halfwid, 2 * halfhei),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .999999f);
                string result = "";
                if (p1Score > p2Score)
                {
                    result = "Player 1 Wins";
                }
                else if(p1Score < p2Score)
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
                    new Vector2(-700, -100),
                    Color.FromNonPremultiplied((int)sw.ElapsedMilliseconds % 256, 256 - (int)sw.ElapsedMilliseconds % 256, 256, (int)sw.ElapsedMilliseconds % 1024 / 4),
                    -.1f * (float)Math.PI,
                    Vector2.Zero,
                    3f,
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

            float scale = 2;
            animator.Draw();
            animator2.Draw();
            /*
            animator3.Draw();
            animator4.Draw();
            animator5.Draw();
            */

            

            Drawer.Draw(TextureStatic.Get("pod"),
                new Rectangle(300, 0, (int)(209*1.3), (int)(302*1.3)),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                .8f);

            Drawer.Draw(TextureStatic.Get("pod"),
                new Rectangle(700, 0, (int)(209 * 1.3), (int)(302 * 1.3)),
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                .8f);

            Drawer.DrawString(
                Cloud.fontCollege,
                "TRIVIA CHALLENGE",
                new Vector2(-395 * scale, -300 * scale),
                Color.DarkBlue,
                0f,
                Vector2.Zero,
                .75f * scale,
                SpriteEffects.None,
                .8f);
            Drawer.DrawString(
                Cloud.fontCollege,
                "Player 1",
                new Vector2(-380 * scale, -145 * scale),
                Color.DarkBlue,
                0f,
                Vector2.Zero,
                .6f * scale,
                SpriteEffects.None,
                .8f);

            Drawer.DrawString(
                Cloud.fontCollege,
                p1Score.ToString(),
                new Vector2(-330 * scale, -115 * scale),
                Color.Black,
                0f,
                Vector2.Zero,
                .8f * scale,
                SpriteEffects.None,
                .8f);
            Drawer.DrawString(
                Cloud.fontCollege,
                "Player 2",
                new Vector2(-160 * scale, -145 * scale),
                Color.DarkBlue,
                0f,
                Vector2.Zero,
                .6f * scale,
                SpriteEffects.None,
                .8f);
            Drawer.DrawString(
                Cloud.fontCollege,
                p2Score.ToString(),
                new Vector2(-110 * scale, -115 * scale),
                Color.Black,
                0f,
                Vector2.Zero,
                .8f * scale,
                SpriteEffects.None,
                .8f);
            //new Rectangle(-640, -360, 640 * 2 + 10, 360 *2),
            Drawer.Draw(TextureStatic.Get("Strosacker"),
                new Rectangle(-1280, -720, 640 * 4 + 10, 360 * 4),
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.05f);
            renderables.Draw();

            //Console.WriteLine((int)(256 * (sw.ElapsedMilliseconds / 5000)));

            if(readyToStart && !readyToAsk)
            {
                int alpha = (int)(256 * (double)((double)sw.ElapsedMilliseconds / (double)1000));
                if (alpha > 256)
                    alpha = 256;
                Drawer.Draw(
                    TextureStatic.Get("questionBG"),
                    new Rectangle(-500 * (int)scale, -350 * (int)scale, 1000 * (int)scale, 200 * (int)scale),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, alpha),
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9f);
            }
            if(readyToAsk)
            {
                Drawer.Draw(
                    TextureStatic.Get("questionBG"),
                    new Rectangle(-500 * (int)scale, -350 * (int)scale, 1000 * (int)scale, 200 * (int)scale),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, 256),
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9f);
                String tString = question;
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
                    );

                
                //Drawer.DrawString(new SpriteFont(), 
            }
            if (asked)
            {
                Color a = Color.WhiteSmoke;
                Color x = Color.WhiteSmoke;
                Color b = Color.WhiteSmoke;
                Color y = Color.WhiteSmoke;
                if (reveal)
                {
                    a = Color.Red;
                    x = Color.Red;
                    b = Color.Red;
                    y = Color.Red;
                    switch (rightNum)
                    {
                        case 0:
                            a = Color.LightGreen;
                            break;
                        case 1:
                            y = Color.LightGreen;
                            break;
                        case 2:
                            x = Color.LightGreen;
                            break;
                        case 3:
                            b = Color.LightGreen;
                            break;
                        default:
                            break;
                    }
                }

                    
                Drawer.Draw(
                    TextureStatic.Get("questionBG"),
                    new Rectangle(-505 * (int)scale, 205 * (int)scale, 500 * (int)scale, 100 * (int)scale),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, 200),
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9f);
                Drawer.Draw(
                    TextureStatic.Get("Abutton"),
                    new Rectangle((-505 + 33) * (int)scale, 255 * (int)scale, 46 * (int)scale, 46 * (int)scale),
                    null,
                    Color.White,
                    0f,
                    new Vector2(23,23),
                    SpriteEffects.None,
                    .999f);

                Vector2 textSize = Cloud.fontAnswer.MeasureString(WrapText(Cloud.fontAnswer, answers.ElementAt(0), 434));
                Drawer.DrawString(Cloud.fontAnswer,
                    WrapText(Cloud.fontAnswer, answers.ElementAt(0), 434),
                    new Vector2((-505 + 33 + 233) * (int)scale, 255 * (int)scale),
                    a,
                    0.0f,
                    new Vector2(textSize.X / 2, textSize.Y / 2),
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.99f
                    );

                Drawer.Draw(
                    TextureStatic.Get("questionBG"),
                    new Rectangle(-505 * (int)scale, 100 * (int)scale, 500 * (int)scale, 100 * (int)scale),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, 200),
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9f);
                Drawer.Draw(
                    TextureStatic.Get("Xbutton"),
                    new Rectangle((-503 + 33) * (int)scale, 150 * (int)scale, 46 * (int)scale, 46 * (int)scale),
                    null,
                    Color.White,
                    0f,
                    new Vector2(23, 23),
                    SpriteEffects.None,
                    .999f);
                textSize = Cloud.fontAnswer.MeasureString(WrapText(Cloud.fontAnswer, answers.ElementAt(2), 434));
                Drawer.DrawString(Cloud.fontAnswer,
                    WrapText(Cloud.fontAnswer, answers.ElementAt(2), 434),
                    new Vector2((-505 + 33 + 233) * (int)scale, 155 * (int)scale),
                    x,
                    0.0f,
                    new Vector2(textSize.X / 2, textSize.Y / 2),
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.99f
                    );



                Drawer.Draw(
                    TextureStatic.Get("questionBG"),
                    new Rectangle(5 * (int)scale, 205 * (int)scale, 500 * (int)scale, 100 * (int)scale),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, 200),
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9f);
                Drawer.Draw(
                    TextureStatic.Get("Bbutton"),
                    new Rectangle((5 + 33) * (int)scale, 255 * (int)scale, 46 * (int)scale, 46 * (int)scale),
                    null,
                    Color.White,
                    0f,
                    new Vector2(23, 23),
                    SpriteEffects.None,
                    .999f);

                textSize = Cloud.fontAnswer.MeasureString(WrapText(Cloud.fontAnswer, answers.ElementAt(3), 434));
                Drawer.DrawString(Cloud.fontAnswer,
                    WrapText(Cloud.fontAnswer, answers.ElementAt(3), 434),
                    new Vector2((5 + 33 + 233) * (int)scale, 255 * (int)scale),
                    b,
                    0.0f,
                    new Vector2(textSize.X / 2, textSize.Y / 2),
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.99f
                    );

                Drawer.Draw(
                    TextureStatic.Get("questionBG"),
                    new Rectangle(5 * (int)scale, 100 * (int)scale, 500 * (int)scale, 100 * (int)scale),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, 200),
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9f);
                Drawer.Draw(
                    TextureStatic.Get("Ybutton"),
                    new Rectangle((5 + 33) * (int)scale, 150 * (int)scale, 46 * (int)scale, 46 * (int)scale),
                    null,
                    Color.White,
                    0f,
                    new Vector2(23, 23),
                    SpriteEffects.None,
                    .999f);
                textSize = Cloud.fontAnswer.MeasureString(WrapText(Cloud.fontAnswer, answers.ElementAt(1), 434));
                Drawer.DrawString(Cloud.fontAnswer,
                    WrapText(Cloud.fontAnswer, answers.ElementAt(1), 434),
                    new Vector2((5 + 33 + 233) * (int)scale, 155 * (int)scale),
                    y,
                    0.0f,
                    new Vector2(textSize.X / 2, textSize.Y / 2),
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.99f
                    );
            }
            if (startTimer)
            {
                int x = -23 * (int)scale;
                int y = 15 * (int)scale;
                int timeLeft = (int)(10 - sw.ElapsedMilliseconds / 1000);
                if (timeLeft < 0)
                    timeLeft = 0;
                Drawer.DrawString(Cloud.fontCollege,
                    (timeLeft).ToString(),
                    new Vector2(x, y),
                    Color.WhiteSmoke,
                    0.0f,
                    Vector2.Zero,
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.99f
                    );
                Drawer.DrawString(Cloud.fontCollege,
                    (timeLeft).ToString(),
                    new Vector2(x-1, y-1),
                    Color.Black,
                    0.0f,
                    Vector2.Zero,
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.98f
                    );
                Drawer.DrawString(Cloud.fontCollege,
                    (timeLeft).ToString(),
                    new Vector2(x+1, y+1),
                    Color.Black,
                    0.0f,
                    Vector2.Zero,
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.98f
                    );
                Drawer.DrawString(Cloud.fontCollege,
                    (timeLeft).ToString(),
                    new Vector2(x, y-1),
                    Color.Black,
                    0.0f,
                    Vector2.Zero,
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.98f
                    );
                Drawer.DrawString(Cloud.fontCollege,
                    (timeLeft).ToString(),
                    new Vector2(x-1, y),
                    Color.Black,
                    0.0f,
                    Vector2.Zero,
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.98f
                    );
                Drawer.DrawString(Cloud.fontCollege,
                    (timeLeft).ToString(),
                    new Vector2(x + 1, y),
                    Color.Black,
                    0.0f,
                    Vector2.Zero,
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.98f
                    );
                Drawer.DrawString(Cloud.fontCollege,
                    (timeLeft).ToString(),
                    new Vector2(x, y +1),
                    Color.Black,
                    0.0f,
                    Vector2.Zero,
                    1.0f * (int)scale,
                    SpriteEffects.None,
                    0.98f
                    );
                
                Drawer.Draw(
                    TextureStatic.Get("playerAnswerBG"),
                    new Rectangle(-505 * (int)scale, 50 * (int)scale, 500 * (int)scale, 50 * (int)scale),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, 200),
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9999f);
                Drawer.Draw(
                    TextureStatic.Get("playerAnswerBG"),
                    new Rectangle(5 * (int)scale, 50 * (int)scale, 500 * (int)scale, 50 * (int)scale),
                    null,
                    Color.FromNonPremultiplied(256, 256, 256, 200),
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    .9999f);
                if (answered1 != -1)
                {
                    if (sw.ElapsedMilliseconds / 1000 < 10)
                    {
                        Drawer.Draw(
                            TextureStatic.Get("unknown"),
                            new Rectangle(-70 * (int)scale, 75 * (int)scale, 46 * (int)scale, 46 * (int)scale),
                            null,
                            Color.White,
                            0f,
                            new Vector2(23, 23),
                            SpriteEffects.None,
                            .99991f);
                    }
                    else
                    {
                        Texture2D tx = null;
                        if (answered1 == 0)
                        {
                            tx = TextureStatic.Get("Xbutton");
                        }
                        if (answered1 == 1)
                        {
                            tx = TextureStatic.Get("Ybutton");
                        }
                        if (answered1 == 2)
                        {
                            tx = TextureStatic.Get("Abutton");
                        }
                        if (answered1 == 3)
                        {
                            tx = TextureStatic.Get("Bbutton");
                        }
                        Drawer.Draw(
                            tx,
                            new Rectangle(-70 * (int)scale, 75 * (int)scale, 46 * (int)scale, 46 * (int)scale),
                            null,
                            Color.White,
                            0f,
                            new Vector2(23, 23),
                            SpriteEffects.None,
                            .99991f);
                    }
                    Drawer.DrawString(
                        Cloud.fontAnswer,
                        "Player 1 Answered:",
                        new Vector2(-440 * (int)scale, 50 * (int)scale),
                        Color.White,
                        0f,
                        Vector2.Zero,
                        1f * (int)scale,
                        SpriteEffects.None,
                        .99991f);
                }
                if (answered2 != -1)
                {
                    if (sw.ElapsedMilliseconds / 1000 < 10)
                    {
                        Drawer.Draw(
                            TextureStatic.Get("unknown"),
                            new Rectangle(430 * (int)scale, 75 * (int)scale, 46 * (int)scale, 46 * (int)scale),
                            null,
                            Color.White,
                            0f * (int)scale,
                            new Vector2(23, 23),
                            SpriteEffects.None,
                            .99991f);
                    }
                    else
                    {
                        Texture2D tx = null;
                        if (answered2 == 0)
                        {
                            tx = TextureStatic.Get("Xbutton");
                        }
                        if (answered2 == 1)
                        {
                            tx = TextureStatic.Get("Ybutton");
                        }
                        if (answered2 == 2)
                        {
                            tx = TextureStatic.Get("Abutton");
                        }
                        if (answered2 == 3)
                        {
                            tx = TextureStatic.Get("Bbutton");
                        }
                        Drawer.Draw(
                            tx,
                            new Rectangle(430 * (int)scale, 75 * (int)scale, 46 * (int)scale, 46 * (int)scale),
                            null,
                            Color.White,
                            0f,
                            new Vector2(23, 23),
                            SpriteEffects.None,
                            .99991f);
                    }
  

                    Drawer.DrawString(
                        Cloud.fontAnswer,
                        "Player 2 Answered:",
                        new Vector2(65 * (int)scale, 50 * (int)scale),
                        Color.White,
                        0f,
                        Vector2.Zero,
                        1f * (int)scale,
                        SpriteEffects.None,
                        .99991f);

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
        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder sb = new StringBuilder();

            float lineWidth = 0f;

            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }
    }
}