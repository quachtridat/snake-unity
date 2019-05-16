using UnityEngine;

public static class Vector2IntUtils {
    public static Vector2Int FromDirection(MovingDirection direction) {
        switch (direction.Value) {
            case MovingDirection.NONE:
                return Vector2Int.zero;
            case MovingDirection.UP:
                return Vector2Int.up;
            case MovingDirection.LEFT:
                return Vector2Int.left;
            case MovingDirection.DOWN:
                return Vector2Int.down;
            case MovingDirection.RIGHT:
                return Vector2Int.right;
            default:
                return Vector2Int.zero;
        }
    }

    public static Vector2Int FromDirection(IteratingDirection direction) {
        switch (direction.Value) {
            case IteratingDirection.LEFT_TO_RIGHT:
                return Vector2Int.right;
            case IteratingDirection.RIGHT_TO_LEFT:
                return Vector2Int.left;
            case IteratingDirection.TOP_TO_BOTTOM:
                return Vector2Int.down;
            case IteratingDirection.BOTTOM_TO_TOP:
                return Vector2Int.up;
            default:
                return Vector2Int.zero;
        }
    }

    public static Vector2Int GetRandomIn(RectInt rect) {
        return new Vector2Int(Random.Range(rect.x, rect.x + rect.width - 1), Random.Range(rect.y, rect.y + rect.height - 1));
    }
}
