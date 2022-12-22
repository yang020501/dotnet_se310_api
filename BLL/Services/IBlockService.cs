using DAL.Aggregates;
using BLL.DTOs.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBlockService
    {
        Block CreaetBlock(CreateBlockRequest? request);
        Guid? DeleteBlock(Guid? blockId);
        IEnumerable<GetBlocksResponse>? GetAllBlocksFromCourse(Guid? courseId);
    }
}
