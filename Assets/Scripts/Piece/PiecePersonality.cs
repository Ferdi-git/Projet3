
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PiecePersonality : MonoBehaviour
{

    [SerializeField] private SoPieces soPieces;
    [SerializeField] private Transform[] posCases;
    [SerializeField] SOEventGridManager sOEventGridManager;

    public PiecePersonality[] surroundingPieces;
    public bool wasUsed = false;
    public string TEMPORARYSTRING;

    AudioSource audioSource;

    [SerializeField] private float glowIntensity = 2f;   // above 1 = triggers bloom
    [SerializeField] private float glowDuration = 0.3f;

    public SpriteRenderer[] spriteRenderers;

    private Color baseColor;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        sOEventGridManager.ResetGrid += ResetPiece;
    }

    private void OnDisable()
    {
        sOEventGridManager.ResetGrid -= ResetPiece;

    }


    public PieceContext GetContext()
    {
        wasUsed = true;
        PieceContext context  = new PieceContext();
        context.personality.Add(this);
        context.actions += TEMPORARYSTRING;
        return context;
    }

    private void ResetPiece()
    {
        wasUsed = false;
    }


    public void PlayAnimations(int number)
    {
        transform.DOScale(1.05f + 0.005f * number, 0.1f).OnComplete(() =>
        {
            audioSource.pitch = 0.2f + 0.05f * number;
            audioSource.Play();
            transform.DOScale(1f, 0.1f);
        });


        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].DOColor(new Color(baseColor.r * glowIntensity,
                                         baseColor.g * glowIntensity,
                                         baseColor.b * glowIntensity), glowDuration * 0.3f)
            .OnComplete(() =>
            {
                spriteRenderers[i].DOColor(baseColor, glowDuration);
            });
        }
    }



}


public class PieceContext
{
    public List<PiecePersonality> personality = new List<PiecePersonality>();
    public string actions = "";
    public ActionEffet effets;

}
