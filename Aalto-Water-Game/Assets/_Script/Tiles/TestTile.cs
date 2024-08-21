using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestTile : MonoBehaviour, ITile
{
    public TileType Type
    {
        get => TileType.Water;
        set { }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public TileType ApplyRulesAndGetNewType(List<ITile> surroundingTiles)
    {
        throw new System.NotImplementedException();
    }
}
