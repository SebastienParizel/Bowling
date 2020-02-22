
namespace Bowling.Model
{
    internal class SpareFrame : Frame
    {
        public SpareFrame(int firstLaunch, int secondLaunch) : base(firstLaunch, secondLaunch)
        {
            IsSpare = true;
        }

        protected override int CalculateFrameScore()
        {
            int baseScore = base.CalculateFrameScore();
            if (!HasNextFrame())
                return baseScore;
            return baseScore + NextFrame.FirstLaunch;
        }
    }
}
