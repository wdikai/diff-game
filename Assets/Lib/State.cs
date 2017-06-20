namespace Assets.Lib
{
    using System;
    using Assets.Lib.Players;
    using Assets.Lib.Utils;
    using UnityEngine;

    public enum StateType { FIRST, SECOND }

    public class State
    {
        public readonly StateType type;

        protected State(Car first, Man second, StateType type)
        {
            this.FirstPlayer = first;
            this.SecondPlayer = second;

            this.type = type;
        }

        public Car FirstPlayer { get; set; }

        public Man SecondPlayer { get; set; }

        public State Next{ get; set; }

        public CheckEnd<State> checkEnd { get; set; }
        public CalculatePrice<State> getPrice { get; set; }
        public StapBuilder<State> stapBuilder { get; set; }

        public bool CheckGameEnded()
        {
            if (this.checkEnd == null && !(this.checkEnd is CheckEnd<State >)) throw new Exception("Expect checkEnd of CheckEnd type");

            return this.checkEnd(this);
        }

        public float GetPrice()
        {
            if (this.getPrice == null && !(this.getPrice is CalculatePrice<State >)) throw new Exception("Expect getPrice of CalculatePrice type");

            return this.getPrice(this);
        }

        public Vector4[] GetNextSteps()
        {
            if (this.stapBuilder == null && !(this.stapBuilder is StapBuilder<State >)) throw new Exception("Expect stapBuilder of StapBuilder type");

            return this.stapBuilder(this);
        }

        public static State BuildState(Car carPlayer, Man manPlayer, StateType type, CheckEnd<State > checkEnd, CalculatePrice<State > getPrice, StapBuilder<State > stapBuilder)
        {
            if (carPlayer == null && !(carPlayer is Car)) throw new Exception("Expect car player of Car type");
            if (manPlayer == null && !(manPlayer is Man)) throw new Exception("Expect man player of Man type");

            var state = new State (carPlayer, manPlayer, type);
            state.checkEnd = checkEnd;
            state.getPrice = getPrice;
            state.stapBuilder = stapBuilder;

            return state;
        }

        public State NextState(State state, Vector4 step)
        {
            var type = StateType.FIRST == state.type? StateType.FIRST: StateType.SECOND;
            var man = new Man(step.x, step.y, state.SecondPlayer.Speed, step.z);

            var car =  state.FirstPlayer;
            car.Direction = step.w;

            return State.BuildState(car, man, type, state.checkEnd, state.getPrice, state.stapBuilder);
        }
    }
}
