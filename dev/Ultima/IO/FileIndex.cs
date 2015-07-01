﻿/***************************************************************************
 *   FileIndex.cs
 *   Based on code from UltimaSDK: http://ultimasdk.codeplex.com/
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using System.IO;
using System.Runtime.InteropServices;
using UltimaXNA.Core;
using UltimaXNA.Core.IO;

#endregion

namespace UltimaXNA.Ultima.IO
{
    public class FileIndex
    {
        private Entry3D[] m_Index;
        private Stream m_Stream;

        public Entry3D[] Index { get { return m_Index; } }
        public Stream Stream { get { return m_Stream; } }

        public BinaryFileReader Seek(int index, out int length, out int extra, out bool patched)
        {
            if (index < 0 || index >= m_Index.Length)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            Entry3D e = m_Index[index];

            if (e.lookup < 0)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            length = e.length & 0x7FFFFFFF;
            extra = e.extra;

            if ((e.length & 0xFF000000) != 0)
            {
                patched = true;

                VerData.Stream.Seek(e.lookup, SeekOrigin.Begin);
                return new BinaryFileReader(new BinaryReader(VerData.Stream));
            }
            else if (m_Stream == null)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            patched = false;

            m_Stream.Position = e.lookup;
            return new BinaryFileReader(new BinaryReader(m_Stream));
        }

        /// <summary>
        /// Creates a reference to an index file. (Ex: anim.idx)
        /// </summary>
        /// <param name="idxFile">Name of .idx file in UO base directory.</param>
        /// <param name="mulFile">Name of .mul file that this index file provides an index for.</param>
        /// <param name="length">Number of indexes in this index file.</param>
        /// <param name="patch_file">Index to patch data in Versioning.</param>
        public FileIndex(string idxFile, string mulFile, int length, int patch_file)
        {
            m_Index = new Entry3D[length];

            string idxPath = FileManager.GetFilePath(idxFile);
            string mulPath = FileManager.GetFilePath(mulFile);

            if (idxPath != null && mulPath != null)
            {
                using (FileStream index = new FileStream(idxPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader bin = new BinaryReader(index);
                    m_Stream = new FileStream(mulPath, FileMode.Open, FileAccess.Read, FileShare.Read);

                    int count = (int)(index.Length / 12);
                    int max = (count < length) ? count : length;

                    unsafe
                    {
                        byte[] data = bin.ReadBytes(max * 12);
                        fixed (byte* pBuff = data)
                        {
                            byte* pBuffRef = pBuff;
                            for (int i = 0; i < max; i++)
                            {
                                m_Index[i] = *((Entry3D*)pBuffRef);
                                pBuffRef += 12;
                            }
                        }

                        int[] empty = new int[3] { -1, -1, -1 };
                        fixed (byte* pBuff = data)
                        {
                            byte* pBuffRef = pBuff;
                            for (int i = max; i < length; i++)
                            {
                                m_Index[i] = *((Entry3D*)pBuffRef);
                            }
                        }
                    }
                }
            }

            Entry5D[] patches = VerData.Patches;

            for (int i = 0; i < patches.Length; ++i)
            {
                Entry5D patch = patches[i];

                if (patch.file == patch_file && patch.index >= 0 && patch.index < length)
                {
                    m_Index[patch.index].lookup = patch.lookup;
                    m_Index[patch.index].length = patch.length | (1 << 31);
                    m_Index[patch.index].extra = patch.extra;
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
    public struct Entry3D
    {
        public int lookup;
        public int length;
        public int extra;
    }
}