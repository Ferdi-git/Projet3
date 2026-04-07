using UnityEngine;

public class SinglePieceSquare : MonoBehaviour
{
    public ParticleSystem healParticule;
    public ParticleSystem shieldParticule;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
