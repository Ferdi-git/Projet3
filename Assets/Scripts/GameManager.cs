using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Combat combat;
    [SerializeField] private SOEventGridManager gridManager;
    [SerializeField] private SOEventState gameState;

    private void OnEnable()
    {
        gameState.EndOfCombat += CombateEnded;
        gameState.EndOfShoping += ShopingEnded;
    }
    private void OnDisable()
    {
        gameState.EndOfCombat -= CombateEnded;
        gameState.EndOfShoping -= ShopingEnded;
    }
    public void ButtonPressed ()
    {
        // effet à l'appuie 
        //gridManager?.InvokeActualiseBoard();
        //gridManager?.InvokeResetGrid();
        combat.StartTurn(); 
    }

    private void Start()
    {
        gameState.InvokeStartCombat();
    }


    private void CombateEnded ()
    {
        print("combat ended");
        gameState.InvokeStartShoping();
    }


    private void ShopingEnded ()
    {
        print("shop ended");
    }
}
