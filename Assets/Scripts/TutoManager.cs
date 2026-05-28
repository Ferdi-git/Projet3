using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ecransTuto ;
    private int currentStep = 0;
    [SerializeField] private SOEventEndPlayerTurn eventEndPlayerTurn ;
    [SerializeField] private SOEventState eventState ;

    private void OnEnable()
    {
        eventEndPlayerTurn.EndTurn += NextStep;
        eventState.EndOfCombat += EndTuto;
    }

    private void OnDisable()
    {
        eventEndPlayerTurn.EndTurn -= NextStep;
        eventState.EndOfCombat -= EndTuto;

    }

    private void Start()
    {
        currentStep = 0;

        for (int i = 0; i < ecransTuto.Length; i++)
        {
            ecransTuto[i].gameObject.SetActive(false);
        }
        ecransTuto[0].gameObject.SetActive(true);
    }

    private void EndTuto()
    {
        SceneManager.LoadScene("MainMenu");

    }

    private void NextStep()
    {
        if (currentStep + 1 < ecransTuto.Length)
        {
            ecransTuto[currentStep].SetActive(false);
            currentStep++;
            ecransTuto[currentStep].SetActive(true);
        }
    }
}
