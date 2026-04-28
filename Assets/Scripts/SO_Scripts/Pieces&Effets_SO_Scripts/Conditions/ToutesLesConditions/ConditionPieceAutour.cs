using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "XPiecesAroundCondition", menuName = "Conditions/XPiecesAround")]
public class ConditionXPieceAutour : SoCondition
{
    public override bool Condition(ConditionOutput conditionOutput) //  ajouter context 
    {
        if (conditionOutput.context.voisins.Count > conditionOutput.variableList[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
