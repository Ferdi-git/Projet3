using DG.Tweening;
using Sirenix.OdinInspector;
using System;
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
        gridManager.OnePieceIsPlaced -= CheckIfCanSave;

    }

    private void Start()
    {
        SaveGrid();
    }

    public void CheckIfCanSave()
    {
        gridManager.InvokeActualiseBoard();

        if(isReseting || theBoard.boardPieces.Count != 0) return;
        //print(theBoard.boardPieces.Count);
        //print("HALLOO");
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
            PieceAnimations pieceOnSlot = gridSlots[i].GetPieceOnIt();

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
        float delay = 0;

        //gridManager.InvokeResetGridSlots();
        //EmptyInventoryGridSlots();

        int lastMovingIndex = -1;
        for (int i = 0; i < soSaveInventory.pieces.Count; i++)
        {
            if (soSaveInventory.pieces[i].transform.position != soSaveInventory.piecesPos[i] ||
                soSaveInventory.pieces[i].transform.rotation != soSaveInventory.piecesRot[i])
                lastMovingIndex = i;
        }


        if (lastMovingIndex == -1)
        {
            gridManager.InvokeResetGridSlots();
            EmptyInventoryGridSlots();
            ReSnapEverything();
            isReseting = false;
            return;
        }



        for (int i = 0; i < soSaveInventory.pieces.Count; i++)
        {
            if (soSaveInventory.pieces[i].transform.position == soSaveInventory.piecesPos[i] &&
                soSaveInventory.pieces[i].transform.rotation == soSaveInventory.piecesRot[i])
                continue;

            int index = i;
            bool isLast = index == lastMovingIndex;

            soSaveInventory.pieces[index].transform
                .DOMove(soSaveInventory.piecesPos[index], timeToGoBackToInventory)
                .SetDelay(delay);

            soSaveInventory.pieces[index].transform
                .DORotateQuaternion(soSaveInventory.piecesRot[index], timeToGoBackToInventory)
                .SetDelay(delay)
                .OnComplete(() =>
                {
                    soSaveInventory.pieces[i].GetComponent<PieceMouvement>().SnapToGrid();

                    if (isLast)
                    {
                        gridManager.InvokeResetGridSlots();
                        EmptyInventoryGridSlots();
                        ReSnapEverything();
                        isReseting = false;
                    }
                });

            delay += delayInBetweenBackToInventory;
        }

    
    }








    private void ReSnapEverything()
    {
        for (int i = 0; i < soSaveInventory.pieces.Count; i++)
        {
            soSaveInventory.pieces[i].GetComponent<PieceMouvement>().SnapToGrid();
        }
        SaveGrid();
    }


    public void EmptyInventoryGridSlots()
    {
        for (int nbr = 0; nbr < gridSlots.Length; nbr++)
        {
            gridSlots[nbr].isFilled = false;
        }
    }
}
