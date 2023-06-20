using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public LayerMask RaycastLayer;
    Camera cam;
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 25, RaycastLayer))
            {
                if (hit.transform.GetComponent<TileInfo>().IsWalkable)
                {
                    transform.position = hit.transform.position;
                    if (CheckTile(hit.transform.GetComponent<TileInfo>()))
                    {
                        manager.VillagerNum -= 1;
                        manager.CheckVillagers();
                    }
                }
            }
        }
    }

    public bool CheckTile(TileInfo CurrentTile)
    {
        if(CurrentTile.HasVillager == true)
        {
            Destroy(CurrentTile.transform.GetChild(0));
            return true;
        }
        else
            return false;
    }
}
