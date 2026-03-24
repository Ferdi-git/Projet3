
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PieceAnimations : MonoBehaviour
{
    public SoPieces soPiece;
    //[SerializeField] private Transform[] posCases;
    [SerializeField] SOEventGridManager sOEventGridManager;

    [SerializeField] private Transform[] surroundingPoints;
    public bool wasGridChecked = false;

    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] private float glowIntensity = 2f;   // above 1 = triggers bloom
    [SerializeField] private float glowDuration = 0.25f;
    public SpriteRenderer[] spriteRenderers;

    [Tooltip("Normal,Repeat,Atk,Defend,Heal")] 
    [SerializeField, ColorUsage(true, true)] private Color[] glowColors;




    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        sOEventGridManager.ResetPieceGridChecked += ResetPiece;
    }

    private void OnDisable()
    {
        sOEventGridManager.ResetPieceGridChecked -= ResetPiece;

    }


    private void ResetPiece()
    {
        wasGridChecked = false;
    }

    public Transform[] GetSurroundingPoints()
    {
        return surroundingPoints;
    }
    public Transform[] GetSelfPoints()
    {
        Transform[] transforms = new Transform[surroundingPoints.Length];
        for (int i = 0; i< spriteRenderers.Length; i++)
        {
            transforms[i] = spriteRenderers[i].transform;
        }
        return transforms;
    }

    public IEnumerator PlayAnimations(int number, TypeAnim typeAnim)//c'est la combientieme a etre activé (pour son de + en + aigu )
    {
        Color baseColor = glowColors[0];
        Color glowColor = baseColor;
        float intensityMultiplier = Mathf.Pow(2f, glowIntensity);

        switch (typeAnim)
        {
            case TypeAnim.classic:
                glowColor = baseColor * intensityMultiplier;
                break;

            case TypeAnim.repeat:
                glowColor = glowColors[1] * intensityMultiplier;
                break;

            case TypeAnim.atk:
                glowColor = glowColors[2] * intensityMultiplier;
                break;

            case TypeAnim.shield:
                glowColor = glowColors[3] * intensityMultiplier;
                break;

            case TypeAnim.heal:
                glowColor = glowColors[4] * intensityMultiplier;
                break;
        }

        transform.DOKill();

        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);

        transform.DOScale(1.05f + 0.005f * number, 0.1f).OnComplete(() =>
        {
            int intClip = Mathf.Clamp(number, 0, audioClips.Length-1);
            audioSource.pitch = 1f;

            audioSource.clip = audioClips[intClip];
            audioSource.Play();

            /*float randStartPitch = Random.Range(0.18f, 0.22f);
            //float randStartPitch = 0.2f;
            //audioSource.pitch = randStartPitch + 0.05f * number;*/

            transform.DOScale(1f, 0.1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        });

        float glowIn = Mathf.Max(0.07f, glowDuration * 0.3f - 0.01f * number);
        float glowOut = Mathf.Max(0.13f, glowDuration - 0.01f * number);




        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Material mat = spriteRenderers[i].material;

            mat.DOKill();
            float capturedIn = glowIn;
            float capturedOut = glowOut;


            mat.DOColor(glowColor, "_GlowColor", capturedIn)
               .OnComplete(() =>
               {
                   mat.DOColor(baseColor, "_GlowColor", capturedOut);
               });
        }
        yield return new WaitForSeconds(glowIn + glowOut);
    }


    public void DestroyPiece()
    {
        Destroy(gameObject);
    }

    public enum TypeAnim
    {
        classic,
        repeat,
        atk,
        shield,
        heal,
        failed,
    }
}
