using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConditionTest", menuName = "Conditions/Test")]
public class SoConditionTestPieceAutour : SoCondition
{
    public override bool Condition(Context context, List<int> CombienDeTour) //  ajouter context 
    {
        if (context.voisins.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
