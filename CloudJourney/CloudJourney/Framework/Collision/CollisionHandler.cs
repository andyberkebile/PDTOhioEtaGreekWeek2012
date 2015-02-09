using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Renderables;
using Microsoft.Xna.Framework;

namespace CloudJourney.Framework.Collision
{
    public class CollisionHandler
    {
        public RenderableCollection renderables;

        public CollisionHandler(RenderableCollection collection)
        {
            renderables = collection;
        }

        public bool PositionFree(Actor a, Vector2 pos)
        {
            Vector2 oldpos = a.position;
            a.position = pos;

            List<Vector2> arect = new List<Vector2> {
                                      a.position - a.sprite.bposition,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitX),
                                      a.position - a.sprite.bposition + a.sprite.bounding,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitY)
                                  };

            foreach (Renderable actors in renderables)
            {
                if (actors is Actor)
                {
                    Actor act = actors as Actor;
                    if (a.Equals(act)) continue;

                    List<Vector2> orect = new List<Vector2> {
                                      act.position - act.sprite.bposition,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitX),
                                      act.position - act.sprite.bposition + act.sprite.bounding,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitY)
                                  };

                    if (Collision2D.CheckRectangles(arect, orect))
                    {
                        a.position = oldpos;
                        return false;
                    }
                }
            }

            a.position = oldpos;
            return true;
        }

        public bool NextPositionFree(Actor a)
        {
            a.position += a.velocity;

            List<Vector2> arect = new List<Vector2> {
                                      a.position - a.sprite.bposition,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitX),
                                      a.position - a.sprite.bposition + a.sprite.bounding,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitY)
                                  };

            foreach (Renderable actors in renderables)
            {
                if (actors is Actor)
                {
                    Actor act = actors as Actor;
                    if (a.Equals(act)) continue;

                    List<Vector2> orect = new List<Vector2> {
                                      act.position - act.sprite.bposition,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitX),
                                      act.position - act.sprite.bposition + act.sprite.bounding,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitY)
                                  };

                    if (Collision2D.CheckRectangles(arect, orect))
                    {
                        a.position -= a.velocity;
                        return false;
                    }
                }
            }

            a.position -= a.velocity;
            return true;
        }

        public bool NextPositionFreeX(Actor a)
        {
            a.position += a.velocity * Vector2.UnitX;

            List<Vector2> arect = new List<Vector2> {
                                      a.position - a.sprite.bposition,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitX),
                                      a.position - a.sprite.bposition + a.sprite.bounding,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitY)
                                  };

            foreach (Renderable actors in renderables)
            {
                if (actors is Actor)
                {
                    Actor act = actors as Actor;
                    if (a.Equals(act)) continue;

                    List<Vector2> orect = new List<Vector2> {
                                      act.position - act.sprite.bposition,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitX),
                                      act.position - act.sprite.bposition + act.sprite.bounding,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitY)
                                  };

                    if (Collision2D.CheckRectangles(arect, orect))
                    {
                        a.position -= a.velocity * Vector2.UnitX;
                        return false;
                    }
                }
            }

            a.position -= a.velocity * Vector2.UnitX;
            return true;
        }

        public bool NextPositionFreeY(Actor a)
        {
            a.position += a.velocity * Vector2.UnitY;

            List<Vector2> arect = new List<Vector2> {
                                      a.position - a.sprite.bposition,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitX),
                                      a.position - a.sprite.bposition + a.sprite.bounding,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitY)
                                  };

            foreach (Renderable actors in renderables)
            {
                if (actors is Actor)
                {
                    Actor act = actors as Actor;
                    if (a.Equals(act)) continue;

                    List<Vector2> orect = new List<Vector2> {
                                      act.position - act.sprite.bposition,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitX),
                                      act.position - act.sprite.bposition + act.sprite.bounding,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitY)
                                  };

                    if (Collision2D.CheckRectangles(arect, orect))
                    {
                        a.position -= a.velocity * Vector2.UnitY;
                        return false;
                    }
                }
            }

            a.position -= a.velocity * Vector2.UnitY;
            return true;
        }

        public CollisionData CheckNextPosition(Actor a)
        {
            a.position += a.velocity;

            List<Vector2> arect = new List<Vector2> {
                                      a.position - a.sprite.bposition,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitX),
                                      a.position - a.sprite.bposition + a.sprite.bounding,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitY)
                                  };

            foreach (Renderable actors in renderables)
            {
                if (actors is Actor)
                {
                    Actor act = actors as Actor;
                    if (a.Equals(act)) continue;

                    List<Vector2> orect = new List<Vector2> {
                                      act.position - act.sprite.bposition,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitX),
                                      act.position - act.sprite.bposition + act.sprite.bounding,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitY)
                                  };

                    if (Collision2D.CheckRectangles(arect, orect))
                    {
                        a.position -= a.velocity;
                        CollisionData cd = new CollisionData(a, act, DetermineSide(arect, orect, a.velocity));
                        return cd;
                    }
                }
            }

            a.position -= a.velocity;
            return null;
        }

        public CollisionData CheckCollision(Actor a)
        {
            List<Vector2> arect = new List<Vector2> {
                                      a.position - a.sprite.bposition,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitX),
                                      a.position - a.sprite.bposition + a.sprite.bounding,
                                      a.position - a.sprite.bposition + (a.sprite.bounding * Vector2.UnitY)
                                  };

            foreach (Renderable actors in renderables)
            {
                if (actors is Actor)
                {
                    Actor act = actors as Actor;

                    if (a.Equals(act)) continue;
                    
                    List<Vector2> orect = new List<Vector2> {
                                      act.position - act.sprite.bposition,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitX),
                                      act.position - act.sprite.bposition + act.sprite.bounding,
                                      act.position - act.sprite.bposition + (act.sprite.bounding * Vector2.UnitY)
                                  };

                    if (Collision2D.CheckRectangles(arect, orect))
                    {
                        CollisionData cd = new CollisionData(a, act, DetermineSide(arect, orect, a.velocity));
                        return cd;
                    }
                }
            }
            return null;
        }

        private Side DetermineSide(List<Vector2> r1, List<Vector2> r2, Vector2 velocity)
        {
            // Corner cases
            if (r1[0].X > r2[0].X && r1[0].Y > r2[0].Y &&
                r1[0].X < r2[1].X && r1[0].Y > r2[1].Y &&
                r1[0].X < r2[2].X && r1[0].Y < r2[2].Y &&
                r1[0].X > r2[3].X && r1[0].Y < r2[3].Y)
            {
                if (Math.Abs(velocity.X) >= Math.Abs(velocity.Y))
                    return Side.RIGHT;
                else
                    return Side.BOTTOM;
            }

            if (r1[1].X > r2[0].X && r1[1].Y > r2[0].Y &&
                r1[1].X < r2[1].X && r1[1].Y > r2[1].Y &&
                r1[1].X < r2[2].X && r1[1].Y < r2[2].Y &&
                r1[1].X > r2[3].X && r1[1].Y < r2[3].Y)
            {
                if (Math.Abs(velocity.X) >= Math.Abs(velocity.Y))
                    return Side.LEFT;
                else
                    return Side.BOTTOM;
            }

            if (r1[2].X > r2[0].X && r1[2].Y > r2[0].Y &&
                r1[2].X < r2[1].X && r1[2].Y > r2[1].Y &&
                r1[2].X < r2[2].X && r1[2].Y < r2[2].Y &&
                r1[2].X > r2[3].X && r1[2].Y < r2[3].Y)
            {
                if (Math.Abs(velocity.X) >= Math.Abs(velocity.Y))
                    return Side.RIGHT;
                else
                    return Side.TOP;
            }

            if (r1[3].X > r2[0].X && r1[3].Y > r2[0].Y &&
                r1[3].X < r2[1].X && r1[3].Y > r2[1].Y &&
                r1[3].X < r2[2].X && r1[3].Y < r2[2].Y &&
                r1[3].X > r2[3].X && r1[3].Y < r2[3].Y)
            {
                if (Math.Abs(velocity.X) >= Math.Abs(velocity.Y))
                    return Side.LEFT;
                else
                    return Side.TOP;
            }

            // Sides
            if (r1[0].X > r2[0].X && r1[0].X < r2[1].X)
                return Side.RIGHT;

            if (r1[1].X > r2[0].X && r1[1].X < r2[1].X)
                return Side.LEFT;

            if (r1[1].Y > r2[0].Y && r1[1].Y < r2[3].Y)
                return Side.BOTTOM;

            if (r1[2].Y > r2[0].Y && r1[2].Y < r2[3].Y)
                return Side.TOP;

            return Side.NONE;
        }
    }
}
