using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GridManager;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSlot[] gridSlots;
    [SerializeField] private SOEventGridManager gridManager;

    [SerializeField] private SoBoard theBoard;

    [SerializeField] private SoSaveInventory soSaveInventory;
    [SerializeField] private PiecePersonality[] piecesExist;

    public SortMode baseSortMode;

    public enum SortMode { ByRow, ByColumn }


    private void OnEnable()
    {
        gridManager.ActualiseBoard += ActualiseBoard;
        gridManager.ResetGridSlots += ResetGridSlots;

    }

    private void OnDisable()
    {
        gridManager.ActualiseBoard -= ActualiseBoard;
        gridManager.ResetGridSlots -= ResetGridSlots;

    }

    private void Start()
    {
        SortBoard(baseSortMode);
        soSaveInventory.listBoardPiecesExist.Clear();
        for (int i = 0; i < piecesExist.Length; i++)
        {
            BoardPiece newBoardPiece = new();
            newBoardPiece.piecePersonality = piecesExist[i];
            newBoardPiece.soPieces = piecesExist[i].soPiece;
            soSaveInventory.listBoardPiecesExist.Add(newBoardPiece); 
        }

    }


    [Button]
    private void ActualiseBoard()
    {
        SortBoard(baseSortMode);
        theBoard.boardPieces.Clear();

        for (int i = 0; i < gridSlots.Length; i++)
        {
            PiecePersonality pieceOnSlot = gridSlots[i].GetPieceOnIt();

            if (!gridSlots[i].isFilled || pieceOnSlot.wasUsed)
                continue;

            pieceOnSlot.wasUsed = true;
            BoardPiece currentBoardPiece = GetBoardPiece(pieceOnSlot);

            currentBoardPiece.context.voisins = GetVoisins(pieceOnSlot);
            theBoard.boardPieces.Add(currentBoardPiece);
        }


    }
    
    private List<BoardPiece> GetVoisins(PiecePersonality piecePerso)
    {
        var listToReturn = new List<BoardPiece>();
        for (int i = 0; i < piecePerso.GetSurroundingPoints().Length; i++)
        {
            foreach (var hit in Physics2D.OverlapPointAll(piecePerso.GetSurroundingPoints()[i].transform.position))
            {
                var voisinPiecePerso = hit.gameObject.GetComponent<PiecePersonality>();

                if (voisinPiecePerso != null && !listToReturn.Contains(GetBoardPiece(voisinPiecePerso)))
                    listToReturn.Add(GetBoardPiece(voisinPiecePerso));


            }
        }

        listToReturn = baseSortMode == SortMode.ByRow
      ? listToReturn.OrderBy(p => -p.piecePersonality.transform.position.y).ThenBy(p => p.piecePersonality.transform.position.x).ToList()
      : listToReturn.OrderBy(p => p.piecePersonality.transform.position.x).ThenBy(p => -p.piecePersonality.transform.position.y).ToList();

        return listToReturn;
    }


    private BoardPiece GetBoardPiece(PiecePersonality piecePersonality)
    {
        for (int nbr = 0; nbr < soSaveInventory.listBoardPiecesExist.Count; nbr++)
        {
            if (soSaveInventory.listBoardPiecesExist[nbr].piecePersonality == piecePersonality)
            {
                return soSaveInventory.listBoardPiecesExist[nbr];
            }
        }
        Debug.LogError("Weird");
        return null;
    }

    private void SortBoard(SortMode sortMode)
    {
        gridSlots = sortMode == SortMode.ByRow
            ? gridSlots.OrderBy(p => -p.transform.position.y).ThenBy(p => p.transform.position.x).ToArray()
            : gridSlots.OrderBy(p => p.transform.position.x).ThenBy(p => -p.transform.position.y).ToArray();
    }


    public void ResetGridSlots()
    {
        for(int nbr = 0; nbr < gridSlots.Length ; nbr++)
        {
            gridSlots[nbr].isFilled = false;
        }
    }

}


