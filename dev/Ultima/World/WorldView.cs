﻿/***************************************************************************
 *   WorldView.cs
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using InterXLib.Patterns.MVC;
using UltimaXNA.Ultima.World.Views;
#endregion

namespace UltimaXNA.Ultima.World
{
    class WorldView : AView
    {
        public IsometricRenderer Isometric
        {
            get;
            private set;
        }

        protected new WorldModel Model
        {
            get { return (WorldModel)base.Model; }
        }

        public WorldView(WorldModel model)
            : base(model)
        {
            Isometric = new IsometricRenderer();
            Isometric.Initialize();
            Isometric.LightDirection = -0.6f;
        }

        public override void Draw(double frameTime)
        {
            Isometric.Draw(Model.Map, EntityManager.GetPlayerObject().Position, Model.Input.MousePick);
        }
    }
}
