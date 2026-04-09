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
    [SerializeField] private PieceHealthManager pieceHealthManager;
    [SerializeField] private SOEventEnnemy eventEnnemi;

    [SerializeField] private SOEventState eventState;


    public int NbrOfCombat;
    private void OnEnable()
    {
        eventState.StartCombat += StartCombat;
    }
    private void OnDisable()
    {
        eventState.StartCombat -= StartCombat;
    }

    private void Start()
    {
        NbrOfCombat = 0;
    }

    public void StartCombat ()
    {
        
        bouton.SetActive(true);
        ennemiManager.GenerateEnnemi(NbrOfCombat);
        ennemiManager.ShowAtk();
        eventEnnemi.InvokeNewEnnemi();
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
        int indexPieceDamaged = 0;
        yield return null;
        int zoneCount = ennemiManager.GetAtkZoneNbr();
        //print ("Nombre de case que prend l'attque : "+zoneCount);
        for (int i = 0; i < soBoard.boardPieces.Count; i++)
        {
            if (soBoard.boardPieces[i].context.NbrCaseAtk != 0)
            {
                yield return soBoard.boardPieces[i].pieceAnimation.PlayAnimations(indexPieceDamaged, PieceAnimations.TypeAnim.takeDamage, null);
                zoneCount -= soBoard.boardPieces[i].context.NbrCaseAtk;
                pieceHealthManager.GiveStats(soBoard.boardPieces[i].healthPoint, soBoard.boardPieces[i].shield , soBoard.boardPieces[i]);
                pieceHealthManager.TakeDamage(ennemiManager.GetDamageValue() * soBoard.boardPieces[i].context.NbrCaseAtk);
                indexPieceDamaged++;
            }
        }
        
        statsPlayer.InvokeTakeDamage(ennemiManager.GetDamageValue() * zoneCount); // degats que recoit le joueur 
        //print ("Nombre de case qui vont touché le joueur : "+zoneCount);
        //print("le joeur se prend " + ennemiManager.GetDamageValue() * zoneCount + " degats");
        
        StartCoroutine(ResoudreTurn());
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
            yield return soBoard.boardPieces[i].pieceAnimation.PlayAnimations(i, PieceAnimations.TypeAnim.failed, null);
        }
        
    }


    IEnumerator ResoudreTurn ()
    {
        yield return null;
        //check si des pieces sont mortes 
        //enlever bouclier aux pieces (mettre bouclier dans boardpiece)
        //print("test");
        ennemiManager.RemoveAtk();
        ennemiManager.ShowAtk();

        if (statsPlayer.GetPV() <= 0 )
        {
            //print("perdu");
        }
        else
        {
            if (statsEnnemi.GetPV() <= 0 )
            {
                NbrOfCombat += 1;
                eventState.InvokeEndOfCombat();
            }
        }
        eventGridManager.InvokeSetAllPieceCanMove(true);
    }



    
}

