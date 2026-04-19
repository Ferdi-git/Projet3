using UnityEngine;

public class SinglePieceSquare : MonoBehaviour
{
    public ParticleSystem healParticule;
    public ParticleSystem shieldParticule;
    public SpriteRenderer spriteRenderer;
    public GameObject shieldGO;
    void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
