namespace ParkingFeeCalculatorLab;

public interface IParkingSessionRepository
{
    void Save(ParkingSession parkingSession);
    ParkingSession Find(string plate);
}