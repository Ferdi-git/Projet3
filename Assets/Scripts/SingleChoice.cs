using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleChoice : MonoBehaviour
{

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    private SoPieces currentSO ;
    
    public void Initialize(SoPieces piece)
    {
        image.sprite = piece.image;
        text.text = piece.description;
        currentSO = piece;
    }


    public void CreateBlock()
    {
        print("Cretate");
        Instantiate(currentSO.prefab);

    }

}
