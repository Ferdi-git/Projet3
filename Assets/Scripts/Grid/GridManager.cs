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
    [SerializeField] private SOEventPieceHealth healthManager;

    [SerializeField] private SoBoard theBoard;

    [SerializeField] private SoSaveInventory soSaveInventory;
    [SerializeField] private PieceInfo[] piecesExist;

    public List<GameObject> listBoardPrefabAtk ;

    public SortMode baseSortMode;

    public enum SortMode { ByRow, ByColumn }


    private void OnEnable()
    {
        gridManager.ActualiseBoard += ActualiseBoard;
        gridManager.ResetGridSlots += ResetGridSlots;
        gridManager.OnePieceIsPlaced += ActualiseBoard;
        gridManager.AddBoardPiece += AddBoardPiece;
        gridManager.SelectRandomSlot += SelectRandomSlot;
        gridManager.RemoveAtk += RemoveAtk;
        healthManager.PieceDie += DestroyPiece;

    }

    private void OnDisable()
    {
        gridManager.ActualiseBoard -= ActualiseBoard;
        gridManager.ResetGridSlots -= ResetGridSlots;
        gridManager.OnePieceIsPlaced -= ActualiseBoard;
        gridManager.AddBoardPiece -= AddBoardPiece;
        gridManager.SelectRandomSlot -= SelectRandomSlot;
        gridManager.RemoveAtk -= RemoveAtk;
        healthManager.PieceDie -= DestroyPiece;




    }



    private void Awake()
    {
        SortBoard(baseSortMode);
        soSaveInventory.listBoardPiecesExist.Clear();
        for (int i = 0; i < piecesExist.Length; i++)
        {
            BoardPiece newBoardPiece = new();
            newBoardPiece.pieceInfo = piecesExist[i];
            newBoardPiece.pieceAnimation = piecesExist[i].GetComponent<PieceAnimations>();
            newBoardPiece.soPieces = piecesExist[i].soPiece;
            newBoardPiece.healthPoint = newBoardPiece.soPieces.healthPoint;
            soSaveInventory.listBoardPiecesExist.Add(newBoardPiece); 
        }
        ActualiseBoard();

    }


    [Button]
    private void ActualiseBoard()
    {
        gridManager.InvokeResetPieceGridCheckedd();
        SortBoard(baseSortMode);
        theBoard.boardPieces.Clear();
        ResetNbrAtckCase();
        SetNbrAtckCase();
        for (int i = 0; i < gridSlots.Length; i++)
        {
            PieceInfo pieceOnSlot = gridSlots[i].GetPieceOnIt();

            if (pieceOnSlot == null || pieceOnSlot.wasGridChecked)
                continue;


            pieceOnSlot.wasGridChecked = true;

            BoardPiece currentBoardPiece = GetBoardPiece(pieceOnSlot);
            currentBoardPiece.context.voisins = GetVoisins(pieceOnSlot);

            theBoard.boardPieces.Add(currentBoardPiece);
        }
        gridManager.InvokeTrySaveInventory();
    }
    
    private List<BoardPiece> GetVoisins(PieceInfo piecePerso)
    {
        var listToReturn = new List<BoardPiece>();
        for (int i = 0; i < piecePerso.GetSurroundingPoints().Length; i++)
        {
            foreach (var hit in Physics2D.OverlapPointAll(piecePerso.GetSurroundingPoints()[i].transform.position))
            {
                var voisinPiecePerso = hit.gameObject.GetComponent<PieceInfo>();

                if (voisinPiecePerso != null && !listToReturn.Contains(GetBoardPiece(voisinPiecePerso)))
                    listToReturn.Add(GetBoardPiece(voisinPiecePerso));


            }
        }

        listToReturn = baseSortMode == SortMode.ByRow
      ? listToReturn.OrderBy(p => -p.pieceInfo.transform.position.y).ThenBy(p => p.pieceInfo.transform.position.x).ToList()
      : listToReturn.OrderBy(p => p.pieceInfo.transform.position.x).ThenBy(p => -p.pieceInfo.transform.position.y).ToList();

        return listToReturn;
    }


    private BoardPiece GetBoardPiece(PieceInfo pieceInfo)
    {
        for (int nbr = 0; nbr < soSaveInventory.listBoardPiecesExist.Count; nbr++)
        {
            if (soSaveInventory.listBoardPiecesExist[nbr].pieceInfo == pieceInfo)
            {
                return soSaveInventory.listBoardPiecesExist[nbr];
            }
        }
        Debug.LogError("Weird board piece doesnt exist");
        return null;
    }

    private void ResetNbrAtckCase()
    {
        for(int i = 0; i < gridSlots.Length; i++)
        {
            PieceInfo piece = gridSlots[i].GetPieceOnIt();
            if(piece != null)
            {
                BoardPiece bp = GetBoardPiece(piece);
                bp.context.NbrCaseAtk = 0;
            }
        }
    }

    private void SetNbrAtckCase()
    {
        for (int i = 0; i < gridSlots.Length; i++)
        {
            PieceInfo piece = gridSlots[i].GetPieceOnIt();
            if (piece != null && gridSlots[i].isAttacked)
            {
                BoardPiece bp = GetBoardPiece(piece);
                bp.context.NbrCaseAtk +=1;
            }
        }
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
            gridSlots[nbr].ClearSlot();
        }
    }


    public void SelectRandomSlot(GameObject basePrefabAtk)
    {
        GameObject prefabAtk = Instantiate(basePrefabAtk);
        listBoardPrefabAtk.Add(prefabAtk);

        EnemyZoneAtk enemyAtk = prefabAtk.GetComponent<EnemyZoneAtk>();


        int randInt = Random.Range(0, gridSlots.Length);
        int randIntRota = Random.Range(0, 4);

        prefabAtk.transform.position = gridSlots[randInt].transform.position;
        prefabAtk.transform.position = gridSlots[randInt].transform.position;


        while (!enemyAtk.CheckIfCanBePlaced())
        {
            randInt = Random.Range(0, gridSlots.Length);
            randIntRota = Random.Range(0, 4);
            prefabAtk.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 90f * randIntRota));
            prefabAtk.transform.position = gridSlots[randInt].transform.position;
        }

        enemyAtk.SetAtk();


    }

    public void RemoveAtk()
    {
        for (int i = 0; i < listBoardPrefabAtk.Count; i++)
        {
            listBoardPrefabAtk[i].GetComponent<EnemyZoneAtk>().RemoveAtk();
            Destroy(listBoardPrefabAtk[i].gameObject);
        }
        listBoardPrefabAtk.Clear();
    }
        

    public void AddBoardPiece(GameObject go)
    {
        BoardPiece newBoardPiece = new();
        PieceInfo pieceInfo = go.GetComponent<PieceInfo>();
        newBoardPiece.pieceInfo = pieceInfo;
        newBoardPiece.pieceAnimation = go.GetComponent<PieceAnimations>();
        newBoardPiece.soPieces = pieceInfo.soPiece;
        soSaveInventory.listBoardPiecesExist.Add(newBoardPiece);
    }
    
    public void RemoveBoardPiece(BoardPiece bp)
    {
        for (int i = 0; i <soSaveInventory.listBoardPiecesExist.Count; i++)
        {
            if (soSaveInventory.listBoardPiecesExist[i] == bp)
            {
                soSaveInventory.listBoardPiecesExist.RemoveAt(i);
            }
        }
    }

    private void DestroyPiece(BoardPiece bp)
    {
        bp.pieceAnimation.DestroyPieceAnim();
        RemoveBoardPiece(bp);
        Debug.Log("IICICICICIICIICIC C PTET ELE PRBLM");
        ActualiseBoard();
    }

}


