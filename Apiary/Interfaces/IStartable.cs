namespace ApiaryEngine.Interfaces
{
    public interface IStartable
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
