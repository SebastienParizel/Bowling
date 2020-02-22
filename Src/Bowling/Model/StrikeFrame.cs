
namespace Bowling.Model
{
    internal class StrikeFrame : Frame
    {
        protected internal StrikeFrame(int firstLaunch, int secondLaunch) : base(firstLaunch, secondLaunch)
        {
            IsStrike = true;
        }

        protected override int CalculateFrameScore()
        {
            int baseScore = base.CalculateFrameScore();
            if (!HasNextFrame())
                return baseScore;
            return baseScore + NextFrame.FirstLaunch + NextFrame.SecondLaunch;
        }
    }
}
