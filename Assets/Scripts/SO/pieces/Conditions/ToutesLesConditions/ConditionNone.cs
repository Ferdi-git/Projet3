using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoneCondition", menuName = "Conditions/None")]
public class ConditionNone : SoCondition
{
    public override bool Condition(Context context, List<int> CombienDeTour) //  ajouter context 
    {
        return true;
    }
}
