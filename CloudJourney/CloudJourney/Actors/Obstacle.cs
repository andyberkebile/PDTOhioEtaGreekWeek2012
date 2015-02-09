using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Collision;
using CloudJourney.Framework.Scenes;
using CloudJourney.Actors;
using CloudJourney.Scenes;
using CloudJourney.Actors.Obstacles;

namespace CloudJourney.Framework.Renderables
{
    public class Obstacle : Actor
    {
        private float lastPlayerCol { get; set; }

        public Object parent { get; protected set; }
        public Sprite sprite { get; protected set; }

        public float initialWeight { get; set; }
        public float rotVel {get; set;}
        public float weight {get; set;}

        public bool hit { get; set; }

        public Vector2 position { get; set; }
        public Vector2 velocity { get { return vel; } set { vel = value; } }
        public float depth { get; set; }

        public float accel { get; set; }
        
        private Vector2 vel;

        public void Update()
        {
            lastPlayerCol = 0;
            if (this.position.X < -((Cloud.screenWidth * (1 / Camera2D.cam.Zoom)) / 2 + 200))
            {
                (parent as Scene).RemoveRenderable(this);
            }
            CollisionData cd = Physics.collisions.CheckNextPosition(this);
            if (cd != null)
            {
                if (cd.collidee is Player)
                {
                    accel = -2;
                    velocity = new Vector2(GameScene.worldSpeed - 5, -27);
                    rotVel = 75;
                    hit = true;
                    cd.collidee.velocity = new Vector2(-100, cd.collidee.velocity.Y);
                    GameScene.vanSpeedA = 20;
                    Cloud.controller.Rumble(0.7f, 0.7f, 800000);

                    lastPlayerCol = GameScene.counter;

                    if (this is car)
                    {
                        cd.collidee.position = new Vector2(cd.collidee.position.X + 5 * GameScene.worldSpeed, cd.collidee.position.Y);
                    }
                }
                else
                {
                    if (this is car && GameScene.counter - lastPlayerCol > 60)
                    {
                        hit = false;
                    }
                }

            }
            else
            {
                if (this is car && GameScene.counter - lastPlayerCol >60)
                {
                    hit = false;
                }
            }
            if(!hit)
            {
                depth = .7f + ((this.position.Y) / 2000f);
                velocity = new Vector2(GameScene.worldSpeed, 0);
                position = new Vector2(position.X + this.velocity.X, position.Y);
                
            }

            if (hit)
            {
                    depth = 1f;
                    if (this is Mailbox)
                    {
                        velocity = new Vector2(velocity.X, velocity.Y - accel);
                        position = new Vector2(position.X + velocity.X, position.Y + velocity.Y);
                        this.sprite.rotation += rotVel;
                    }
                    if (this is crate)
                    {
                        Particle plank1 = new Particle(this.position, (Scene)this.parent, true, 1f, this.depth);
                       
                        Particle plank2 = new Particle(this.position, (Scene)this.parent, true, 1f, this.depth);
                        Particle plank3 = new Particle(this.position, (Scene)this.parent, true, 1f, this.depth);
                        Particle plank4 = new Particle(this.position, (Scene)this.parent, true, 1f, this.depth);
                        Particle plank5 = new Particle(this.position, (Scene)this.parent, true, 1f, this.depth);
                        Particle plank6 = new Particle(this.position, (Scene)this.parent, true, 1f, this.depth);
                        (parent as Scene).AddRenderable(plank1);
                        (parent as Scene).AddRenderable(plank2);
                        (parent as Scene).AddRenderable(plank3);
                        (parent as Scene).AddRenderable(plank4);
                        (parent as Scene).AddRenderable(plank5);
                        (parent as Scene).AddRenderable(plank6);
                        (parent as Scene).RemoveRenderable(this);
                    }


            }


            
            
        }

        public void Draw()
        {
            sprite.depth = depth;
            sprite.position = position;
            sprite.Draw();
        }

        public void Unload()
        {
            sprite.Unload();
        }

        public float getWeight()
        {
            return weight;
        }
    }
}
