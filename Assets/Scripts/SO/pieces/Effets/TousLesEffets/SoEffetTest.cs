using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetBeatTest")]
public class SoEffetTest : SoEffet
{
    public override void Effet(Context context,OutputPort port, List<int> test)
    {

        port.TakeDamage(test[0]);
    }
}
