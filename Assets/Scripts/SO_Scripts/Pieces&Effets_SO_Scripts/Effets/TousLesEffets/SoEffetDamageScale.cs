using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageScaleEffet", menuName = "Effet/DamageScale")]
public class SoEffetDamageScale : SoEffet
{
    public override IEnumerator Effet(Context context, OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.PiecePlayedUp();
        port.thisBoardPiece.pieceInfo.soPiece.TempEffectValues[1] += amount[0];
        port.DoDamageToEnnemi(port.thisBoardPiece.pieceInfo.soPiece.TempEffectValues[1]);
        yield return port.thisBoardPiece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.atk, null);
        context.NbrDeRepetition += 1;
    }
    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount, int tour, BoardPiece declencheur)
    {
        port.piecePlayed.RepeatedPieceUp();
        port.thisBoardPiece.pieceInfo.soPiece.TempEffectValues[1] += amount[0];
        port.DoDamageToEnnemi(port.thisBoardPiece.pieceInfo.soPiece.TempEffectValues[1]);
        yield return port.thisBoardPiece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPieceRepeated(), PieceAnimations.TypeAnim.atk, declencheur);
        context.NbrDeRepetition += 1;
    }
}
