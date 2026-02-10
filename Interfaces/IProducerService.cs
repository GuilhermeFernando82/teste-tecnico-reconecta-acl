public interface IProducerService
{
    Task<bool> ActivateProducerAsync(int producerId);
}
