
public class OutputPort 
{
    public StatsPlayer statsPlayer;

    public void TakeDamage (int amount)
    {
        statsPlayer.InvokeLoosePV(amount);
    }
    public void Heal (int amount)
    {
        statsPlayer.InvokeGainPV(amount);
    }
    public void GainShield (int amount)
    {
        statsPlayer.InvokeGainShield (amount);
    }
    public void LooseShield (int amount)
    {
        statsPlayer.InvokeLooseShield (amount);
    }
}
