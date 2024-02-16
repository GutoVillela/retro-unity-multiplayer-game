namespace Assets.Events
{
    public class TakeDamageEvent
    {
        public TakeDamageEvent(string playerId, float newHealth)
        {
            PlayerId = playerId;
            NewHealth = newHealth;
        }

        public string PlayerId { get; set; }
        public float NewHealth { get; set; }
    }
}
