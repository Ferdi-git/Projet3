
using Unity.VisualScripting;
using UnityEngine;

public class EnnemiManager : MonoBehaviour
{
    [SerializeField] private StatsEnnemi stats;
    [SerializeField] private GameObject ennemiUI;
    [SerializeField] private KeepEnnemiSo ennemiList;
    [SerializeField] private SOEventGridManager soEventGridManager;
    [SerializeField] private UIManager uiManager;
    private int index;
    private int atkIndex;

    private void Start()
    {
        ennemiUI.SetActive(false);
    }
    public void GenerateEnnemi ()
    {
        ennemiUI.SetActive (true);
        atkIndex = 0;
        index = Random.Range(0,ennemiList.ennemiList.Count);
        stats.pvMax = (ennemiList.ennemiList[index].resistance/100) * stats.AverageValue ;
        stats.pv = stats.pvMax;
        stats.shield = 0;
        stats.sprite = ennemiList.ennemiList[index].sprite;
        stats.ennemiAttacks = ennemiList.ennemiList[index].attacks;

        uiManager.UpdateUI();
    }

    public void ShowAtk ()
    {
        atkIndex = Random.Range (0, stats.ennemiAttacks.Count);
        soEventGridManager.InvokeSelectRandomSlot(stats.ennemiAttacks[atkIndex].zone);
    }
    public void RemoveAtk ()
    {
        soEventGridManager.InvokeRemoveAtk();
    }
    
    public int GetDamageValue ()
    {
        return stats.ennemiAttacks[atkIndex].damage;
    }

    public int GetAtkZoneNbr ()
    {
        return stats.ennemiAttacks[atkIndex].zone.gameObject.GetComponent<EnemyZoneAtk>().listPoints.Count;
    }

}
