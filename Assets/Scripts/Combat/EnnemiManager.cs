using UnityEngine;

public class EnnemiManager : MonoBehaviour
{
    [SerializeField] private StatsEnnemi stats;
    [SerializeField] private GameObject ennemiUI;

    private void Start()
    {
        ennemiUI.SetActive(false);
    }
    public void GenerateEnnemi ()
    {
        ennemiUI.SetActive (true);
        //genenerate ennemi 
    }
}
