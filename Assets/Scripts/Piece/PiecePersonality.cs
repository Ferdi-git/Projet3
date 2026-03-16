
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PiecePersonality : MonoBehaviour
{
    public SoPieces soPieces;
    [SerializeField] private Transform[] posCases;
    [SerializeField] SOEventGridManager sOEventGridManager;

    [SerializeField] private Transform[] surroundingPoints;
    public bool wasUsed = false;
    public string TEMPORARYSTRING;

    AudioSource audioSource;

    [SerializeField] private float glowIntensity = 2f;   // above 1 = triggers bloom
    [SerializeField] private float glowDuration = 0.3f;
    public SpriteRenderer[] spriteRenderers;

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


    private void ResetPiece()
    {
        wasUsed = false;
    }

    public Transform[] GetSurroundingPoints()
    {
        return surroundingPoints;
    }

    public void PlayAnimations(int number)//c'est la combientieme a etre activé (pour son de + en + aigu )
    {
        transform.DOScale(1.05f + 0.005f * number, 0.1f).OnComplete(() =>
        {
            audioSource.pitch = 0.2f + 0.05f * number;
            audioSource.Play();
            transform.DOScale(1f, 0.1f);
        });

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            SpriteRenderer sr = spriteRenderers[i];
            Color captured = sr.color;              

            sr.DOColor(new Color(captured.r + glowIntensity,
                                 captured.g + glowIntensity,
                                 captured.b + glowIntensity), glowDuration * 0.3f)
              .OnComplete(() =>
              {
                  sr.DOColor(captured, glowDuration);
              });
        }
    }



}
