using ParkingFeeCalculatorLab;
using NUnit.Framework;
using Shouldly;

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
            actual.ShouldBe(0);
        }

        [Test]
        public void CalculateFee_Over_15Minutes_Not_Free()
        {
            DateTime start = new DateTime(2025, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2025, 1, 1, 0, 15, 0);
            long actual = _sut.CalculateFee(start, end);
            actual.ShouldBe(30);
        }

        [Test]
        public void CalculateFee_Over_30Minutes_Then_pay_60()
        {
            DateTime start = new DateTime(2025, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2025, 1, 1, 0, 30, 0);
            long actual = _sut.CalculateFee(start, end);
            actual.ShouldBe(60);
        }
        
        [Test]
        public void CalculateFee_Over_60Minutes_Then_pay_90()
        {
            DateTime start = new DateTime(2025, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2025, 1, 1, 1, 0, 0);
            long actual = _sut.CalculateFee(start, end);
            actual.ShouldBe(90);
        }
        
        [Test]
        public void CalculateFee_Over_150Minutes_Then_pay_150()
        {
            DateTime start = new DateTime(2025, 1, 1, 0, 0, 0);
            DateTime end = new DateTime(2025, 1, 1, 2, 30, 0);
            long actual = _sut.CalculateFee(start, end);
            actual.ShouldBe(150);
        }
    }
}