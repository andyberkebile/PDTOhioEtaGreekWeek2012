﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Renderables;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Scenes;
using CloudJourney.Scenes;
using CloudJourney.Framework.Rendering;

namespace CloudJourney.Actors.Obstacles
{
    class car : Obstacle
    {


        public car(Vector2 pos, Scene par)
        {
            initialWeight = 5;
            weight = initialWeight;
            hit = false;
            velocity = new Vector2(GameScene.vanSpeed, 0);
            position = pos;
            depth = 0.9f;
            parent = par;
            /*anispr = new AnimatedSprite("character", @"Art\Character", pos, new Vector2(8f, 24.0f), new Vector2(16.0f, 24.0f), this);
            anispr.frameCount.Add(4);
            anispr.speed = 0.2f;
            anispr.depth = depth;*/
            sprite = new Sprite("car", @"Art/Obstacles", Vector2.Zero, new Vector2(87, 162), this);
            sprite.bounding = new Vector2(175, 100);
            sprite.bposition = new Vector2(87, 100);
        }
    }
}
