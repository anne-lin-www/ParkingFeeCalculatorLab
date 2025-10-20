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
            Run_Test(new DateTime(2025, 1, 1, 0, 0, 0), new DateTime(2025, 1, 1, 0, 14, 59), 0);
        }

        [Test]
        public void CalculateFee_Over_15Minutes_Not_Free()
        {
            Run_Test(new DateTime(2025, 1, 1, 0, 0, 0), new DateTime(2025, 1, 1, 0, 15, 0), 30);
        }

        [Test]
        public void CalculateFee_Over_30Minutes_Then_pay_60()
        {
            Run_Test(new DateTime(2025, 1, 1, 0, 0, 0), new DateTime(2025, 1, 1, 0, 30, 0), 60);
        }
        
        [Test]
        public void CalculateFee_Over_60Minutes_Then_pay_90()
        {
            Run_Test(new DateTime(2025, 1, 1, 0, 0, 0), new DateTime(2025, 1, 1, 1, 0, 0), 90);
        }

        [Test]
        public void CalculateFee_Over_150Minutes_Then_pay_150()
        {
            Run_Test(new DateTime(2025, 1, 1, 0, 0, 0), new DateTime(2025, 1, 1, 2, 30, 0), 150);
        }

        private void Run_Test(DateTime start, DateTime end, long expected)
        {
            long actual = _sut.CalculateFee(start, end);
            actual.ShouldBe(expected);
        }
    }
}