namespace GameZone.ViewModels.Genre
{
    public class GenreDropdownViewModel
    {
        /* ViewModel -> Daat flow is from TRUSTED Server App (Db) to UNTRUSTED Client App (View) */
        /* No Model Validation is required */
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
