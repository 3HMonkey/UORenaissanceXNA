﻿/***************************************************************************
 *   RequestNamePacket.cs
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
    public class RequestNamePacket : SendPacket
    {
        public RequestNamePacket(Serial serial)
            : base(0x98, "Request Name", 7)
        {
            Stream.Write((short)7);
            Stream.Write((int)serial);
        }
    }
}
