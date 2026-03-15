using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public string ListOfActions;
    // P(piece numero x),  D(degats x),  T(pour le type de degats), B(bouclier x) 
    private void Start()
    {
        ReadAction(ListOfActions);
    }
    public void ReadAction (string ListAction)
    {
        for (int i = 0; i < ListAction.Length; i++)
        {
            if (ListAction[i].ToString() == "D")
            {
                if (ListAction[i+1].ToString() !=  ",")
                {
                    //en cours 
                }
            }
            else
            {
                print("pas cool");
            }
        }
    }
}
