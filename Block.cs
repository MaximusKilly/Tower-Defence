using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
   [SerializeField] GameObject[] Towers = new GameObject[0];

   public Block exploredFrom; // From which Block we Explored this?
   public bool isExplored = false; 
   public bool isPlaceable = true; // can we place a turret?

    const int Size = 10; // Grid Size

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
        Mathf.RoundToInt(transform.position.x / Size),
         Mathf.RoundToInt(transform.position.z / Size)
        );
    }
    public int GetSize()
    {
        return Size;
    }
    private void OnMouseDown()
    {
       
        if (isPlaceable && !MaxReached && HaveResource())
        {
            MinusCost();
          var NewTower = Instantiate(Towers[0], transform.position, Quaternion.identity);
            NewTower.transform.parent = transform;
            isPlaceable = false;
        }
        else
        {

        }
      
    }

}
