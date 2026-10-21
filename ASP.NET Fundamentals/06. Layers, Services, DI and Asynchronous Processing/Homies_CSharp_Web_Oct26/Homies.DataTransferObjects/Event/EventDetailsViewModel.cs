namespace Homies.ViewModels.Event
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Start { get; set; } = null!;

        public string End { get; set; } = null!;

        public string OrganizerName { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public string TypeName { get; set; } = null!;
    }
}
