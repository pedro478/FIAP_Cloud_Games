namespace FIAP_Cloud_Games.Infra.Middleware
{
    public interface ICorrelationIdGenerator
    {
        string Get();
        void Set(string correlationId);

    }
}
