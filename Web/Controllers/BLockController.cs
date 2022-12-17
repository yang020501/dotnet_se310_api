using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs.Block;
using BLL.Services;
using BLL.Exceptions;
using Presentation.Shared;

namespace Presentation.Controllers
{
    [Route("api/courses/block")]
    [ApiController]
    public class BLockController : ControllerBase
    {
        private readonly IBlockService _blockService;

        public BLockController(IBlockService blockService)
        {
            _blockService = blockService;
        }


        [HttpPost, Route("create-block")]
        [Authorize(Roles = "lecturer,mod")]
        public IActionResult CreateBlock([FromBody] CreateBlockRequest request)
        {
            try
            {
                return Ok(_blockService.CreaetBlock(request));
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete, Route("delete-block/{id:Guid?}")]
        [Authorize(Roles = "lecturer,mod")]
        public IActionResult DeleteBlock(Guid? id)
        {
            try
            {
                return Ok(_blockService.DeleteBlock(id));
            }
            catch (BaseCustomApplicationException e)
            {
                return SharedControllerMethods.HandleExceptions(e, this);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


    }
}
