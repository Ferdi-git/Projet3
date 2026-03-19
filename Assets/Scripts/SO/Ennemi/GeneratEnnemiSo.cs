using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnnemi", menuName = "Ennemi/BaseEnnemi")]
public class GeneratEnnemiSo : ScriptableObject
{
    public string Name;
    public List<EnnemiAttribut> Effets;
    public Sprite sprite;
    [Range(0, 200)]
    public int Résistance= 100;
}

[Serializable]
public class EnnemiAttribut
{
    public SoEffet effet;
    public List<int> values;
}
