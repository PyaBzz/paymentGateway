using Core;
public interface IFactory
{
    Request Make(Dto dto);
}

public class Factory
{
    private readonly IRepository<Request> repo;
    public Factory(IRepository<Request> r)
    {
        repo = r;
    }
    public Request Make(Dto dto)
    {
        var date = Date.Create(dto.Card_Expiry_Year, dto.Card_Expiry_Month);
        var card = Card.Create(dto.Card_Number, date, dto.Card_Cvv, dto.Card_Currency);
        return Request.Create(dto.MerchantId, card, dto.Amount, repo);
    }
}