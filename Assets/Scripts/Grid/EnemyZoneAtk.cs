using System.Collections.Generic;
using UnityEngine;

public class EnemyZoneAtk : MonoBehaviour
{
    public List<Transform> listPoints;
    private List<GridSlot> listSlots;


    //private void Start()
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {

    //        listPoints.Add(transform.GetChild(i));

    //    }
    //}
  
    public bool CheckIfCanBePlaced()
    {
        listSlots = new List<GridSlot>();
        for (int i = 0; i < listPoints.Count; i++)
        {
            bool foundSlot = false;

            foreach (var hit in Physics2D.OverlapPointAll(listPoints[i].transform.position))
            {
                GridSlot slot = hit.GetComponent<GridSlot>();

                if(slot != null)
                {
                    listSlots.Add(slot);
                    foundSlot = true;
                }
            }

            if(!foundSlot)
            {
                return false;
            }

        }

        return true;
    }


    public void SetAtk()
    {
        for(int i = 0; i < listSlots.Count; i++)
        {
            listSlots[i].GetSelected();
        }
    }

    public void RemoveAtk()
    {
        for (int i = 0; i < listSlots.Count; i++)
        {
            listSlots[i].isAttacked = false;
        }
    }

}
