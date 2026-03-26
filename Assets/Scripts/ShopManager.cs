using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] ChoiceManager shopM;
    [SerializeField] SOEventState eventState;

    private void OnEnable()
    {
        eventState.StartShoping += OpenShop;
    }

    private void OnDisable()
    {
        eventState.StartShoping -= OpenShop;

    }



    private void OpenShop()
    {
        grid.SetActive(false);
        shopM.gameObject.SetActive(true);
        shopM.GeneratePiece();
    }


    public void CloseShop()
    {
        grid.SetActive(true);
        shopM.gameObject.SetActive(false);
        eventState.InvokeEndOfShoping();
    }
}
