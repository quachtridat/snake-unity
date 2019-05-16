public static class DirectionsExtensions {
    public static MovingDirection InvertX(this MovingDirection direction) {
        switch (direction.Value) {
            case MovingDirection.LEFT:
                return new MovingDirection { Value = MovingDirection.RIGHT };
            case MovingDirection.RIGHT:
                return new MovingDirection { Value = MovingDirection.LEFT };
            default:
                return direction;
        }
    }

    public static MovingDirection InvertY(this MovingDirection direction) {
        switch (direction.Value) {
            case MovingDirection.UP:
                return new MovingDirection { Value = MovingDirection.DOWN };
            case MovingDirection.DOWN:
                return new MovingDirection { Value = MovingDirection.UP };
            default:
                return direction;
        }
    }
}