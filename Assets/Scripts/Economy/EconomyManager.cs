
public class EconomyManager
{
    private EconomyModel model;

    public int coins => model.coins;
    public EconomyManager()
    {
        model = new EconomyModel();
    }
    public void AddCoins(int value)
    {
        model.coins += value;
        EventsModel.COINS_ECONOMY_CHANGED?.Invoke();
    }
}

public class EconomyModel
{
    public int coins;
}