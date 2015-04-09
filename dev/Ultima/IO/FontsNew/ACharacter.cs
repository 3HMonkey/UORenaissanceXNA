﻿using Microsoft.Xna.Framework;

namespace UltimaXNA.Ultima.IO.FontsNew
{
    abstract internal class ACharacter
    {
        public int Width, Height;
        public int XOffset = 0, YOffset = 0;

        protected uint[] m_PixelData;

        public unsafe void WriteToBuffer(uint* dstPtr, int dx, int dy, int linewidth, int maxHeight, int baseLine,
            bool isBold, bool isItalic, bool isUnderlined, bool isOutlined, uint color, uint outline)
        {

            if (m_PixelData != null)
            {
                fixed (uint* srcPtr = m_PixelData)
                {
                    for (int y = 0; (y < Height) && (y + dy < maxHeight); y++)
                    {
                        uint* src = ((uint*)srcPtr) + (Width * y);
                        uint* dest = (((uint*)dstPtr) + (linewidth * (y + dy + YOffset)) + dx);
                        if (isItalic)
                        {
                            dest += (baseLine - YOffset - y - 1) / 2;
                        }

                        for (int x = 0; x < Width; x++)
                        {
                            if (*src != 0x00000000)
                            {
                                if (isOutlined)
                                {
                                    for (int iy = -1; iy <= 1; iy++)
                                    {
                                        uint* idest = dest + (iy * linewidth);
                                        if (*idest == 0x00000000)
                                            *idest = outline;
                                        if (iy == 0)
                                        {
                                            if (isBold)
                                            {
                                                if (*(src - 1) == 0x00000000)
                                                {
                                                    *(idest) = outline;
                                                    *(idest + 1) = color;
                                                }
                                                else
                                                {
                                                    *(idest + 1) = color;
                                                }
                                                *(idest + 2) = color;
                                            }
                                            else
                                            {
                                                *(idest + 1) = color;
                                            }
                                        }
                                        else
                                        {
                                            if (*(idest + 1) == 0x00000000)
                                                *(idest + 1) = outline;
                                        }


                                        if (*(idest + 2) == 0x00000000)
                                            *(idest + 2) = outline;
                                        if (isBold)
                                        {
                                            if (*(idest + 3) == 0x00000000)
                                                *(idest + 3) = outline;
                                        }
                                    }
                                }
                                else
                                {
                                    *dest = color;
                                    if (isBold)
                                        *(dest + 1) = color;
                                }
                            }
                            dest++;
                            src++;
                        }
                    }
                }
            }

            if (isUnderlined)
            {
                int underlineAtY = dy + baseLine + 2;
                if (underlineAtY >= maxHeight)
                    return;
                uint* dest = (((uint*)dstPtr) + (linewidth * (underlineAtY)) + dx);
                int w = isBold ? Width + 2 : Width + 1;
                for (int k = 0; k < w; k++)
                {
                    if (isOutlined)
                    {
                        for (int iy = -1; iy <= 1; iy++)
                        {
                            uint* idest = dest + (iy * linewidth);
                            if (*idest == 0x00000000)
                                *idest = outline;
                            if (iy == 0)
                                *(idest + 1) = color;
                            else
                            {
                                if (*(idest + 1) == 0x00000000)
                                    *(idest + 1) = outline;
                            }

                            if (*(idest + 2) == 0x00000000)
                                *(idest + 2) = outline;
                        }
                    }
                    else
                    {
                        *dest = color;
                    }
                    dest++;
                }
            }
        }
    }
}