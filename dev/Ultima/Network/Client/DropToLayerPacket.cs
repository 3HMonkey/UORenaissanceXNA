﻿/***************************************************************************
 *   DropToLayerPacket.cs
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
    public class DropToLayerPacket : SendPacket
    {
        public DropToLayerPacket(Serial itemSerial, byte layer, Serial playerSerial)
            : base(0x13, "Drop To Layer", 10)
        {
            Stream.Write(itemSerial);
            Stream.Write((byte)layer);
            Stream.Write(playerSerial);
        }
    }
}
