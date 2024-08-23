using System;
using UnityEngine;

namespace Assets._Script.Tiles
{
    public class TileSprite
    {
        public GameObject Sprite { get; private set; }

        public TileSprite(TileType type, Vector3Int position)
        {
            GameObject spriteObject;
            switch (type)
            {
                case TileType.Water:
                    spriteObject = null;
                    break;
                case TileType.Grass:
                    spriteObject = null;
                    break;
                case TileType.Dirt:
                    spriteObject = null;
                    break;
                case TileType.Sand:
                    spriteObject = null;
                    break;
                case TileType.SaltyWater:
                    spriteObject = null;
                    break;
                // Add cases for other tile types
                default:
                    throw new ArgumentException("Unknown tile type");
            }
            Sprite = Instantiate(spriteObject, position, Quaternion.identity);
        }
    }
}
