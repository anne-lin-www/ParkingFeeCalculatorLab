namespace ParkingFeeCalculatorLab;

public class ParkingSessionRepository : IParkingSessionRepository
{
    private ParkingSession _parkingSession;
    
    public void Save(ParkingSession parkingSession)
    {
        _parkingSession = parkingSession;
    }

    public ParkingSession Find()
    {
        return _parkingSession;
    }
}