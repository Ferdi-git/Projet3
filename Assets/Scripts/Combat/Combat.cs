using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private SoBoard soBoard;
    [SerializeField] private bool skipFight;
    [SerializeField] private StatsPlayer statsPlayer;
    [SerializeField] private StatsEnnemi statsEnnemi;

    [SerializeField] private int index;
    [SerializeField] private GameObject bouton;
    [SerializeField] private EnnemiManager ennemiManager;
    [SerializeField] private SoNbrOfPiecePlayed piecePlayed;
    [SerializeField] private SOEventGridManager eventGridManager;


    private void Start() // à enlever
    {
        StartCombat();
    }

    public void StartCombat ()
    {
        bouton.SetActive(true);
        ennemiManager.GenerateEnnemi();
        ennemiManager.ShowAtk();
    }

    public void StartTurn ( )
    {
        eventGridManager.InvokeSetAllPieceCanMove(false);
        piecePlayed.ResetInt();
        index = 0;
        if (index >= soBoard.boardPieces.Count)
        {
            StartCoroutine(EnnemiTurn());
            return;
        }
        StartCoroutine(PlayerTurn(0));
    }

    public void NextPiece ()
    {
        print(index);
        index++;
        if (index >= soBoard.boardPieces.Count)
        {
            StartCoroutine(EnnemiTurn());

            return;
        }
        StartCoroutine(PlayerTurn(index));
    }
    
    IEnumerator EnnemiTurn ()
    {
        yield return null;
        int zoneCount = ennemiManager.GetAtkZoneNbr();
        print (zoneCount);
        for (int i = 0; i < soBoard.boardPieces.Count; i++)
        {
            if (soBoard.boardPieces[i].context.NbrCaseAtk != 0)
            {
                //degats sur les pieces 
                zoneCount -= soBoard.boardPieces[i].context.NbrCaseAtk;
            }
        }
        
        statsPlayer.InvokeTakeDamage(ennemiManager.GetDamageValue() * zoneCount); // degats que recoit le joueur 
        print (zoneCount);
        print("le joeur se prend " + ennemiManager.GetDamageValue() * zoneCount + " degats");
        //enlever l'ancienne atk 
        ennemiManager.ShowAtk();

        eventGridManager.InvokeSetAllPieceCanMove(true);
    }

    IEnumerator PlayerTurn (int i)
    {
        
        yield return ResoudreEffet(soBoard.boardPieces[i].soPieces, i );
        NextPiece();
    }

    private IEnumerator ResoudreEffet ( SoPieces piece , int i)
    {
        if (piece.pieceEffet.condition.Condition(soBoard.boardPieces[i].context, piece.ConditionValues))
        {
            OutputPort port = new OutputPort();
            port.statsPlayer = statsPlayer;
            port.statsEnnemi = statsEnnemi;
            port.thisBoardPiece = soBoard.boardPieces[i];
            port.piecePlayed = piecePlayed;
            yield return piece.pieceEffet.effet.Effet(soBoard.boardPieces[i].context,port, piece.EfectValues , i);
            
        }
        else
        {
            yield return soBoard.boardPieces[i].piecePersonality.PlayAnimations(i, PieceAnimations.TypeAnim.failed);
        }
        
    }






    /* CACA DE FERDINAND C'etait le debut des pv des pieces


    public void TakeDamage(float dmg)
    {
        if (shield > 0)
        {
            float shieldToLose = shield - dmg;

            shieldToLose = Mathf.Clamp(shieldToLose, 0, shield);
            dmg -= shieldToLose;
            shield -= shieldToLose;
        }
        healthPoint -= dmg;

        if (healthPoint <= 0)
        {
            DestroyPiece();
        }
    }

    public void Heal(float healPoint)
    {
        healthPoint += healPoint;
    }

    public void GetShields(float nbrShield)
    {
        shield += nbrShield;
    }

    */
}

