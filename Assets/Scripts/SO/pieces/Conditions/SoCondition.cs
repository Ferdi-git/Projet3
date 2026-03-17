using System.Collections.Generic;
using UnityEngine;


public class  SoCondition : ScriptableObject
{
    public virtual bool Condition (Context context, List<int> exemple)
    {
        return false; 
    }
}




[CreateAssetMenu(fileName = "NewConditionRepetition", menuName = "Conditions/Repetition")]
public class SoConditionRepetition : SoCondition
{
    public override bool Condition (Context context, List<int> CombienDeRepetition)
    {
        if (context.NbrDeRepetition >= CombienDeRepetition[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
