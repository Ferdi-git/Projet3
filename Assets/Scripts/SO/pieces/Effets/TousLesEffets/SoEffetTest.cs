using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEffetTest", menuName = "Effet/effetBeatTest")]
public class SoEffetTest : SoEffet
{
    public override PieceAction Effet(Context context, int test) //  ajouter context 
    {
        PieceAction action = new PieceAction();
        action.DamageToEnnemi = test;
        return action;
    }
}
