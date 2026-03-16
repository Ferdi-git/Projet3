using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private SoBoard soBoard;

    public void StartTurn ( )
    {
        StartCoroutine(Tour());
    }

    

    IEnumerator Tour ()
    {
        for ( int i = 0; i < soBoard.boardPieces.Count; i++ )
        {
            print("tour 1, piece numero :" + i);
            yield return new WaitForSeconds(1f);
            soBoard.boardPieces[i].piecePersonality.PlayAnimations(i);
            ResoudreEffet(soBoard.boardPieces[i].soPieces, i);

        }
        


    }

    private void ResoudreEffet ( SoPieces piece , int i)
    {
        if (piece.pieceEffet.condition.Condition(soBoard.boardPieces[i].context, piece.ConditionValue))
        {
            PieceAction newAction = piece.pieceEffet.effet.Effet(soBoard.boardPieces[i].context, piece.EfectValue);
            ResoudreAction(newAction);
        }
        //condition pas completé 
        //passer à piece suivante 
    }

    private void ResoudreAction (PieceAction action)
    {
        if (action.DamageToEnnemi != 0)
        {
            //damage ... 
        }
    }


}
