using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Renderables;
using CloudJourney.Framework;

namespace CloudJourney.Framework.Collision
{
    public enum Side
    {
        NONE,
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }

    public class CollisionData
    {
        public Actor collider { get; private set; }
        public Actor collidee { get; private set; }
        public Side side { get; private set; }

        public CollisionData(Actor a1, Actor a2, Side s)
        {
            collider = a1;
            collidee = a2;
            side = s;
        }
    }
}
