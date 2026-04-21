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
    [SerializeField] private Slider ManaSlider;
    [SerializeField] private Slider EnnemiSlider;

    [SerializeField] private SpriteRenderer ennemiSprite;
    [SerializeField] private TextMeshProUGUI ennemiName;

    [SerializeField] private TextMeshProUGUI ennemiAtktext;
    [SerializeField] private SOEventUpdateUI updateEvent;
    [SerializeField] private SOEventGiveUICurrentAtk GiveUICurrentAtk;

    
    private void OnEnable()
    {
        eventPlayer.GainPV += GainPV;
        eventPlayer.LoosePV += LoosePV;

        eventPlayer.GainMana += GainMana;
        eventPlayer.LooseMana += LooseMana;

        eventPlayer.GainShield += GainShield;
        eventPlayer.LooseShield += LooseShield;


        EventEnnemy.EnnemiGainPV += EnnemiGainPV;
        EventEnnemy.EnnemiLoosePV += EnnemiLoosePV;

        EventEnnemy.EnnemiGainShield += EnnemiGainShield;
        EventEnnemy.EnnemiLooseShield += EnnemiLostShield;

        EventEnnemy.NewEnnemi += UpdateUI;

        updateEvent.UpdateUI += UpdateUI;
        GiveUICurrentAtk.GiveUICurrentAtk += GiveEnnemiCurrentAtkIndex;
    }
    private void OnDisable()
    {
        eventPlayer.GainPV -= GainPV;
        eventPlayer.LoosePV -= LoosePV;

        eventPlayer.GainMana -= GainMana;
        eventPlayer.LooseMana -= LooseMana;

        eventPlayer.GainShield -= GainShield;
        eventPlayer.LooseShield -= LooseShield;


        EventEnnemy.EnnemiGainPV -= EnnemiGainPV;
        EventEnnemy.EnnemiLoosePV -= EnnemiLoosePV;

        EventEnnemy.EnnemiGainShield -= EnnemiGainShield;
        EventEnnemy.EnnemiLooseShield -= EnnemiLostShield;
        EventEnnemy.NewEnnemi -= UpdateUI;

        updateEvent.UpdateUI -= UpdateUI;
        GiveUICurrentAtk.GiveUICurrentAtk -= GiveEnnemiCurrentAtkIndex;
    }



    public void UpdateUI ()
    {
        int pvPlayer = statsPlayer.GetPV();
        int pvMaxPlayer = statsPlayer.pvMax;

        int manaPlayer = statsPlayer.GetMana();
        int manaMaxPlayer = statsPlayer.manaMax;


        int shieldPlayer = statsPlayer.GetShield();

        int pvEnnemi = statsEnnemi.GetPV();
        int pvMaxEnnemi = statsEnnemi.pvMax;
        int shieldEnnemi = statsEnnemi.GetShield();

        PlayerSlider.maxValue = pvMaxPlayer;
        EnnemiSlider.maxValue =  pvMaxEnnemi;
        ManaSlider.maxValue =  manaMaxPlayer;
        PlayerSlider.value = pvPlayer;
        EnnemiSlider.value = pvEnnemi;
        ManaSlider.value = manaPlayer;

        ennemiName.text  = statsEnnemi.ennemiName;
        ennemiSprite.sprite = statsEnnemi.sprite;
        
    }

    private void GainPV (int amount)
    {
        UpdateUI();
    }
    private void LoosePV (int amount)
    {
        UpdateUI();
    }

    private void GainMana(int amount)
    {
        UpdateUI();
    }
    private void LooseMana(int amount)
    {
        UpdateUI();
    }

    private void GainShield (int amount)
    {
        UpdateUI();
    }
    private void LooseShield(int amount)
    {
        UpdateUI();
    }


    private void EnnemiGainPV (int amount )
    {
        UpdateUI();
    }
    private void EnnemiLoosePV (int amount )
    {
        UpdateUI();
    }
    private void EnnemiGainShield (int amount )
    {
        UpdateUI();
    }
    private void EnnemiLostShield (int amount )
    {
        UpdateUI();
    }


    public void GiveEnnemiCurrentAtkIndex (int index)
    {
        ennemiAtktext.text = statsEnnemi.ennemiAttacks[index].damage.ToString();
    }
}
