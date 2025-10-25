using ParkingFeeCalculatorLab.Interfaces;

namespace ParkingFeeCalculatorLab.Repositories;

public class ParkingSessionRepository : IParkingSessionRepository
{
    private readonly Dictionary<string, ParkingSessionPO> _parkingSessions = new(); // PersistenceObject
    
    public void Save(ParkingSession parkingSession)
    {
        ParkingSessionPO parkingSessionPO = new ParkingSessionPO();
        parkingSessionPO.SetPlate(parkingSession.Plate);
        parkingSessionPO.SetStart(parkingSession.Start.ToTimestamp());
        parkingSessionPO.SetEnd(parkingSession.End?.ToTimestamp());
        _parkingSessions[parkingSession.Plate] = parkingSessionPO; // 直接覆蓋，確保 End 能更新
    }

    public ParkingSession? Find(string plate)
    {
        ParkingSessionPO? parkingSessionPO = _parkingSessions.GetValueOrDefault(plate);
        
        return parkingSessionPO is null ? null: new ParkingSession(
            parkingSessionPO.Plate,
            parkingSessionPO.Start.FromTimestamp(),
            parkingSessionPO.End?.FromTimestamp()
        );
    }
}