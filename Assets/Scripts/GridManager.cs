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

    [SerializeField] private PiecePersonality[] piecesExist;
    [SerializeField] private List<BoardPiece> listBoardPiecesExist;

    private void OnEnable()
    {
        gridManager.ActualiseBoard += ActualiseBoard;

    }

    private void OnDisable()
    {
        gridManager.ActualiseBoard -= ActualiseBoard;

    }

    private void Start()
    {
        for(int i = 0; i < piecesExist.Length; i++)
        {
            BoardPiece newBoardPiece = new();
            newBoardPiece.piecePersonality = piecesExist[i];
            newBoardPiece.soPieces = piecesExist[i].soPieces;
            listBoardPiecesExist.Add(newBoardPiece); 
        }

    }


    [Button]
    private void ActualiseBoard()
    {
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
        return listToReturn;
    }


    private BoardPiece GetBoardPiece(PiecePersonality piecePersonality)
    {
        for (int nbr = 0; nbr < listBoardPiecesExist.Count; nbr++)
        {
            if (listBoardPiecesExist[nbr].piecePersonality == piecePersonality)
            {
                return listBoardPiecesExist[nbr];
            }
        }
        Debug.LogError("Weird");
        return null;
    }
}


