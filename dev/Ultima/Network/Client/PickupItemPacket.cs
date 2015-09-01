﻿/***************************************************************************
 *   PickupItemPacket.cs
 *   Copyright (c) 2009 UltimaXNA Development Team
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using UltimaXNA.Core.Network.Packets;
#endregion

namespace UltimaXNA.Ultima.Network.Client
{
    public class PickupItemPacket : SendPacket
    {
        public PickupItemPacket(Serial serial, int count)
            : base(0x07, "Pickup Item", 7)
        {
            Stream.Write(serial);
            Stream.Write((short)count);
        }
    }
}
