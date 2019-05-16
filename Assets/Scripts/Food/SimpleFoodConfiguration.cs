using System;
using UnityEngine;

[Serializable]
public class SimpleFoodConfiguration {
    [SerializeField]
    [Tooltip("Default sprite for the food.")]
    private Sprite _sprite = null;

    public Sprite Sprite { get => _sprite; set => _sprite = value; }
}