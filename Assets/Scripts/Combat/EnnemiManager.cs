
using UnityEngine;

public class EnnemiManager : MonoBehaviour
{
    [SerializeField] private StatsEnnemi stats;
    [SerializeField] private GameObject ennemiUI;
    [SerializeField] private KeepEnnemiSo ennemiList;
    private int index;

    private void Start()
    {
        ennemiUI.SetActive(false);
    }
    public void GenerateEnnemi ()
    {
        ennemiUI.SetActive (true);
        index = Random.Range(0,ennemiList.ennemiList.Count);
        stats.pvMax = ennemiList.ennemiList[index].Résistance * 30;
        stats.pv = stats.pvMax;
        stats.shield = 0;
    }

    public void Attack (OutputPort port , Context context)
    {
        int index2 = Random.Range(0, ennemiList.ennemiList[index].Effets.Count);
        ennemiList.ennemiList[index].Effets[index2].effet.Effet(context, port, ennemiList.ennemiList[index].Effets[index2].values, 2);// il faudra mettre un tour au hasard je suppose
    }
}
