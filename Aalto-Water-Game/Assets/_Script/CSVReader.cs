using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader
{
    public static Dictionary<Vector2Int, TileType> ReadCSV(int levelIndex, out int mapWidth, out int mapHeight)
    {
        Dictionary<Vector2Int, TileType> mapDict = new Dictionary<Vector2Int, TileType>();

        mapWidth = 0;
        mapHeight = 0;

        // Load the CSV file from Resources folder
        TextAsset csvFile = Resources.Load<TextAsset>($"Level CSV/level_csv_{levelIndex + 1}");
        if (csvFile == null)
        {
            Debug.LogError($"CSV file for level {levelIndex} not found.");
            return mapDict;
        }

        // Split the text into lines
        string[] splitLine = csvFile.text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        mapHeight = splitLine.Length;
        mapWidth = splitLine[0].Split(',').Length;

        for (int y = 0; y < mapHeight-1; y++)
        {
            // Split each line into grid cells
            string[] splitGrid = splitLine[y].Split(',');
            mapWidth = Math.Max(mapWidth, splitGrid.Length);
            for (int x = 0; x < splitGrid.Length; x++)
            {
                if (int.TryParse(splitGrid[x], out int tileTypeInt))
                {
                    int rotatedY = mapWidth - x - 1;
                    // Add to the dictionary
                    mapDict.Add(new Vector2Int(y, rotatedY), (TileType)tileTypeInt);
                }
            }
        }

        // avoiding uneven sizes is good because the coordinates are centered
        if (mapHeight % 2 == 0) mapHeight++;
        if (mapWidth % 2 == 0) mapWidth++;

        return mapDict;
    }
}
