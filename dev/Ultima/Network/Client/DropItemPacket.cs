﻿/***************************************************************************
 *   DropItemPacket.cs
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
    public class DropItemPacket : SendPacket
    {
        public DropItemPacket(Serial serial, ushort x, ushort y, byte z, byte gridIndex, Serial containerSerial)
            : base(0x08, "Drop Item", 15)
        {
            Stream.Write(serial);
            Stream.Write((ushort)x);
            Stream.Write((ushort)y);
            Stream.Write((byte)z);
            Stream.Write((byte)gridIndex);
            Stream.Write(containerSerial);
        }
    }
}
