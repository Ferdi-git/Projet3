using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Effet/Heal")]
public class SoEffetHeal : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> test)
    {
        port.Heal(test[0]);
    }
}
