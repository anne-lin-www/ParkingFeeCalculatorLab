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
            Given_Parking_Starts_At("2025-01-02T00:00:00");
            Given_Parking_Ends_At("2025-01-02T00:15:00");
            When_Calculate();
            Then_Should_Pay(0L);
        }

        [Test]
        public void CalculateFee_Over_15Minutes_Not_Free()
        {
            Given_Parking_Starts_At("2025-01-02T00:00:00");
            Given_Parking_Ends_At("2025-01-02T00:15:01");
            When_Calculate();
            Then_Should_Pay(30L);
        }

        [Test]
        public void CalculateFee_Over_30Minutes_Then_pay_60()
        {
            Given_Parking_Starts_At("2025-01-02T00:01:00");
            Given_Parking_Ends_At("2025-01-02T00:31:01");
            When_Calculate();
            Then_Should_Pay(60L);
        }

        [Test]
        public void CalculateFee_Over_60Minutes_Then_pay_90()
        {
            Given_Parking_Starts_At("2025-01-02T00:00:00");
            Given_Parking_Ends_At("2025-01-02T01:00:01");
            When_Calculate();
            Then_Should_Pay(90L);
        }

        [Test]
        public void CalculateFee_Over_150Minutes_Then_pay_150()
        {
            Given_Parking_Starts_At("2025-01-02T00:00:00");
            Given_Parking_Ends_At("2025-01-02T02:30:01");
            When_Calculate();
            Then_Should_Pay(150L);
        }

        [Test]
        public void CalculateFee_Two_Whole_Days()
        {
            Given_Parking_Starts_At("2025-01-02T00:00:00");
            Given_Parking_Ends_At("2025-01-04T00:00:00");
            When_Calculate();
            Then_Should_Pay(150L + 150L);
        }

        [Test]
        public void CalculateFee_Partial_Day_Then_Whole_day()
        {
            Given_Parking_Starts_At("2025-01-02T23:50:00");
            Given_Parking_Ends_At("2025-01-04T00:00:00");
            When_Calculate();
            Then_Should_Pay(30L + 150L);
        }

        [Test]
        public void CalculateFee_Whole_Day_Then_Partial_day()
        {
            Given_Parking_Starts_At("2025-01-02T00:00:00");
            Given_Parking_Ends_At("2025-01-03T00:10:00");
            When_Calculate();
            Then_Should_Pay(150L + 30L);
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