
using UnityEngine;

public class EnnemiManager : MonoBehaviour
{
    [SerializeField] private SOEventEnnemy ennemiEvent;
    [SerializeField] private StatsEnnemi stats;
    [SerializeField] private GameObject ennemiUI;
    [SerializeField] private KeepEnnemiSo ennemiList;
    [SerializeField] private SOEventGridManager soEventGridManager;
    [SerializeField] private SOEventGiveUICurrentAtk UIeventGiveAtk;
    [SerializeField] private SOEventUpdateUI UIeventUpdateUI;
    private int index;
    private int atkIndex;

    private void OnEnable()
    {
        ennemiEvent.GenerateEnnemi += GenerateEnnemi;
        ennemiEvent.EnnemiShowAttack += ShowAtk;
        ennemiEvent.EnnemiRemoveAttack += RemoveAtk;
    }
    private void OnDisable()
    {
        ennemiEvent.GenerateEnnemi -= GenerateEnnemi;
        ennemiEvent.EnnemiShowAttack -= ShowAtk;
        ennemiEvent.EnnemiRemoveAttack -= RemoveAtk;
    }


    private void Start()
    {
        ennemiUI.SetActive(false);
    }
    public void GenerateEnnemi (int NbrOfCombat)
    {
        ennemiUI.SetActive (true);
        atkIndex = 0;
        index = Random.Range(0,ennemiList.ennemiList.Count);
        stats.pvMax = ((ennemiList.ennemiList[index].resistance/100) * stats.AverageValue) * (NbrOfCombat+1) ;
        stats.pv = stats.pvMax;
        stats.shield = 0;
        stats.ennemiName = ennemiList.ennemiList [index].Name;
        stats.sprite = ennemiList.ennemiList[index].sprite;
        stats.ennemiAttacks = ennemiList.ennemiList[index].attacks;
        UIeventGiveAtk.InvokeGiveUICurrentAtk(0);
        UIeventUpdateUI.InvokeUpdateUI();
        
    }

    private void ShowAtk ()
    {
        atkIndex = Random.Range (0, stats.ennemiAttacks.Count);
        UIeventGiveAtk.InvokeGiveUICurrentAtk(atkIndex);
        soEventGridManager.InvokeSelectRandomSlot(stats.ennemiAttacks[atkIndex].zone);
        stats.actualAtkDamage = GetDamageValue();
        stats.actualAtkZoneNbr = GetAtkZoneNbr();
    }
    private void RemoveAtk ()
    {
        soEventGridManager.InvokeRemoveAtk();
    }
    
    private int GetDamageValue ()
    {
        return stats.ennemiAttacks[atkIndex].damage;
    }

    private int GetAtkZoneNbr ()
    {
        return stats.ennemiAttacks[atkIndex].zone.gameObject.GetComponent<EnemyZoneAtk>().listPoints.Count;
    }

}
