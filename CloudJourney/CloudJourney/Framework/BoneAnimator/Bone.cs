using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Collision;

namespace CloudJourney.Framework.BoneAnimation
{
    public class Bone
    {
        public Bone Parent;
        public float Length;
        public float Width;
        public float Angle;
        public Color Blend;
        public float Alpha;
        public float Depth; // depth is relative to the BoneAnimator's depth (negative depth is ok)
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 AttachPoint;
        public Vector2 Dimensions;
        public Vector2 Scale;
        public String Texture;
        public BoneAnimator Animator;
        public bool ManualControl;

        public List<Vector2> BoundingRect
        {
            get
            {
                if (!Animator.Flip)
                {
                    return new List<Vector2>()
                    {
                         Rotations.RotatePoint((Position - Origin), Position, Angle),
                         Rotations.RotatePoint(new Vector2((Position - Origin).X + Length, (Position - Origin).Y), Position, Angle),
                         Rotations.RotatePoint(new Vector2((Position - Origin).X + Length, (Position - Origin).Y + Width), Position, Angle),
                         Rotations.RotatePoint(new Vector2((Position - Origin).X, (Position - Origin).Y + Width), Position, Angle)
                    };
                }
                else
                {
                    float nAngle = (float)Math.PI - Angle;
                    Vector2 nOrigin = new Vector2(Origin.X, Width - Origin.Y);
                    return new List<Vector2>()
                    {
                         Rotations.RotatePoint((Position - nOrigin), Position, nAngle),
                         Rotations.RotatePoint(new Vector2((Position - nOrigin).X + Length, (Position - nOrigin).Y), Position, nAngle),
                         Rotations.RotatePoint(new Vector2((Position - nOrigin).X + Length, (Position - nOrigin).Y + Width), Position, nAngle),
                         Rotations.RotatePoint(new Vector2((Position - nOrigin).X, (Position - nOrigin).Y + Width), Position, nAngle)
                    };
                }
            }
        }

        public Bone(Bone parent, Vector2 attachPoint, Vector2 origin, float length, float width, String texture, float depth)
        {
            Parent = parent;
            AttachPoint = attachPoint;
            Origin = origin;
            Length = length;
            Width = width;
            Texture = texture;
            Depth = depth;
            Dimensions = new Vector2(length, width);
            Scale = Vector2.One;

            // defaults
            Angle = 0;
            Blend = Color.White;
            Alpha = 1.0f;

            // position gets set automatically
            Position = new Vector2();
        }

        public Bone(Bone parent, Vector2 attachPoint, Vector2 origin, float length, float width, String texture, float depth, float angle, Color color, float alpha)
        {
            Parent = parent;
            AttachPoint = attachPoint;
            Origin = origin;
            Length = length;
            Width = width;
            Texture = texture;
            Depth = depth;
            Dimensions = new Vector2(length, width);
            Scale = Vector2.One;
            Angle = angle;
            Blend = color;
            Alpha = alpha;

            // position gets set automatically
            Position = new Vector2();
        }
    }
}
