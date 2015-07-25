﻿/***************************************************************************
 *   StringExtensions.cs
 *   Copyright (c) 2015 UltimaXNA Development Team
 * 
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

public static class SecureStringExtensions
{
    public static string ConvertToUnsecureString(this SecureString securePassword)
    {
        if (securePassword == null)
            throw new ArgumentNullException("securePassword");

        IntPtr unmanagedString = IntPtr.Zero;
        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }
}

public static class StringExtensions
{
    public static string Wrap(this string sentence, int limit, int indentationCount, char indentationCharacter)
    {
        string[] words = sentence.Replace("\n", " ").Replace("\r", " ").Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int counter = 0;
        StringBuilder builder = new StringBuilder();

        for(int index = 0; index < words.Length; index++)
        {
            string word = words[index];

            if((builder.Length + word.Length) / limit > counter)
            {
                counter++;
                builder.AppendLine();

                for (int i = 0; i < indentationCount; i++)
                {
                    builder.Append(indentationCharacter);
                }
            }

            builder.Append(word);

            if (index < words.Length)
            {
                builder.Append(" ");
            }
        }

        return builder.ToString();
    }
}