using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetBeatTest")]
public class SoEffetTestDoDamage : SoEffet
{
    public override IEnumerator Effet(Context context,OutputPort port, List<int> test, int tour)
    {
        port.piecePlayed.PiecePlayedUp();
        port.DoDamage(test[0]);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic);
    }
    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.RepeatedPieceUp();
        port.DoDamage(amount[0]);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPieceRepeated(), PieceAnimations.TypeAnim.classic);
    }
}
