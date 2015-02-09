using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CloudJourney.Framework.Renderables;
using CloudJourney.Framework.Rendering;
using CloudJourney.Framework.Collision;

namespace CloudJourney.Framework.BoneAnimation
{
    public class BoneAnimator
    {
        private BoneSet bones;
        private Stance currentStance;
        private Stance previousStance;
        private StanceAnimation animation;
        private int frame;
        private float speed;
        private Vector2 origin;
        private float progress;
        private bool flip;
        private float depth;
        private float yoffset, startYoff, endYoff;
        private Actor entityAttach;

        public BoneSet Bones { get { return bones; } }
        public Stance CurrentStance { get { return currentStance; } set { this.SetStance(value); } }
        public Stance PreviousStance { get { return previousStance; } }
        public Vector2 Origin { get { return origin; } set { this.SetOrigin(value); } }
        public float AnimationProgress { get { return progress; } }
        public bool AnimationComplete { get { return progress > 0.99f; } }
        public bool Flip { get { return flip; } set { flip = value; } }
        public float Depth { get { return depth; } set { depth = value; } }
        public Actor Entity { get { return entityAttach; } }
        public float Speed { get { return speed; } set { speed = value; } }

        public BoneAnimator(Actor attach, BoneSet set, Stance initStance, Vector2 drawOrigin)
        {
            bones = set;
            currentStance = initStance;
            previousStance = currentStance;
            origin = drawOrigin;
            progress = 0.0f;
            depth = 0.5f;
            speed = 20.0f;
            yoffset = 0.0f;
            entityAttach = attach;

            foreach (Bone b in bones) b.Animator = this;
        }

        public BoneAnimator(Actor attach, BoneSet set, Stance initStance, float originX, float originY)
        {
            bones = set;
            currentStance = initStance;
            previousStance = currentStance;
            origin = new Vector2(originX, originY);
            progress = 0.0f;
            depth = 0.5f;
            speed = 20.0f;
            yoffset = 0.0f;
            entityAttach = attach;

            foreach (Bone b in bones) b.Animator = this;
        }

        public void Detatch()
        {
            entityAttach = null;
        }

        public static float Tween(float val, float steps, float start, float end)
        {
            float a = start;
            float b = val + (end - start) / steps;
            float c = end;

            // find the median
            if (a >= b && b >= c)
                return b;
            else
                if (a <= b && b <= c)
                    return b;
                else
                    if (a <= b && b >= c && a <= c)
                        return c;
                    else
                        if (a <= b && b >= c && a >= c)
                            return a;
                        else
                            if (a >= b && b <= c && a >= c)
                                return c;
                            else
                                return a;
            //return Math.Max(start, Math.Min(end, val + (end - start) / steps));
        }

        public static Color TweenRGB(Color val, float steps, Color start, Color end)
        {
            float r = Tween(val.R, steps, start.R, end.R);
            float g = Tween(val.G, steps, start.G, end.G);
            float b = Tween(val.B, steps, start.B, end.B);

            return new Color((int)r, (int)g, (int)b);
        }

        public static Vector2 TweenV2(Vector2 val, float steps, Vector2 start, Vector2 end)
        {
            float x = Tween(val.X, steps, start.X, end.X);
            float y = Tween(val.Y, steps, start.Y, end.Y);
            return new Vector2(x, y);
        }

        public void SetStance(Stance newStance)
        {
            if (newStance.Type != currentStance.Type)
            {
                previousStance = currentStance;
                currentStance = newStance;
                progress = 0;
            }
        }

        public void SetAnimation(StanceAnimation newAnimation)
        {
            if (animation == null || animation.Type != newAnimation.Type)
            {
                animation = newAnimation;
                this.SetStance(newAnimation[0].stance);
                frame = 0;
                speed = newAnimation[0].speed;
                startYoff = yoffset;
                endYoff = newAnimation[0].yoffset;
            }
        }

        public void ClearAnimation()
        {
            animation = null;
            frame = 0;
            speed = 20.0f;
            progress = 0;
        }

        public void SetOrigin(Vector2 newOrigin)
        {
            origin = newOrigin;
        }

        public void SetOrigin(float x, float y)
        {
            origin.X = x;
            origin.Y = y;
        }

        public bool CheckCollisions(BoneAnimator other)
        {
            foreach (Bone a in this.Bones)
            {
                foreach (Bone b in other.Bones)
                {
                    //if (Collision2D.CheckPerPixel(TextureStatic.Get(a.Texture), TextureStatic.Get(b.Texture), a.Position, b.Position, a.Origin, b.Origin, a.BoundingRect, b.BoundingRect, a.Angle, b.Angle))
                    if (Collision2D.CheckRectangles(a.BoundingRect, b.BoundingRect))
                    {
                        this.OnCollision(other, b, a);
                        other.OnCollision(this, a, b);
                    }
                }
            }

            return false;
        }

        public void OnCollision(BoneAnimator other, Bone bone, Bone otherBone)
        {
            bone.Blend = Color.Red;

            /*entityAttach.OnCollision(new CollisionData
                {
                    Other = other.Entity,
                    Position = new Vector3((bone.Position + otherBone.Position) / 2.0f, 0.0f),
                    Angle = (float)Math.Atan2((double)(otherBone.Position.X - bone.Position.X), (double)(otherBone.Position.Y - bone.Position.Y)),
                    MyBone = bone,
                    OtherBone = otherBone
                }
            );*/
        }

        public void Update()
        {
            foreach (Bone b in bones)
            {
                // skip manually controlled bones
                if (!b.ManualControl)
                {
                    // tween the angle to the new stance
                    float off = /*b is WeaponBone ? (b as WeaponBone).AngleOffset :*/ 0.0f;
                    float startAngle = previousStance.Bones[bones.IndexOf(b)].Angle + off;
                    float endAngle = currentStance.Bones[bones.IndexOf(b)].Angle + off;
                    Color startColor = previousStance.Bones[bones.IndexOf(b)].Blend;
                    Color endColor = currentStance.Bones[bones.IndexOf(b)].Blend;
                    float startAlpha = previousStance.Bones[bones.IndexOf(b)].Alpha;
                    float endAlpha = currentStance.Bones[bones.IndexOf(b)].Alpha;

                    b.Angle = Tween(b.Angle, speed, startAngle, endAngle);
                    b.Blend = TweenRGB(b.Blend, speed, startColor, endColor);
                    b.Alpha = Tween(b.Alpha, speed, startAlpha, endAlpha);
                    b.Blend *= b.Alpha; // account for alpha in the blend color (this is weird)

                    Vector2 startScale = previousStance.Bones[bones.IndexOf(b)].Dimensions / b.Dimensions;
                    Vector2 endScale = currentStance.Bones[bones.IndexOf(b)].Dimensions / b.Dimensions;
                    b.Scale = TweenV2(b.Scale, speed, startScale, endScale);
                }

                // for debugging
                //if (b is WeaponBone)
                //{
                //    foreach (Bone o in (b as WeaponBone).Limb)
                //    {
                //        o.Blend = Color.Blue;
                //    }
                //}

                // calculate new positions
                if (b.Parent != null)
                {
                    if (!flip)
                    {
                        b.Position.X = b.Parent.Position.X + b.AttachPoint.Y * (float)Math.Cos(b.Parent.Angle);
                        b.Position.X += b.AttachPoint.X * (float)Math.Cos(b.Parent.Angle + (Math.PI / 2));
                    }
                    else
                    {
                        float nAng = Math.Abs((float)Math.PI - b.Parent.Angle);
                        b.Position.X = b.Parent.Position.X + b.AttachPoint.Y * (float)Math.Cos(nAng);
                        b.Position.X += b.AttachPoint.X * (float)Math.Cos(nAng + (Math.PI / 2));
                    }

                    b.Position.Y = b.Parent.Position.Y + b.AttachPoint.Y * (float)Math.Sin(b.Parent.Angle);
                    b.Position.Y += b.AttachPoint.X * (float)Math.Sin(b.Parent.Angle + (Math.PI / 2));
                }
                else
                {
                    b.Position = this.origin; // root bone gets set to the origin of the animator
                }
            }

            //progress /= bones.Count; // an average
            progress = Tween(progress, speed, 0.0f, 1.0f);
            yoffset = Tween(yoffset, speed, startYoff, endYoff);

            if (animation != null && progress == 1.0f)
            {
                int f = (frame + 1) % animation.Count;
                SetStance(animation[f].stance);
                startYoff = yoffset;
                endYoff = animation[f].yoffset;
                frame = f;
            }
        }

        public void Draw()
        {
            foreach (Bone b in bones)
            {
                //if (b.Texture == "") continue;

                Drawer.Draw(
                    TextureStatic.Get(b.Texture),
                    b.Position + (Vector2.UnitY * yoffset),
                    null,
                    b.Blend,
                    flip ? (float)Math.PI - b.Angle : b.Angle,
                    flip ? new Vector2(b.Origin.X, b.Width - b.Origin.Y) : b.Origin,
                    b.Scale,
                    flip ? SpriteEffects.FlipVertically : SpriteEffects.None,
                    this.depth + b.Depth);

                //Drawer.DrawLine(b.BoundingRect[0], b.BoundingRect[1], 1.0f, 1.0f, Color.Orange);
                //Drawer.DrawLine(b.BoundingRect[1], b.BoundingRect[2], 1.0f, 1.0f, Color.Orange);
                //Drawer.DrawLine(b.BoundingRect[2], b.BoundingRect[3], 1.0f, 1.0f, Color.Orange);
                //Drawer.DrawLine(b.BoundingRect[3], b.BoundingRect[0], 1.0f, 1.0f, Color.Orange);

                //Drawer.DrawRectangle(Collision2D.BoundingRectangle(b.BoundingRect), 1.0f, 1.0f, Color.Yellow * 0.5f);

                //List<Vector2> tri1 = Collision2D.TriangleFromRectangle(b.BoundingRect, true);
                //List<Vector2> tri2 = Collision2D.TriangleFromRectangle(b.BoundingRect, false);
                //for (int i = 0; i < 3; i++)
                //{
                //    Drawer.DrawLine(tri1[i], tri1[(i + 1) % 3], 1.0f, 1.0f, Color.Pink);
                //}

                //for (int i = 0; i < 3; i++)
                //{
                //    Drawer.DrawLine(tri2[i], tri2[(i + 1) % 3], 1.0f, 1.0f, Color.Pink);
                //}
            }

            /*Drawer.DrawString(
                FontStatic.Get("defaultFont"),
                progress.ToString(),
                this.Origin,
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.9f);*/
        }
    }
}
