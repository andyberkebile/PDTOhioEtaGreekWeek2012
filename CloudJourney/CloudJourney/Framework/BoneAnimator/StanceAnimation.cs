using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudJourney.Framework.BoneAnimation
{
    public class StanceAnimation : List<StanceAnimation.StanceAnimationContainer>
    {
        public int Type;

        public class StanceAnimationContainer
        {
            public Stance stance;
            public float speed;
            public float yoffset;

            public StanceAnimationContainer(Stance s, float spd, float yoff)
            {
                stance = s;
                speed = spd;
                yoffset = yoff;
            }
        }

        public StanceAnimation(int type)
            : base()
        {
            Type = type;
        }

        public void AddStance(Stance stance, float speed, float yoffset)
        {
            StanceAnimationContainer s = new StanceAnimationContainer(stance, speed, yoffset);
            this.Add(s);
        }
    }
}
