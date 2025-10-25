namespace ParkingFeeCalculatorLab.Interfaces;

public interface IParkingSessionRepository
{
    void Save(ParkingSession parkingSession);
    ParkingSession? Find(string plate);
}