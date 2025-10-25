namespace ParkingFeeCalculatorLab;

public class ParkingSessionPO
{
    public string Plate { get; private set; }
    public long Start { get; private set; }
    public long? End { get; private set; }

    private void SetPlate(string plate)
    {
        Plate = plate;
    }

    private void SetStart(long start)
    {
        Start = start;
    }

    private void SetEnd(long? end)
    {
        End = end;
    }

    public static ParkingSessionPO GetParkingSessionPO(ParkingSession parkingSession)
    {
        ParkingSessionPO parkingSessionPO = new ParkingSessionPO();
        parkingSessionPO.SetPlate(parkingSession.Plate);
        parkingSessionPO.SetStart(parkingSession.Start.ToTimestamp());
        parkingSessionPO.SetEnd(parkingSession.End?.ToTimestamp());
        return parkingSessionPO;
    }
}