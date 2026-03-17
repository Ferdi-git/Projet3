using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetBeatTest")]
public class SoEffetTest : SoEffet
{
    public override void Effet(Context context,OutputPort port, int test)
    {
        port.Heal(test);
    }
}
