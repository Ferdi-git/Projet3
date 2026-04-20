using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageToEnnemyAndItself", menuName = "Effet/DamageToEnnemyAndItself")]

public class SoEffetDamageToEnnemyAndItself : SoEffet
{
    public override IEnumerator Effet(Context context, OutputPort port, List<int> amount, int tour)
    {
        port.piecePlayed.PiecePlayedUp();
        port.DoDamageToEnnemi(amount[0]);
        //port.PlayerTakeDamage(amount[1]);
        yield return port.thisBoardPiece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.atk, null);
    }
    public override IEnumerator RepeatEffet(Context context, OutputPort port, List<int> amount, int tour, BoardPiece declencheur)
    {
        port.piecePlayed.RepeatedPieceUp();
        port.DoDamageToEnnemi(amount[0]);
        //port.PlayerTakeDamage(amount[1]);
        yield return port.thisBoardPiece.pieceAnimation.PlayAnimations(port.piecePlayed.GetPieceRepeated(), PieceAnimations.TypeAnim.atk, declencheur);
    }
}
