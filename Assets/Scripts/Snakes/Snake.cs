using System;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// An abstract class for snake.
/// </summary>
/// <typeparam name="TBodyPart">A single body part or cell of a snake.</typeparam>
/// <typeparam name="TContainer">A container containing <typeparamref name="TBodyPart"/>s.</typeparam>
[Serializable]
public abstract class Snake<TBodyPart> {
    protected IEnumerable<TBodyPart> _body;
    protected MovingDirection _heading;

    public IEnumerable<TBodyPart> Body { get => _body; protected set => _body = value; }
    public MovingDirection Heading { get => _heading; set => _heading = value; }  

    /// <summary>
    /// Make the snake moves <paramref name="distance"/> units at a certain direction.
    /// </summary>
    /// <param name="distance">How far in units.</param>
    /// <param name="direction">At which direction to move.</param>
    public virtual void Move(MovingDirection direction) {
        throw new NotImplementedException($"{nameof(Move)} is not implemented!");
    }
}
