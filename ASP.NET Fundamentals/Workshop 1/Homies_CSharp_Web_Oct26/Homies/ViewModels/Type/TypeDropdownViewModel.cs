namespace Homies.ViewModels.Type
{
    public class TypeDropdownViewModel
    {
        /* Data flow is from TRUSTED Serverto UNRUSTED Client -> No Model Validation */
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
