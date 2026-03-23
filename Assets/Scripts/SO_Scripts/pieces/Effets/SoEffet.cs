using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/Beta")]
public class SoEffet : ScriptableObject
{
    public virtual IEnumerator Effet (Context context,OutputPort port , List<int> exemple) // faudra retourner une action 
    {
        //appeler une fonction dans le port 
        //ex : 
        //port.AplyEnemyDamage(15);
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(2);
        //port.EndEffet();
    }
    public virtual IEnumerator RepeatEffet(Context context, OutputPort port, List<int> exemple)
    {
        // lui n'aura pas de EndEffet ()
        yield return port.thisBoardPiece.piecePersonality.PlayAnimations(2);
        port.FinishedRepeatedEffect();
    }

}

