﻿
namespace Components
{
    public enum Direction
    {
        Stopped,
        Up,
        Down,
        Left,
        Right
    }

    public class Movable : Component
    {
        public Direction movementDirection { get; set; }
    }
}
