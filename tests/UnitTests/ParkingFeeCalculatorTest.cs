using ParkingFeeCalculatorLab;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {  
        private ParkingFeeCalculator _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ParkingFeeCalculator();
        }

        [Test]
        public void CalculateFee_15Minutes_Free()
        {
            DateTime start = new DateTime(2025, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2025, 1, 1, 0, 14, 59);
            long actual = _sut.CalculateFee(start, end);
            Assert.That(actual, Is.EqualTo(0));
        }
    }
}