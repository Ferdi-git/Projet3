
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using static PieceAnimations;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PieceAnimations : MonoBehaviour
{
    AudioSource audioSource;
    private BoardPiece boardPiece;

    private SinglePieceSquare[] squares;

    [SerializeField] AudioClip[] audioClips;


    [Header("---Glow")]

    [SerializeField] private float glowIntensity = 2f;   // above 1 = triggers bloom
    [SerializeField] private float glowDuration = 0.25f;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    [Tooltip("Normal,Repeat,Atk,Defend,Heal")] 
    [SerializeField, ColorUsage(true, true)] private Color[] glowColors;


    [Header("---Health Display")]

    [SerializeField] TextMeshPro textHealth;
    [SerializeField] TextMeshPro textShield;


    [Header("---Events")]
    [SerializeField] SOEventPieceHealth eventPieceHealth;
    [SerializeField] SOEventTrail eventTrail;
    [SerializeField] SOEventVisualNumber eventVisualNumber;
    [SerializeField] SOEventVisuelEffect visualEffect;


    private void OnEnable()
    { 
        eventPieceHealth.PieceTakeDamage += PieceTakeDamage;
        eventPieceHealth.PieceShieldBreak += PieceLooseShield;
        //eventPieceHealth.PieceDie += PieceTakeDamage;
    }
    private void OnDisable()
    {
        eventPieceHealth.PieceTakeDamage -= PieceTakeDamage;
        eventPieceHealth.PieceShieldBreak -= PieceLooseShield;
        //eventPieceHealth.PieceDie -= PieceTakeDamage;
    }

    private void Start()
    {
        boardPiece =  gameObject.GetComponent<PieceInfo>().currentBoardPiece;
        squares = gameObject.GetComponent<PieceInfo>().GetSelfPoints();
        for (int i = 0; i < squares.Length; i++)
        {
            spriteRenderers.Add(gameObject.GetComponent<PieceInfo>().GetSelfPoints()[i].spriteRenderer);
        }
        audioSource = GetComponent<AudioSource>();
        RefreshHealth(boardPiece);
    }


    public IEnumerator PlayAnimations(int number, TypeAnim typeAnim, BoardPiece declencheur)//c'est la combientieme a etre activé (pour son de + en + aigu )
    {
        Color glowColor = GetGlowColor(typeAnim);

        yield return Parabole(typeAnim, glowColor, number, declencheur);

        EffetPiece(typeAnim);

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

        yield return Glow(glowColor, number);


    }

    private IEnumerator Glow(Color glowColor , int numberSpeed)
    {
        Color baseColor = glowColors[0];

        float glowIn = Mathf.Max(0.07f, glowDuration * 0.3f - 0.01f * numberSpeed);
        float glowOut = Mathf.Max(0.13f, glowDuration - 0.01f * numberSpeed);


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
                glowColor = glowColors[3] * intensityMultiplier;
                break;

            case TypeAnim.heal:
                glowColor = glowColors[4] * intensityMultiplier;
                break;
            case TypeAnim.takeDamage:
                glowColor = glowColors[2] * intensityMultiplier;
                break;
            case TypeAnim.loseShield:
                glowColor = glowColors[2] * intensityMultiplier;
                break;
            case TypeAnim.generateMana:
                glowColor = glowColors[6] * intensityMultiplier;
                break;
        }
        return glowColor;
    }

    private IEnumerator Parabole(TypeAnim typeAnim, Color glowColor,int number, BoardPiece declencheur)
    {
        if (typeAnim == TypeAnim.takeDamage)
        {
            bool ended = false;
            Action trailEvent = () => ended = true;

            visualEffect.InvokeEffectEnemyDealAtk(new VisuelAttakData()
            {
                posAttacker = transform.position,
                eventEndVisuel = trailEvent,
            });
            yield return new WaitUntil(() => ended);
        }
        else
        {
            Color repeatColor = glowColors[1] * Mathf.Pow(2f, glowIntensity);

            if (declencheur != null && typeAnim == TypeAnim.atk)
            {
                bool ended = false;
                Action trailEvent = () => ended = true;
                eventTrail.InvokeCreateTrail(new EventTrailData()
                {
                    pos1 = declencheur.pieceInfo.transform.position,
                    pos2 = transform.position,
                    height = 1,
                    trailTime = 0.15f - 0.005f * number,
                    glowColor = repeatColor,
                    eventEndTrail = trailEvent,
                });
                yield return new WaitUntil(() => ended);
            }
            else if (declencheur != null)
            {
                bool ended = false;
                Action trailEvent = () => ended = true;
                eventTrail.InvokeCreateTrail(new EventTrailData()
                {
                    pos1 = declencheur.pieceInfo.transform.position,
                    pos2 = transform.position,
                    height = 1,
                    trailTime = 0.15f - 0.005f * number,
                    glowColor = glowColor,
                    eventEndTrail = trailEvent,
                });
                yield return new WaitUntil(() => ended);
            }


            if (typeAnim == TypeAnim.atk)
            {
                bool ended = false;
                Action trailEvent = () => ended = true;
                visualEffect.InvokeEffectAtkEnemy(new VisuelAttakData()
                {
                    posAttacker = transform.position,
                    eventEndVisuel = trailEvent,
                });
                yield return new WaitUntil(() => ended);

            }

            if (typeAnim == TypeAnim.heal || typeAnim == TypeAnim.shield || typeAnim == TypeAnim.loseShield) RefreshHealth(null);
        }
    }

    private void EffetPiece(TypeAnim typeAnim)
    {
        switch (typeAnim)
        {
            case TypeAnim.classic:

                break;

            case TypeAnim.repeat:

                break;

            case TypeAnim.atk:

                break;

            case TypeAnim.shield:
                PlayShieldAnim();
                break;

            case TypeAnim.heal:
                PlayHealAnim();
                break;
            case TypeAnim.takeDamage:
                break;
            case TypeAnim.loseShield:
                break;
            case TypeAnim.generateMana: 
                break;
        }
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
        loseShield,
        generateMana,
    }


    public void PlayHealAnim()
    {
        foreach (SinglePieceSquare s in squares) s.healParticule.Play();

    }
    public void PlayShieldAnim()
    {

        foreach (SinglePieceSquare s in squares)
        {
            s.shieldGO.transform.localScale = Vector3.zero;
            s.shieldGO.transform.DOScale(Vector3.one,0.2f).SetEase(Ease.InOutSine);

            //s.shieldParticule.Play();
        }

    }

    public void PieceTakeDamage(BoardPiece piece, int nbr)
    {
        if (piece != boardPiece) return;

        EventVisualNbrData visualNbrData = new EventVisualNbrData();
        visualNbrData.nbr = nbr;
        visualNbrData.color = Color.red;
        visualNbrData.isPositive = false;

        float randX = UnityEngine.Random.Range(0f, 1f);
        visualNbrData.spawnPoint = new Vector2(transform.position.x + randX, transform.position.y + randX);

        eventVisualNumber.InvokeCreateVisualNumber(visualNbrData);
        RefreshHealth(piece);
    }

    public void PieceLooseShield(BoardPiece piece, int nbr)
    {
        if (piece != boardPiece) return;

        EventVisualNbrData visualNbrData = new EventVisualNbrData();
        visualNbrData.nbr = nbr;
        visualNbrData.color = Color.cyan;
        visualNbrData.isPositive = false;
        float randX = UnityEngine.Random.Range(0f, 1f);
        visualNbrData.spawnPoint = new Vector2(transform.position.x + randX, transform.position.y+ randX+0.75f);

        eventVisualNumber.InvokeCreateVisualNumber(visualNbrData);
        RefreshHealth(piece);
    }

    public void RefreshHealth(BoardPiece piece)
    {   
        textHealth.text  = boardPiece.healthPoint.ToString();
        textShield.gameObject.SetActive(boardPiece.shield > 0);
        if (boardPiece.shield <= 0 && int.Parse(textShield.text) > 0) foreach (SinglePieceSquare s in squares) s.shieldGO.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutSine);


        textShield.text = boardPiece.shield.ToString();

    }
}
