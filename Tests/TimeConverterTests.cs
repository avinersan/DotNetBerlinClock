using BerlinClock.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BerlinClock.Tests
{
    /// <summary>
    /// Unit tests covering TimeConverter. Should have used DataTestMethod in many cases if it would have been available.
    /// </summary>
    [TestClass]
    public class TimeConverterTests
    {
        #region Fields

        private readonly TimeConverter _timeConverter = new TimeConverter();

        #endregion

        #region Seconds row

        [TestMethod]
        public void SecondsRow_SecondsIsEven_RetrunsYellow()
        {
            Assert.AreEqual("Y", _timeConverter.SecondsRow(new TimeOfDay(0, 0, 2)));
        }

        [TestMethod]
        public void SecondsRow_SecondsIsZero_RetrunsYellow()
        {
            Assert.AreEqual("Y", _timeConverter.SecondsRow(new TimeOfDay(0, 0, 0)));
        }

        [TestMethod]
        public void SecondsRow_SecondsIsOdd_RetrunsOff()
        {
            Assert.AreEqual("O", _timeConverter.SecondsRow(new TimeOfDay(0, 0, 1)));
        }

        #endregion
        
        #region Hours top row

        [TestMethod]
        public void HoursTopRow_HoursIs0_ReturnsOOOO()
        {
            var expected = "OOOO";
            var actual = _timeConverter.HoursTopRow(new TimeOfDay(0, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HoursTopRow_HoursIs4_ReturnsOOOO()
        {
            var expected = "OOOO";
            var actual = _timeConverter.HoursTopRow(new TimeOfDay(4, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HoursTopRow_HoursIs5_ReturnsROOO()
        {
            var expected = "ROOO";
            var actual = _timeConverter.HoursTopRow(new TimeOfDay(5, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HoursTopRow_HoursIs16_ReturnsRRRO()
        {
            var expected = "RRRO";
            var actual = _timeConverter.HoursTopRow(new TimeOfDay(16, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HoursTopRow_HoursIs24_ReturnsRRRR()
        {
            var expected = "RRRR";
            var actual = _timeConverter.HoursTopRow(new TimeOfDay(24, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Hours bottom row

        [TestMethod]
        public void ThirdRowHours_HoursIs0_ReturnsOOOO()
        {
            var expected = "OOOO";
            var actual = _timeConverter.HoursBottomRow(new TimeOfDay(0, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThirdRowHours_HoursIs5_ReturnsOOOO()
        {
            var expected = "OOOO";
            var actual = _timeConverter.HoursBottomRow(new TimeOfDay(5, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThirdRowHours_HoursIs3_ReturnsRRRO()
        {
            var expected = "RRRO";
            var actual = _timeConverter.HoursBottomRow(new TimeOfDay(3, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThirdRowHours_HoursIs24_ReturnsRRRR()
        {
            var expected = "RRRR";
            var actual = _timeConverter.HoursBottomRow(new TimeOfDay(24, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Minutes top row

        [TestMethod]
        public void MinutesTopRow_MinutesIs4_ReturnsOOOOOOOOOOO()
        {
            var expected = "OOOOOOOOOOO";
            var actual = _timeConverter.MinutesTopRow(new TimeOfDay(0, 4, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinutesTopRow_MinutesIs8_ReturnsYOOOOOOOOOO()
        {
            var expected = "YOOOOOOOOOO";
            var actual = _timeConverter.MinutesTopRow(new TimeOfDay(0, 8, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinutesTopRow_MinutesIs15_ReturnsYYROOOOOOOO()
        {
            var expected = "YYROOOOOOOO";
            var actual = _timeConverter.MinutesTopRow(new TimeOfDay(0, 15, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinutesTopRow_MinutesIs45_ReturnsYYRYYRYYROO()
        {
            var expected = "YYRYYRYYROO";
            var actual = _timeConverter.MinutesTopRow(new TimeOfDay(0, 45, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinutesTopRow_MinutesIs55_ReturnsYYRYYRYYRYY()
        {
            var expected = "YYRYYRYYRYY";
            var actual = _timeConverter.MinutesTopRow(new TimeOfDay(0, 55, 0));
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Minutes bottom row

        [TestMethod]
        public void MinutesBottomRow_MinutesIs4_ReturnsYYYY()
        {
            var expected = "YYYY";
            var actual = _timeConverter.MinutesBottomRow(new TimeOfDay(0, 4, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinutesBottomRow_MinutesIs8_ReturnsYYYO()
        {
            var expected = "YYYO";
            var actual = _timeConverter.MinutesBottomRow(new TimeOfDay(0, 8, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinutesBottomRow_MinutesIs15_ReturnsOOOO()
        {
            var expected = "OOOO";
            var actual = _timeConverter.MinutesBottomRow(new TimeOfDay(0, 15, 0));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinutesBottomRow_MinutesIs59_ReturnsYYYY()
        {
            var expected = "YYYY";
            var actual = _timeConverter.MinutesBottomRow(new TimeOfDay(0, 59, 0));
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
