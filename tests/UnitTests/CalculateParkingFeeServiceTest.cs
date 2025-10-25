using ParkingFeeCalculatorLab;
using NUnit.Framework;
using Shouldly;

namespace UnitTests
{
    public class CalculateParkingFeeServiceTest
    {
        // DDD 物件的生命週期
        // 1. Factory
        // 2. Repository
        //  透過 Repository 來取得或儲存物件
        // 3. Entity
        //  一個實體或物件(具有狀態)會存在於系統裡面，通常會放在持久化的裝置中 e.g. 資料庫
        
        private long actual;
        private CalculateParkingFeeService sut;
        private IPriceBookRepository _priceBookRepository;
        private IParkingSessionRepository _parkingSessionRepository;

        [SetUp]
        public void Setup()
        {
            _priceBookRepository = new PriceBookRepository(new PriceBook());
            _parkingSessionRepository = new ParkingSessionRepository();
            sut = new CalculateParkingFeeService(_priceBookRepository, _parkingSessionRepository);
        }

        [Test]
        public void CalculateFee_15Minutes_Free()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-02T00:15:00");
            When_Calculate("ABC-1234");
            Then_Should_Pay(0L);
        }

        [Test]
        public void CalculateFee_Over_15Minutes_Not_Free()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-02T00:15:01");
            When_Calculate("ABC-1234");
            Then_Should_Pay(30L);
        }

        [Test]
        public void CalculateFee_Over_30Minutes_Then_Pay_60()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T00:01:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-02T00:31:01");
            When_Calculate("ABC-1234");
            Then_Should_Pay(60L);
        }
        
        [Test]
        public void Another_Car()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T00:01:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-02T00:31:01");
            When_Calculate("NOT_MY_CAR");
            Then_Should_Pay(0L);
        }
        
        [Test]
        public void CalculateFee_Over_60Minutes_Then_Pay_90()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-02T01:00:01");
            When_Calculate("ABC-1234");
            Then_Should_Pay(90L);
        }

        [Test]
        public void CalculateFee_Over_150Minutes_Then_Pay_150()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-02T02:30:01");
            When_Calculate("ABC-1234");
            Then_Should_Pay(150L);
        }

        [Test]
        public void CalculateFee_Two_Whole_Days()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-04T00:00:00");
            When_Calculate("ABC-1234");
            Then_Should_Pay(150L + 150L);
        }

        [Test]
        public void CalculateFee_Partial_Day_Then_Whole_Day()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T23:50:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-04T00:00:00");
            When_Calculate("ABC-1234");
            Then_Should_Pay(30L + 150L);
        }

        [Test]
        public void CalculateFee_Whole_Day_Then_Partial_Day()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-02T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-03T00:10:00");
            When_Calculate("ABC-1234");
            Then_Should_Pay(150L + 30L);
        }

        [Test]
        public void CalculateFee_Saturday_Pay_50_Per_Half_Hour()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-04T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-04T00:15:01");
            When_Calculate("ABC-1234");
            Then_Should_Pay(50L);
        }

        [Test]
        public void CalculateFee_Saturday_Daily_Limit_Is_2400()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-04T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-05T00:00:00");
            When_Calculate("ABC-1234");
            Then_Should_Pay(2400L);
        }

        [Test]
        public void CalculateFee_Sunday_Pay_50_Per_Half_Hour()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-05T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-05T00:15:01");
            When_Calculate("ABC-1234");
            Then_Should_Pay(50L);
        }

        [Test]
        public void CalculateFee_National_Holiday_Pay_50_Per_Half_Hour()
        {
            Given_Car_Drives_In_At("ABC-1234", "2025-01-01T00:00:00");
            Given_Car_Drives_Out_At("ABC-1234", "2025-01-01T00:15:01");
            When_Calculate("ABC-1234");
            Then_Should_Pay(50L);
        }

        private void Given_Car_Drives_In_At(string plate, string startText)
        {
            ParkingSession parkingSession = ParkingSession.StartParking(plate, DateTime.Parse(startText));
            _parkingSessionRepository.Save(parkingSession);
        }

        private void Given_Car_Drives_Out_At(string plate, string endText)
        {
            ParkingSession? parkingSession = _parkingSessionRepository.Find(plate);
            parkingSession.SetEnd(DateTime.Parse(endText));
        }

        private void When_Calculate(string plate)
        {
            actual = sut.CalculateFee(plate);
        }

        private void Then_Should_Pay(long expected)
        {
            actual.ShouldBe(expected);
        }
    }
}