using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSlot[] gridSlots;
    [SerializeField] private SOEventGridManager gridManager;

    [SerializeField] private SoBoard theBoard;


    [Button]
    public SoBoard GetTheBoard()
    {
        ActualiseBoard();
        print(theBoard.boardPiecesDico);
        return theBoard;
    }


    [Button]
    private void ActualiseBoard()
    {
        //theBoard.boardPieces.Clear();
        //theBoard.boardPieces.Clear();

        for (int i = 0; i < gridSlots.Length; i++)
        {
            PiecePersonality pieceOnSlot = gridSlots[i].GetPieceOnIt();

            if (!gridSlots[i].isFilled || pieceOnSlot.wasUsed)
                continue;

            //BoardPiece newBoardPiece = pieceOnSlot.boardPiece;
         
            //theBoard.voisins[i].AddRange(GetVoisins(pieceOnSlot));
            //theBoard.boardPieces.Append(newBoardPiece);
            theBoard.boardPiecesDico[pieceOnSlot.boardPiece] = GetVoisins(pieceOnSlot);
        }


    }
    
    private List<BoardPiece> GetVoisins(PiecePersonality piecePerso)
    {
        var listToReturn = new List<BoardPiece>();
        for (int i = 0; i < piecePerso.GetSurroundingPoints().Length; i++)
        {
            foreach (var hit in Physics2D.OverlapPointAll(transform.position))
            {
                var voisinPiecePerso = hit.gameObject.GetComponent<PiecePersonality>();

                if (voisinPiecePerso != null)
                    listToReturn.Append(voisinPiecePerso.boardPiece);

            }
        }
        return listToReturn;
    }

}


