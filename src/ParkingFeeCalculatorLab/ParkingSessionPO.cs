namespace ParkingFeeCalculatorLab;

public class ParkingSessionPO
{
    public string Plate { get; private set; }
    public long Start { get; private set; }
    public long? End { get; private set; }

    public void SetPlate(string plate)
    {
        Plate = plate;
    }

    public void SetStart(long start)
    {
        Start = start;
    }

    public void SetEnd(long? end)
    {
        End = end;
    }
}