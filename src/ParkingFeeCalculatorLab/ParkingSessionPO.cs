namespace ParkingFeeCalculatorLab;

public class ParkingSessionPO
{
    public string Plate { get; private set; }
    public long Start { get; private set; }
    public long? End { get; private set; }

    public static ParkingSessionPO GetParkingSessionPO(ParkingSession parkingSession)
    {
        return new ParkingSessionPO
        {
            Plate = parkingSession.Plate,
            Start = parkingSession.Start.ToTimestamp(),
            End = parkingSession.End?.ToTimestamp()
        };
    }
}