using Factories.PlayerFactories;

namespace Players
{
    public class BasePlayer : Player
    {
        public BasePlayer(PlayerFactory playerFactory) : base(playerFactory)
        {
        }
    }
}
