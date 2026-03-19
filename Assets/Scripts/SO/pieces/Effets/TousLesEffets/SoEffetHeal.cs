using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Effet/Heal")]
public class SoEffetHeal : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> amount)
    {
        port.Heal(amount[0]);
        port.EndEffet();
    }

    public override void RepeatEffet(Context context, OutputPort port, List<int> amount)
    {
        port.Heal(amount[0]);
        port.FinishedRepeatedEffect();
    }
}
