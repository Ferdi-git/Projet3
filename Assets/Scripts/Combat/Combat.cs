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


    private void Start()
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
        StartCoroutine(Tour(0, 0f));
    }

    public void NextPiece (float delai)
    {
        print(index);
        index++;
        if (index >= soBoard.boardPieces.Count)
        {
            return;
        }
        StartCoroutine(Tour(index, delai));
    }
    

    IEnumerator Tour (int i, float delai)
    {
        

        print("tour 1, piece numero :" + i);
        if (delai == 0f)
        {
            float timeToWait = 0.3f - 0.01f * i;
            timeToWait = Mathf.Clamp(timeToWait, 0.05f, 0.7f);
            timeToWait = skipFight ? 0 : timeToWait;
            yield return new WaitForSeconds(timeToWait+3);
            soBoard.boardPieces[i].piecePersonality.PlayAnimations(i);
            ResoudreEffet(soBoard.boardPieces[i].soPieces, i);
        }
        else
        {
            yield return new WaitForSeconds(delai+3);
            soBoard.boardPieces[i].piecePersonality.PlayAnimations(i);
            ResoudreEffet(soBoard.boardPieces[i].soPieces, i);
        }
        



    }

    private void ResoudreEffet ( SoPieces piece , int i)
    {
        if (piece.pieceEffet.condition.Condition(soBoard.boardPieces[i].context, piece.ConditionValues))
        {
            OutputPort port = new OutputPort();
            port.statsPlayer = statsPlayer;
            port.statsEnnemi = statsEnnemi;
            port.combat = this;
            piece.pieceEffet.effet.Effet(soBoard.boardPieces[i].context,port, piece.EfectValues);
            //ResoudreAction(newAction, piece.EfectValue);
        }
        else
        {
            NextPiece(0f);
        }
        //condition pas completé 
        //passer à piece suivante 
    }
}

