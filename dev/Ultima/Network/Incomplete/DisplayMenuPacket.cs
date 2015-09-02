﻿/***************************************************************************
 *   DisplayMenuPacket.cs
 *   Copyright (c) 2009 UltimaXNA Development Team
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using UltimaXNA.Core.Network;
using UltimaXNA.Core.Network.Packets;
#endregion

namespace UltimaXNA.Ultima.Network.Server
{
    public class DisplayMenuPacket : RecvPacket
    {
        public DisplayMenuPacket(PacketReader reader)
            : base(0x7C, "Display Menu")
        {
            // TODO: Write this packet.
        }
    }
}
