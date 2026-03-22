using Sirenix.OdinInspector;
using UnityEngine;

public class VisualSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefabVisuel;
    [Button]
    public void CreateVisual()
    {
        float randFloat = Random.Range(0,4.5f) ;
        float randFloat2 = Random.Range(0,2f) ;
        Vector2 pos = new Vector2(transform.position.x + randFloat, transform.position.y + randFloat2);

        GameObject newSingle = Instantiate(prefabVisuel , pos, transform.rotation , transform);
        
        //newSingle.GetComponent<SingleUIVisuel>().Initialise();    
    }


}
