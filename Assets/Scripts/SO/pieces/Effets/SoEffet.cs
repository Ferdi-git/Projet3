using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/Beta")]
public class SoEffet : ScriptableObject
{
    public virtual void Effet (Context context,OutputPort port , List<int> exemple) // faudra retourner une action 
    {
        //appeler une fonction dans le port 
        //ex : 
        //port.AplyEnemyDamage(15);
        port.EndEffet();
    }
    public virtual void RepeatEffet(Context context, OutputPort port, List<int> exemple)
    {
        // lui n'aura pas de EndEffet ()
        port.FinishedRepeatedEffect();
    }

}

