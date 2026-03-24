using Sirenix.OdinInspector;
using UnityEngine;

public class VisualDamageNbrSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefabVisuel;
    [SerializeField] Transform spawnPoint;

    [Button]
    public void CreateVisual()
    {
        float randFloat = Random.Range(0,4.5f) ;
        float randFloat2 = Random.Range(0,2f) ;
        Vector2 pos = new Vector2(spawnPoint.position.x + randFloat, spawnPoint.position.y + randFloat2);

        GameObject newSingle = Instantiate(prefabVisuel, spawnPoint.position, transform.rotation, transform);


        newSingle.transform.localPosition += new Vector3(randFloat, randFloat2, 0f);


        DataUIVisuel dataUIVisuel = new DataUIVisuel();
        dataUIVisuel.textColor = Random.ColorHSV();
        dataUIVisuel.nbr = Random.Range(10, 1000);


        newSingle.GetComponent<SingleNbrDamageVisuel>().Initialise(dataUIVisuel);    
    }



}
