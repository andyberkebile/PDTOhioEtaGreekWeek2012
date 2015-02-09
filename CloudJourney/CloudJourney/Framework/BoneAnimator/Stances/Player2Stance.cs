using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.BoneAnimation;
using CloudJourney.Framework.Rendering;

namespace CloudJourney.Framework.BoneAnimator.Stances
{
    public enum player2Stance { Idle, Idle1, Answer};
    public enum player2Animation { Idle, Answer};

    public class StaticStanceplayer2
    {
        public static float scale = 1f;
        // returns the set of bones used for the tentacle (sort of like a model)
        public static BoneSet GetBoneSet()
        {
            /* TODO: Complete this set */
            BoneSet b = new BoneSet();
            // Bone(Parent, Offset, Origin, Length, Width, Texture, Depth, [angle, color, alpha])

            int rando = Cloud.randCloud.Next(1, 14);

            string a = "lArm";
            string d = "rArm";
            string c = "torso";

            switch (rando)
            {
                case 1:
                    a = "lArmBeta";
                    d = "rArmBeta";
                    c = "torsoBeta";
                    break;
                case 2:
                    a = "lArmDelt";
                    d = "rArmDelt";
                    c = "torsoDelt";
                    break;
                case 3:
                    a = "lArmDeltaChi";
                    d = "rArmDeltaChi";
                    c = "torsoDeltaChi";
                    break;
                case 4:
                    a = "lArmDU";
                    d = "rArmDU";
                    c = "torsoDU";
                    break;
                case 5:
                    a = "lArmFIJI";
                    d = "rArmFIJI";
                    c = "torsoFIJI";
                    break;
                case 6:
                    a = "lArmPhiKappaTheta";
                    d = "rArmPhiKappaTheta";
                    c = "torsoPhiKappaTheta";
                    break;
                case 7:
                    a = "lArmPhiPsi";
                    d = "rArmPhiPsi";
                    c = "torsoPhiPsi";
                    break;
                case 8:
                    a = "lArmPhiTau";
                    d = "rArmPhiTau";
                    c = "torsoPhiTau";
                    break;
                case 14:
                    a = "lArmSigChi";
                    d = "rArmSigChi";
                    c = "torsoSigChi";
                    break;
                case 9:
                    a = "lArmSigEp";
                    d = "rArmSigEp";
                    c = "torsoSigEp";
                    break;
                case 10:
                    a = "lArmSigNu";
                    d = "rArmSigNu";
                    c = "torsoSigNu";
                    break;
                case 11:
                    a = "lArmThetaChi";
                    d = "rArmThetaChi";
                    c = "torsoThetaChi";
                    break;
                case 12:
                    a = "lArmZBT";
                    d = "rArmZBT";
                    c = "torsoZBT";
                    break;
                case 13:
                    a = "lArmZetaPsi";
                    d = "rArmZetaPsi";
                    c = "torsoZetaPsi";
                    break;
                default:
                    break;

            }
            TextureStatic.Load("fArm", @"Art/body");
            TextureStatic.Load("head2", @"Art/body");
            TextureStatic.Load(a, @"Art/body");
            TextureStatic.Load("lHand", @"Art/body");
            TextureStatic.Load("lowerLeg", @"Art/body");
            TextureStatic.Load(d, @"Art/body");
            TextureStatic.Load("rHand", @"Art/body");
            TextureStatic.Load("thigh", @"Art/body");
            TextureStatic.Load(c, @"Art/body");

            //float a = 0;
            //float b = 0;

            Bone torso = new Bone(null, Vector2.Zero, new Vector2(160 , 53 ), 107 , 230 , c, -0.06f);
            Bone rThigh = new Bone(torso, new Vector2(-26 , 30 ), new Vector2(10 , 30 ), 58 , 192 , "thigh", 0.02f);
            Bone lThigh = new Bone(torso, new Vector2(27 , 30 ), new Vector2(10 , 30 ), 58 , 192 , "thigh", 0.02f);
            Bone rShin = new Bone(rThigh, new Vector2(-2 , 100 ), new Vector2(18 , 30 ), 110 , 223 , "lowerLeg", 0.02f);
            Bone lShin = new Bone(lThigh, new Vector2(-2 , 100 ), new Vector2(18 , 30 ), 110 , 223 , "lowerLeg", 0.02f);
            Bone lArm = new Bone(torso, new Vector2(-35 , -116 ), new Vector2(10 , 15 ), 35 , 145 , d, 0.021f);
            Bone rArm = new Bone(torso, new Vector2(42 , -116), new Vector2(10 , 20 ), 35 , 145 , a, 0.021f);
            Bone lFore = new Bone(lArm, new Vector2(0 , 96 ), new Vector2(8 , 12 ), 26 , 137 , "fArm", 0.0211f);
            Bone rFore = new Bone(rArm, new Vector2(0 , 96 ), new Vector2(8 , 12 ), 26 , 137 , "fArm", 0.0211f);
            Bone lHand = new Bone(lFore, new Vector2(-1 , 90), new Vector2(3 , 38 ), 65 , 68 , "lHand", 0.022f);
            Bone rHand = new Bone(rFore, new Vector2(-1 , 90), new Vector2(3 , 28 ), 65 , 68 , "rHand", 0.022f);
            //scale = .5f;
            Bone head = new Bone(torso, new Vector2(0, -154), new Vector2(5 , 55 ), 103 , 116 , "head2", 0.02f);
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
        public static Stance Get(player2Stance s)
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
                case player2Stance.Idle:


                    //torso = new Bone(null, Vector2.Zero, new Vector2(53, 160), 107, 230, "torso", 0.06f, (float)Math.PI * .5f, Color.White, 1.0f);
                    torso = new Bone(null, Vector2.Zero, new Vector2(0, 0), 107, 230, "torso", 0.06f, (float)Math.PI * .49f, Color.White, 1.0f);
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
                    head = new Bone(torso, new Vector2(0, 0), new Vector2(0, 0), 103, 116, "head2", 0.02f, (float)Math.PI * -.5f, Color.White, 1.0f);

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
                    st.Type = (int)player2Stance.Idle;

                    return st;


                case player2Stance.Idle1:


                    //torso = new Bone(null, Vector2.Zero, new Vector2(53, 160), 107, 230, "torso", 0.06f, (float)Math.PI * .5f, Color.White, 1.0f);
                    torso = new Bone(null, Vector2.Zero, new Vector2(0, 0), 107, 230, "torso", 0.06f, (float)Math.PI * .50f, Color.White, 1.0f);
                    rThigh = new Bone(torso, new Vector2(60, -26), new Vector2(10, 30), 58, 192, "thigh", 0.05f, (float)Math.PI * .5f, Color.White, 1.0f);
                    lThigh = new Bone(torso, new Vector2(60, 27), new Vector2(10, 30), 58, 192, "thigh", 0.04f, (float)Math.PI * .5f, Color.White, 1.0f);
                    rShin = new Bone(rThigh, new Vector2(170, -2), new Vector2(18, 30), 110, 223, "lowerLeg", 0.03f, (float)Math.PI * .45f, Color.White, 1.0f);
                    lShin = new Bone(lThigh, new Vector2(-2, 170), new Vector2(18, 30), 110, 223, "lowerLeg", 0.02f, (float)Math.PI * .5f, Color.White, 1.0f);
                    lArm = new Bone(torso, new Vector2(-122, -15), new Vector2(10, 20), 35, 145, "Arm", 0.02f, (float)Math.PI * .39f, Color.White, 1.0f);
                    rArm = new Bone(torso, new Vector2(-122, 32), new Vector2(10, 15), 35, 145, "lArm", 0.02f, (float)Math.PI * .71f, Color.White, 1.0f);
                    lFore = new Bone(lArm, new Vector2(126, 0), new Vector2(8, 12), 26, 137, "fArm", 0.02f, (float)Math.PI * .50f, Color.White, 1.0f);
                    rFore = new Bone(rArm, new Vector2(126, 0), new Vector2(8, 12), 26, 137, "fArm", 0.02f, (float)Math.PI * .0f, Color.White, 1.0f);
                    lHand = new Bone(lFore, new Vector2(124, -1), new Vector2(3, 38), 65, 68, "lHand", 0.02f, (float)Math.PI * -.51f, Color.White, 1.0f);
                    rHand = new Bone(rFore, new Vector2(124, -1), new Vector2(3, 28), 65, 68, "rHand", 0.02f, (float)Math.PI * -.1f, Color.White, 1.0f);
                    head = new Bone(torso, new Vector2(0, 0), new Vector2(0, 0), 103, 116, "head2", 0.02f, (float)Math.PI * -.52f, Color.White, 1.0f);

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
                    st.Type = (int)player2Stance.Idle1;

                    return st;

                case player2Stance.Answer:
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
                    head = new Bone(torso, new Vector2(0, 0), new Vector2(0, 0), 103, 116, "head2", 0.02f, (float)Math.PI * -.51f, Color.White, 1.0f);

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
                    st.Type = (int)player2Stance.Answer;

                    return st;
               

                default:
                    return null;
            }
        }

        public static StanceAnimation GetAnimation(player2Animation a)
        {
            StanceAnimation an;
            switch (a)
            {
                case player2Animation.Idle:
                    an = new StanceAnimation((int)player2Animation.Idle);
                    an.AddStance(StaticStanceplayer2.Get(player2Stance.Idle), 35, 0);
                    an.AddStance(StaticStanceplayer2.Get(player2Stance.Idle1), 100, 0);
                    
                    return an;
                case player2Animation.Answer:
                    an = new StanceAnimation((int)player2Animation.Answer);
                    an.AddStance(StaticStanceplayer2.Get(player2Stance.Answer), 5, 0);
                    return an;
                default:
                    return null;
            }
        }
    }
}
