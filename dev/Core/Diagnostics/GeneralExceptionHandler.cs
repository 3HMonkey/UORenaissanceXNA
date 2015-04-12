﻿using System;
using UltimaXNA.Core.Diagnostics.Tracing;

namespace UltimaXNA.Core.Diagnostics
{
    public class GeneralExceptionHandler
    {
        private static GeneralExceptionHandler m_instance;

        public static GeneralExceptionHandler Instance
        {
            get { return m_instance ?? (m_instance = new GeneralExceptionHandler()); }
            set { m_instance = value; }
        }

        public void OnError(Exception e)
        {
            Tracer.Error(e);
            OnErrorOverride(e);
        }
        
        protected virtual void OnErrorOverride(Exception e)
        {
        }
    }
}