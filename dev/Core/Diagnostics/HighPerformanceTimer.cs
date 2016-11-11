﻿/***************************************************************************
 *   HighPerformanceTimer.cs
 *   
 *   No license information - can't remember where I got this, found it in
 *   one of my experiments folders :( If anyone can help me track down the
 *   original source (I can only find other hobby games using the same
 *   code), please let me know!
 *
 ***************************************************************************/
using System.Runtime.InteropServices;

namespace UltimaXNA.Core.Diagnostics
{
    /// <summary>
    /// A high resolution query performance timer.
    /// </summary>
    public class HighPerformanceTimer
    {
        #region Imported Methods
        /// <summary>
        /// The current system ticks (count).
        /// </summary>
        /// <param name="lpPerformanceCount">Current performance count of the system.</param>
        /// <returns>False on failure.</returns>
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        /// <summary>
        /// Ticks per second (frequency) that the high performance counter performs.
        /// </summary>
        /// <param name="lpFrequency">Frequency the higher performance counter performs.</param>
        /// <returns>False if the high performance counter is not supported.</returns>
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);
        #endregion

        #region Member Variables
        private long m_StartTime = 0;
        #endregion

        public HighPerformanceTimer()
        {
        }

        #region Methods
        public void Start()
        {
            // Record when the timer was started.
            m_StartTime = HighPerformanceTimer.Counter;
        }

        public static double SecondsFromTicks(long ticks)
        {
            return ((double)ticks) / HighPerformanceTimer.Frequency;
        }
        #endregion

        #region Static Properties
        private static long frequency;

        static HighPerformanceTimer()
        {
            QueryPerformanceFrequency(out HighPerformanceTimer.frequency);
        }

        /// <summary>
        /// Gets the frequency that this HighPerformanceTimer performs at.
        /// </summary>
        public static long Frequency
        {
            get
            {
                return HighPerformanceTimer.frequency;
            }
        }

        /// <summary>
        /// Gets the current system ticks.
        /// </summary>
        public static long Counter
        {
            get
            {
                long ticks = 0;
                QueryPerformanceCounter(out ticks);
                return ticks;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tick count of when this HighPerformanceTimer was started.
        /// </summary>
        public long StartTime
        {
            get
            {
                return m_StartTime;
            }
        }

        public long ElapsedTicks
        {
            get
            {
                return HighPerformanceTimer.Counter - m_StartTime;
            }
        }

        public double ElapsedSeconds
        {
            get
            {
                return ((double)ElapsedTicks) / HighPerformanceTimer.Frequency;
            }
        }
        #endregion
    }
}
