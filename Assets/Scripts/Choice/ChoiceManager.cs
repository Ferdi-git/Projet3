using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] Transform[] spotChoice;

    [SerializeField] SOEventGridManager eventGridManager;
    [SerializeField] SoPieces[] difPieces;

    public List<GameObject> lastGeneratedPiece = new List<GameObject>(); 

    public ShopManager shopManager;

    private void OnEnable()
    {
        eventGridManager.PiecePlaced += CheckIfPiecePlaced;
    }

    private void OnDisable()
    {
        eventGridManager.PiecePlaced -= CheckIfPiecePlaced;
    }

    [Button]
    public void GeneratePiece()
    {
        ClearChoice();
        for (int i = 0; i < spotChoice.Length ; i++)
        {

            int randInt = Random.Range(0, difPieces.Length);

            if(lastGeneratedPiece.Count !=0)
            {
                bool foundNonRepeatPiece = false;
                while (!foundNonRepeatPiece)
                {
                    randInt = Random.Range(0, difPieces.Length);
                    foundNonRepeatPiece = CheckIfPieceAlreadyChecked(difPieces[randInt]);
                }
            }
            else
            {
                randInt = Random.Range(0, difPieces.Length);
            }

            GameObject newPiece = Instantiate(difPieces[randInt].prefab, spotChoice[i].transform.position, spotChoice[i].transform.rotation, transform);

            PieceInfo pieceInfo = newPiece.GetComponent<PieceInfo>();
            pieceInfo.soPiece.TempEffectValues = pieceInfo.soPiece.BaseEffectValues;
            lastGeneratedPiece.Add(newPiece);
        }
    }



    private bool CheckIfPieceAlreadyChecked(SoPieces piece)
    {
        for (int i = 0; i < lastGeneratedPiece.Count; i++)
        {
            if (lastGeneratedPiece[i].GetComponent<PieceInfo>().soPiece == piece)
            {
                lastGeneratedPiece.RemoveAt(i);
                return false;
            }
        }
        return true;
    }

    private void ClearChoice()
    {
        for (int i = 0; i < lastGeneratedPiece.Count; i++) { Destroy(lastGeneratedPiece[i]); }
        lastGeneratedPiece.Clear();
    }


    private void CheckIfPiecePlaced(GameObject go)
    {
        if(lastGeneratedPiece == null) return;

        for (int i = 0; i < lastGeneratedPiece.Count; i++)
        {
            if (go == lastGeneratedPiece[i])
            {
                lastGeneratedPiece.RemoveAt(i);
                go.transform.SetParent(null);
                eventGridManager.InvokeAddBoardPiece(go);
                eventGridManager.InvokeTrySaveInventory();
                EndChoice();
            }
        }

    }

    public void EndChoice()
    {
        ClearChoice();
        shopManager.CloseShop();
    }

}
