using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloudJourney.Framework.Collision
{
    public class Collision2D
    {
        /// <summary>
        /// The render target for creating collision masks for per-pixel collisions.
        /// </summary>
        public static RenderTarget2D rtarg;

        /// <summary>
        /// The alpha value of a pixel must be greater than this to be counted as solid. Max 255.
        /// </summary>
        private const byte TOLERANCE = 128;

        /// <summary>
        /// Checks if two rectangles represented by lists of 4 Vector2s overlap.
        /// </summary>
        public static bool CheckRectangles(List<Vector2> rect1, List<Vector2> rect2)
        {
            List<Vector2> Triangle11 = TriangleFromRectangle(rect1, true);
            List<Vector2> Triangle12 = TriangleFromRectangle(rect1, false);
            foreach (Vector2 point in rect2)
            {
                if (CheckPointTriangle(Triangle11, point)) return true;
                if (CheckPointTriangle(Triangle12, point)) return true;
            }
            List<Vector2> Triangle21 = TriangleFromRectangle(rect2, true);
            List<Vector2> Triangle22 = TriangleFromRectangle(rect2, false);
            foreach (Vector2 point in rect1)
            {
                if (CheckPointTriangle(Triangle21, point)) return true;
                if (CheckPointTriangle(Triangle22, point)) return true;
            }
            return false;
        }

        #region Per-Pixel
        /// <summary>
        /// Checks if two sprites overlap on a per-pixel level.
        /// </summary>
        public static bool CheckPerPixel(Texture2D Texture1, Texture2D Texture2, Vector2 Pos1, Vector2 Pos2, Vector2 Orig1, Vector2 Orig2, List<Vector2> Rect1, List<Vector2> Rect2,
                                      float Theta1, float Theta2)
        {

            if (!CheckRectangles(Rect1, Rect2)) return false; // if the bounding rectangles don't collide, don't bother going to per-pixel level
            Color[] TextureData1 = GetSpriteData(Texture1, Pos1, Orig1, Theta1, Rect1);
            Color[] TextureData2 = GetSpriteData(Texture2, Pos2, Orig2, Theta2, Rect2);

            Rectangle Rectangle1 = BoundingRectangle(Rect1);
            Rectangle Rectangle2 = BoundingRectangle(Rect2);

            int top = Math.Max(Rectangle1.Top, Rectangle2.Top);
            int bottom = Math.Min(Rectangle1.Bottom, Rectangle2.Bottom);
            int left = Math.Max(Rectangle1.Left, Rectangle2.Left);
            int right = Math.Min(Rectangle1.Right, Rectangle2.Right);

            for (int y = top; y < bottom; y++)
                for (int x = left; x < right; x++)
                {
                    Color colorA = TextureData1[(x - Rectangle1.Left) + (y - Rectangle1.Top) * Rectangle1.Width];
                    Color colorB = TextureData2[(x - Rectangle2.Left) + (y - Rectangle2.Top) * Rectangle2.Width];
                    if (colorA.A > TOLERANCE && colorB.A > TOLERANCE) return true;
                }

            return false;
        }

        /// <summary>
        /// Returns the pixel data of a rotated sprite.
        /// </summary>
        private static Color[] GetSpriteData(Texture2D textureToDraw, Vector2 Position, Vector2 Origin, float Theta, List<Vector2> RectanglePoints)
        {
            // draw the sprite rotated on a separate render target
            Cloud.spriteBatch.GraphicsDevice.SetRenderTarget(rtarg);
            Cloud.spriteBatch.GraphicsDevice.Clear(Color.Black * 0.0f); // clear target with 0 alpha
            Cloud.spriteBatch.Begin();
            Cloud.spriteBatch.Draw(textureToDraw, Position, null, Color.White, Theta, Origin, 1.0f, SpriteEffects.None, 0);
            Cloud.spriteBatch.End();
            Cloud.spriteBatch.GraphicsDevice.SetRenderTarget(null);

            Rectangle bound = BoundingRectangle(RectanglePoints);
            int size = bound.Width * bound.Height;
            Color[] pixels = new Color[size];
            rtarg.GetData(0, bound, pixels, 0, size);

            return pixels;
        }
        #endregion

        /// <summary>
        /// Returns whether a point lies within a given triangle.
        /// </summary>
        private static bool CheckPointTriangle(List<Vector2> triangle, Vector2 point)
        {
            #region Old Grossness
            /*Vector2 e0 = point - triangle[0];
            Vector2 e1 = triangle[1] - triangle[0];
            Vector2 e2 = triangle[2] - triangle[0];

            float u, v = 0;
            if (e1.X == 0)
            {
                if (e2.X == 0) return false;
                u = e0.X / e2.X;
                if (u < 0 || u > 1) return false;
                if (e1.Y == 0) return false;
                v = (e0.Y - e2.Y * u) / e1.Y;
                if (v < 0) return false;
            }
            else
            {
                float d = e2.Y * e1.X - e2.X * e1.Y;
                if (d == 0) return false;
                u = (e0.Y * e1.X - e0.X * e1.Y) / d;
                if (u < 0 || u > 1) return false;
                v = (e0.X - e2.X * u) / e1.X;
                if (v < 0) return false;
                if ((u + v) > 1) return false;
            }

            return true;*/
            #endregion

            return IsPointInTri(point, triangle[0], triangle[1], triangle[2]);
        }

        private static float Sign(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        private static bool IsPointInTri(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
        {
            bool b1, b2, b3;
            b1 = Sign(pt, v1, v2) < 0.0f;
            b2 = Sign(pt, v2, v3) < 0.0f;
            b3 = Sign(pt, v3, v1) < 0.0f;
            return ((b1 == b2) && (b2 == b3));
        }

        /// <summary>
        /// Cuts a rectangle into two triangles. Returns the specified half.
        /// </summary>
        public static List<Vector2> TriangleFromRectangle(List<Vector2> rect, bool firsthalf)
        {
            if (firsthalf)
            {
                return new List<Vector2>()
                {
                    new Vector2(rect[0].X, rect[0].Y),
                    new Vector2(rect[1].X, rect[1].Y),
                    new Vector2(rect[3].X, rect[3].Y),
                };
            }
            else
            {
                return new List<Vector2>()
                {
                    new Vector2(rect[1].X, rect[1].Y),
                    new Vector2(rect[2].X, rect[2].Y),
                    new Vector2(rect[3].X, rect[3].Y),
                };
            }
        }

        /// <summary>
        /// Returns the non-rotated rectangle around a probably rotated rectangle.
        /// </summary>
        public static Rectangle BoundingRectangle(List<Vector2> rect)
        {
            Vector2 BoundingRectangleStart = new Vector2(Min4(rect[0].X, rect[1].X, rect[2].X, rect[3].X), Min4(rect[0].Y, rect[1].Y, rect[2].Y, rect[3].Y));

            int BoundingRectangleWidth = -(int)BoundingRectangleStart.X + (int)Max4(rect[0].X, rect[1].X, rect[2].X, rect[3].X);
            int BoundingRectangleHeight = -(int)BoundingRectangleStart.Y + (int)Max4(rect[0].Y, rect[1].Y, rect[2].Y, rect[3].Y);

            return new Rectangle((int)BoundingRectangleStart.X, (int)BoundingRectangleStart.Y, BoundingRectangleWidth, BoundingRectangleHeight);
        }

        /// <summary>
        /// Returns the minimum of 4 floats.
        /// </summary>
        private static float Min4(float a, float b, float c, float d)
        {
            return MathHelper.Min(MathHelper.Min(MathHelper.Min(a, b), c), d);
        }

        /// <summary>
        /// Returns the maximum of 4 floats.
        /// </summary>
        private static float Max4(float a, float b, float c, float d)
        {
            return MathHelper.Max(MathHelper.Max(MathHelper.Max(a, b), c), d);
        }
    }
}