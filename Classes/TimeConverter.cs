using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BerlinClock.Classes;

namespace BerlinClock
{
    /// <summary>
    /// Displays the time of day similar to the Berlin Clock
    /// </summary>
    /// <seealso cref="BerlinClock.ITimeConverter" />
    public class TimeConverter : ITimeConverter
    {
        // we can inject the values in the ctor to allow a more extendable implementation
        private const char Yellow = 'Y';
        private const char Off = 'O';
        private const char Red = 'R';

        /// <summary>
        /// Converts the time.
        /// </summary>
        /// <param name="aTime">a time.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">aTime</exception>
        public string convertTime(string aTime)
        {
            if (aTime == null) throw new ArgumentNullException("aTime");
            var timeOfDay = TimeOfDay.Parse(aTime);
            
            var result = new StringBuilder();

            result.AppendLine(SecondsRow(timeOfDay));
            result.AppendLine(HoursTopRow(timeOfDay));
            result.AppendLine(HoursBottomRow(timeOfDay));
            result.AppendLine(MinutesTopRow(timeOfDay));
            result.Append(MinutesBottomRow(timeOfDay));
            
            return result.ToString();
        }

        /// <summary>
        /// Retrieves the first row of the clock that shows seconds.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public string SecondsRow(TimeOfDay time)
        {
            if (time == null) throw new ArgumentNullException("time");
            return time.Seconds % 2 == 0 ? Yellow.ToString() : Off.ToString();
        }

        /// <summary>
        /// Returns 4 lamps.
        /// A lamp will light red for every 5 hours .
        /// </summary>
        /// <param name="time">The time of day.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string HoursTopRow(TimeOfDay time)
        {
            if (time == null) throw new ArgumentNullException("time");

            const int totalLamps = 4;
            var onLamps = time.Hours / 5;

            return CreateLamps(totalLamps, onLamps, Red);
        }

        /// <summary>
        /// Returns 4 lamps.
        /// A lamp will light red for every hour in the reminder of the division of hours by 5.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">time</exception>
        public string HoursBottomRow(TimeOfDay time)
        {
            if (time == null) throw new ArgumentNullException("time");

            const int totalLamps = 4;
            var onLamps = time.Hours % 5;

            return CreateLamps(totalLamps, onLamps, Red);
        }

        /// <summary>
        /// Retrieves 11 lamps.
        /// A yellow lamp will light for every 5 minutes and a red one for each quarter.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">time</exception>
        public string MinutesTopRow(TimeOfDay time)
        {
            if (time == null) throw new ArgumentNullException("time");

            const int totalLamps = 11;
            var noOfOnLamps = time.Minutes / 5;
            var onLamps = new char[noOfOnLamps];
            
            // every third lamp will light up red so we need to start from 1
            for (var i = 1; i <= noOfOnLamps; i++)
            {
                onLamps[i - 1] = i % 3 == 0 ? Red : Yellow;
            }

            return new string(onLamps) + new string(Off, totalLamps - noOfOnLamps);
        }

        /// <summary>
        /// Retrieves 4 lamps.
        /// A lamp will light yellow for every minute in the reminder of the division of minutes by 5.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">time</exception>
        public string MinutesBottomRow(TimeOfDay time)
        {
            if (time == null) throw new ArgumentNullException("time");

            const int totalLamps = 4;
            var onLamps = time.Minutes % 5;

            return CreateLamps(totalLamps, onLamps, Yellow);
        }

        /// <summary>
        /// Creates a row with the given total number of lamps and lights the first noOfOnLamps with the given onValue.
        /// </summary>
        /// <param name="total">The total.</param>
        /// <param name="noOfOnLamps">The on lamps.</param>
        /// <param name="onValue">The on value.</param>
        /// <returns></returns>
        private string CreateLamps(int total, int noOfOnLamps, char onValue)
        {
            return new string(onValue, noOfOnLamps) + new string(Off, total - noOfOnLamps);
        }
    }
}
