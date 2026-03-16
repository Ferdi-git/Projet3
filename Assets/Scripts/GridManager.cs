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
        return theBoard;
    }


    [Button]
    private void ActualiseBoard()
    {
        List<PiecePersonality> piecePersonalities = new List<PiecePersonality>();

        theBoard.boardPieces.Clear();

        for (int i = 0; i < gridSlots.Length; i++)
        {
            PiecePersonality pieceOnSlot = gridSlots[i].GetPieceOnIt();

            if (!gridSlots[i].isFilled || pieceOnSlot.wasUsed)
                continue;

            BoardPiece newBoardPiece = new BoardPiece();
            newBoardPiece.piecePersonality = pieceOnSlot;
            newBoardPiece.soPieces = pieceOnSlot.soPieces;


            theBoard.boardPieces.Append(newBoardPiece);
            piecePersonalities.Append(pieceOnSlot);
            
        }
        print(piecePersonalities);
    }
    


}


