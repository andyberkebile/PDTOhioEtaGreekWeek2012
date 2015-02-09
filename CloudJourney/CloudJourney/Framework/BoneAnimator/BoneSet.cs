using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CloudJourney.Framework.BoneAnimation
{
    public class BoneSet : List<Bone>
    {
        public void AddBone(Bone newBone)
        {
            if (newBone.Parent != null)
            {
                int pos = this.IndexOf(newBone.Parent);
                this.Insert(pos+1, newBone);
            }
            else
            {
                this.Insert(0, newBone);
            }
        }

        public void AddBoneMod(Bone parent, float width, float height, float angle, Color color, float alpha)
        {
            Bone newBone = new Bone(parent, Vector2.Zero, Vector2.Zero, width, height, "", 0, angle, color, alpha);

            if (newBone.Parent != null)
            {
                int pos = this.IndexOf(newBone.Parent);
                this.Insert(pos + 1, newBone);
            }
            else
            {
                this.Insert(0, newBone);
            }
        }
    }
}
