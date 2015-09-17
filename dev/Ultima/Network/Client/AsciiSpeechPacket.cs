﻿/***************************************************************************
 *   AsciiSpeechPacket.cs
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using System;
using System.Collections.Generic;
using UltimaXNA.Core.Network.Packets;
using UltimaXNA.Ultima.Resources;
using UltimaXNA.Ultima.Data;
#endregion

namespace UltimaXNA.Ultima.Network.Client
{
    public class AsciiSpeechPacket : SendPacket
    {
        public AsciiSpeechPacket(MessageTypes type, int font, int hue, string lang, string text)
            : base(0xAD, "Ascii Speech")
        {
            // get triggers
            int triggerCount; int[] triggers;
            SpeechData.GetSpeechTriggers(text, lang, out triggerCount, out triggers);
            if (triggerCount > 0)
                type = (MessageTypes)(type | MessageTypes.EncodedTriggers);

            Stream.Write((byte)type);
            Stream.Write((short)hue);
            Stream.Write((short)font);
            Stream.WriteAsciiNull(lang);
            if (triggerCount > 0)
            {
                byte[] t = new byte[(int)Math.Ceiling((triggerCount + 1) * 1.5f)];
                // write 12 bits at a time. first write count: byte then half byte.
                t[0] = (byte)((triggerCount & 0x0FF0) >> 4);
                t[1] = (byte)((triggerCount & 0x000F) << 4);
                for (int i = 0; i < triggerCount; i++)
                {
                    int index = (int)((i + 1) * 1.5f);
                    if (i % 2 == 0) // write half byte and then byte
                    {
                        t[index + 0] |= (byte)((triggers[i] & 0x0F00) >> 8);
                        t[index + 1] = (byte)(triggers[i] & 0x00FF);
                    }
                    else // write byte and then half byte
                    {
                        t[index] = (byte)((triggers[i] & 0x0FF0) >> 4);
                        t[index + 1] = (byte)((triggers[i] & 0x000F) << 4);
                    }
                }
                Stream.BaseStream.Write(t, 0, t.Length);
                Stream.WriteAsciiNull(text);
            }
            else
            {
                Stream.WriteBigUniNull(text);
            }
        }

        List<int> getSpeechTriggers(string text)
        {
            List<int> triggers = new List<int>();

            return triggers;
        }
    }
}
