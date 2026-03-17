using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StatsPlayer statsPlayer;
    [SerializeField] private StatsEnnemi statsEnnemi;

    private void OnEnable()
    {
        statsPlayer.GainPV += GainPV;
        statsPlayer.LoosePV += LoosePV;

        statsPlayer.GainShield += GainShield;
        statsPlayer.LooseShield += LooseShield;


        statsEnnemi.EnnemiGainPV += EnnemiGainPV;
        statsEnnemi.EnnemiLoosePV += EnnemiLoosePV;

        statsEnnemi.EnnemiGainShield += EnnemiGainShield;
        statsEnnemi.EnnemiLooseShield += EnnemiLostShield;
    }
    private void OnDisable()
    {
        statsPlayer.GainPV -= GainPV;
        statsPlayer.LoosePV -= LoosePV;

        statsPlayer.GainShield -= GainShield;
        statsPlayer.LooseShield -= LooseShield;


        statsEnnemi.EnnemiGainPV -= EnnemiGainPV;
        statsEnnemi.EnnemiLoosePV -= EnnemiLoosePV;

        statsEnnemi.EnnemiGainShield -= EnnemiGainShield;
        statsEnnemi.EnnemiLooseShield -= EnnemiLostShield;
    }


    private void GainPV (int amount)
    {
        print("pv gained ");
    }
    private void LoosePV (int amount)
    {
        print("pv lost ");
    }

    private void GainShield (int amount)
    {
        print("gain Shield ");
    }
    private void LooseShield(int amount)
    {
        print("lost Shield ");
    }


    private void EnnemiGainPV (int amount )
    {
        print("Ennemi gained health");
    }
    private void EnnemiLoosePV (int amount )
    {
        print("ennemi lost health");
    }
    private void EnnemiGainShield (int amount )
    {
        print("ennemi gained shield");
    }
    private void EnnemiLostShield (int amount )
    {
        print("ennemi lost shield");
    }
}
