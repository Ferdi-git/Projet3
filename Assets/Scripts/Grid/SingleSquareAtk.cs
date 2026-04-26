using TMPro;
using UnityEngine;

public class SingleSquareAtk : MonoBehaviour
{
    [SerializeField] Transform atkPoint;
    [SerializeField] TextMeshPro text;
    
    public Transform GetTransform() { return transform; }

    public void SetText(string texte) { text.text = texte; } 


}
