using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleChoice : MonoBehaviour
{

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private SoPieces currentSO ;
    public GameObject onePieceChoice ;
    
    public void Initialize(SoPieces piece)
    {
        image.sprite = piece.image;
        text.text = piece.description;
        currentSO = piece;
    }


    public void CreateBlock()
    {
        print("Create");
        Instantiate(currentSO.prefab, onePieceChoice.transform.position, onePieceChoice.transform.rotation, onePieceChoice.transform);

    }

}
