namespace FIAP_Cloud_Games.Infra.Middleware
{
    public class CorrelationIdGenerator : ICorrelationIdGenerator
    {
        private static string _correlationId;

        public string Get() => _correlationId;

        public void Set(string correlationId) => _correlationId = correlationId;

    }
}
