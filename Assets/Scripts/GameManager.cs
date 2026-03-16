using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Combat combat; 
    public void ButtonPressed ()
    {
        // effet à l'appuie 
        combat.StartTurn(); 
    }
}
