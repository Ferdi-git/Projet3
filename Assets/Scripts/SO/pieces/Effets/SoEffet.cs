using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/Beta")]
public class SoEffet : ScriptableObject
{
    public virtual PieceAction Effet (Context context, int exemple) // faudra retourner une action 
    {
        PieceAction action = new PieceAction();
        return action;
    }

}
[Serializable]
public class PieceAction
{
    public int DamageToEnnemi;
    public int DamageToMe;
    public int AmountHeal;
    public int AmountShieldGained;
    public int AmountShieldLost;
    public bool RepeatArround; 

}

[Serializable]
[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/Test")]
public class SoEffetTestDegat : SoEffet
{
    public override PieceAction Effet(Context context, int Damage) //  ajouter context 
    {
        PieceAction action = new PieceAction();
        action.DamageToEnnemi = Damage;
        return action;
    }
}
