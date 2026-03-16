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
            yield return new WaitForSeconds(1f);
            soBoard.boardPieces[i].piecePersonality.PlayAnimations(i);
        }
        


    }


}
