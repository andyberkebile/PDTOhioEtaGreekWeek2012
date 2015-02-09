using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Renderables;
using CloudJourney.Framework.Collision;

namespace CloudJourney.Framework
{
    public class Physics
    {
        public static Vector2 gravity = new Vector2(0.0f, 0.0f);
        public static Vector2 friction = new Vector2(0.3f, 0.3f);
        public static CollisionHandler collisions;

        public static void Do(Actor r)
        {
            // Remember old position
            Vector2 oldpos = r.position;

            // Get collision side
            CollisionData col = collisions.CheckCollision(r);

            // Do gravity
            r.velocity += Physics.gravity;

            // Do vertical friction
            if (r.velocity.Y < -Physics.friction.Y) r.velocity += Physics.friction * Vector2.UnitY;
            else
                if (r.velocity.Y > Physics.friction.Y) r.velocity -= Physics.friction * Vector2.UnitY;
                else
                    r.velocity *= Vector2.UnitX;

            // Do horizontal friction
            if (r.velocity.X < -Physics.friction.X) r.velocity += Physics.friction * Vector2.UnitX;
            else
                if (r.velocity.X > Physics.friction.X) r.velocity -= Physics.friction * Vector2.UnitX;
                else
                    r.velocity *= Vector2.UnitY;

            // Update position and/or velocity
            if (collisions.NextPositionFreeX(r))
                r.position += r.velocity * Vector2.UnitX;
            else
            {
                int sign = Math.Sign(r.velocity.X);
                Vector2 p = new Vector2(r.position.X, r.position.Y);
                while (collisions.PositionFree(r, p))
                    p.X += sign;
                p.X -= sign;
                r.position = p;
                r.velocity = r.velocity * Vector2.UnitY;
            }

            if (collisions.NextPositionFreeY(r))
                r.position += r.velocity * Vector2.UnitY;
            else
            {
                int sign = Math.Sign(r.velocity.Y);
                Vector2 p = new Vector2(r.position.X, r.position.Y);
                while(collisions.PositionFree(r,p))
                    p.Y += sign;
                p.Y -= sign;
                r.position = p;
                r.velocity = r.velocity * Vector2.UnitX;
            }
        }
    }
}
