
public class EconomyManager
{
    public EconomyModel model { get; private set; }

    public EconomyManager()
    {
        model = new EconomyModel();
    }

    public void AddCoins(int value)
    {
        model.coins += value;
    }
}

public class EconomyModel
{
    public int coins;
}