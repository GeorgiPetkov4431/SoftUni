using Homies.Data;
using Homies.Services.Core.Contracts;
using Homies.ViewModels.Type;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services.Core.Services
{
    public class TypeService : ITypeService
    {
        private readonly HomiesDbContext _dbContext;

        public TypeService(HomiesDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<TypeDropdownViewModel>> GetTypeDropdownEntriesAsync()
        {
            IEnumerable<TypeDropdownViewModel> typeDropdownEntries = await this._dbContext.Types
                .Select(t => new TypeDropdownViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .OrderBy(t => t.Name)
                .ToArrayAsync();

            return typeDropdownEntries;
        }
    }
}
