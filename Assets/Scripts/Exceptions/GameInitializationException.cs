using System;

namespace Assets.Scripts.Exceptions
{
    public class GameInitializationException: Exception
    {
        public GameInitializationException(string message): base(message)
        { }
    }
}
