namespace ParkingFeeCalculatorLab;

public class PriceBookRepository : IPriceBookRepository
{
    private PriceBook _priceBook;

    public PriceBookRepository(PriceBook priceBook)
    {
        _priceBook = priceBook;
    }

    public PriceBook GetPriceBook()
    {
        return _priceBook;
    }
    
    public void SetPriceBook(PriceBook priceBook)
    {
        _priceBook = priceBook;
    }
}