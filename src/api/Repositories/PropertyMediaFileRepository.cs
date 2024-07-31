using api.Contexts;
using api.Models;
using api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PropertyMediaFileRepository : Repository<int,PropertyMediaFile>, IPropertyMediaFileRepository
    {
        public PropertyMediaFileRepository(DbSql context) : base(context)
        {
        }

        public override async Task<bool> IsDuplicate(PropertyMediaFile entity)
        {
            return await _context.MediaFiles.AnyAsync(x => x.Title == entity.Title && x.PropertyId == entity.PropertyId || x.Url == entity.Url);
        }
    }
}
