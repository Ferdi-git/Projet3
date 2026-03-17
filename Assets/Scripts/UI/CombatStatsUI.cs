using UnityEngine;

public class CombatStatsUI : MonoBehaviour
{
    [SerializeField] private StatsPlayer statsPlayer;

    private void OnEnable()
    {
        statsPlayer.GainPV += GainPV;
        statsPlayer.LoosePV += LoosePV;

        statsPlayer.GainShield += GainShield;
        statsPlayer.LooseShield += LooseShield;
    }
    private void OnDisable()
    {
        statsPlayer.GainPV -= GainPV;
        statsPlayer.LoosePV -= LoosePV;

        statsPlayer.GainShield -= GainShield;
        statsPlayer.LooseShield -= LooseShield;
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
}
