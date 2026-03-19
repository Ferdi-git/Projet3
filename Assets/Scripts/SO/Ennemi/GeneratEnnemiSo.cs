using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnnemi", menuName = "Ennemi/BaseEnnemi")]
public class GeneratEnnemiSo : ScriptableObject
{
    public string Name;
    public List<EnnemiAttribut> Effets;
    public Sprite sprite; // pour l'instant un seul sprite par ennemi mais possibilité de changement de sprite entre etat idle, pris un coup et mort peut etre 
    [Range(0, 200)]
    public int Résistance= 100;
}

[Serializable]
public class EnnemiAttribut
{
    public SoEffet effet;
    public List<int> values;
}
