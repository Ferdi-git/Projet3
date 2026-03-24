using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleDamage", menuName = "Effet/SimpleDamage")]
public class SoEffetSimpleDoDamage : SoEffet
{
    public override IEnumerator Effet(Context context,OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.PiecePlayedUp();
        Debug.Log("effet simple attack");
        port.DoDamage(amount[0]);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic);
    }
    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.RepeatedPieceUp();
        Debug.Log("effet répété simple attack");
        port.DoDamage(amount[0]);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPieceRepeated(), PieceAnimations.TypeAnim.repeat);
    }
}
