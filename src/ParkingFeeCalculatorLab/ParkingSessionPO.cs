namespace ParkingFeeCalculatorLab;

public class ParkingSessionPO
{
    private string Plate { get; set; }
    private long Start { get; set; }
    private long? End { get; set; }

    public static ParkingSessionPO GetParkingSessionPO(ParkingSession parkingSession)
    {
        return new ParkingSessionPO
        {
            Plate = parkingSession.Plate,
            Start = parkingSession.Start.ToTimestamp(),
            End = parkingSession.End?.ToTimestamp()
        };
    }

    public static ParkingSession ToEntity(ParkingSessionPO parkingSessionPO)
    {
        return new ParkingSession(
            parkingSessionPO.Plate,
            parkingSessionPO.Start.FromTimestamp(),
            parkingSessionPO.End?.FromTimestamp()
        );
    }
}