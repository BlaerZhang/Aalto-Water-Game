using System;
using UnityEngine;

    public class TileSprite : MonoBehaviour
    {
        public GameObject Sprite { get; private set; }

        public TileSprite(TileType type, Vector3 position)
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
                case TileType.Rock:
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
