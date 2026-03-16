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
            yield return new WaitForSeconds(0.15f);
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
        if (action.DamageToMe != 0)
        {
            //damage to me ...
        }
        if (action.AmountShieldGained != 0)
        {
            //gain shield 
        }
        if (action.AmountShieldLost != 0)
        {
            //loose shield
        }
        if (action.AmountHeal != 0)
        {
            //heal 
        }
        //autre action possible 
    }

    public void DoDamage ()
    {
        //ui
        //change value pv ennemi 
    }
    public void TakeDamage ()
    {
        //ui
        //change value pv player 
    }
    public void GainShield ()
    {
        //ui 
        //change shield value 
    }
    public void LooseShield ()
    {
        //ui 
        //change Shield Value 
    }
    public void Heal ()
    {
        //ui 
        //change pv player 
    }
}

