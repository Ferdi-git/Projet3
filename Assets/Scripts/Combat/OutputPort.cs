
public class OutputPort 
{
    public BoardPiece thisBoardPiece;
    public StatsPlayer statsPlayer;
    public StatsEnnemi statsEnnemi;
    public SoNbrOfPiecePlayed piecePlayed;
    private int i;
    private float delai;

    //player
    public void TakeDamage (int amount)
    {
        if (statsPlayer.GetShield() <= 0 )
        {
            statsPlayer.InvokeLoosePV(amount);
        }
        else
        {
            statsPlayer.InvokeLooseShield(amount);
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
        statsPlayer.InvokeLooseShield (amount);
    }

    //ennemi 

    public void DoDamage (int amount )
    {
        if (statsEnnemi.GetShield() <= 0)
        {
            statsEnnemi.InvokeEnnemiLoosePV(amount);
        }
        else
        {
            statsEnnemi.InvokeEnnemiLooseShield(amount);
        }
    }
    public void EnnemiHeal (int amount)
    {
        statsEnnemi.InvokeEnnemiGainPV(amount);
    }
    public void EnnemiGainShield (int amount )
    {
        statsEnnemi.InvokeEnnemiGainShield(amount);
    }
    public void EnnemiLooseShield(int amount)
    {
        statsEnnemi.InvokeEnnemiLooseShield(amount);
    }



    
}
