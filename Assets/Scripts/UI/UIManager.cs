using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SOEventPlayer eventPlayer;
    [SerializeField] private StatsPlayer statsPlayer;
    [SerializeField] private StatsEnnemi statsEnnemi;
    [SerializeField] private SOEventEnnemy EventEnnemy;

    [SerializeField] private Slider PlayerSlider;
    [SerializeField] private Slider EnnemiSlider;

    [SerializeField] private SpriteRenderer ennemiSprite;
    [SerializeField] private TextMeshProUGUI ennemiName;

    
    private void OnEnable()
    {
        eventPlayer.GainPV += GainPV;
        eventPlayer.LoosePV += LoosePV;

        eventPlayer.GainShield += GainShield;
        eventPlayer.LooseShield += LooseShield;


        EventEnnemy.EnnemiGainPV += EnnemiGainPV;
        EventEnnemy.EnnemiLoosePV += EnnemiLoosePV;

        EventEnnemy.EnnemiGainShield += EnnemiGainShield;
        EventEnnemy.EnnemiLooseShield += EnnemiLostShield;

        EventEnnemy.NewEnnemi += UpdateUI;
    }
    private void OnDisable()
    {
        eventPlayer.GainPV -= GainPV;
        eventPlayer.LoosePV -= LoosePV;

        eventPlayer.GainShield -= GainShield;
        eventPlayer.LooseShield -= LooseShield;


        EventEnnemy.EnnemiGainPV -= EnnemiGainPV;
        EventEnnemy.EnnemiLoosePV -= EnnemiLoosePV;

        EventEnnemy.EnnemiGainShield -= EnnemiGainShield;
        EventEnnemy.EnnemiLooseShield -= EnnemiLostShield;
        EventEnnemy.NewEnnemi -= UpdateUI;
    }



    public void UpdateUI ()
    {
        int pvPlayer = statsPlayer.GetPV();
        print("pv player " + pvPlayer);
        int pvMaxPlayer = statsPlayer.pvMax;
        int shieldPlayer = statsPlayer.GetShield();

        int pvEnnemi = statsEnnemi.GetPV();
        print ("pv ennemi " + pvEnnemi);
        int pvMaxEnnemi = statsEnnemi.pvMax;
        int shieldEnnemi = statsEnnemi.GetShield();

        PlayerSlider.maxValue = pvMaxPlayer;
        EnnemiSlider.maxValue =  pvMaxEnnemi;
        PlayerSlider.value = pvPlayer;
        EnnemiSlider.value = pvEnnemi;

        ennemiName.text  = statsEnnemi.ennemiName;
        ennemiSprite.sprite = statsEnnemi.sprite;
    }
    private void GainPV (int amount)
    {
        print("pv gained ");
        UpdateUI();
    }
    private void LoosePV (int amount)
    {
        print("pv lost ");
        UpdateUI();
    }

    private void GainShield (int amount)
    {
        print("gain Shield ");
        UpdateUI();
    }
    private void LooseShield(int amount)
    {
        print("lost Shield ");
        UpdateUI();
    }


    private void EnnemiGainPV (int amount )
    {
        print("Ennemi gained health");
        UpdateUI();
    }
    private void EnnemiLoosePV (int amount )
    {
        print("ennemi lost health");
        UpdateUI();
    }
    private void EnnemiGainShield (int amount )
    {
        print("ennemi gained shield");
        UpdateUI();
    }
    private void EnnemiLostShield (int amount )
    {
        print("ennemi lost shield");
        UpdateUI();
    }
}
