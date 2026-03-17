using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "Effet/Shield")]
public class SoEffetShield : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> test)
    {
        port.GainShield(test[0]);
    }
}
