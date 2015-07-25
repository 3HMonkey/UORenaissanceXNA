﻿/***************************************************************************
 *   DoubleClickPacket.cs
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
    public class DoubleClickPacket : SendPacket
    {
        public DoubleClickPacket(Serial serial)
            : base(0x06, "Double Click", 5)
        {
            Stream.Write(serial);
        }
    }
}
