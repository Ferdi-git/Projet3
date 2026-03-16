using UnityEngine;

[CreateAssetMenu(fileName = "NewConditionTest", menuName = "Conditions/Test")]
public class SoConditionTest : SoCondition
{
    public override bool Condition(Context context, int CombienDeTour) //  ajouter context 
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
