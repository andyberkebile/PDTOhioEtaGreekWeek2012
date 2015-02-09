using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.BoneAnimation;
using CloudJourney.Framework.Rendering;

namespace CloudJourney.Framework.BoneAnimator.Stances
{
    public enum playerStance { Idle, Idle1, Answer};
    public enum playerAnimation { Idle, Answer};

    public class StaticStanceplayer
    {
        public static float scale = 1f;
        // returns the set of bones used for the tentacle (sort of like a model)
        public static BoneSet GetBoneSet()
        {
            /* TODO: Complete this set */
            BoneSet b = new BoneSet();
            // Bone(Parent, Offset, Origin, Length, Width, Texture, Depth, [angle, color, alpha])

            TextureStatic.Load("fArm", @"Art/body");
            TextureStatic.Load("head1", @"Art/body");
            TextureStatic.Load("lArm", @"Art/body");
            TextureStatic.Load("lHand", @"Art/body");
            TextureStatic.Load("lowerLeg", @"Art/body");
            TextureStatic.Load("rArm", @"Art/body");
            TextureStatic.Load("rHand", @"Art/body");
            TextureStatic.Load("thigh", @"Art/body");
            TextureStatic.Load("torso", @"Art/body");

            float a = 0;
            //float b = 0;

            Bone torso = new Bone(null, Vector2.Zero, new Vector2(160 , 53 ), 107 , 230 , "torso", -0.06f);
            Bone rThigh = new Bone(torso, new Vector2(-26 , 30 ), new Vector2(10 , 30 ), 58 , 192 , "thigh", 0.02f);
            Bone lThigh = new Bone(torso, new Vector2(27 , 30 ), new Vector2(10 , 30 ), 58 , 192 , "thigh", 0.02f);
            Bone rShin = new Bone(rThigh, new Vector2(-2 , 100 ), new Vector2(18 , 30 ), 110 , 223 , "lowerLeg", 0.02f);
            Bone lShin = new Bone(lThigh, new Vector2(-2 , 100 ), new Vector2(18 , 30 ), 110 , 223 , "lowerLeg", 0.02f);
            Bone lArm = new Bone(torso, new Vector2(-35 , -116 ), new Vector2(10 , 15 ), 35 , 145 , "rArm", 0.021f);
            Bone rArm = new Bone(torso, new Vector2(42 , -116), new Vector2(10 , 20 ), 35 , 145 , "lArm", 0.021f);
            Bone lFore = new Bone(lArm, new Vector2(0 , 96 ), new Vector2(8 , 12 ), 26 , 137 , "fArm", 0.0211f);
            Bone rFore = new Bone(rArm, new Vector2(0 , 96 ), new Vector2(8 , 12 ), 26 , 137 , "fArm", 0.0211f);
            Bone lHand = new Bone(lFore, new Vector2(-1 , 90), new Vector2(3 , 38 ), 65 , 68 , "lHand", 0.022f);
            Bone rHand = new Bone(rFore, new Vector2(-1 , 90), new Vector2(3 , 28 ), 65 , 68 , "rHand", 0.022f);
            //scale = .5f;
            Bone head = new Bone(torso, new Vector2(0, -154), new Vector2(5 , 55 ), 103 , 116 , "head1", 0.02f);
            //Bone head = new Bone(torso, new Vector2(0 , -65), new Vector2(3.25f, 35.75f), 103 , 116 , "head1", 0.02f);


            b.AddBone(torso);
            b.AddBone(rThigh);
            b.AddBone(lThigh);
            b.AddBone(rShin);
            b.AddBone(lShin);
            b.AddBone(lArm);
            b.AddBone(rArm);
            b.AddBone(lFore);
            b.AddBone(rFore);
            b.AddBone(lHand);
            b.AddBone(rHand);
            b.AddBone(head);

            return b;
        }

        // returns a given stance (these are premade)
        public static Stance Get(playerStance s)
        {
            /* TODO: Add stances */
            Stance st;
            BoneSet b = new BoneSet();
            Bone torso;
            Bone rThigh;
            Bone lThigh;
            Bone rShin;
            Bone lShin;
            Bone lArm;
            Bone rArm;
            Bone lFore;
            Bone rFore;
            Bone lHand;
            Bone rHand;
            Bone head;


            switch (s)
            {
                case playerStance.Idle:


                    //torso = new Bone(null, Vector2.Zero, new Vector2(53, 160), 107, 230, "torso", 0.06f, (float)Math.PI * .5f, Color.White, 1.0f);
                    torso = new Bone(null, Vector2.Zero, new Vector2(0, 0), 107, 230, "torso", 0.06f, (float)Math.PI * .50f, Color.White, 1.0f);
                    rThigh = new Bone(torso, new Vector2(60, -26), new Vector2(10, 30), 58, 192, "thigh", 0.05f, (float)Math.PI * .5f, Color.White, 1.0f);
                    lThigh = new Bone(torso, new Vector2(60, 27), new Vector2(10, 30), 58, 192, "thigh", 0.04f, (float)Math.PI * .5f, Color.White, 1.0f);
                    rShin = new Bone(rThigh, new Vector2(170, -2), new Vector2(18, 30), 110, 223, "lowerLeg", 0.03f, (float)Math.PI * .45f, Color.White, 1.0f);
                    lShin = new Bone(lThigh, new Vector2(-2, 170), new Vector2(18, 30), 110, 223, "lowerLeg", 0.02f, (float)Math.PI * .5f, Color.White, 1.0f);
                    lArm = new Bone(torso, new Vector2(-122, -15), new Vector2(10, 20), 35, 145, "rArm", 0.02f, (float)Math.PI * .4f, Color.White, 1.0f);
                    rArm = new Bone(torso, new Vector2(-122, 32), new Vector2(10, 15), 35, 145, "lArm", 0.02f, (float)Math.PI * .7f, Color.White, 1.0f);
                    lFore = new Bone(lArm, new Vector2(126, 0), new Vector2(8, 12), 26, 137, "fArm", 0.02f, (float)Math.PI * .5f, Color.White, 1.0f);
                    rFore = new Bone(rArm, new Vector2(126, 0), new Vector2(8, 12), 26, 137, "fArm", 0.02f, (float)Math.PI * .0f, Color.White, 1.0f);
                    lHand = new Bone(lFore, new Vector2(124, -1), new Vector2(3, 38), 65, 68, "lHand", 0.02f, (float)Math.PI *-.5f, Color.White, 1.0f);
                    rHand = new Bone(rFore, new Vector2(124, -1), new Vector2(3, 28), 65, 68, "rHand", 0.02f, (float)Math.PI * -.1f, Color.White, 1.0f);
                    head = new Bone(torso, new Vector2(0, 0), new Vector2(0, 0), 103, 116, "head1", 0.02f, (float)Math.PI * -.5f, Color.White, 1.0f);

                    b.AddBone(torso);
                    b.AddBone(rThigh);
                    b.AddBone(lThigh);
                    b.AddBone(rShin);
                    b.AddBone(lShin);
                    b.AddBone(lArm);
                    b.AddBone(rArm);
                    b.AddBone(lFore);
                    b.AddBone(rFore);
                    b.AddBone(lHand);
                    b.AddBone(rHand);
                    b.AddBone(head);

                    st = new Stance(b);
                    st.Type = (int)playerStance.Idle;

                    return st;


                case playerStance.Idle1:


                    //torso = new Bone(null, Vector2.Zero, new Vector2(53, 160), 107, 230, "torso", 0.06f, (float)Math.PI * .5f, Color.White, 1.0f);
                    torso = new Bone(null, Vector2.Zero, new Vector2(0, 0), 107, 230, "torso", 0.06f, (float)Math.PI * .51f, Color.White, 1.0f);
                    rThigh = new Bone(torso, new Vector2(60, -26), new Vector2(10, 30), 58, 192, "thigh", 0.05f, (float)Math.PI * .5f, Color.White, 1.0f);
                    lThigh = new Bone(torso, new Vector2(60, 27), new Vector2(10, 30), 58, 192, "thigh", 0.04f, (float)Math.PI * .5f, Color.White, 1.0f);
                    rShin = new Bone(rThigh, new Vector2(170, -2), new Vector2(18, 30), 110, 223, "lowerLeg", 0.03f, (float)Math.PI * .45f, Color.White, 1.0f);
                    lShin = new Bone(lThigh, new Vector2(-2, 170), new Vector2(18, 30), 110, 223, "lowerLeg", 0.02f, (float)Math.PI * .5f, Color.White, 1.0f);
                    lArm = new Bone(torso, new Vector2(-122, -15), new Vector2(10, 20), 35, 145, "Arm", 0.02f, (float)Math.PI * .39f, Color.White, 1.0f);
                    rArm = new Bone(torso, new Vector2(-122, 32), new Vector2(10, 15), 35, 145, "lArm", 0.02f, (float)Math.PI * .70f, Color.White, 1.0f);
                    lFore = new Bone(lArm, new Vector2(126, 0), new Vector2(8, 12), 26, 137, "fArm", 0.02f, (float)Math.PI * .50f, Color.White, 1.0f);
                    rFore = new Bone(rArm, new Vector2(126, 0), new Vector2(8, 12), 26, 137, "fArm", 0.02f, (float)Math.PI * .0f, Color.White, 1.0f);
                    lHand = new Bone(lFore, new Vector2(124, -1), new Vector2(3, 38), 65, 68, "lHand", 0.02f, (float)Math.PI * -.5f, Color.White, 1.0f);
                    rHand = new Bone(rFore, new Vector2(124, -1), new Vector2(3, 28), 65, 68, "rHand", 0.02f, (float)Math.PI * -.1f, Color.White, 1.0f);
                    head = new Bone(torso, new Vector2(0, 0), new Vector2(0, 0), 103, 116, "head1", 0.02f, (float)Math.PI * -.51f, Color.White, 1.0f);

                    b.AddBone(torso);
                    b.AddBone(rThigh);
                    b.AddBone(lThigh);
                    b.AddBone(rShin);
                    b.AddBone(lShin);
                    b.AddBone(lArm);
                    b.AddBone(rArm);
                    b.AddBone(lFore);
                    b.AddBone(rFore);
                    b.AddBone(lHand);
                    b.AddBone(rHand);
                    b.AddBone(head);

                    st = new Stance(b);
                    st.Type = (int)playerStance.Idle1;

                    return st;

                case playerStance.Answer:


                    //torso = new Bone(null, Vector2.Zero, new Vector2(53, 160), 107, 230, "torso", 0.06f, (float)Math.PI * .5f, Color.White, 1.0f);
                    torso = new Bone(null, Vector2.Zero, new Vector2(0, 0), 107, 230, "torso", 0.06f, (float)Math.PI * .51f, Color.White, 1.0f);
                    rThigh = new Bone(torso, new Vector2(60, -26), new Vector2(10, 30), 58, 192, "thigh", 0.05f, (float)Math.PI * .5f, Color.White, 1.0f);
                    lThigh = new Bone(torso, new Vector2(60, 27), new Vector2(10, 30), 58, 192, "thigh", 0.04f, (float)Math.PI * .5f, Color.White, 1.0f);
                    rShin = new Bone(rThigh, new Vector2(170, -2), new Vector2(18, 30), 110, 223, "lowerLeg", 0.03f, (float)Math.PI * .45f, Color.White, 1.0f);
                    lShin = new Bone(lThigh, new Vector2(-2, 170), new Vector2(18, 30), 110, 223, "lowerLeg", 0.02f, (float)Math.PI * .5f, Color.White, 1.0f);
                    lArm = new Bone(torso, new Vector2(-122, -15), new Vector2(10, 20), 35, 145, "Arm", 0.02f, (float)Math.PI * .39f, Color.White, 1.0f);
                    rArm = new Bone(torso, new Vector2(-122, 32), new Vector2(10, 15), 35, 145, "lArm", 0.02f, (float)Math.PI * .70f, Color.White, 1.0f);
                    lFore = new Bone(lArm, new Vector2(126, 0), new Vector2(8, 12), 26, 137, "fArm", 0.02f, (float)Math.PI * .50f, Color.White, 1.0f);
                    rFore = new Bone(rArm, new Vector2(126, 0), new Vector2(8, 12), 26, 137, "fArm", 0.02f, (float)Math.PI * .25f, Color.White, 1.0f);
                    lHand = new Bone(lFore, new Vector2(124, -1), new Vector2(3, 38), 65, 68, "lHand", 0.02f, (float)Math.PI * -.5f, Color.White, 1.0f);
                    rHand = new Bone(rFore, new Vector2(124, -1), new Vector2(3, 28), 65, 68, "rHand", 0.02f, (float)Math.PI * .5f, Color.White, 1.0f);
                    head = new Bone(torso, new Vector2(0, 0), new Vector2(0, 0), 103, 116, "head1", 0.02f, (float)Math.PI * -.51f, Color.White, 1.0f);

                    b.AddBone(torso);
                    b.AddBone(rThigh);
                    b.AddBone(lThigh);
                    b.AddBone(rShin);
                    b.AddBone(lShin);
                    b.AddBone(lArm);
                    b.AddBone(rArm);
                    b.AddBone(lFore);
                    b.AddBone(rFore);
                    b.AddBone(lHand);
                    b.AddBone(rHand);
                    b.AddBone(head);

                    st = new Stance(b);
                    st.Type = (int)playerStance.Answer;

                    return st;
               

                default:
                    return null;
            }
        }

        public static StanceAnimation GetAnimation(playerAnimation a)
        {
            StanceAnimation an;
            switch (a)
            {
                case playerAnimation.Idle:
                    an = new StanceAnimation((int)playerAnimation.Idle);
                    an.AddStance(StaticStanceplayer.Get(playerStance.Idle), 35, 0);
                    an.AddStance(StaticStanceplayer.Get(playerStance.Idle1), 100, 0);
                    an.AddStance(StaticStanceplayer.Get(playerStance.Idle), 35, 0);
                    return an;
                case playerAnimation.Answer:
                    an = new StanceAnimation((int)playerAnimation.Answer);
                    an.AddStance(StaticStanceplayer.Get(playerStance.Answer), 5, 0);
                    return an;
                default:
                    return null;
            }
        }
    }
}
