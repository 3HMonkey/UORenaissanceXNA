﻿/***************************************************************************
 *   ObjectPropertyListUpdatePacket.cs
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
    public class ObjectPropertyListUpdatePacket : RecvPacket
    {
        readonly Serial m_serial;
        readonly int m_revisionHash;

        public Serial Serial
        {
            get { return m_serial; }
        }

        public int RevisionHash 
        {
            get { return m_revisionHash; }
        }

        public ObjectPropertyListUpdatePacket(PacketReader reader)
            : base(0xDC, "Object Property List Update")
        {
            m_serial = reader.ReadInt32();
            m_revisionHash = reader.ReadInt32();
        }
    }
}
