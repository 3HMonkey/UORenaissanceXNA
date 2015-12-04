﻿/***************************************************************************
 *   TextLabelAscii.cs
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
#endregion

namespace UltimaXNA.Ultima.UI.Controls
{
    class TextLabelAscii : AControl
    {
        public int Hue = 0;
        public int FontID = 0;

        private RenderedText m_Rendered;
        private string m_Text;
        private int m_Width;

        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                    m_Rendered.Text = string.Format("<span style=\"font-family:ascii{0}\">{1}", FontID, m_Text);
                }
            }
        }

        public TextLabelAscii(AControl parent, int width = 400)
            : base(parent)
        {
            m_Width = width;
            m_Rendered = new RenderedText(string.Empty, m_Width, true);
        }

        public TextLabelAscii(AControl parent, int x, int y, int font, int hue, string text, int width = 400)
            : this(parent, width)
        {
            buildGumpling(x, y, font, hue, text);
        }

        void buildGumpling(int x, int y, int font, int hue, string text)
        {
            Position = new Point(x, y);
            Hue = hue;
            FontID = font;
            Text = text;
        }

        public override void Draw(SpriteBatchUI spriteBatch, Point position)
        {
            m_Rendered.Draw(spriteBatch, position, Utility.GetHueVector(Hue, true, false, true));
            base.Draw(spriteBatch, position);
        }
    }
}
