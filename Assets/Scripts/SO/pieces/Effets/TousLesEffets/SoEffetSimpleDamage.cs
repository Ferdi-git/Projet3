using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleDamage", menuName = "Effet/SimpleDamage")]
public class SoEffetSimpleDoDamage : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> test)
    {
        port.DoDamage(test[0]);
    }
}
