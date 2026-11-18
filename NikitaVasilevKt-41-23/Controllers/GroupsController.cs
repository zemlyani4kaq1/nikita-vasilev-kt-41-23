using Microsoft.AspNetCore.Mvc;
using NikitaVasilevKt_41_23.Filters;
using NikitaVasilevKt_41_23.Interfaces;
using NikitaVasilevKt_41_23.Models;

namespace NikitaVasilevKt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _groupService;

        public GroupsController(ILogger<GroupsController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpPost("GetGroups")]
        public async Task<IActionResult> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsAsync(filter, cancellationToken);
            return Ok(groups);
        }

        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            var result = await _groupService.AddGroupAsync(group, cancellationToken);
            return Ok(result);
        }

        [HttpPut("UpdateGroup")]
        public async Task<IActionResult> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default)
        {
            var result = await _groupService.UpdateGroupAsync(group, cancellationToken);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("DeleteGroup/{groupId}")]
        public async Task<IActionResult> DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {
            var result = await _groupService.DeleteGroupAsync(groupId, cancellationToken);
            return result ? Ok() : NotFound();
        }
    }
}