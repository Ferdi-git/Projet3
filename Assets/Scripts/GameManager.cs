using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Combat combat;
    [SerializeField] private SOEventGridManager gridManager;
    public void ButtonPressed ()
    {
        // effet à l'appuie 
        gridManager?.InvokeActualiseBoard();
        combat.StartTurn(); 
    }
}
