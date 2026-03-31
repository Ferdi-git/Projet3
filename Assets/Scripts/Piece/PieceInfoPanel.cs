using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class PieceInfoPanel : MonoBehaviour
{

    [SerializeField] PieceInfo pieceInfo;
    Canvas canvas;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject panelInfo;
    [SerializeField] float timeBeforeApearing = 0.15f;
    float baseScaleY;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
    private void Start()
    {
        baseScaleY = panelInfo.transform.localScale.y;
        HidePanel();
        Initialyse();
    }

    public void Initialyse()
    {
        text.gameObject.SetActive(false);
        RefreshPanel();
    }

    public void ShowPanel()
    {
        transform.rotation = Camera.main.transform.rotation;
        RefreshPanel();
        panelInfo.transform.DOKill();
        text.gameObject.SetActive (false);
        panelInfo.transform.localScale = new Vector3(panelInfo.transform.localScale.x , 0, panelInfo.transform.localScale.z) ;
        panelInfo.SetActive(true);
        panelInfo.transform.DOScaleY(baseScaleY,0.2f).SetEase(Ease.InOutSine).SetDelay(timeBeforeApearing).OnComplete(() => {
            audioSource.pitch = Random.Range(1,1.1f);
            audioSource.Play();
            text.gameObject.SetActive(true);

        }
        );

    }

    public void HidePanel()
    {
        panelInfo.transform.DOKill();
        text.gameObject.SetActive(false);
        panelInfo.transform.DOScaleY(0, 0.2f).OnComplete(() => panelInfo.SetActive(false));

    }


    private void RefreshPanel()
    {
        text.text = $"HP : {pieceInfo.currentBoardPiece.healthPoint}\nShield : {pieceInfo.currentBoardPiece.shield}\n{pieceInfo.soPiece.description}";
    }

}
