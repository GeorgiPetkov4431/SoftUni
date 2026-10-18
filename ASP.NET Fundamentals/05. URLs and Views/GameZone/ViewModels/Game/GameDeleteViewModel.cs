namespace GameZone.ViewModels.Game
{
    public class GameDeleteViewModel
    {
        /* ViewModel -> Data flow is from TRUSTED Server to UNTRUSTED Client -> No Model Validation */
        public int Id { get; set; }

        public string Title { get; set; } = null!;
    }
}
