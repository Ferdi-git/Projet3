using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetBeatTest")]
public class SoEffetTestDoDamage : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> test)
    {

        port.DoDamage(test[0]);
        port.EndEffet();
    }
    public override void RepeatEffet(Context context, OutputPort port, List<int> amount)
    {
        port.DoDamage(amount[0]);
        Debug.Log("Finished ");
        port.FinishedRepeatedEffect();
    }
}
