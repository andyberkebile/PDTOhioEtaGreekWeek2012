using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Renderables;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Scenes;
using CloudJourney.Framework;
using CloudJourney.Framework.Input;
using CloudJourney.Framework.Collision;
using CloudJourney.Scenes;

namespace CloudJourney.Actors
{
    public class Player : Actor
    {
        public float deathCount { set; get; }
        public bool startDeath { set; get; }
        public bool isDead { get; set; }
        public Vector2 position { get; set; }
        public Vector2 velocity { get { return vel; } set { vel = value; } }
        public float depth { get; set; }
        public Object parent { get; private set; }
        public Sprite sprite { get; private set; }
        private Vector2 vel;
        public float weight;
        public float momentum;
        private Sprite wheelL;
        private Sprite wheelR;

        public Player(Vector2 pos, Scene par)
        {
            startDeath = false;
            isDead = false;
            weight = 30;
            position = pos;
            depth = 0.9f;
            parent = par;
            /*anispr = new AnimatedSprite("character", @"Art\Character", pos, new Vector2(8f, 24.0f), new Vector2(16.0f, 24.0f), this);
            anispr.frameCount.Add(4);
            anispr.speed = 0.2f;
            anispr.depth = depth;*/
            sprite = new Sprite("bus", @"Art\Character", Vector2.Zero, new Vector2(150,120), this);//anispr;
            sprite.bounding = new Vector2(300, 80);
            sprite.bposition = new Vector2(150, 70);
            vel = new Vector2(0, 0);
            wheelL = new Sprite("wheel", @"Art\Character", Vector2.Zero, new Vector2(20.5f,20), this);
            wheelR = new Sprite("wheel", @"Art\Character", Vector2.Zero, new Vector2(20.5f, 20), this);
        }

        public void Update()
        {

        }

        public void Draw()
        {
            sprite.position = new Vector2(position.X, position.Y - Cloud.randCloud.Next(3));
            sprite.Draw();

            Vector2 wL = new Vector2(position.X - 95, position.Y);
            wheelL.position = Rotations.RotatePoint(wL, position, sprite.rotation);
            Vector2 wR = new Vector2(position.X + 95, position.Y);
            wheelR.position = Rotations.RotatePoint(wR, position, sprite.rotation);

            wheelL.rotation += 10 + GameScene.vanSpeed;
            wheelR.rotation += 10 + GameScene.vanSpeed;

            wheelL.depth = sprite.depth + 0.00001f;
            wheelR.depth = sprite.depth + 0.00001f;

            wheelL.Draw();
            wheelR.Draw();
        }

        public void Unload()
        {

        }
    }
}
