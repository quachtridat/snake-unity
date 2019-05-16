using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[Serializable]
public class SimpleSnakeGameManager {
    #region Fields

    public Configuration GameConfiguration;

    [SerializeField]
    protected SimpleSnakeManager snakeManager;

    [SerializeField]
    protected SimpleFoodManager foodManager;

    #endregion

    #region Properties

    public SimpleSnakeManager SnakeManager { get => snakeManager; protected set => snakeManager = value; }
    public SimpleFoodManager FoodManager { get => foodManager; protected set => foodManager = value; }

    #endregion

    #region Main Operations

    public void Start() {
        var rect = GameConfiguration.PlayfieldRect;
        var initLength = GameConfiguration.InitialSnakeLength;
        var initDir = GameConfiguration.InitialDirection.InvertY();
        var snakeSpeed = GameConfiguration.SnakeSpeed;
        var snakeRect = new RectInt(rect.xMin + initLength, rect.yMin + initLength, rect.width - (initLength << 1), rect.height - (initLength << 1));
        var foodScore = GameConfiguration.FoodScore;

        snakeManager.InitializeSnake(Vector2IntUtils.GetRandomIn(snakeRect), initLength, initDir, snakeSpeed);
        foodManager.InitializeFood(Vector2IntUtils.GetRandomIn(rect), foodScore);
    }

    /// <summary>
    /// Makes the snake move.
    /// Awards score if food is eaten.
    /// Game is over if the snake bites itself.
    /// </summary>
    public States Update() {
        States result = States.None;

        snakeManager?.SnakeMove();

        if (snakeManager.SelfCollided()) {
            snakeManager?.StopSnake();
            return result | States.Loss;
        }

        if (TeleportSnakeOffscreen() && !GameConfiguration.TeleportOffscreen) return result | States.Loss;

        if (FoodEaten()) {
            snakeManager?.IncreaseScore(foodManager.Food.Score);
            snakeManager?.AdoptHead(snakeManager.Snake.Head.Position); // the current head position is also the food position
            foodManager?.SetFoodPosition(Vector2IntUtils.GetRandomIn(GameConfiguration.PlayfieldRect));
            result |= States.FoodEaten;
        }

        return result | States.Normal;
    }

    /// <summary>
    /// Pause the game by stopping the snake.
    /// </summary>
    public void Pause() {
        snakeManager.ForceDirection(MovingDirection.None);
    }

    #endregion

    /// <summary>
    /// Checks if the food is eaten.
    /// </summary>
    /// <returns></returns>
    protected bool FoodEaten() {
        return snakeManager.Snake.Head.Position == FoodManager.Food.Position;
    }

    /// <summary>
    /// Checks if the snake is offscreen and teleports it if it is.
    /// </summary>
    /// <returns>True if the snake is offscreen.</returns>
    protected bool TeleportSnakeOffscreen() {
        var headPos = snakeManager.Snake.Head.Position;
        var playRect = GameConfiguration.PlayfieldRect;
        if (headPos.x < playRect.xMin) {
            snakeManager.SetSnakeHeadPosition(new Vector2Int(playRect.xMax, headPos.y));
            return true;
        }
        if (headPos.x > playRect.xMax) {
            snakeManager.SetSnakeHeadPosition(new Vector2Int(playRect.xMin, headPos.y));
            return true;
        }
        if (headPos.y < playRect.yMin) {
            snakeManager.SetSnakeHeadPosition(new Vector2Int(headPos.x, playRect.yMax));
            return true;
        }
        if (headPos.y > playRect.yMax) {
            snakeManager.SetSnakeHeadPosition(new Vector2Int(headPos.x, playRect.yMin));
            return true;
        }
        return false;
    }

    #region Input

    /// <summary>
    /// Gives a specified <see cref="IEnumerable{KeyCode}"/> as input to the manager to process.
    /// </summary>
    /// <param name="keyCodes"></param>
    public void InputKeys(IEnumerable<KeyCode> keyCodes) {
        MovingDirection d = MovingDirection.FromKey(keyCodes.FirstOrDefault());
        InputDirection(d);
    }

    /// <summary>
    /// Give a specified <see cref="MovingDirection"/> as input to the manager to process.
    /// </summary>
    /// <param name="d"></param>
    public void InputDirection(MovingDirection d) {
        if (SnakeManager.IsValidDirection(d)) SnakeManager.QueueDirection(d);
    }

    #endregion

    [Serializable]
    public struct Configuration {
        [Header("General")] // General Settings:

        [Tooltip("Whether the snake should be teleported back to the playfield instead of being killed when it goes off-screen.")]
        public bool TeleportOffscreen;

        [Header("Snake")] // Snake Settings:

        [Tooltip("Initial length of the snake.")]
        [Range(1, 4)]
        public int InitialSnakeLength;

        [Tooltip("Intial heading direction of the snake.")]
        public MovingDirection InitialDirection;

        [Tooltip("How fast the snake moves.")]
        public float SnakeSpeed;

        [Header("Food")] // Food Settings:

        public int FoodScore;

        [Header("Misc.")] // Miscellaneous Settings:

        [HideInInspector]
        public RectInt PlayfieldRect;

        public static KeyCode[] RecognizableKeyCodes = {
            KeyCode.UpArrow,
            KeyCode.LeftArrow,
            KeyCode.DownArrow,
            KeyCode.RightArrow
        };
    }

    [Flags]
    public enum States {
        None = 0,
        Normal = 1 << 0,
        FoodEaten = 1 << 1,
        SelfCollided = 1 << 2,
        Grown = FoodEaten,
        Loss = SelfCollided
    }
}