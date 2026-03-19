using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "Effet/Shield")]
public class SoEffetShield : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> amount)
    {
        port.GainShield(amount[0]);
        port.EndEffet();
    }
    public override void RepeatEffet(Context context, OutputPort port, List<int> amount)
    {
        port.GainShield(amount[0]);
        port.FinishedRepeatedEffect();
    }
}
