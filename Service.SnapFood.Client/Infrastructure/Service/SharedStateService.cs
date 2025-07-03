namespace Service.SnapFood.Client.Infrastructure.Service
{
    public class SharedStateService
    {
        public event Func<Task>? OnTrigger;

        public async Task TriggerUpdateAsync()
        {
            if (OnTrigger != null)
                await OnTrigger.Invoke();
        }
    }
}
