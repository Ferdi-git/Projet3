using UnityEngine;

public class SoEffet : ScriptableObject
{
    public virtual bool Effet (Context context, int exemple) // faudra retourner une action 
    {
        return false;
    }

}


[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/Test")]
public class SoEffetTest : SoEffet
{
    public override bool Effet(Context context, int CombienDeTour) //  ajouter context 
    {
        if (context.Tour >= CombienDeTour)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
