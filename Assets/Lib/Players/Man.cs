namespace Assets.Lib.Players
{
    public class Man : Player
    {
        public Man (float x, float y, float speed, float direction) : base(x, y, speed)
        {
            this.Direction = direction;
        }

        public float Direction { get; set; }
    }
}
