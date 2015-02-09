using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.BoneAnimation;

namespace CloudJourney.Framework.BoneAnimator.Stances
{
    public enum tentacleStance { Idle, Walk1, Walk2, Walk3, Walk4, Walk5, Walk6, Walk7 };
    public enum tentacleAnimation { Idle, Walk, Walk2, Walk3, Walk4 };

    public class StaticStancetentacle
    {
        // returns the set of bones used for the tentacle (sort of like a model)
        public static BoneSet GetBoneSet()
        {
            /* TODO: Complete this set */
            BoneSet b = new BoneSet();
            // Bone(Parent, Offset, Origin, Length, Width, Texture, Depth, [angle, color, alpha])


            Bone t6 = new Bone(null, Vector2.Zero, new Vector2(0, 50), 89, 105, "tentacle_base", 0.06f);
            Bone t5 = new Bone(t6, new Vector2(-8, 93), new Vector2(15, 30), 68, 69, "tentacle_5", 0.05f);
            Bone t4 = new Bone(t5, new Vector2(8, 40), new Vector2(2, 40), 63, 64, "tentacle_4", 0.04f);
            Bone t3 = new Bone(t4, new Vector2(-10, 53), new Vector2(12, 22), 53, 54, "tentacle_3", 0.03f);
            Bone t2 = new Bone(t3, new Vector2(-2, 34), new Vector2(4, 15), 33, 35, "tentacle_2", 0.02f);
            Bone t1 = new Bone(t2, new Vector2(0, 28), new Vector2(2, 15), 37, 34, "tentacle_tip_1", 0.01f);

            b.AddBone(t6);
            b.AddBone(t5); 
            b.AddBone(t4);
            b.AddBone(t3);
            b.AddBone(t2);
            b.AddBone(t1);

            return b;
        }

        // returns a given stance (these are premade)
        public static Stance Get(tentacleStance s)
        {
            /* TODO: Add stances */
            Stance st;
            BoneSet b = new BoneSet();
            Bone t6;
            Bone t5;
            Bone t4;
            Bone t3;
            Bone t2;
            Bone t1;


            switch (s)
            {
                case tentacleStance.Idle:
                    t6 = new Bone(null, Vector2.Zero, new Vector2(42, 12), 89, 105, "tentacle_base", 0.06f, (float)Math.PI * 1.5f, Color.White, 1.0f);
                    t5 = new Bone(t6, new Vector2(40, 30), new Vector2(0,55), 68, 69, "tentacle_5", 0.05f, (float)Math.PI * 1.77f, Color.White, 1.0f);
                    t4 = new Bone(t5, new Vector2(68, 40), new Vector2(2, 25), 64, 50, "tentacle_4", 0.04f, (float)Math.PI * 1.93f, Color.White, 1.0f);
                    t3 = new Bone(t4, new Vector2(44, 22), new Vector2(12, 22), 54, 53, "tentacle_3", 0.03f, (float)Math.PI * 1.75f, Color.White, 1.0f);
                    t2 = new Bone(t3, new Vector2(44, 21), new Vector2(4, 15), 35, 33, "tentacle_2", 0.02f, (float)Math.PI * 1.70f, Color.White, 1.0f);
                    t1 = new Bone(t2, new Vector2(30, 20), new Vector2(2, 15), 34, 37, "tentacle_tip_1", 0.01f, (float)Math.PI * 1.75f, Color.White, 1.0f);
                    
                    b.AddBone(t6);
                    b.AddBone(t5);
                    b.AddBone(t4);
                    b.AddBone(t3);
                    b.AddBone(t2);
                    b.AddBone(t1);

                    st = new Stance(b);
                    st.Type = (int)tentacleStance.Idle;

                    return st;
                case tentacleStance.Walk1:
                    t6 = new Bone(null, Vector2.Zero, new Vector2(12, 42), 105, 89, "tentacle_base", 0.06f, (float)Math.PI * 1.25f, Color.White, 1.0f);
                    t5 = new Bone(t6, new Vector2(40, 75), new Vector2(0, 60), 69, 68, "tentacle_5", 0.05f, (float)Math.PI * 1.35f, Color.White, 1.0f);
                    t4 = new Bone(t5, new Vector2(68, 35), new Vector2(2, 25), 64, 50, "tentacle_4", 0.04f, (float)Math.PI * 1.25f, Color.White, 1.0f);
                    t3 = new Bone(t4, new Vector2(44, 22), new Vector2(12, 22), 54, 53, "tentacle_3", 0.03f, (float)Math.PI * 1.1f, Color.White, 1.0f);
                    t2 = new Bone(t3, new Vector2(44, 21), new Vector2(4, 15), 35, 33, "tentacle_2", 0.02f, (float)Math.PI * 1.0f, Color.White, 1.0f);
                    t1 = new Bone(t2, new Vector2(30, 15), new Vector2(2, 15), 34, 37, "tentacle_tip_1", 0.01f, (float)Math.PI * 1.0f, Color.White, 1.0f);
                    
                    b.AddBone(t6);
                    b.AddBone(t5);
                    b.AddBone(t4);
                    b.AddBone(t3);
                    b.AddBone(t2);
                    b.AddBone(t1);

                    st = new Stance(b);
                    st.Type = (int)tentacleStance.Walk1;

                    return st;
                case tentacleStance.Walk2:
                    t6 = new Bone(null, Vector2.Zero, new Vector2(12, 42), 105, 89, "tentacle_base", 0.06f, (float)Math.PI * 1.55f, Color.White, 1.0f);
                    t5 = new Bone(t6, new Vector2(40, 75), new Vector2(0, 60), 69, 68, "tentacle_5", 0.05f, (float)Math.PI * 1.69f, Color.White, 1.0f);
                    t4 = new Bone(t5, new Vector2(68, 35), new Vector2(2, 25), 64, 50, "tentacle_4", 0.04f, (float)Math.PI * 1.58f, Color.White, 1.0f);
                    t3 = new Bone(t4, new Vector2(44, 22), new Vector2(12, 22), 54, 53, "tentacle_3", 0.03f, (float)Math.PI * 1.34f, Color.White, 1.0f);
                    t2 = new Bone(t3, new Vector2(44, 21), new Vector2(4, 15), 35, 33, "tentacle_2", 0.02f, (float)Math.PI * 1.33f, Color.White, 1.0f);
                    t1 = new Bone(t2, new Vector2(30, 15), new Vector2(2, 15), 34, 37, "tentacle_tip_1", 0.01f, (float)Math.PI * 1.35f, Color.White, 1.0f);
                    b.AddBone(t6);
                    b.AddBone(t5);
                    b.AddBone(t4);
                    b.AddBone(t3);
                    b.AddBone(t2);
                    b.AddBone(t1);

                    st = new Stance(b);
                    st.Type = (int)tentacleStance.Walk2;

                    return st;
                case tentacleStance.Walk3:
                    t6 = new Bone(null, Vector2.Zero, new Vector2(12, 42), 105, 89, "tentacle_base", 0.06f, (float)Math.PI * (1.25f - .75f), Color.White, 1.0f);
                    t5 = new Bone(t6, new Vector2(40, 75), new Vector2(0, 60), 69, 68, "tentacle_5", 0.05f, (float)Math.PI * (1.35f - .69f), Color.White, 1.0f);
                    t4 = new Bone(t5, new Vector2(68, 35), new Vector2(2, 25), 64, 50, "tentacle_4", 0.04f, (float)Math.PI * (1.25f - .78f), Color.White, 1.0f);
                    t3 = new Bone(t4, new Vector2(44, 22), new Vector2(12, 22), 54, 53, "tentacle_3", 0.03f, (float)Math.PI * (1.1f - .77f), Color.White, 1.0f);
                    t2 = new Bone(t3, new Vector2(44, 21), new Vector2(4, 15), 35, 33, "tentacle_2", 0.02f, (float)Math.PI * (1.0f - .72f), Color.White, 1.0f);
                    t1 = new Bone(t2, new Vector2(30, 15), new Vector2(2, 15), 34, 37, "tentacle_tip_1", 0.01f, (float)Math.PI * (1.0f - .75f), Color.White, 1.0f);

                    b.AddBone(t6);
                    b.AddBone(t5);
                    b.AddBone(t4);
                    b.AddBone(t3);
                    b.AddBone(t2);
                    b.AddBone(t1);

                    st = new Stance(b);
                    st.Type = (int)tentacleStance.Walk1;

                    return st;
                case tentacleStance.Walk4:
                    t6 = new Bone(null, Vector2.Zero, new Vector2(12, 42), 105, 89, "tentacle_base", 0.06f, (float)Math.PI * (1.25f - 1.75f), Color.White, 1.0f);
                    t5 = new Bone(t6, new Vector2(40, 75), new Vector2(0, 60), 69, 68, "tentacle_5", 0.05f, (float)Math.PI * (1.35f - 1.69f), Color.White, 1.0f);
                    t4 = new Bone(t5, new Vector2(68, 35), new Vector2(2, 25), 64, 50, "tentacle_4", 0.04f, (float)Math.PI * (1.25f - 1.78f), Color.White, 1.0f);
                    t3 = new Bone(t4, new Vector2(44, 22), new Vector2(12, 22), 54, 53, "tentacle_3", 0.03f, (float)Math.PI * (1.1f - 1.77f), Color.White, 1.0f);
                    t2 = new Bone(t3, new Vector2(44, 21), new Vector2(4, 15), 35, 33, "tentacle_2", 0.02f, (float)Math.PI * (1.0f - 1.72f), Color.White, 1.0f);
                    t1 = new Bone(t2, new Vector2(30, 15), new Vector2(2, 15), 34, 37, "tentacle_tip_1", 0.01f, (float)Math.PI * (1.0f - 1.75f), Color.White, 1.0f);

                    b.AddBone(t6);
                    b.AddBone(t5);
                    b.AddBone(t4);
                    b.AddBone(t3);
                    b.AddBone(t2);
                    b.AddBone(t1);

                    st = new Stance(b);
                    st.Type = (int)tentacleStance.Walk1;

                    return st;
                case tentacleStance.Walk5:
                    t6 = new Bone(null, Vector2.Zero, new Vector2(12, 42), 105, 89, "tentacle_base", 0.06f, (float)Math.PI * (1.25f + .84f), Color.White, 1.0f);
                    t5 = new Bone(t6, new Vector2(40, 75), new Vector2(0, 60), 69, 68, "tentacle_5", 0.05f, (float)Math.PI * (1.35f + .8f), Color.White, 1.0f);
                    t4 = new Bone(t5, new Vector2(68, 35), new Vector2(2, 25), 64, 50, "tentacle_4", 0.04f, (float)Math.PI * (1.25f + .89f), Color.White, 1.0f);
                    t3 = new Bone(t4, new Vector2(44, 22), new Vector2(12, 22), 54, 53, "tentacle_3", 0.03f, (float)Math.PI * (1.1f + .78f), Color.White, 1.0f);
                    t2 = new Bone(t3, new Vector2(44, 21), new Vector2(4, 15), 35, 33, "tentacle_2", 0.02f, (float)Math.PI * (1.0f + .88f), Color.White, 1.0f);
                    t1 = new Bone(t2, new Vector2(30, 15), new Vector2(2, 15), 34, 37, "tentacle_tip_1", 0.01f, (float)Math.PI * (1.0f +.9f), Color.White, 1.0f);

                    b.AddBone(t6);
                    b.AddBone(t5);
                    b.AddBone(t4);
                    b.AddBone(t3);
                    b.AddBone(t2);
                    b.AddBone(t1);

                    st = new Stance(b);
                    st.Type = (int)tentacleStance.Walk1;

                    return st;
                case tentacleStance.Walk6:
                    t6 = new Bone(null, Vector2.Zero, new Vector2(12, 42), 105, 89, "tentacle_base", 0.06f, (float)Math.PI * (1.25f + .41f), Color.White, 1.0f);
                    t5 = new Bone(t6, new Vector2(40, 75), new Vector2(0, 60), 69, 68, "tentacle_5", 0.05f, (float)Math.PI * (1.35f + .49f), Color.White, 1.0f);
                    t4 = new Bone(t5, new Vector2(68, 35), new Vector2(2, 25), 64, 50, "tentacle_4", 0.04f, (float)Math.PI * (1.25f + .42f), Color.White, 1.0f);
                    t3 = new Bone(t4, new Vector2(44, 22), new Vector2(12, 22), 54, 53, "tentacle_3", 0.03f, (float)Math.PI * (1.1f + .37f), Color.White, 1.0f);
                    t2 = new Bone(t3, new Vector2(44, 21), new Vector2(4, 15), 35, 33, "tentacle_2", 0.02f, (float)Math.PI * (1.0f + .46f), Color.White, 1.0f);
                    t1 = new Bone(t2, new Vector2(30, 15), new Vector2(2, 15), 34, 37, "tentacle_tip_1", 0.01f, (float)Math.PI * (1.0f + .5f), Color.White, 1.0f);

                    b.AddBone(t6);
                    b.AddBone(t5);
                    b.AddBone(t4);
                    b.AddBone(t3);
                    b.AddBone(t2);
                    b.AddBone(t1);

                    st = new Stance(b);
                    st.Type = (int)tentacleStance.Walk1;

                    return st;
                case tentacleStance.Walk7:
                    t6 = new Bone(null, Vector2.Zero, new Vector2(12, 42), 105, 89, "tentacle_base", 0.06f, (float)Math.PI * (1.25f - 1.0f), Color.White, 1.0f);
                    t5 = new Bone(t6, new Vector2(40, 75), new Vector2(0, 60), 69, 68, "tentacle_5", 0.05f, (float)Math.PI * (1.35f - .96f), Color.White, 1.0f);
                    t4 = new Bone(t5, new Vector2(68, 35), new Vector2(2, 25), 64, 50, "tentacle_4", 0.04f, (float)Math.PI * (1.25f - 1.08f), Color.White, 1.0f);
                    t3 = new Bone(t4, new Vector2(44, 22), new Vector2(12, 22), 54, 53, "tentacle_3", 0.03f, (float)Math.PI * (1.1f - 1.03f), Color.White, 1.0f);
                    t2 = new Bone(t3, new Vector2(44, 21), new Vector2(4, 15), 35, 33, "tentacle_2", 0.02f, (float)Math.PI * (1.0f - 1.06f), Color.White, 1.0f);
                    t1 = new Bone(t2, new Vector2(30, 15), new Vector2(2, 15), 34, 37, "tentacle_tip_1", 0.01f, (float)Math.PI * (1.0f -1.1f), Color.White, 1.0f);

                    b.AddBone(t6);
                    b.AddBone(t5);
                    b.AddBone(t4);
                    b.AddBone(t3);
                    b.AddBone(t2);
                    b.AddBone(t1);

                    st = new Stance(b);
                    st.Type = (int)tentacleStance.Walk1;

                    return st;
                default:
                    return null;
            }
        }

        public static StanceAnimation GetAnimation(tentacleAnimation a)
        {
            StanceAnimation an;
            switch (a)
            {
                case tentacleAnimation.Idle:
                    an = new StanceAnimation((int)tentacleAnimation.Idle);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Idle), 35, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk1), 35, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk4), 20, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk6), 12, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk2), 15, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk1), 7, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk5), 22, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk3), 35, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Idle), 10, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk2), 12, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk6), 22, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk4), 12, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk7), 9, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk3), 20, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Idle), 35, 0);
                    return an;
                case tentacleAnimation.Walk:
                    an = new StanceAnimation((int)tentacleAnimation.Walk);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Idle), 55, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk7), 25, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk5), 33, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk4), 20, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk6), 12, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk7), 20, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk3), 20, 0);

                    return an;
                case tentacleAnimation.Walk2:
                    an = new StanceAnimation((int)tentacleAnimation.Walk);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Idle), 55, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk5), 20, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk4), 20, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk5), 20, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk4), 10, 0);

                    return an;
                case tentacleAnimation.Walk3:
                    an = new StanceAnimation((int)tentacleAnimation.Walk);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Idle), 55, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk2), 15, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk1), 8, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk5), 35, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk1), 12, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk4), 12, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk6), 12, 0);

                    return an;

                case tentacleAnimation.Walk4:
                    an = new StanceAnimation((int)tentacleAnimation.Walk);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Idle), 55, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk3), 20, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk4), 18, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk5), 9, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk7), 12, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk5), 9, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk1), 12, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk5), 9, 0);
                    an.AddStance(StaticStancetentacle.Get(tentacleStance.Walk6), 12, 0);

                    return an;

                default:
                    return null;
            }
        }
    }
}
