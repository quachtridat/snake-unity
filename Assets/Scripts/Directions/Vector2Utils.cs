using UnityEngine;

public static class Vector2Utils {
    public static Vector2 FromDirection(MovingDirection direction) {
        switch (direction.Value) {
            case MovingDirection.NONE:
                return Vector2.zero;
            case MovingDirection.UP:
                return Vector2.up;
            case MovingDirection.LEFT:
                return Vector2.left;
            case MovingDirection.DOWN:
                return Vector2.down;
            case MovingDirection.RIGHT:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    public static Vector2 FromDirection(IteratingDirection direction) {
        switch (direction.Value) {
            case IteratingDirection.LEFT_TO_RIGHT:
                return Vector2.right;
            case IteratingDirection.RIGHT_TO_LEFT:
                return Vector2.left;
            case IteratingDirection.TOP_TO_BOTTOM:
                return Vector2.down;
            case IteratingDirection.BOTTOM_TO_TOP:
                return Vector2.up;
            default:
                return Vector2.zero;
        }
    }

    public static Vector2 GetRandomIn(Rect rect) {
        return new Vector2(Random.Range(rect.x, rect.x + rect.width - 1), Random.Range(rect.y, rect.y + rect.height - 1));
    }
}
