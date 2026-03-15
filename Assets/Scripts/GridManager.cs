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


    [Button]
    private string ParcourGrid()
    {
        string listOfActions = "";
        List<PiecePersonality> personalities = new List<PiecePersonality>();

        for (int i = 0; i < gridSlots.Length; i++)
        {
            PiecePersonality pieceOnSlot = gridSlots[i].GetPieceOnIt();

            if (!gridSlots[i].isFilled || pieceOnSlot.wasUsed)
                continue;

            PieceContext context = pieceOnSlot.GetContext();
            listOfActions += context.actions;
            personalities.AddRange(context.personality);
            

        }
        print(listOfActions);
        print(personalities);
        StartCoroutine(ANIMATIONTIMECOROUTINE(listOfActions,personalities));
        gridManager.InvokeResetGrid();
        return listOfActions;
    }


    private IEnumerator ANIMATIONTIMECOROUTINE(string actions, List<PiecePersonality> piecePersonalities)
    {
        for (int i = 0;i < piecePersonalities.Count; i++)
        {

            //DO THEIR FUCKING  ACTIONS
            piecePersonalities[i].PlayAnimations(i);
            yield return new WaitForSeconds(0.15f);

        }

    }



}

public class ActionEffet
{


}

