using UnityEngine;


public class  SoCondition : ScriptableObject
{
    public virtual bool Condition (Context context, int exemple)
    {
        return false; 
    }
}




[CreateAssetMenu(fileName = "NewConditionRepetition", menuName = "Conditions/Repetition")]
public class SoConditionRepetition : SoCondition
{
    public override bool Condition (Context context, int CombienDeRepetition)
    {
        if (context.NbrDeRepetition >= CombienDeRepetition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
