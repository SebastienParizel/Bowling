using Bowling.Model;

namespace Bowling.Builder
{
    public interface IGameParser
    {
        Game BuildGame(string[] parameters);
    }
}
