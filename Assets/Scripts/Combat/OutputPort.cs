
using System.Diagnostics;

public class OutputPort 
{
    public Combat combat;
    public StatsPlayer statsPlayer;
    public StatsEnnemi statsEnnemi;
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



    public void EndEffet ()
    {
        combat.NextPiece(0f);
    }
    public void EndRepeatEffetRequest (int  amount)
    {
        //float timeToWait = 0.3f - 0.01f * i;
        //timeToWait = Mathf.Clamp(timeToWait, 0.05f, 0.7f);

        i = amount;
        delai = 0.3f * i + 0.3f; // valeur approximative 
    }
    public void FinishedRepeatedEffect ()
    {
        i -= 1;
        if (i <= 0)
        {
            combat.NextPiece(delai);
        }
    }
}
