﻿/***************************************************************************
 *   GumpTextEntryDialogReplyPacket.cs
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
    public class GumpTextEntryDialogReplyPacket : SendPacket
    {
        public GumpTextEntryDialogReplyPacket(int id, byte type, byte index, string reply)
            : base(0xAC, "Gump TextEntry Dialog Reply")
        {
            Stream.Write(id);
            Stream.Write((byte)type);
            Stream.Write((byte)index);
            Stream.Write((short)0x0000);
            Stream.Write((byte)0x00);
            Stream.WriteAsciiNull(reply);
        }
    }
}
