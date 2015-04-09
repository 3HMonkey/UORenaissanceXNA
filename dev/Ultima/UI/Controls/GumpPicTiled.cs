﻿/***************************************************************************
 *   GumpPicTiled.cs
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimaXNA.Core.Graphics;
using UltimaXNA.Ultima.UI;

namespace UltimaXNA.Ultima.UI.Controls
{
    class GumpPicTiled : AControl
    {
        Texture2D m_bgGump = null;
        int m_gumpID;

        public GumpPicTiled(AControl owner, int page)
            : base(owner, page)
        {

        }

        public GumpPicTiled(AControl owner, int page, string[] arguements)
            : this(owner, page)
        {
            int x, y, gumpID, width, height;
            x = Int32.Parse(arguements[1]);
            y = Int32.Parse(arguements[2]);
            width = Int32.Parse(arguements[3]);
            height = Int32.Parse(arguements[4]);
            gumpID = Int32.Parse(arguements[5]);
            buildGumpling(x, y, width, height, gumpID);
        }

        public GumpPicTiled(AControl owner, int page, int x, int y, int width, int height, int gumpID)
            : this(owner, page)
        {
            buildGumpling(x, y, width, height, gumpID);
        }

        void buildGumpling(int x, int y, int width, int height, int gumpID)
        {
            Position = new Point(x, y);
            Size = new Point(width, height);
            m_gumpID = gumpID;
        }

        public override void Update(double totalMS, double frameMS)
        {
            if (m_bgGump == null)
            {
                m_bgGump = IO.GumpData.GetGumpXNA(m_gumpID);
            }
            base.Update(totalMS, frameMS);
        }

        public override void Draw(SpriteBatchUI spriteBatch)
        {
            spriteBatch.Draw2DTiled(m_bgGump, new Rectangle(X, Y, Area.Width, Area.Height), 0, false, false);
            base.Draw(spriteBatch);
        }
    }
}
