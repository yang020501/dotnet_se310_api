using BLL.DTOs.MarkdownDocuments;
using BLL.Exceptions;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Shared;

namespace Presentation.Controllers
{
    [Route("api/courses/block/markdown")]
    [ApiController]
    public class MarkdownDocumentController : ControllerBase
    {
        private readonly IMarkdownDocumentService _markdownDocumentService;

        public MarkdownDocumentController(IMarkdownDocumentService markdownDocumentService)
        {
            _markdownDocumentService = markdownDocumentService;
        }

        [HttpPost, Route("add-doc")]
        [Authorize(Roles = "lecturer,mod")]
        public IActionResult AddDocumentIntoBlock(AddDocumentIntoBlockRequest request)
        {
            try
            {
                return Ok(_markdownDocumentService.AddDocumentToBlock(request));
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

        [HttpDelete, Route("delete-doc")]
        [Authorize(Roles = "lecturer,mod")]
        public IActionResult DeleteDocument(DeleteDocumentRequest request)
        {
            try
            {
                return Ok(_markdownDocumentService.DeleteDocumentFromBlock(request));
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

        [HttpPatch, Route("update-doc")]
        [Authorize(Roles = "lecturer,mod")]
        public IActionResult UpdateDocument(UpdateDocumentRequest request)
        {
            try
            {
                return Ok(_markdownDocumentService.UpdateDocument(request));
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
