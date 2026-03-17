using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConditionTest", menuName = "Conditions/Test")]
public class SoConditionTest : SoCondition
{
    public override bool Condition(Context context, List<int> CombienDeTour) //  ajouter context 
    {
        if (context.Tour >= CombienDeTour[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
