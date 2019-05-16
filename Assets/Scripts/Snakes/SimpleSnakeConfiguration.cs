using System;
using UnityEngine;

[Serializable]
public class SimpleSnakeConfiguration {
    [SerializeField]
    [Tooltip("Default sprite for the snake's body.")]
    private Sprite _sprite = null;

    public Sprite Sprite { get => _sprite; set => _sprite = value; }
}