using TMPro;
using UnityEngine;

public class PieceInfoPanel : MonoBehaviour
{

    [SerializeField] PieceInfo pieceInfo;
    Canvas canvas;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject panelInfo;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
    private void Start()
    {
        HidePanel();
        Initialyse();
    }

    public void Initialyse()
    {
        text.text = pieceInfo.soPiece.description;
    }

    public void ShowPanel()
    {
        panelInfo.SetActive(true);
    }

    public void HidePanel()
    {
        panelInfo.SetActive(false);

    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

}
