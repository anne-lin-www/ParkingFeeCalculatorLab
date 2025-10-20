using ParkingFeeCalculatorLab;
using NUnit.Framework;
using Shouldly;

namespace UnitTests
{
    public class Tests
    {
        private DateTime start;
        private DateTime end;
        private long actual;
        private ParkingFeeCalculator sut;
        
        [SetUp]
        public void Setup()
        {
            sut = new ParkingFeeCalculator();
        }

        [Test]
        public void CalculateFee_15Minutes_Free()
        {
            Run_Test("2025-01-01 00:00:00", "2025-01-01 00:14:59", 0);
        }

        [Test]
        public void CalculateFee_Over_15Minutes_Not_Free()
        {
            Run_Test("2025-01-01 00:00:00", "2025-01-01 00:15:00", 30);
        }

        [Test]
        public void CalculateFee_Over_30Minutes_Then_pay_60()
        {
            Run_Test("2025-01-01 00:00:00", "2025-01-01 00:30:00", 60);
        }
        
        [Test]
        public void CalculateFee_Over_60Minutes_Then_pay_90()
        {
            Run_Test("2025-01-01 00:00:00", "2025-01-01 01:00:00", 90);
        }

        [Test]
        public void CalculateFee_Over_150Minutes_Then_pay_150()
        {
            Run_Test("2025-01-01 00:00:00", "2025-01-01 02:30:00", 150);
        }

        private void Run_Test(string startText, string endText, long expected)
        {
            Given_Parking_Starts_At(startText);
            Given_Parking_Ends_At(endText);
            When_Calculate();
            Then_Should_Pay(expected);
        }

        private void Given_Parking_Starts_At(string startText)
        {
            start = DateTime.Parse(startText);
        }

        private void Given_Parking_Ends_At(string endText)
        {
            end = DateTime.Parse(endText);
        }

        private void When_Calculate()
        {
            actual = sut.CalculateFee(start, end);
        }
        
        private void Then_Should_Pay(long expected)
        {
            actual.ShouldBe(expected);
        }
    }
}