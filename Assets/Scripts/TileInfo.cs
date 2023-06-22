using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public enum TileType {SEA, RIVER, MOUNTAIN, PLAINS, FLOODPLAIN, SWAMP};
    public TileType TerrainType;
    public enum SecondaryType {NONE, DESERT, FOREST, JUNGLEFOREST, HILLS };
    public SecondaryType AdditionalInfo;
    public bool IsWalkable;

    public bool IsStartTile;
    public bool HasVillager;
    public GameObject Village;
}
