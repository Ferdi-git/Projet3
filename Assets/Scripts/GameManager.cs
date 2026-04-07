using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Combat combat;
    [SerializeField] private SOEventGridManager gridManager;
    [SerializeField] private SOEventState gameState;
    [SerializeField] private FloorManager floorManager;

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
        combat.StartTurn(); 
    }

    private void Start()
    {
        floorManager.GenerateFloorList();
        FirstFloor();
        
    }
    private void FirstFloor ()
    {
        gameState.InvokeStartCombat();
    }
    private void NextFloor ()
    {
        //gameState.InvokeStartShoping();
        //gameState.InvokeStartCombat();
    }

    private void CombateEnded ()
    {
        print("combat ended");
        NextFloor();
        //gameState.InvokeStartShoping();
    }


    private void ShopingEnded ()
    {
        print("shop ended");
        NextFloor();
        //gameState.InvokeStartCombat();
    }
}
