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
using CloudJourney.Framework.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace CloudJourney.Actors
{
    class ball : Actor
    {
        public Vector2 shotLocation = new Vector2(0, 0);
        private bool dowling = false;
        public float deathCount { set; get; }
        public bool startDeath { set; get; }
        public bool isDead { get; set; }
        public Vector2 position { get; set; }
        public Vector2 velocity { get { return vel; } set { vel = value; } }
        public float depth { get; set; }
        public Object parent { get; private set; }
        public Sprite sprite { get; private set; }
        public Sprite arrowSprite { get; private set; }
        public Sprite barSprite { get; private set; }
        public Sprite arrowSprite2 { get; private set; }
        public Sprite barSprite2 { get; private set; }
        private Vector2 vel;
        private float arrowPoint;
        public float power;
        public Boolean powerUp;
        public Boolean isShooting;
        public Boolean hasShot;
        public float gravity;
        public float rotation;
        public Boolean frontRim;
        public float theta;
        public Vector2 tempVel;
        public Boolean backFlat;

        private BasketballScene bs;

        private bool under = false;
        private bool over = false;
        public Vector2 lastPos = new Vector2(1,1);

        public float lastPower = 0;
        public float lastArrowPoint = 0;

        public Boolean bounce = false;

        private bool nextShot = true;

        private bool overRim = false;

        private bool good = false;
        public ball(Vector2 pos, Scene par)
        {
            backFlat = false;
            tempVel = Vector2.Zero;
            theta = 0;
            frontRim = true;
            rotation = 0;
            gravity = .5f;
            isShooting = false;
            hasShot = false;
            power = 0;
            powerUp = true;
            arrowPoint = 0;
            position = shotLocation;
            depth = 0.9f;
            parent = par;
            /*anispr = new AnimatedSprite("character", @"Art\Character", pos, new Vector2(8f, 24.0f), new Vector2(16.0f, 24.0f), this);
            anispr.frameCount.Add(4);
            anispr.speed = 0.2f;
            anispr.depth = depth;*/
            sprite = new Sprite("bball", @"Art", Vector2.Zero, new Vector2(73, 75), this);//anispr;
            arrowSprite = new Sprite("arrow", @"Art", Vector2.Zero, new Vector2(-30, 20), this);//anispr;
            barSprite = new Sprite("bar", @"Art", Vector2.Zero, new Vector2(6, 19), this);//anispr;
            arrowSprite2 = new Sprite("arrow", @"Art", Vector2.Zero, new Vector2(-30, 20), this);//anispr;
            barSprite2 = new Sprite("bar", @"Art", Vector2.Zero, new Vector2(6, 19), this);//anispr;
            //sprite.bounding = new Vector2(300, 80);
            //sprite.bposition = new Vector2(150, 70);
            vel = new Vector2(0, 0);
            sprite.scale = .25f;
            //Console.WriteLine(sprite.dimensions);
        }

        public void Update()
        {

             /*-380, -87
             *-380, -92
             *-387, -87
             *-397, -92
              */


            /**********************************
             * 
             * Front Rim Deflection
             * 
             **********************************/
            List<float> frontRimX = new List<float>();
            frontRimX.Add(-380);
            frontRimX.Add(-387);
            frontRimX.Add(-384);

            List<float> frontRimY = new List<float>();
            frontRimY.Add(-92);
            frontRimY.Add(-87);
            frontRimY.Add(-90);

            frontRim = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    float mag = (float)Math.Sqrt(Math.Pow((position.X - frontRimX.ElementAt(i)), 2) + Math.Pow((double)(position.Y - frontRimY.ElementAt(j)), 2));
                    if (mag <= 18)
                        frontRim = true;
                }
            }

            int hitX = 0;
            int hitY = 0;
            float minMag = 1000;


            if (frontRim)
            {
                while(frontRim)
                {
                    position = new Vector2(position.X - (velocity.X/20), position.Y - (velocity.Y/20));
                    frontRim = false;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            float mag = (float)Math.Sqrt(Math.Pow((position.X - frontRimX.ElementAt(i)), 2) + Math.Pow((double)(position.Y - frontRimY.ElementAt(j)), 2));
                            if (mag <= 18)
                            {
                                if (mag < minMag)
                                {
                                    hitX = i;
                                    hitY = j;
                                    minMag = mag;
                                }
                                frontRim = true;
                            }
                        }
                    }
                }
                if (position.X > frontRimX.ElementAt(hitX))
                {
                    if (position.Y < frontRimY.ElementAt(hitY))
                        theta = (float)Math.Atan(Math.Abs((double)(frontRimY.ElementAt(hitY) - position.Y)) / (-1 * Math.Abs((double)(position.X - frontRimX.ElementAt(hitX))))) + (float)Math.PI;
                    else
                        theta = (float)Math.Atan((-1*Math.Abs((double)(frontRimY.ElementAt(hitY) - position.Y))) / (-1*Math.Abs((double)(position.X - frontRimX.ElementAt(hitX))))) + (float)Math.PI;
                }
                else
                {
                    if (position.Y < frontRimY.ElementAt(hitY))
                        theta = (float)Math.Atan(Math.Abs((double)(frontRimY.ElementAt(hitY) - position.Y)) / (Math.Abs((double)(position.X - frontRimX.ElementAt(hitX)))));
                    else
                        theta = (float)Math.Atan(-1*Math.Abs((double)(frontRimY.ElementAt(hitY) - position.Y)) / (Math.Abs((double)(position.X - frontRimX.ElementAt(hitX)))));
                }
                
                
                tempVel = new Vector2(velocity.X, velocity.Y);

                if ((position.X < frontRimX.ElementAt(hitX) && position.Y < frontRimY.ElementAt(hitY)))
                {
                    velocity = new Vector2(-(tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), -(-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta)));
                }
                if ((position.X > frontRimX.ElementAt(hitX) && position.Y > frontRimY.ElementAt(hitY)))
                {
                    velocity = new Vector2((tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), (-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta)));
                }
                if ((position.X > frontRimX.ElementAt(hitX) && position.Y < frontRimY.ElementAt(hitY)))
                {
                    velocity = new Vector2((tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), (-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta)));
                }
                if ((position.X < frontRimX.ElementAt(hitX) && position.Y > frontRimY.ElementAt(hitY)))
                {
                    velocity = new Vector2(-(tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), (-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta)));
                }
                Console.WriteLine(theta);
            }

            /****************************
             * 
             * Horizontal Flat Thing Deflection
             * 
             ****************************/
            List<float> backFlatX = new List<float>();
            backFlatX.Add(-473);
            backFlatX.Add(-447);
            backFlatX.Add(-460);

            List<float> backFlatY = new List<float>();
            backFlatY.Add(-90);
            backFlatY.Add(-80);
            backFlatY.Add(-85);

            backFlat = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    float mag = (float)Math.Sqrt(Math.Pow((position.X - backFlatX.ElementAt(i)), 2) + Math.Pow((double)(position.Y - backFlatY.ElementAt(j)), 2));
                    if (mag <= 18)
                        backFlat = true;
                }
            }

            hitX = 0;
            hitY = 0;
            minMag = 1000;

            if (backFlat)
            {
                while (backFlat)
                {
                    position = new Vector2(position.X - (velocity.X / 20), position.Y - (velocity.Y / 20));
                    backFlat = false;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            float mag = (float)Math.Sqrt(Math.Pow((position.X - backFlatX.ElementAt(i)), 2) + Math.Pow((double)(position.Y - backFlatY.ElementAt(j)), 2));
                            if (mag <= 18)
                            {
                                if (mag < minMag)
                                {
                                    hitX = i;
                                    hitY = j;
                                    minMag = mag;
                                }
                                backFlat = true;
                            }
                        }
                    }
                }

                if (position.Y <= (-90 - 18))
                {
                    velocity = new Vector2(velocity.X, -velocity.Y);
                    Console.WriteLine("Ballsack 1");
                }
                else
                {
                    if (position.X >= (-460 + 18))
                    {
                        Console.WriteLine("Ballsack 2");
                        velocity = new Vector2(-velocity.X, velocity.Y);
                    }
                    else
                    {
                        Console.WriteLine("Ballsack 3");
                        velocity = new Vector2(-velocity.X, -velocity.Y);

                        /*
                        tempVel = new Vector2(velocity.X, velocity.Y);

                        //Left + UP
                        if ((position.X <= backFlatX.ElementAt(hitX) && position.Y <= backFlatY.ElementAt(hitY)))
                        {
                            Console.WriteLine("Titty 1");
                            velocity = new Vector2(-(tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), -(-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta)));
                        }
                        //Right + Down
                        if ((position.X > backFlatX.ElementAt(hitX) && position.Y > backFlatY.ElementAt(hitY)))
                        {
                            Console.WriteLine("Titty 2"); 
                            velocity = new Vector2((tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), (-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta)));
                        }
                        //Right + UP
                        if ((position.X > backFlatX.ElementAt(hitX) && position.Y <= backFlatY.ElementAt(hitY)))
                        {
                            Console.WriteLine("Titty 3");
                            velocity = new Vector2((tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), -(-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta)));
                        }
                        //Left + Down
                        if ((position.X <= backFlatX.ElementAt(hitX) && position.Y > backFlatY.ElementAt(hitY)))
                        {
                            Console.WriteLine("Titty 4");
                            velocity = new Vector2(-(tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), (-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta)));
                        }
                        */
                    }
                }
            }

            if (Cloud.controller.ContainsBool(ActionType.XButton) | bounce)
            {
                //nextShot = false;
                over = false;
                under = false;
                
                bounce = false;
                dowling = false;
                isShooting = false;
                hasShot = false;
                if (good)
                {
                    if ((parent as BasketballScene).reset)
                        (parent as BasketballScene).score2 = (parent as BasketballScene).score2 + 50 + (int)(50 * (shotLocation.X / 575));
                    else
                        (parent as BasketballScene).score1 = (parent as BasketballScene).score1 + 50 + (int)(50 * (shotLocation.X / 575));

                    shotLocation = new Vector2(Cloud.randCloud.Next(1, 575),0);
                }
                position = new Vector2(shotLocation.X, shotLocation.Y);
                velocity = new Vector2(0, 0);
                good = false;
            }

            float x = Cloud.controller.ContainsFloat(ActionType.MoveHorizontal);
            float y = Cloud.controller.ContainsFloat(ActionType.MoveVertical);

            if (x != 0 && y != 0)
            {
                arrowPoint = (float)Math.Atan((double)(-y / x));
                if (x < 0)
                {
                    arrowPoint = arrowPoint + (float)Math.PI;
                }
            }
            if (x == 0)
            {
                if (y > 0)
                {
                    arrowPoint = -.5f * (float)Math.PI;
                }
                if(y < 0)
                {
                    arrowPoint = .5f * (float)Math.PI;
                }
            }
            if (y == 0)
            {
                if (x > 0)
                {
                    arrowPoint = 0;
                }
                if (x < 0)
                {
                    arrowPoint = (float)Math.PI;
                }
            }
            
            if (Cloud.controller.ContainsBool(ActionType.AButton) && !dowling && (parent as BasketballScene).s3)
            {
                isShooting = true;
                if (powerUp)
                {
                    power = power + 2;
                    if (power >= 100)
                    {
                        powerUp = false;
                    }
                }
                else
                {
                    power = power - 2;
                    if (power <= 0)
                    {
                        powerUp = true;
                    }
                }
            }
            else
            {
                if (isShooting)
                {
                    overRim = false;
                    bounce = false;
                    lastArrowPoint = arrowPoint;
                    lastPower = power;

                    nextShot = false;
                    good = false;
                    dowling = true;
                    float maxVel = 30 * (power/100);
                    velocity = new Vector2((float)Math.Cos(arrowPoint) * maxVel, (float)Math.Sin(arrowPoint) * maxVel);
                    //Console.WriteLine((float)Math.Cos(arrowPoint));
                    isShooting = false;
                    hasShot = true;
                    power = 0;
                }
            }

            //Set Rotation
            rotation = rotation + velocity.X *.1f;

            //Apply gravity above the bounce line
            if (hasShot && position.Y < 275)
            {
                
                velocity = new Vector2(velocity.X, velocity.Y + gravity);
            }
            
            //Bounce if at or past the bounce line and is moving at a legitimate speed
            if (position.Y >= 275 && velocity.Y>2)
            {
                bounce = true;
                velocity = new Vector2(velocity.X * .98f, (velocity.Y) * -.85f );
            }

            //Kill veritcal movement after a certain point
            if (velocity.Y > 0 && velocity.Y < 2 && position.Y >= 275)
            {
                velocity = new Vector2(velocity.X * .98f, 0);
            }


            //GAME
            //if (hasShot || isShooting)
            //{
                position = new Vector2(position.X + velocity.X, position.Y + velocity.Y);
            //}
            //else
            //{
                //DEBUG
            //    position = new Vector2(position.X + Cloud.controller.ContainsFloat(ActionType.MoveHorizontal) * 10, position.Y + Cloud.controller.ContainsFloat(ActionType.MoveVertical) * -10);
            //}
            //backboard bounce
            if (position.Y > -205 && position.Y < -50 && position.X > -473 && position.X + velocity.X < -473)
            {
                velocity = new Vector2(Math.Abs(velocity.X) * .75f, velocity.Y);
            }

            sprite.depth = 0.5f;
            /*if(position.X >-460 && position.X <-387 && position.Y > 90)
            {
                overRim = true;
            }
            //if (position.X > -460 && position.X < -387 && position.Y < 88 && position.Y > 48 && overRim)
            if (lastPos.Y != position.Y && lastPos.X != position.X)
            {
                int slope = (int)(lastPos.Y - position.Y) / (int)(lastPos.X - position.X);
                if (slope != 0)
                {

                    if (-90 / slope > -460 && -90 / slope < -387)
                    {
                        good = true;
                    }
                }
            }*/

            for (int i = 0; i < 40; i++)
            {
                float testX = position.X - (i / 40) * velocity.X;
                float testY = position.Y - (i / 40) * velocity.Y;
                if (testX > -460 && testX < -387 && testY > -90 && testY < -48)
                    under = true;
                if (testX > -460 && testX < -387 && testY <= -90)
                    over = true;
            }
            if (over && under)
                good = true;

            Console.WriteLine(position.X + "\t" + position.Y);

            if (bounce && !good)
            {
                nextShot = false;
            }
            if (good && bounce)
            {
                //over = false;
                //under = false;
                //good = false;
                bounce = true;
                nextShot = true;
            }


            lastPos = new Vector2(position.X, position.Y);
        }

        public void Draw()
        {
            if (good)
            {
                Drawer.DrawString(
                    Cloud.fontCollege,
                    "It's GOOD!",
                    new Vector2(-450, -150),
                    Color.GreenYellow,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    .999999f);
                Drawer.DrawString(
                    Cloud.fontCollege,
                    "It's GOOD!",
                    new Vector2(-450+2, -150+2),
                    Color.Black,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    .999998f);
            }

            float dist = 30 + ((power/100) * 125);

            sprite.rotation = rotation;
            float addY = dist * (float)Math.Sin(arrowPoint);
            float addX = dist * (float)Math.Cos(arrowPoint);
            sprite.position = new Vector2(position.X, position.Y);
            arrowSprite.position = new Vector2(position.X, position.Y);
            barSprite.position = new Vector2(position.X+addX, position.Y+addY);
            barSprite.rotation = arrowPoint;
            arrowSprite.rotation = arrowPoint;

            dist = 30 + ((lastPower / 100) * 125);
            addY = dist * (float)Math.Sin(lastArrowPoint);
            addX = dist * (float)Math.Cos(lastArrowPoint);
            arrowSprite2.position = new Vector2(position.X, position.Y);
            barSprite2.position = new Vector2(position.X + addX, position.Y + addY);
            barSprite2.rotation = lastArrowPoint;
            arrowSprite2.rotation = lastArrowPoint;


            if (frontRim)
                sprite.blend = Color.Blue;
            else
                sprite.blend = Color.White;
            sprite.Draw();
            
            if (!hasShot)
            {
                arrowSprite.Draw();
                barSprite.blend = Color.FromNonPremultiplied(255, (int)(((100 - power) / 100) * 255), 0, 255);
                barSprite.Draw();
                if (!nextShot)
                {
                    arrowSprite2.blend = Color.FromNonPremultiplied(256, 256, 256, 100);
                    arrowSprite2.Draw();
                    barSprite2.blend = Color.FromNonPremultiplied(255, (int)(((100 - lastPower) / 100) * 255), 0, 100);
                    barSprite2.Draw();
                }
            }

            //float diameter = 18;
            //Drawer.DrawLine(new Vector2(sprite.position.X, sprite.position.Y), new Vector2(sprite.position.X + 30 * (float)Math.Cos(theta), sprite.position.Y + 30 * (float)Math.Sin(theta)), 1, sprite.depth + .0001f, Color.Yellow);
            //Drawer.DrawLine(new Vector2(sprite.position.X, sprite.position.Y), new Vector2(sprite.position.X + 30 * tempVel.X, sprite.position.Y + 30 * tempVel.Y), 1, sprite.depth + .0001f, Color.Blue);

            //Drawer.DrawLine(new Vector2(sprite.position.X, sprite.position.Y), new Vector2(sprite.position.X - 30 * (tempVel.X * (float)Math.Cos(theta) + tempVel.Y * (float)Math.Sin(theta)), sprite.position.Y - 30 * (-tempVel.X * (float)Math.Sin(theta) + tempVel.Y * (float)Math.Cos(theta))), 1, sprite.depth + .0001f, Color.Crimson);
        }

        public void Unload()
        {

        }
    }
}
