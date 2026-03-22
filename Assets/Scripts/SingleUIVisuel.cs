using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleUIVisuel : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI outlineText;
    Vector3 baseScale;

    private void Start()
    {
        StartAnim();

    }

    public void Initialise(DataUIVisuel data)
    {
        image.sprite = data.sprite;
        mainText.text = data.nbr.ToString();
        outlineText.text = data.nbr.ToString();
        StartAnim();
    }


    private void StartAnim()
    {
        baseScale = transform.localScale;
        transform.localScale = baseScale*3;
        transform.DOScale(baseScale, 0.3f);
        //transform.DOScale(0, 2f);
        transform.DOMoveY(transform.position.y +0.2f ,1.5f).SetDelay(0.3f).OnComplete(()=> Destroy(gameObject));
    }

}

public class DataUIVisuel
{
    public Sprite sprite;
    public int nbr;
    public Color textColor;
}
