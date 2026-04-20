using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsSurroundedCondition", menuName = "Conditions/IsSurrounded")]

public class ConditionIsCompetlySurrounded : SoCondition
{
    public override bool Condition(ConditionOutput conditionOutput)
    {
        if(conditionOutput.context.NbrCaseOccupé == conditionOutput.variableList[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
