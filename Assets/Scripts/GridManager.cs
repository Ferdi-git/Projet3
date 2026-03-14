using Sirenix.OdinInspector;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSlot[] gridSlots;
    [SerializeField] private SOEventGridManager gridManager;


    [Button]
    private string ParcourGrid()
    {
        string listOfActions = "";


        for (int i = 0; i < gridSlots.Length; i++)
        {
            PiecePersonality pieceOnSlot = gridSlots[i].GetPieceOnIt();

            if (!gridSlots[i].isFilled || pieceOnSlot.wasUsed)
                continue;
            listOfActions += pieceOnSlot.GetContext();
        }
        print(listOfActions);
        gridManager.InvokeResetGrid();
        return listOfActions;
    }


}

public class ActionEffet
{


}
