using System;
using UnityEngine;
using TPosition = UnityEngine.Vector2Int;

[Serializable]
public class SimpleFood {
    public TPosition Position = TPosition.zero;
    public Sprite Sprite = null;
    public int Score = 0;
}