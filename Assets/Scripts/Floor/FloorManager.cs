using System;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{

    public int BoosToutLescombienDeTour;

    public List<FloorStep> CustomFloors = new List<FloorStep>();
    public void GenerateFloorList ()
    {

    }


}
[Serializable]
public class FloorStep
{
    public FloorEvent Evenement;
    public int NumeroDuPalier;
}
public enum FloorEvent
{
    NormalFight,
    BossFight,
    Shop,
    Heal
}
