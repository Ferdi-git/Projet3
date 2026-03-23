using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetBeatTest")]
public class SoEffetTestDoDamage : SoEffet
{
    public override IEnumerator Effet(Context context,OutputPort port, List<int> test)
    {

        port.DoDamage(test[0]);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(2);
    }
    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount)
    {
        port.DoDamage(amount[0]);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(2);
    }
}
