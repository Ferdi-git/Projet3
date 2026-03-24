using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/Beta")]
public class SoEffet : ScriptableObject
{
    public virtual IEnumerator Effet (Context context,OutputPort port , List<int> exemple , int tour) // faudra retourner une action 
    {
        port.piecePlayed.PiecePlayedUp();

        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPiecePlayed(), PieceAnimations.TypeAnim.classic);
    }
    public virtual IEnumerator RepeatEffet(Context context, OutputPort port, List<int> exemple , int tour)
    {
        port.piecePlayed.RepeatedPieceUp();
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(port.piecePlayed.GetPieceRepeated(), PieceAnimations.TypeAnim.classic);
    }

}

