using ParkingFeeCalculatorLab.Interfaces;

namespace ParkingFeeCalculatorLab.Repositories;

public class ParkingSessionRepository : IParkingSessionRepository
{
    private readonly Dictionary<string, ParkingSessionPO> _parkingSessions = new(); // PersistenceObject
    
    public void Save(ParkingSession parkingSession)
    {
        var parkingSessionPO = ParkingSessionPO.GetParkingSessionPO(parkingSession);
        _parkingSessions[parkingSession.Plate] = parkingSessionPO; // 直接覆蓋，確保 End 能更新
    }

    public ParkingSession? Find(string plate)
    {
        ParkingSessionPO? parkingSessionPO = _parkingSessions.GetValueOrDefault(plate);

        if (parkingSessionPO is null)
        {
            return null;
        }
        var parkingSession = ParkingSession.Restore(parkingSessionPO);
        return parkingSession;
    }
}