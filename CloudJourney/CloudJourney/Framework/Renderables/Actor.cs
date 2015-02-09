using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CloudJourney.Framework.Renderables
{
    public interface Actor : Renderable
    {
        Vector2 velocity { get; set; }
        Sprite sprite { get; }
        
        void Update();
    }
}
