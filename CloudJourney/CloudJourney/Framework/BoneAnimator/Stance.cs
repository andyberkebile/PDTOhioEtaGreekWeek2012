using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudJourney.Framework.BoneAnimation
{
    public class Stance
    {
        private BoneSet bones;
        private int type;

        public BoneSet Bones { get { return bones; } }
        public int Type { get { return type; } set { type = (int)value; } }

        public Stance(BoneSet boneSet)
        {
            bones = boneSet;
        }
    }
}
