namespace ParkingFeeCalculatorLab;

public class ParkingSessionRepository : IParkingSessionRepository
{
    private readonly Dictionary<string, ParkingSession> _parkingSessions = new();
    
    public void Save(ParkingSession parkingSession)
    {
        _parkingSessions.TryAdd(parkingSession.Plate, parkingSession);
    }

    public ParkingSession? Find(string plate)
    {
        return _parkingSessions.GetValueOrDefault(plate);
    }
}