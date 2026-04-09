
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PieceAnimations : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] private float glowIntensity = 2f;   // above 1 = triggers bloom
    [SerializeField] private float glowDuration = 0.25f;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    [Tooltip("Normal,Repeat,Atk,Defend,Heal")] 
    [SerializeField, ColorUsage(true, true)] private Color[] glowColors;

    [SerializeField] TrailPiece trailPiece;
    private SinglePieceSquare[] squares;
    [SerializeField] TextMeshPro textHealth;
    private BoardPiece boardPiece;
    [SerializeField] StatsEnnemi statEnnemy;


    private void Start()
    {
        boardPiece =  gameObject.GetComponent<PieceInfo>().currentBoardPiece;
        squares = gameObject.GetComponent<PieceInfo>().GetSelfPoints();
        for (int i = 0; i < squares.Length; i++)
        {
            spriteRenderers.Add(gameObject.GetComponent<PieceInfo>().GetSelfPoints()[i].spriteRenderer);
        }
        audioSource = GetComponent<AudioSource>();
        RefreshHealth();
    }


    public IEnumerator PlayAnimations(int number, TypeAnim typeAnim, BoardPiece declencheur)//c'est la combientieme a etre activé (pour son de + en + aigu )
    {
        RefreshHealth();
        Color glowColor = GetGlowColor(typeAnim);


        if (typeAnim == TypeAnim.takeDamage)
        {
            trailPiece.gameObject.SetActive(true);
            yield return StartCoroutine(trailPiece.CreateParaBole(statEnnemy.transform, transform, 1, 0.15f - 0.005f * number, glowColor)); ;
        }
        else
        {   
            if (declencheur != null)
            {
                trailPiece.gameObject.SetActive(true);
                yield return StartCoroutine(trailPiece.CreateParaBole(declencheur.pieceInfo.transform, transform, 1, 0.15f - 0.005f * number, glowColor));
            }

            if (typeAnim == TypeAnim.atk)
            {
                trailPiece.gameObject.SetActive(true);
                yield return StartCoroutine(trailPiece.CreateParaBole(transform, statEnnemy.transform, 1, 0.15f - 0.005f * number, glowColor)); ;
            }
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

        yield return StartCoroutine(Glow(glowColor, number));


    }





    private IEnumerator Glow(Color glowColor , int numberSpeed)
    {
        Color baseColor = glowColors[0];

        float glowIn = Mathf.Max(0.07f, glowDuration * 0.3f - 0.01f * numberSpeed);
        float glowOut = Mathf.Max(0.13f, glowDuration - 0.01f * numberSpeed);

        print(spriteRenderers.Count);

        for (int i = 0; i < spriteRenderers.Count; i++)
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

    private Color GetGlowColor(TypeAnim typeAnim)
    {
        Color glowColor = glowColors[0];
        float intensityMultiplier = Mathf.Pow(2f, glowIntensity);

        switch (typeAnim)
        {
            case TypeAnim.classic:
                glowColor = glowColors[0] * intensityMultiplier;
                break;

            case TypeAnim.repeat:
                glowColor = glowColors[1] * intensityMultiplier;
                break;

            case TypeAnim.atk:
                glowColor = glowColors[2] * intensityMultiplier;
                break;

            case TypeAnim.shield:
                foreach (SinglePieceSquare s in squares) s.shieldParticule.Play() ;
                glowColor = glowColors[3] * intensityMultiplier;
                break;

            case TypeAnim.heal:
                foreach (SinglePieceSquare s in squares) s.healParticule.Play();
                glowColor = glowColors[4] * intensityMultiplier;
                break;
            case TypeAnim.takeDamage:
                glowColor = glowColors[2] * intensityMultiplier;
                break;
        }
        return glowColor;
    }

    public void DestroyPieceAnim()
    {
        gameObject.GetComponent<PieceInfo>().Unfill();
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
        takeDamage,
    }


    public void PlayHealAnim()
    {

    }
    public void PlayShieldAnim()
    {

    }

    public void PlayTakeDamageAnim()
    {

    }
    public void PlayLoseShielddAnim()
    {

    }

    public void RefreshHealth()
    {
        textHealth.text  = boardPiece.healthPoint.ToString();
    }
}
