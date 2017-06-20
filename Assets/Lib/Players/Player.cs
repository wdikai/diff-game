namespace Assets.Lib.Players
{
    using UnityEngine;

    public abstract class Player
    {
        public Player(float x = 0, float y = 0, float speed = 1)
        {
            this.Position = new Vector2(x, y);
            this.Speed = speed;
        }

        public Vector2 Position { get; set; }

        public float Speed { get; set; }
    }
}
