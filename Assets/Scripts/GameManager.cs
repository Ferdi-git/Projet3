using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Combat combat;
    [SerializeField] private SOEventGridManager gridManager;
    [SerializeField] private SOEventState gameState;
    [SerializeField] private FloorManager floorManager;
    [SerializeField] private FloorListSo floorListSo;


    public int ActualFloorCount;
    public FloorEvent ActualEvent;
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
        if (ActualEvent == FloorEvent.NormalFight || ActualEvent == FloorEvent.BossFight)
        {
            combat.StartTurn();
        }
    }

    private void Start()
    {
        floorManager.GenerateFirstFloorList();
        ActualFloorCount = 0;
        FirstFloor();
        
    }
    private void FirstFloor ()
    {
        ActualEvent = floorListSo.list[0];
        StartEvent(ActualEvent);
    }
    private void NextFloor ()
    {
        ActualFloorCount++;
        ActualEvent = floorListSo.list[ActualFloorCount];
        StartEvent(ActualEvent);
    }

    private void CombateEnded ()
    {
        print("combat ended");
        NextFloor();
    }
    private void ShopingEnded ()
    {
        print("shop ended");
        NextFloor();
    }



    private void StartEvent (FloorEvent floorEvent)
    {
        if (floorEvent == FloorEvent.NormalFight)
        {
            gameState.InvokeStartCombat();
        }
        else if (floorEvent == FloorEvent.Shop)
        {
            gameState.InvokeStartShoping();
        }
        else if (floorEvent == FloorEvent.BossFight)
        {
            //gameState.Invoke boos fight 
            print("BossFight");
        }
    }
}
