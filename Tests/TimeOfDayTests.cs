using System;
using BerlinClock.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ObjectCreationAsStatement

namespace BerlinClock.Tests
{
    /// <summary>
    ///     Unit tests covering the TimeOfDay type
    /// </summary>
    [TestClass]
    public class TimeOfDayTests
    {
        #region Create

        /// <summary>
        ///     Avoid naming Negative... (better english) to see the tests results nicely ordered
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_HoursNegative_ThrowsException()
        {
            new TimeOfDay(-1, 0, 0);
        }

        /// <summary>
        ///     Avoid naming NegativeMinutes (better english) to see the tests results nicely ordered
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_HoursGreaterThan24_ThrowsException()
        {
            new TimeOfDay(25, 0, 0);
        }

        /// <summary>
        ///     Avoid naming Negative... (better english) to see the tests results nicely ordered
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_MinutesNegative_ThrowsException()
        {
            new TimeOfDay(0, -1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_MinutesGreaterThan59_ThrowsException()
        {
            new TimeOfDay(0, 60, 0);
        }

        /// <summary>
        ///     Avoid naming Negative... (better english) to see the tests results nicely ordered
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_SecondsNegative_ThrowsException()
        {
            new TimeOfDay(0, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_SecondsGreaterThan59_ThrowsException()
        {
            new TimeOfDay(0, 0, 60);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Create_OverflowDayWhenHoursIs24AndMinutesGreaterThan0_ThrowsException()
        {
            new TimeOfDay(24, 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Create_OverflowDayWhenHoursIs24AndSecondsGreaterThan0_ThrowsException()
        {
            new TimeOfDay(24, 0, 1);
        }

        [TestMethod]
        public void Create_HoursIs1_Expected1()
        {
            var expected = 1;

            var actual = new TimeOfDay(1, 0, 0);

            Assert.AreEqual(expected, actual.Hours);
        }

        [TestMethod]
        public void Create_MinutesIs1_Expected1()
        {
            var expected = 1;

            var actual = new TimeOfDay(0, 1, 0);

            Assert.AreEqual(expected, actual.Minutes);
        }

        [TestMethod]
        public void Create_SecondsIs1_Expected1()
        {
            var expected = 1;

            var actual = new TimeOfDay(0, 0, 1);

            Assert.AreEqual(expected, actual.Seconds);
        }

        #endregion

        #region Parse

        [TestMethod]
        public void Parse_StringIs01_00_00_Expecting1Hours()
        {
            Assert.AreEqual(1, TimeOfDay.Parse("01:00:00").Hours);
        }

        [TestMethod]
        public void Parse_StringIs00_01_00_Expecting1Minutes()
        {
            Assert.AreEqual(1, TimeOfDay.Parse("00:01:00").Minutes);
        }

        [TestMethod]
        public void Parse_StringIs00_00_01_Expecting1Seconds()
        {
            Assert.AreEqual(1, TimeOfDay.Parse("00:00:01").Seconds);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_EmptyString_ThrowsException()
        {
            TimeOfDay.Parse("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_Null_ThrowsException()
        {
            TimeOfDay.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidFormatWith2PartsInsteadOf3_ThrowsException()
        {
            TimeOfDay.Parse("10:10");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidFormatWith1PartInsteadOf3_ThrowsException()
        {
            TimeOfDay.Parse("10");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidFormatWith4PartsInsteadOf3_ThrowsException()
        {
            TimeOfDay.Parse("10:10:10:10");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Parse_HoursGreaterThan24_ThrowsException()
        {
            TimeOfDay.Parse("25:00:00");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Parse_MinutesGreaterThan59_ThrowsException()
        {
            TimeOfDay.Parse("00:60:00");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Parse_OverflowDayWhenHoursIs24AndMinutesGreaterThan0_ThrowsException()
        {
            TimeOfDay.Parse("24:01:00");
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Parse_OverflowDayWhenHoursIs24AndSecondsGreaterThan0_ThrowsException()
        {
            TimeOfDay.Parse("24:00:01");
        }

        #endregion

        #region Equals

        [TestMethod]
        public void Equals_2InstancesWithEqualValuesAreEqual()
        {
            var left = new TimeOfDay(1, 2, 3);
            var right = new TimeOfDay(1, 2, 3);

            Assert.AreEqual(left, right);
        }

        [TestMethod]
        public void Equals_2InstancesWithDifferentMinutesAreNotEqual()
        {
            var left = new TimeOfDay(1, 2, 3);
            var right = new TimeOfDay(1, 3, 3);

            Assert.AreNotEqual(left, right);
        }

        [TestMethod]
        public void EqualsOperator_2InstancesWithEqualValuesAreEqual()
        {
            var left = new TimeOfDay(1, 1, 1);
            var right = new TimeOfDay(1, 1, 1);

            Assert.IsTrue(left == right);
        }

        [TestMethod]
        public void NotEqualsOperator_2InstancesWithEqualValuesAreEqual()
        {
            var left = new TimeOfDay(1, 1, 1);
            var right = new TimeOfDay(1, 1, 1);

            Assert.IsFalse(left != right);
        }

        [TestMethod]
        public void GetHashCode_2InstancesWithEqualValues_ReturnEqualHashCodes()
        {
            var left = new TimeOfDay(1, 2, 3);
            var right = new TimeOfDay(1, 2, 3);

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        #endregion

        #region ToString

        [TestMethod]
        public void ToString_HoursIs1AndMinutesIs23AndSecondsIs45_Returns01_23_45()
        {
            var expected = "01:23:45";
            var actual = new TimeOfDay(1, 23, 45).ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToString_HoursIs1AndMinutesIs2AndSecondsIs3_Returns01_02_03()
        {
            var expected = "01:02:03";
            var actual = new TimeOfDay(1, 2, 3).ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}