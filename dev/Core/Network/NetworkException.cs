﻿/***************************************************************************
 *   NetworkException.cs
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using System;
#endregion

namespace UltimaXNA.Core.Network
{
    public class NetworkException : Exception
    {
        public NetworkException(string message)
            : base(message)
        {

        }
    }
}
