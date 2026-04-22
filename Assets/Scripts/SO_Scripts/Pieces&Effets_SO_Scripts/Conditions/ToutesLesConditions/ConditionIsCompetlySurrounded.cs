using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsCompletlySurroundedCondition", menuName = "Conditions/IsCompletlySurrounded")]

public class ConditionIsCompetlySurrounded : SoCondition
{
    public override bool Condition(ConditionOutput conditionOutput)
    {
        if(conditionOutput.context.NbrCaseOccupe == 0 && conditionOutput.context.NbrCaseLibre == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
