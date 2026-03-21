using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    [SerializeField] private GridSlot[] gridSlots;
    [SerializeField] private SoSaveInventory soSaveInventory;
    [SerializeField] private SOEventGridManager gridManager;

    private void Start()
    {
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
        ResetGridSlots();
        for (int i = 0; i < soSaveInventory.pieces.Count; i++)
        {
            soSaveInventory.pieces[i].transform.position = soSaveInventory.piecesPos[i];
            soSaveInventory.pieces[i].transform.rotation = soSaveInventory.piecesRot[i];
            soSaveInventory.pieces[i].GetComponent<PieceMouvement>().SnapToGrid();
        }
        gridManager.InvokeResetGridSlots();

    }

    public void ResetGridSlots()
    {
        for (int nbr = 0; nbr < gridSlots.Length; nbr++)
        {
            gridSlots[nbr].isFilled = false;
        }
    }
}
