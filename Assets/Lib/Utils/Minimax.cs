namespace Assets.Lib.Utils
{
    using System;

    public class Minimax
    {
        public static Tuple<State, float> Root(State  initState, int maxDept = 10)
        {
            var steps = initState.GetNextSteps();
            var bestMove = Double.MinValue;
            var bestStep = initState;


            foreach (var step in steps)
            {
                var nextState = initState.NextState(initState, step);
                var value =  Minimax.MinMax(maxDept - 1, nextState);
                if (value >= bestMove)
                {
                    bestMove = value;
                    bestStep = nextState;
                }
            }

            return Tuple.New<State, float>(bestStep, (float)bestMove);
        }

        private static float MinMax(int dept, State state)
        {
            if (dept <= 0 || state.CheckGameEnded())
            {
                return state.GetPrice();
            }

            var steps = state.GetNextSteps();
            if (state.type == StateType.FIRST)
            {
                var bestMove = Double.MinValue;

                foreach (var step in steps)
                {
                    var nextState = state.NextState(state, step);
                    var value = Minimax.MinMax(dept - 1, nextState);
                    if (value >= bestMove)
                    {
                        bestMove = value;
                        state.Next = nextState;
                    }
                }

                return (float) bestMove;
            }
            else
            {
                var bestMove = Double.MaxValue;

                foreach (var step in steps)
                {
                    var nextState = state.NextState(state, step);
                    var value = Minimax.MinMax(dept - 1, nextState);
                    if (value <= bestMove)
                    {
                        bestMove = value;
                        state.Next = nextState;
                    }
                }

                return (float)bestMove;
            }
        }
    }
}
