using System;
using UnityEngine;
using TPosition = UnityEngine.Vector2Int;

[Serializable]
public class SimpleSnakeCell {
    public TPosition Position = TPosition.zero;
    public Sprite Sprite = null;
}