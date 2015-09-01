﻿/***************************************************************************
 *   TalkRequestPacket.cs
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
    public class TalkRequestPacket : SendPacket
    {
        public TalkRequestPacket(byte speechType, short color, short font, string message)
            : base(0x03, "Talk Request")
        {
            Stream.Write((byte)speechType);
            Stream.Write((short)color);
            Stream.Write((short)font);
            Stream.WriteAsciiNull(message);
        }
    }
}
