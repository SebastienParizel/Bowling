
namespace Bowling.Model
{
    internal class FrameSpare : Frame
    {
        protected internal FrameSpare(int firstLaunch, int secondLaunch) : base(firstLaunch, secondLaunch)
        {
            IsSpare = true;
        }

        public override int CalculateFrameScore()
        {
            int baseScore = base.CalculateFrameScore();
            if (!HasNextFrame())
                return baseScore;
            return baseScore + NextFrame.FirstLaunch;
        }
    }
}
