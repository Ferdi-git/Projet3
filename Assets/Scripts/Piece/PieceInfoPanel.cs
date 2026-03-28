using DG.Tweening;
using TMPro;
using UnityEngine;

public class PieceInfoPanel : MonoBehaviour
{

    [SerializeField] PieceInfo pieceInfo;
    Canvas canvas;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject panelInfo;
    [SerializeField] float timeBeforeApearing = 0.15f;
    float baseScaleY;


    private void Awake()
    {
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
        text.text = pieceInfo.soPiece.description;
    }

    public void ShowPanel()
    {
        panelInfo.transform.DOKill();
        text.gameObject.SetActive (false);
        panelInfo.transform.localScale = new Vector3(panelInfo.transform.localScale.x , 0, panelInfo.transform.localScale.z) ;
        panelInfo.SetActive(true);
        panelInfo.transform.DOScaleY(baseScaleY,0.2f).SetDelay(timeBeforeApearing).OnComplete(() => text.gameObject.SetActive(true));

    }

    public void HidePanel()
    {
        panelInfo.transform.DOKill();
        text.gameObject.SetActive(false);
        panelInfo.transform.DOScaleY(0, 0.2f).OnComplete(() => panelInfo.SetActive(false));

    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

}
