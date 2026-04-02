using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] int nbrOfChoice;
    [SerializeField] GameObject choiceLayout;
    [SerializeField] GameObject onePieceScreen;
    [SerializeField] GameObject onePiecePiece;
    [SerializeField] GameObject prefabPieceChoice;

    [SerializeField] SOEventGridManager eventGridManager;
    [SerializeField] SoPieces[] difPieces;

    List<GameObject> choices = new List<GameObject>();
    public GameObject lastGeneratedPiece = null; 

    public ShopManager shopManager;

    private void OnEnable()
    {
        eventGridManager.PiecePlaced += CheckIfPiecePlaced;
    }

    private void OnDisable()
    {
        eventGridManager.PiecePlaced -= CheckIfPiecePlaced;
    }

    [Button]
    public void GeneratePiece()
    {
        choiceLayout.SetActive(true);

        ClearChoice();
        for (int i = 0; i < nbrOfChoice ; i++)
        {
            var prefab = Instantiate(prefabPieceChoice, choiceLayout.transform);
            int randInt = Random.Range(0, difPieces.Length);
            SingleChoice script =  prefab.GetComponent<SingleChoice>();
            script.Initialize(difPieces[randInt]);
            script.choiceManager = this;
            prefab.GetComponent<Button>().onClick.AddListener(OpenPieceScreen);
            script.onePieceChoice = onePiecePiece;
            choices.Add(prefab);
        }
    }

    private void ClearChoice()
    {
        for (int i = 0; i < choices.Count; i++) { Destroy(choices[i]); }
        choices.Clear();
    }

    public void OpenPieceScreen()
    {
        choiceLayout.SetActive(false);
        onePieceScreen.SetActive(true);
    }

    public void ReopenLayout()
    {
        choiceLayout.SetActive(true);
        onePieceScreen.SetActive(false);

        DestroyOnePieceChildren();
    }

    private void DestroyOnePieceChildren()
    {

        var children = new List<Transform>();
        for (int i = 0; i < onePiecePiece.transform.childCount; i++)
        {

            children.Add(onePiecePiece.transform.GetChild(i));
        }

        for (int i = 0; i < children.Count; i++)
        {

            Destroy(children[i].gameObject);
        }
    }

    private void CheckIfPiecePlaced(GameObject go)
    {
        if(lastGeneratedPiece == null) return;
        if(go == lastGeneratedPiece)
        {
            lastGeneratedPiece = null;
            go.transform.SetParent(null);
            eventGridManager.InvokeAddBoardPiece(go);
            eventGridManager.InvokeTrySaveInventory();
            EndChoice();
        }
    }

    private void EndChoice()
    {
        onePieceScreen.SetActive(false);
        ClearChoice();
        shopManager.CloseShop();
    }

}
