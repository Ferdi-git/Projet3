using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConditionTest", menuName = "Conditions/Test")]
public class SoConditionPieceAutour : SoCondition
{
    public override bool Condition(ConditionOutput conditionOutput) //  ajouter context 
    {
        if (conditionOutput.context.voisins.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
