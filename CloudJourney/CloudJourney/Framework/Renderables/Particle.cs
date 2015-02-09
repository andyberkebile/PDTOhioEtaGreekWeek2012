using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Renderables;
using CloudJourney.Framework.Scenes;
using CloudJourney.Scenes;

namespace CloudJourney.Actors
{
    public class Particle : Renderable
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public float depth { get; set; }
        public Object parent { get; private set; }
        public Sprite sprite { get; private set; }
        public Boolean Crumbles { get; set; }
        public int lifecycle { get; set; }
        public float spriteScale { get; set; }
        public float rotation { get; set; }
        public float rotVel {get; set;}

        public Particle(Vector2 pos, Scene par, Boolean crumbles, float scaleTrack, float d)
        {
            spriteScale = scaleTrack;
            lifecycle = 10;
            position = pos;
            parent = par;
            depth = d;
            int randImg = Cloud.randCloud.Next(3);
            if (randImg == 0)
            {
                sprite = new Sprite("plank1", @"Art/Obstacles", position, Vector2.Zero, this);
            }
            if (randImg == 1)
            {
                sprite = new Sprite("plank2", @"Art/Obstacles", position, Vector2.Zero, this);
            }
            if (randImg == 2)
            {
                sprite = new Sprite("plank3", @"Art/Obstacles", position, Vector2.Zero, this);
            }
            sprite.blend = Color.White;
            sprite.depth = depth;
            sprite.scale = spriteScale;
            Crumbles = crumbles;

            lifecycle = 0;

            this.velocity = new Vector2(GameScene.worldSpeed - Cloud.randCloud.Next(100), Cloud.randCloud.Next(20)+30);
            rotation = Cloud.randCloud.Next(100) / 50;
            rotVel = Cloud.randCloud.Next(100);
            //sprite.scale = 0.5f;
        }

        public void Update()
        {
            /*
            // nuffin muffin
            Random randNum = new Random(5);
            float num = Cloud.randCloud.Next(10);
            if (Crumbles)
            {
                randNum = new Random();
                num = Cloud.randCloud.Next(3);
                //Console.Write(num);
                if (num > 8 && lifecycle <= 0)
                {
                    lifecycle = 8;
                    Console.Write(num);
                    Particle split = new Particle(new Vector2(position.X + num, position.Y + 3), this.parent as Scene, true, spriteScale * .6f);
                    Particle split2 = new Particle(new Vector2(position.X - num, position.Y + 2), this.parent as Scene, true, spriteScale * .6f);
                    split.velocity = new Vector2(this.velocity.X - (Cloud.randCloud.Next(3)), this.velocity.Y + Cloud.randCloud.Next(3));
                    split2.velocity = new Vector2(this.velocity.X - (Cloud.randCloud.Next(3)), this.velocity.Y + Cloud.randCloud.Next(3));
                    //split.sprite.scale = split.sprite.scale * .5f;
                    //split2.sprite.scale = split2.sprite.scale * .5f;
                    split.rotation = 0;
                    split.rotVel = Cloud.randCloud.Next(100) - 50;
                    split2.rotation = 0;
                    split2.rotVel = Cloud.randCloud.Next(100) - 50;
                    if (split.sprite.scale > .2f)
                    {
                        (parent as Scene).AddRenderable(split);
                    }
                    if (split2.sprite.scale > .2f)
                    {
                        (parent as Scene).AddRenderable(split2);
                    }
                    (parent as Scene).RemoveRenderable(this);

                }
                else
                {
                    lifecycle--;
                }
                this.sprite.rotation = rotation + rotVel;
                this.position = new Vector2(position.X + velocity.X - GameScene.vanSpeed, position.Y + velocity.Y);
             * */

            this.velocity = new Vector2(this.velocity.X, this.velocity.Y - 2);
            this.position = new Vector2(this.position.X + this.velocity.X, this.position.Y - this.velocity.Y);
            rotation += rotVel;
            this.sprite.rotation= rotation;
            lifecycle++;
            if (lifecycle > 1000)
            {
                (parent as Scene).RemoveRenderable(this);
            }
        }

        public void Draw()
        {
            sprite.position = position;
            sprite.Draw();
            Update();
        }

        public void Unload()
        {
            sprite.Unload();
        }
    }
}
