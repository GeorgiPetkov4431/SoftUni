namespace EventManager.ViewModels.Category
{
    public class CategoryDropdownViewModel
    {
        /* Data Direction is Server to Client => No Model Validation is required! */
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
