﻿/***************************************************************************
 *   OverheadView.cs
 *   Copyright (c) 2015 UltimaXNA Development Team
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using Microsoft.Xna.Framework;
using UltimaXNA.Core.Graphics;
using UltimaXNA.Core.UI;
using UltimaXNA.Ultima.World.Entities;
using UltimaXNA.Ultima.World.Input;
using UltimaXNA.Ultima.World.Maps;
#endregion

namespace UltimaXNA.Ultima.World.EntityViews
{
    class OverheadView : AEntityView
    {
        new Overhead Entity => (Overhead)base.Entity;
        RenderedText m_Text;

        public OverheadView(Overhead entity)
            : base(entity)
        {
            m_Text = new RenderedText(Entity.Text, collapseContent: true);
            DrawTexture = m_Text.Texture;
        }

        public override bool Draw(SpriteBatch3D spriteBatch, Vector3 drawPosition, MouseOverList mouseOver, Map map, bool roofHideFlag)
        {
            HueVector = Utility.GetHueVector(Entity.Hue, false, false, true);
            return base.Draw(spriteBatch, drawPosition, mouseOver, map, roofHideFlag);
        }
    }
}
