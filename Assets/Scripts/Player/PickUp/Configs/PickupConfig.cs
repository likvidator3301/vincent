namespace Assets.Scripts.Player.PickUp.Configs
{
    public class PickupConfig
    {
        public float CriticalDistance { get; }

        public PickupConfig(float criticalDistance)
        {
            CriticalDistance = criticalDistance;
        }
    }
}
