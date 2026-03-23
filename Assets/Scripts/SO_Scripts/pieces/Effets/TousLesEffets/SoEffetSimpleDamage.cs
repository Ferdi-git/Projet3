using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleDamage", menuName = "Effet/SimpleDamage")]
public class SoEffetSimpleDoDamage : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> amount)
    {
        Debug.Log("effet simple attack");
        port.DoDamage(amount[0]);
        port.EndEffet();
    }
    public override void RepeatEffet(Context context, OutputPort port, List<int> amount)
    {
        Debug.Log("effet répété simple attack");
        port.DoDamage(amount[0]);
        Debug.Log("Finished ");
        port.FinishedRepeatedEffect();
    }
}
