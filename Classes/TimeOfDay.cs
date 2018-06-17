using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BerlinClock.Classes
{
    /// <summary>
    /// Represents the time of day with second precision in a 24h format (00:00 - 24:00) ignoring time zone or locale.
    /// The timespan does not support hour 24:00 (end of day) in an exact format hh:mm:ss. The max value is 23.
    /// </summary>
    public class TimeOfDay
    {
        // private static readonly Regex ParseRegex = new Regex("^([0,1][0-9]|[2][0-4]):([0-5][0-9]):([0-5][0-9])$", RegexOptions.Compiled);

        /// <summary>
        /// The parse regex. Use a more lax regex to allow more specific exceptions. 
        /// </summary>
        private static readonly Regex ParseRegex = new Regex("^([0-9]{2}):([0-9]{2}):([0-9]{2})$", RegexOptions.Compiled);

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeOfDay" /> class.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// hours - The value must be between 0 and 24
        /// or
        /// minutes - The value must be between 0 and 59
        /// or
        /// seconds - The value must be between 0 and 59
        /// </exception>
        /// <exception cref="System.OverflowException">The max value is 24 hours. When the hours value is 24 the minutes and seconds arguments must be 0.</exception>
        public TimeOfDay(int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours > 24)
            {
                throw new ArgumentOutOfRangeException("hours", hours, "The value must be between 0 and 24");
            }

            if (minutes < 0 || minutes > 59)
            {
                throw new ArgumentOutOfRangeException("minutes", minutes, "The value must be between 0 and 59");
            }

            if (seconds < 0 || seconds > 59)
            {
                throw new ArgumentOutOfRangeException("seconds", seconds, "The value must be between 0 and 59");
            }

            if (hours == 24 && (minutes > 0 || seconds > 0))
            {
                throw new OverflowException("The max value is 24 hours. When the hours value is 24 the minutes and seconds arguments must be 0.");
            }

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        /// <summary>
        /// Gets the hours.
        /// </summary>
        /// <value>
        /// The hours.
        /// </value>
        public int Hours { get; private set; }

        /// <summary>
        /// Gets the minutes.
        /// </summary>
        /// <value>
        /// The minutes.
        /// </value>
        public int Minutes { get; private set; }

        /// <summary>
        /// Gets the seconds.
        /// </summary>
        /// <value>
        /// The seconds.
        /// </value>
        public int Seconds { get; private set; }

        public override string ToString()
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minutes, Seconds);
        }

        public static TimeOfDay Parse(string s)
        {
            if (s == null) throw new ArgumentNullException("s");

            const string format = "hh:mm:ss";

            var match = ParseRegex.Match(s);
            if (!match.Success)
                throw new FormatException(string.Format(
                    "String '{0}' cannot be parsed as a valid {1}. The allowed format is {2}.",
                    s, typeof(TimeOfDay).Name, format));

            var hours = int.Parse(match.Groups[1].Value);
            var minutes = int.Parse(match.Groups[2].Value);
            var seconds = int.Parse(match.Groups[3].Value);

            try
            {
                return new TimeOfDay(hours, minutes, seconds);
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new OverflowException(
                    string.Format(
                        "Cannot parse '{0}' as a valid {1}. The value of one of the components is out of range.", s,
                        typeof(TimeOfDay)), e);
            }
            catch (OverflowException e)
            {
                throw new OverflowException(
                    string.Format(
                        "Cannot parse '{0}' as a valid {1}. The value is out of range.", s,
                        typeof(TimeOfDay)), e);
            }
        }

        #region Equals

        protected bool Equals(TimeOfDay other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TimeOfDay)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Hours;
                hashCode = (hashCode * 397) ^ Minutes;
                hashCode = (hashCode * 397) ^ Seconds;
                return hashCode;
            }
        }

        public static bool operator ==(TimeOfDay left, TimeOfDay right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TimeOfDay left, TimeOfDay right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
