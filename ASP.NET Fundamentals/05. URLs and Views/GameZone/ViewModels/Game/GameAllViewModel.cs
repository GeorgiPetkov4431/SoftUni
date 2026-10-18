namespace GameZone.ViewModels.Game
{
    public class GameAllViewModel
    {
        /* ViewModel -> Data flow is from TRUSTED Server App to UNTRUSTED Client App */
        /* No need for Data Validation in ViewModel!!! */
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }
        
        /* Formatted Release Date */
        public string ReleasedOn { get; set; } = null!;

        public string Publisher { get; set; } = null!;

        public string GenreName { get; set; } = null!;



    }
}
