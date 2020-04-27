namespace Business.Interfaces
{
    public interface IHostedService
    {
        void StartAutomaticUpdateRecords();
        void AutomaticUpdateRecords(object o);
    }
}
