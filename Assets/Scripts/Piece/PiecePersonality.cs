
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PiecePersonality : MonoBehaviour
{
    public SoPieces soPiece;
    //[SerializeField] private Transform[] posCases;
    [SerializeField] SOEventGridManager sOEventGridManager;

    [SerializeField] private Transform[] surroundingPoints;
    public bool wasUsed = false;

    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] private float glowIntensity = 2f;   // above 1 = triggers bloom
    [SerializeField] private float glowDuration = 0.25f;
    public SpriteRenderer[] spriteRenderers;

    [SerializeField, ColorUsage(true, true)] private Color repeatGlowColor = Color.white;



    public float healthPoint;
    public float shield;



    private void Start()
    {
        healthPoint = soPiece.healthPoint;
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
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);

        transform.DOScale(1.05f + 0.005f * number, 0.1f).OnComplete(() =>
        {
            int intClip = Mathf.Clamp(number, 0, audioClips.Length);
            audioSource.pitch = 1f;

            audioSource.clip = audioClips[intClip];
            //float randStartPitch = Random.Range(0.18f, 0.22f);
            //float randStartPitch = 0.2f;
            //audioSource.pitch = randStartPitch + 0.05f * number;
            audioSource.Play();

            transform.DOScale(1f, 0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        });

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Material mat = spriteRenderers[i].material;
            Color baseColor = mat.GetColor("_GlowColor");

            float intensityMultiplier = Mathf.Pow(2f, glowIntensity);
            Color glowColor = baseColor * intensityMultiplier;

            mat.DOColor(glowColor, "_GlowColor", glowDuration * 0.3f)
               .OnComplete(() =>
               {
                   mat.DOColor(baseColor, "_GlowColor", glowDuration);
               });
        }
    }


    public void PlayRepeatAnimations(int number, float delai)//c'est la combientieme a etre activé (pour son de + en + aigu )
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);

        transform.DOScale(1.05f + 0.005f * number, 0.1f).SetDelay(delai).OnComplete(() =>
        {
            //float randStartPitch = Random.Range(0.15f, 0.19f);
            //float randStartPitch = 0.2f;
            //audioSource.pitch = randStartPitch + 0.05f * number;
            audioSource.pitch =0.8f;

            int intClip = Mathf.Clamp(number, 0, audioClips.Length);
            audioSource.clip = audioClips[intClip];
            audioSource.Play();

            transform.DOScale(1f, 0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        });


        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Material mat = spriteRenderers[i].material;
            Color baseColor = mat.GetColor("_GlowColor");

            float intensityMultiplier = Mathf.Pow(2f, glowIntensity);
            Color glowColor = repeatGlowColor * intensityMultiplier; // different color

            mat.DOColor(glowColor, "_GlowColor", glowDuration * 0.3f).SetDelay(delai)
               .OnComplete(() =>
               {
                   mat.DOColor(baseColor, "_GlowColor", glowDuration); // still returns to original
               });
        }
    }



    public void TakeDamage(float dmg)
    {
        if(shield > 0)
        {
            float shieldToLose = shield - dmg;

            shieldToLose = Mathf.Clamp(shieldToLose, 0, shield);
            dmg -= shieldToLose;
            shield -= shieldToLose;
        }
        healthPoint  -= dmg;

        if(healthPoint <= 0)
        {
            DestroyPiece();
        }
    }

    public void Heal(float healPoint)
    {
        healthPoint += healPoint;
    }

    public void GetShields(float nbrShield)
    {
        shield += nbrShield;
    }

    public void DestroyPiece()
    {
        Destroy(gameObject);
    }
}
