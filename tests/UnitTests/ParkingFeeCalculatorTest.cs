using ParkingFeeCalculatorLab;
using NUnit.Framework;
using Shouldly;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
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

        private void Run_Test(string start, string end, long expected)
        {
            ParkingFeeCalculator sut = new ParkingFeeCalculator();
            long actual = sut.CalculateFee(DateTime.Parse(start),  DateTime.Parse(end));
            actual.ShouldBe(expected);
        }
    }
}