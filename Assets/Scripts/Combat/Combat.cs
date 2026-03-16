using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private SoBoard soBoard;
    [SerializeField] private bool skipFight;
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
            PieceAction newAction = piece.pieceEffet.effet.Effet(soBoard.boardPieces[i].context, piece.EfectValue);
            ResoudreAction(newAction, piece.EfectValue);
        }
        //condition pas completé 
        //passer à piece suivante 
    }

    private void ResoudreAction (PieceAction action, int value)
    {
        if (action.DamageToEnnemi != 0)
        {
            DoDamage(value);
        }
        if (action.DamageToMe != 0)
        {
            TakeDamage(value);
        }
        if (action.AmountShieldGained != 0)
        {
            GainShield(value);
        }
        if (action.AmountShieldLost != 0)
        {
            LooseShield(value);
        }
        if (action.AmountHeal != 0)
        {
            Heal(value);
        }
        //autre action possible 
    }

    public void DoDamage (int amount)
    {
        print ("Damage :"+ amount);
        //ui
        //change value pv ennemi 
    }
    public void TakeDamage (int amount)
    {
        print("Damage to me :" + amount);
        //ui
        //change value pv player 
    }
    public void GainShield (int amount)
    {
        print("Shield : +" + amount);
        //ui 
        //change shield value 
    }
    public void LooseShield (int amount)
    {
        print("Shield : -" + amount);
        //ui 
        //change Shield Value 
    }
    public void Heal (int amount)
    {
        print("Heal :" + amount);
        //ui 
        //change pv player 
    }
}

