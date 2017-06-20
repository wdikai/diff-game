namespace Assets.Lib.Players
{
    public class Car : Player
    {
        public Car(float x, float y, float speed, float radius, float direction) : base(x, y, speed)
        {
            this.Radius = radius;
            this.Direction = direction;
        }

        public float Radius { get; set; }

        public float Direction { get; set; }
    }
}
