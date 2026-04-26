using DG.Tweening;
using UnityEngine;

public class RoadMapVisualizer : MonoBehaviour
{
    [SerializeField] FloorListSo listFloor;
    [SerializeField] SOEventFloor eventFloor;
    [SerializeField] SpriteRenderer[] spriteRenderers;
    [SerializeField] Sprite EnemySprite;
    [SerializeField] Sprite BossSprite;
    [SerializeField] Sprite ShopSprite;
    [SerializeField] Sprite HealSprite;

    private int currentFloor = 0;

    private void OnEnable()
    {
        eventFloor.NextFloor += NextFloor;
        eventFloor.FirstFloorGeneration += Initialized;
    }

    private void OnDisable()
    {
        eventFloor.NextFloor -= NextFloor;
        eventFloor.FirstFloorGeneration -= Initialized;

    }

    private void Initialized()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = GetCorrespondingSprite(listFloor.list[i]) ;
        }

    }


    private void NextFloor()
    {
        currentFloor++;
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            int index = i;

            if (index == 0)
            {
                Vector3 oldPos = spriteRenderers[index].transform.position;

                spriteRenderers[index].transform
                    .DOMoveY(spriteRenderers[i].transform.position.y -0.5f, 0.7f)
                    .OnComplete(() =>
                    {
                        spriteRenderers[index].transform.position = oldPos;
                        spriteRenderers[index].sprite = GetCorrespondingSprite(listFloor.list[index + currentFloor]);
                    });
            }
            else
            {
                Vector3 oldPos = spriteRenderers[index].transform.position;

                spriteRenderers[index].transform
                    .DOMoveY(spriteRenderers[index - 1].transform.position.y, 0.7f)
                    .OnComplete(() =>
                    {
                        spriteRenderers[index].transform.position = oldPos;
                        spriteRenderers[index].sprite = GetCorrespondingSprite(listFloor.list[index + currentFloor]);
                    });
            }
        }
    }



    private Sprite GetCorrespondingSprite(FloorEvent floorEvent)
    {
        switch (floorEvent)
        {
            case FloorEvent.Heal:
                return HealSprite;
            case FloorEvent.NormalFight:
                return EnemySprite;
            case FloorEvent.BossFight:
                return BossSprite;
            case FloorEvent.Shop:
                return ShopSprite;
            
        }
        return null;

    }
}
