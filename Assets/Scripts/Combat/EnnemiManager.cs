
using UnityEngine;

public class EnnemiManager : MonoBehaviour
{
    [SerializeField] private StatsEnnemi stats;
    [SerializeField] private GameObject ennemiUI;
    [SerializeField] private KeepEnnemiSo ennemiList;
    [SerializeField] private SOEventGridManager soEventGridManager;
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
        stats.pvMax = ennemiList.ennemiList[index].resistance * 30;
        stats.pv = stats.pvMax;
        stats.shield = 0;
        stats.sprite = ennemiList.ennemiList[index].sprite;
        stats.ennemiAttacks = ennemiList.ennemiList[index].attacks;
    }

    public void ShowAtk ()
    {
        atkIndex = Random.Range (0, stats.ennemiAttacks.Count);
        soEventGridManager.InvokeSelectRandomSlot(stats.ennemiAttacks[atkIndex].zone);
    }
    

}
