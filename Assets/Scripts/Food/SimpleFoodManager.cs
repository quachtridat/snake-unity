using System;
using Random = UnityEngine.Random;
using TPosition = UnityEngine.Vector2Int;

[Serializable]
public class SimpleFoodManager {
    public SimpleFood Food { get; protected set; }

    public SimpleFoodConfiguration FoodConfiguration;

    public SimpleFoodManager(SimpleFoodConfiguration config) {
        FoodConfiguration = config;
    }

    public SimpleFoodManager(SimpleFoodConfiguration config, SimpleFood food) : this(config) {
        Food = food;
    }

    public void InitializeFood(TPosition position, int score) {
        Food = new SimpleFood { Position = position, Score = score, Sprite = FoodConfiguration is null ? null : FoodConfiguration.Sprite };
    }

    public void SetFoodPosition(TPosition position) {
        Food.Position = position;
    }
}