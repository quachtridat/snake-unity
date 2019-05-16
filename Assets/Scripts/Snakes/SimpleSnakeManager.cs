using System;
using TPosition = UnityEngine.Vector2Int;

[Serializable]
public class SimpleSnakeManager {
    public SimpleSnake Snake { get; protected set; }

    public SimpleSnakeConfiguration SnakeConfiguration;

    public int Score { get; protected set; } = 0;

    public int Length => Snake.Length;

    protected MovingDirection queuedDirection = MovingDirection.None;

    public SimpleSnakeManager(SimpleSnakeConfiguration config) {
        SnakeConfiguration = config;
    }

    public SimpleSnakeManager(SimpleSnakeConfiguration config, SimpleSnake snake) : this(config) {
        Snake = snake;
    }

    /// <summary>
    /// Initializes a <paramref name="length"/>-specified snake having the tail cell at <paramref name="tailPosition"/> (with a certain <paramref name="speed"/>).
    /// The parameter <paramref name="heading"/> indicates the initial direction of the snake.
    /// </summary>
    /// <param name="tailPosition"></param>
    /// <param name="length"></param>
    /// <param name="heading"></param>
    /// <param name="speed"></param>
    public void InitializeSnake(TPosition tailPosition, int length, MovingDirection heading, float speed = 0f) {
        if (length < 1) throw new ArgumentException($"Invalid {nameof(length)}.");

        if (heading.Value == MovingDirection.NONE) throw new ArgumentException($"Invalid {nameof(heading)}.");

        Snake = new SimpleSnake();

        AdoptHead(tailPosition);

        TPosition last = tailPosition;
        for (int i = 1; i < length; ++i) {
            TPosition curr = last + Vector2IntUtils.FromDirection(heading);
            AdoptHead(curr);
            last = curr;
        }

        SetSnakeSpeed(speed);
    }

    public void SetSnakeSpeed(float speed) {
        Snake.Speed = speed;
    }

    public void SetSnakeHeadPosition(TPosition position) {
        Snake.Head.Position = position;
    }

    public void SnakeMove() {
        Snake?.Move(queuedDirection);
    }

    /// <summary>
    /// Creates a new cell at <paramref name="headPosition"/> and set it as a new head.
    /// </summary>
    /// <param name="headPosition"></param>
    public void AdoptHead(TPosition headPosition) {
        Snake?.AdoptHead(new SimpleSnakeCell { Position = headPosition, Sprite = SnakeConfiguration.Sprite });
    }

    public bool SelfCollided() {
        if (Snake is null) return false;
        return Snake.SelfCollided();
    }

    /// <summary>
    /// Forces the snake to have its <see cref="MovingDirection"/> set as a specified <paramref name="direction"/>.
    /// </summary>
    /// <param name="direction"></param>
    public void ForceDirection(MovingDirection direction) {
        Snake.Heading = direction;
    }

    /// <summary>
    /// Queues a specified <see cref="MovingDirection"/> if it is valid (indicated by <see cref="IsValidDirection(MovingDirection)"/>).
    /// </summary>
    /// <param name="direction"></param>
    public void QueueDirection(MovingDirection direction) {
        if (IsValidDirection(direction)) queuedDirection = direction;
    }

    /// <summary>
    /// Validates a specified <see cref="MovingDirection"/>.
    /// For example, <see cref="MovingDirection.Left"/> is otherwise valid if the snake is moving <see cref="MovingDirection.Right"/>.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool IsValidDirection(MovingDirection direction) {
        if (direction != null && direction.Value != MovingDirection.NONE) {
            if (
                direction.Value == MovingDirection.LEFT && Snake.Heading.Value != MovingDirection.RIGHT ||
                direction.Value == MovingDirection.RIGHT && Snake.Heading.Value != MovingDirection.LEFT ||
                direction.Value == MovingDirection.UP && Snake.Heading.Value != MovingDirection.DOWN ||
                direction.Value == MovingDirection.DOWN && Snake.Heading.Value != MovingDirection.UP
                ) return true;
        }
        return false;
    }

    public void StopSnake() {
        Snake.Heading = MovingDirection.None;
    }

    public void IncreaseScore(int x) {
        Score += x;
    }
}