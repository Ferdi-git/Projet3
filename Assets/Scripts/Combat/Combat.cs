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


    private void Start() // à enlever
    {
        StartCombat();
    }

    public void StartCombat ()
    {
        bouton.SetActive(true);
        ennemiManager.GenerateEnnemi();
    }

    public void StartTurn ( )
    {
        index = 0;
        if (index >= soBoard.boardPieces.Count)
        {
            return;
        }
        StartCoroutine(Tour(0));
    }

    public void NextPiece ()
    {
        print(index);
        index++;
        if (index >= soBoard.boardPieces.Count)
        {
            return;
        }
        StartCoroutine(Tour(index));
    }
    

    IEnumerator Tour (int i)
    {
        piecePlayed.ResetInt();
        yield return ResoudreEffet(soBoard.boardPieces[i].soPieces, i );
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
            NextPiece();
        }
        else
        {
            yield return soBoard.boardPieces[i].piecePersonality.PlayAnimations(i);
            NextPiece();
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

