﻿/***************************************************************************
 *   Extended0x78Packet.cs
 *   
 *   begin                : May 31, 2009
 *   email                : poplicola@ultimaxna.com
 *
 ***************************************************************************/

/***************************************************************************
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
    public class Extended0x78Packet : RecvPacket
    {
        public Extended0x78Packet(PacketReader reader)
            : base(0xD3, "Extended 0x78")
        {
            // TODO: Write this packet.
        }
    }
}
