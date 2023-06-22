using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerControls : MonoBehaviour
{
    public LayerMask RaycastLayer;
    Camera cam;
    public GameManager manager;
    public Transform DestinationTile;
    bool InMovement;
    public float speed;
    Vector3 Difference;
    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
        GameManager.Pause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Pause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 25, RaycastLayer))
                {
                    if (hit.transform.GetComponent<TileInfo>().IsWalkable)
                    {
                        InMovement = true;
                        DestinationTile = hit.transform;
                        Difference = DestinationTile.position - transform.position;
                        CheckTile(DestinationTile.GetComponent<TileInfo>());
                    }
                }
            }
            if (Vector2.Distance(new(transform.position.x, transform.position.z), new(DestinationTile.position.x, DestinationTile.position.z)) > 0.1f && InMovement)
            {
                transform.Translate(new(0, -Difference.x * speed * Time.deltaTime, Difference.z * speed * Time.deltaTime));
            }
            else
            {
                transform.position = DestinationTile.position;
                InMovement = false;
            }
        }
    }

    public bool CheckTile(TileInfo CurrentTile)
    {
        if(CurrentTile.HasVillager == true)
        {
            manager.GetComponent<DialogueManager>().StartDialogue(CurrentTile.Village);
            manager.GetComponent<DialogueManager>().IsVillager = true;
            return true;
        }
        else
            return false;
    }
}
