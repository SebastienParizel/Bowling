
namespace Bowling.Model
{
    internal class StrikeFrame : Frame
    {
        public StrikeFrame(int firstLaunch, int secondLaunch) : base(firstLaunch, secondLaunch)
        {
            IsStrike = true;
        }

        protected override int CalculateFrameScore()
        {
            int baseScore = base.CalculateFrameScore();
            if (!HasNextFrame())
                return baseScore;
            return baseScore + GetFirstNextLaunch() + GetSecondNextLaunch();
        }

        private int GetFirstNextLaunch()
        {
            if (HasNextFrame())
                return NextFrame.FirstLaunch;
            return 0;
        }

        private int GetSecondNextLaunch()
        {
            if (HasNextFrame())
            {
                if (!NextFrame.IsStrike)
                    return NextFrame.SecondLaunch;
                if (NextFrame.NextFrame != null)
                    return NextFrame.NextFrame.FirstLaunch;
            }
            return 0;
        }
    }
}
