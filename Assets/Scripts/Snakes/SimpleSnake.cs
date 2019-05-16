using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using TBodyPart = SimpleSnakeCell;
using TContainer = System.Collections.Generic.List<SimpleSnakeCell>;
using TPosition = UnityEngine.Vector2Int;

[Serializable]
public class SimpleSnake : Snake<TBodyPart> {
    #region Properties

    public TBodyPart Head => Body.LastOrDefault();

    public int Length => (Body as TContainer).Count;

    #endregion

    #region Fields

    public float Speed;

    public LinkedList<TPosition> Turns = new LinkedList<TPosition>();

    #endregion

    public SimpleSnake() {
        Body = new TContainer();
    }

    public SimpleSnake(SimpleSnake snake) {
        Body = new TContainer(snake.Body);
    }

    /// <summary>
    /// Make the snake moves once at a certain direction.
    /// </summary>
    /// <param name="heading">At which direction to move.</param>
    public override void Move(MovingDirection heading) {
        if (Body is null || heading.Value == MovingDirection.NONE) return;

        if (heading.Value != Heading.Value) 
            Turns.AddLast(new TPosition(Head.Position.x, Head.Position.y));

        Heading = heading;

        TBodyPart last = default;
        using (var e = Body.GetEnumerator()) {
            if (e.MoveNext()) {
                last = e.Current;
                while (!(Turns.First is null) && last.Position == Turns.First.Value)
                    Turns.RemoveFirst();
                while (e.MoveNext()) {
                    last.Position = e.Current.Position;
                    last = e.Current;
                }
            }
        }

        Head.Position = Head.Position + Vector2IntUtils.FromDirection(heading);
    }

    /// <summary>
    /// Takes a new <see cref="TBodyPart"/> as a new head.
    /// </summary>
    /// <param name="head"></param>
    public void AdoptHead(TBodyPart head) {
        (Body as TContainer).Add(head);
    }

    /// <summary>
    /// Checks if the snake has collided with itself.
    /// </summary>
    /// <returns></returns>
    public bool SelfCollided() {
        if (Body is null) return false;

        var last = Body.FirstOrDefault();
        if (last is null) return false;

        var headPos = Head.Position;
        var lastPos = last.Position;

        foreach (var currPos in Turns) {
            if (lastPos.x == currPos.x && currPos.x == headPos.x && lastPos.y <= headPos.y && headPos.y <= currPos.y) return true;
            if (lastPos.y == currPos.y && currPos.y == headPos.y && lastPos.x <= headPos.x && headPos.x <= currPos.x) return true;
            lastPos = currPos;
        }

        return false;
    }
}