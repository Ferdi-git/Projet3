using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    [SerializeField] private GridSlot[] gridSlots;
    [SerializeField] private SoSaveInventory soSaveInventory;
    [SerializeField] private SOEventGridManager gridManager;
    [SerializeField] private float timeToGoBackToInventory = 0.2f;
    [SerializeField] private float delayInBetweenBackToInventory = 0.05f;
    [SerializeField] private SoBoard theBoard;

    private bool isReseting = false;

    private void OnEnable()
    {
        gridManager.SaveInventory += SaveGrid;
        gridManager.ResetInventory += ResetInventory;
        gridManager.OnePieceIsPlaced += CheckIfCanSave;
    }

    private void OnDisable()
    {
        gridManager.SaveInventory -= SaveGrid;
        gridManager.ResetInventory -= ResetInventory;
    }

    private void Start()
    {
        SaveGrid();
    }

    public void CheckIfCanSave()
    {

        if(isReseting || theBoard.boardPieces.Count != 0) return;

        SaveGrid();
        
    }


    [Button]
    public void SaveGrid()
    {
        soSaveInventory.pieces.Clear(); 
        soSaveInventory.piecesPos.Clear(); 
        soSaveInventory.piecesRot.Clear();


        for (int i = 0; i < gridSlots.Length; i++)
        {
            PiecePersonality pieceOnSlot = gridSlots[i].GetPieceOnIt();

            if (!gridSlots[i].isFilled || soSaveInventory.pieces.Contains(pieceOnSlot.gameObject))
                continue;

            soSaveInventory.pieces.Add(pieceOnSlot.gameObject);
            soSaveInventory.piecesPos.Add(pieceOnSlot.transform.position);
            soSaveInventory.piecesRot.Add(pieceOnSlot.transform.rotation);
        }

    }

    [Button]
    public void ResetInventory()
    {
        isReseting = true;
        ResetGridSlots();
        float delay = 0;

        for (int i = 0; i < soSaveInventory.pieces.Count; i++)
        {
            int index = i;
            soSaveInventory.pieces[index].transform.DOMove(soSaveInventory.piecesPos[index], timeToGoBackToInventory)
                .SetDelay(delay);
            soSaveInventory.pieces[index].transform.DORotateQuaternion(soSaveInventory.piecesRot[index], timeToGoBackToInventory)
                .SetDelay(delay)
                .OnComplete(() => soSaveInventory.pieces[index].GetComponent<PieceMouvement>().SnapToGrid());
            delay += delayInBetweenBackToInventory;
        }
        gridManager.InvokeResetGridSlots();
        isReseting = false;

    }

    public void ResetGridSlots()
    {
        for (int nbr = 0; nbr < gridSlots.Length; nbr++)
        {
            gridSlots[nbr].isFilled = false;
        }
    }
}
