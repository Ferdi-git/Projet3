using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleNbrDamageVisuel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI outlineText;
    Vector3 baseScale;


    public void Initialise(DataUIVisuel data)
    {
        mainText.color = data.textColor;
        mainText.text = data.nbr.ToString();
        outlineText.text = data.nbr.ToString();
        StartAnim();
    }


    private void StartAnim()
    {
        baseScale = transform.localScale;
        float basLocalY = transform.localPosition.y;

        transform.localScale = baseScale*3;

        transform.DOScale(baseScale, 0.3f);

        transform.DOLocalMoveY(transform.localPosition.y + 0.2f, 1.5f)
            .SetDelay(0.3f)
            .OnComplete(() => Destroy(gameObject));
    }

}



public class DataUIVisuel
{
    public int nbr;
    public Color textColor;
}
