using UnityEngine;

namespace Assets.Scripts.Player.Movement.Helpers
{
    public class DirectionHelper
    {
        public Direction Direction { get; set; }

        //lividator: классический паттер Singleton. пока нет DI контейнера, придется делать так
        public static DirectionHelper Instance = new DirectionHelper();
    }
}
