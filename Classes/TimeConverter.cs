using System;
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
            return time.Seconds % 2 == 0 ? "Y" : "O";
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

            var onLamps = time.Hours / 5;
            return CreateRowLamps(onLamps, "RRRR");
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

            var onLamps = time.Hours % 5;
            return CreateRowLamps(onLamps, "RRRR");
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

            var noOfOnLamps = time.Minutes / 5;
            return CreateRowLamps(noOfOnLamps, "YYRYYRYYRYY");
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

            var onLamps = time.Minutes % 5;
            return CreateRowLamps(onLamps, "YYYY");
        }

        /// <summary>
        /// Creates a row of lamps starting with the first noOfOnLamps from the rowWithAllLampsOn and then continuing with off 'O'.
        /// </summary>
        /// <param name="noOfOnLamps">The on lamps.</param>
        /// <param name="rowWithAllLampsOn">The row with all lamps on.</param>
        /// <returns></returns>
        private static string CreateRowLamps(int noOfOnLamps, string rowWithAllLampsOn)
        {
            return rowWithAllLampsOn.Substring(0, noOfOnLamps) + new string('O', rowWithAllLampsOn.Length - noOfOnLamps);
        }
    }
}
