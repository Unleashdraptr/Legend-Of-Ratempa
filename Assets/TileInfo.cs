using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int TileID;
    public enum TileType {SEA, RIVER, MOUNTAIN, PLAINS, FLOODPLAIN, SWAMP};
    public TileType TerrainType;
    public enum SecondaryType {NONE, DESERT, FOREST, JUNGLEFOREST, HILLS };
    public SecondaryType AdditionalInfo;

    public bool IsStartTile;
    public bool IsCity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
