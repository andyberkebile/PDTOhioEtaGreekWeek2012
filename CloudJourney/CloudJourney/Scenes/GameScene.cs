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

namespace CloudJourney.Scenes
{
    public class GameScene : Scene
    {
        public static bool isDead { set; get; }

        public BoneAnimator animator;
        public BoneAnimator animator2;
        public BoneAnimator animator3;
        public BoneAnimator animator4;
        public BoneAnimator animator5;

        private RenderableCollection renderables;
        private Player player;
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
        private Background roadbg;
        public static Background mainbg;

        public static long timer;

        private Color sky;
        private Color grass;

        public GameScene()
        {
            sky = Color.FromNonPremultiplied(78, 142, 203, 255);
            grass = Color.FromNonPremultiplied(45, 179, 75, 255);

            isDead = false;
            string dir = "BonePix";
            TextureStatic.Load("tentacle_tip_1", dir);
            TextureStatic.Load("tentacle_2", dir);
            TextureStatic.Load("tentacle_3", dir);
            TextureStatic.Load("tentacle_4", dir);
            TextureStatic.Load("tentacle_5", dir);
            TextureStatic.Load("tentacle_base", dir);

            animator = new BoneAnimator(null, StaticStancetentacle.GetBoneSet(), StaticStancetentacle.Get(tentacleStance.Idle), 0, 0);
            animator.Depth = .6f;

            animator2 = new BoneAnimator(null, StaticStancetentacle.GetBoneSet(), StaticStancetentacle.Get(tentacleStance.Walk1), 0, 0);
            animator2.Depth = .7f;

            animator3 = new BoneAnimator(null, StaticStancetentacle.GetBoneSet(), StaticStancetentacle.Get(tentacleStance.Walk4), 0, 0);
            animator3.Depth = .7f;

            animator4 = new BoneAnimator(null, StaticStancetentacle.GetBoneSet(), StaticStancetentacle.Get(tentacleStance.Walk4), 0, 0);
            animator4.Depth = .6f;

            animator5 = new BoneAnimator(null, StaticStancetentacle.GetBoneSet(), StaticStancetentacle.Get(tentacleStance.Walk4), 0, 0);
            animator5.Depth = .7f;

            roadHeight = 200;
            counter = 0;
            worldSpeed = -10;
            particleTimer = 0;
            edgeLine = -250;
            renderables = new RenderableCollection();
            Physics.collisions = new CollisionHandler(renderables);

            toAdd = new List<Renderable>();
            toRemove = new List<Renderable>();

            player = new Player(new Vector2(120, 150), this);
            renderables.Add(player);

            mainbg = new Background("road_railing", @"Art\Backgrounds", new Vector2(-360, 60), new Vector2(-10, 0), 0.15f, true, this);
            renderables.Add(mainbg);
            farbg = new Background("far_bg", @"Art\Backgrounds", new Vector2(-360, -370), new Vector2(-10, 0), 0.1f, true, this);
            renderables.Add(farbg);
            midbg = new Background("middle_bg", @"Art\Backgrounds", new Vector2(-360, -340), new Vector2(-10, 0), 0.12f, true, this);
            renderables.Add(midbg);
            forebg = new Background("foreground_railing", @"Art\Backgrounds", new Vector2(-360, 290), new Vector2(-10, 0), 0.999f, true, this);
            renderables.Add(forebg);
            roadbg = new Background("road", @"Art\Backgrounds", new Vector2(-360, 110), new Vector2(-10, 0), 0.11f, true, this);
            renderables.Add(roadbg);
            /*voidbg = new Background("void", @"Art", new Vector2(-360, -202), Vector2.Zero, 0.2f, false, this);
            renderables.Add(voidbg);*/
            TextureStatic.Load("void", "Art");

            //tentacle tn = new tentacle(new Vector2(0, 0), this);
            //renderables.Add(tn);

            vanSpeed = 0;
            voidLine = -360;
            oscillator = 0;

            animator.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Idle));
            animator.Update();
            animator2.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Walk));
            animator2.Update();
            animator3.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Walk2));
            animator3.Update();
            animator4.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Walk3));
            animator4.Update();
            animator5.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Walk4));
            animator5.Update();
            
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
            timer++;
            counter++;

            Camera2D.cam.Zoom = Math.Max(0.2f, 1 - (voidLine / -4000))-.2f;

            animator.SetOrigin(new Vector2(voidLine- 220, 50));
            animator2.SetOrigin(new Vector2(voidLine - 140, 95));
            animator3.SetOrigin(new Vector2(voidLine - 50, 85));
            animator4.SetOrigin(new Vector2(voidLine - 90, 60));
            animator5.SetOrigin(new Vector2(voidLine - 86, 77));

            animator.Update();
            animator2.Update();
            animator3.Update();
            animator4.Update();
            animator5.Update();

            animator.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Idle));
            animator2.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Walk));
            animator3.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Walk2));
            animator4.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Walk));
            animator5.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Walk2));
            

            int mailGen = Cloud.randCloud.Next(20000);
            int crateGen = Cloud.randCloud.Next(40000);
            int carGen = Cloud.randCloud.Next(80000);
            if((float)(mailGen)<80f+counter/5f)
            {
                float randHeight = (float)(Cloud.randCloud.Next((int)roadHeight));
                Mailbox mb = new Mailbox(new Vector2((Cloud.screenWidth * ((1/Camera2D.cam.Zoom))/2)+200, randHeight+100f), this);
                renderables.Add(mb);
            }
            if ((float)(crateGen) < 80f + counter / 5f)
            {
                float randHeight = (float)(Cloud.randCloud.Next((int)roadHeight));
                crate cr = new crate(new Vector2((Cloud.screenWidth * ((1 / Camera2D.cam.Zoom)) / 2) + 200, randHeight+100f), this);
                renderables.Add(cr);
            }
            if ((float)(carGen) < 80f + counter / 5f && timer>600)
            {
                float randHeight = (float)(Cloud.randCloud.Next((int)roadHeight));
                //car c = new car(new Vector2((Cloud.screenWidth + (1 / Camera2D.cam.Zoom)) / 2 + 200, randHeight), this);
                car c = new car(new Vector2((Cloud.screenWidth * (1 / Camera2D.cam.Zoom)) / 2 + 200, randHeight+100f), this);
                renderables.Add(c);
            }

            //mainbg.velocity= new Vector2(worldSpeed, 0);
            if (vanSpeedA > 0) vanSpeedA -= 1; else vanSpeedA = 0;
            vanSpeed += 0.5f - vanSpeedA;
            mainbg.velocity = new Vector2(mainbg.velocity.X - (vanSpeed / 1000), mainbg.velocity.Y);
            farbg.velocity = new Vector2(mainbg.velocity.X / 5, farbg.velocity.Y);
            midbg.velocity = new Vector2(mainbg.velocity.X / 2, midbg.velocity.Y);
            forebg.velocity = new Vector2(mainbg.velocity.X * 2, forebg.velocity.Y);
            roadbg.velocity = new Vector2(mainbg.velocity.X * 1.25f, farbg.velocity.Y);
            worldSpeed = mainbg.velocity.X;
            edgeLine -= vanSpeed;
            voidLine -= vanSpeed;
            //voidbg.position = new Vector2(voidLine, voidbg.position.Y);

            oscillator = (oscillator + 1.0f) % ((float)Math.PI * 2.0f);

            if (voidLine + 360 > -1080 && !isDead)
            {
                float amt = Math.Min(1 - ((voidLine + 360) / -1080), 1);
                Camera2D.cam.rumble = amt * 10;
                if (amt > 0.3)
                {
                    Cloud.controller.Rumble(
                        amt/* * (((float)Math.Sin(oscillator) + 1) / 2.0f)*/,
                        amt/* * (((float)Math.Sin(oscillator) + 1) / 2.0f)*/,
                        1);
                }
            }

            if (particleTimer <= 0)
            {
                CloudParticle p1 = new CloudParticle(1, new Vector2(edgeLine + 60, 230 + Cloud.randCloud.Next(40) - 20), 0.999f, this);
                CloudParticle p2 = new CloudParticle(2, new Vector2(edgeLine + 60, 150 + Cloud.randCloud.Next(40) - 20), 0.92f, this);
                CloudParticle p3 = new CloudParticle(3, new Vector2(edgeLine + 60, 80 + Cloud.randCloud.Next(40) - 20), 0.7f, this);
                CloudParticle p4 = new CloudParticle(4, new Vector2(edgeLine + 60, -20 + Cloud.randCloud.Next(40) - 20), 0.6f, this);
                //CloudParticle p5 = new CloudParticle(5, new Vector2(edgeLine + 60, 100 + Cloud.randCloud.Next(200) - 100), 0.91f, this);
                renderables.Add(p1);
                renderables.Add(p2);
                renderables.Add(p3);
                renderables.Add(p4);
                //renderables.Add(p5);
                particleTimer = 12 + Cloud.randCloud.Next(10) - 5;
            }
            else
            {
                particleTimer--;
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
            animator.Draw();
            animator2.Draw();
            animator3.Draw();
            animator4.Draw();
            animator5.Draw();

            int halfwid = (int)(Cloud.screenWidth * Math.Pow(Camera2D.cam.Zoom, -1)) / 2;
            int halfhei = (int)(Cloud.screenHeight * Math.Pow(Camera2D.cam.Zoom, -1)) / 2;
            Drawer.Draw(TextureStatic.Get("void"),
                new Rectangle((int)voidLine, -halfhei - 10, 150, halfhei * 2 + 10),
                null,
                Color.Black,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.2f);
            Drawer.Draw(TextureStatic.Get("Blank"),
                new Rectangle(-halfwid - 10, -halfhei - 10, (int)Math.Ceiling(voidLine) + halfwid + 10, halfhei * 2 + 10),
                null,
                Color.Black,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.2f);
            Drawer.Draw(TextureStatic.Get("Blank"),
                new Rectangle(-halfwid - 10, 290, (int)Math.Ceiling(voidLine) + halfwid + 10, halfhei),
                null,
                Color.Black,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.9999f);

            Drawer.Draw(TextureStatic.Get("Blank"),
                new Rectangle(-halfwid - 10, -halfhei - 10, halfwid*2 + 10, halfhei + 10),
                null,
                sky,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.05f);

            Drawer.Draw(TextureStatic.Get("Blank"),
                new Rectangle(-halfwid - 10, 0, halfwid * 2 + 10, halfhei + 10),
                null,
                grass,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.05f);

            Drawer.DrawString(Cloud.fontFixed,
                String.Format("{0:00}:{1:00}:{2:000}", Math.Floor(timer / 3600.0), Math.Floor(timer / 60.0) % 60, Math.Floor(((timer % 60) / 60.0f) * 100.0f)),
                new Vector2(-580, -340),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.99f
                );

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
