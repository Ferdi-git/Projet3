using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private SoBoard soBoard;
    [SerializeField] private bool skipFight;
    [SerializeField] private StatsPlayer statsPlayer;
    public void StartTurn ( )
    {
        StartCoroutine(Tour());
    }

    

    IEnumerator Tour ()
    {
        for ( int i = 0; i < soBoard.boardPieces.Count; i++ )
        {
            print("tour 1, piece numero :" + i);
            float timeToWait = 0.3f - 0.01f*i ;
            timeToWait = Mathf.Clamp(timeToWait, 0.05f, 0.7f);
            timeToWait = skipFight ? 0 : timeToWait;
            yield return new WaitForSeconds(timeToWait);
            soBoard.boardPieces[i].piecePersonality.PlayAnimations(i);
            ResoudreEffet(soBoard.boardPieces[i].soPieces, i);

        }
        


    }

    private void ResoudreEffet ( SoPieces piece , int i)
    {
        if (piece.pieceEffet.condition.Condition(soBoard.boardPieces[i].context, piece.ConditionValue))
        {
            OutputPort port = new OutputPort();
            port.statsPlayer = statsPlayer;
            piece.pieceEffet.effet.Effet(soBoard.boardPieces[i].context,port, piece.EfectValue);
            //ResoudreAction(newAction, piece.EfectValue);
        }
        //condition pas completé 
        //passer à piece suivante 
    }
}

