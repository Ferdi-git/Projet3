using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] int nbrOfChoice;

    [SerializeField] GameObject layout;

    [SerializeField] SoPieces[] difPieces;

    [SerializeField] GameObject prefabPieceChoice;


    [Button]
    public void GeneratePiece()
    {
        for (int i = 0; i < nbrOfChoice ; i++)
        {
            var prefab = Instantiate(prefabPieceChoice, layout.transform);
            int randInt = Random.Range(0, difPieces.Length);
            prefab.GetComponent<SingleChoice>().Initialize(difPieces[randInt]); ;
            prefab.GetComponent<Button>().onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        layout.SetActive(false);
    }


}
