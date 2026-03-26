
public class OutputPort 
{
    public BoardPiece thisBoardPiece;
    public StatsPlayer statsPlayer;
    public StatsEnnemi statsEnnemi;
    public SoNbrOfPiecePlayed piecePlayed;

    //player
    public void TakeDamage (int amount)
    {
        if (statsPlayer.GetShield() <= 0 )
        {
            statsPlayer.InvokeLoosePV(amount);
        }
        else
        {
            statsPlayer.InvokeTakeDamage(amount);
        }

        
    }
    public void LoosePV (int amount)
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
        statsPlayer.InvokeTakeDamage (amount);
    }

    //ennemi 

    public void DoDamage (int amount )
    {
        if (statsEnnemi.GetShield() <= 0)
        {
            statsEnnemi.EnnemiLoosePV(amount);
        }
        else
        {
            statsEnnemi.EnnemiTakeDamager(amount);
        }
    }
    public void EnnemiHeal (int amount)
    {
        statsEnnemi.EnnemiGainPV(amount);
    }
    public void EnnemiGainShield (int amount )
    {
        statsEnnemi.EnnemiGainShield(amount);
    }
    public void EnnemiLooseShield(int amount)
    {
        statsEnnemi.EnnemiTakeDamager(amount);
    }



    
}
