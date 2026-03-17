using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/Beta")]
public class SoEffet : ScriptableObject
{
    public virtual void Effet (Context context,OutputPort port , int exemple) // faudra retourner une action 
    {
        //appeler une fonction dans le port 
        //ex : 
        //port.AplyEnemyDamage(15);
    }

}

