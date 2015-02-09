using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CloudJourney.Framework.Renderables
{
    public interface Renderable
    {
        Vector2 position { get; set; }
        float depth { get; set; }
        Object parent { get; }

        void Draw();
        void Unload();
    }
}
