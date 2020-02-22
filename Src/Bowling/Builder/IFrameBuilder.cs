using Bowling.Model;

namespace Bowling.Builder
{
    public interface IFrameBuilder
    {
        Frame CreateStrike();
        Frame CreateSpare(int firstLaunch);
        Frame CreateFrame(int firstLaunch, int secondLaunch);
    }
}