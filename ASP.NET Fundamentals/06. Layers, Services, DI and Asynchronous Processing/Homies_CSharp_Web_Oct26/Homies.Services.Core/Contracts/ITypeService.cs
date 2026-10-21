using Homies.ViewModels.Type;

namespace Homies.Services.Core.Contracts
{
    public interface ITypeService
    {
        Task<IEnumerable<TypeDropdownViewModel>> GetTypeDropdownEntriesAsync();
    }
}
