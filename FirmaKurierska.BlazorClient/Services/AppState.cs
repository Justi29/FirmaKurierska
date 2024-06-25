namespace FirmaKurierska.BlazorClient.Services
{
    public class AppState
    {
        private bool isClientLoggedIn;
        private bool isCourierLoggedIn;

        public bool IsClientLoggedIn
        {
            get => isClientLoggedIn;
            set
            {
                if (isClientLoggedIn != value)
                {
                    isClientLoggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        public bool IsCourierLoggedIn
        {
            get => isCourierLoggedIn;
            set
            {
                if (isCourierLoggedIn != value)
                {
                    isCourierLoggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        public int? ClientId { get; set; }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
