using System;
using UnityEngine;
[CreateAssetMenu]
public class SOEventFloor : ScriptableObject
{
    public event Action FirstFloorGeneration;

    public void InvokeFirstFloor()
    {
        FirstFloorGeneration?.Invoke();
    }
}
