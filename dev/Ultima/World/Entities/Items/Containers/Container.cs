﻿/***************************************************************************
 *   Container.cs
 *   Copyright (c) 2015 UltimaXNA Development Team
 * 
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using System.Collections.Generic;
using UltimaXNA.Ultima.World.Maps;
#endregion

namespace UltimaXNA.Ultima.World.Entities.Items.Containers
{
    public class Container : Item
    {
        List<Item> m_Contents;
        bool m_ContentsUpdated;

        public List<Item> Contents
        {
            get
            {
                if (m_Contents == null)
                    m_Contents = new List<Item>();
                return m_Contents;
            }
        }

        public Container(Serial serial, Map map)
            : base(serial, map)
        {
            m_ContentsUpdated = true;
        }

        public override void Update(double frameMS)
        {
            base.Update(frameMS);
            if (m_ContentsUpdated)
            {
                m_OnUpdated?.Invoke(this);
                m_ContentsUpdated = false;
            }
        }

        public override void Dispose()
        {
            for (int i = 0; i < Contents.Count; i++)
            {
                Contents[i].Dispose();
            }
            base.Dispose();
        }

        public void AddItem(Item item)
        {
            if (!Contents.Contains(item))
            {
                Contents.Add(item);
                item.Parent = this;
            }
            m_ContentsUpdated = true;
        }

        public virtual void RemoveItem(Serial serial)
        {
            foreach (Item item in Contents)
            {
                if (item.Serial == serial)
                {
                    item.SaveLastParent();
                    Contents.Remove(item);
                    break;
                }
            }
            m_ContentsUpdated = true;
        }
    }
}
