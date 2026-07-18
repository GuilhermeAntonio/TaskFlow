using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Contracts.Projects;
using TaskFlow.Api.Domain.Enums;
using TaskFlow.Api.Services.Projects;

namespace TaskFlow.Api.Controllers
{
    [ApiController]
    [Route("projetos")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = project.Id }, project);
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync([FromQuery] ProjectStatus? status, CancellationToken cancellationToken)
        {
            var projects = await _projectService.ListAsync(status, cancellationToken);
            return Ok(projects);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var project = await _projectService.GetByIdAsync(id, cancellationToken);
            return Ok(project);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectService.UpdateAsync(id, request, cancellationToken);
            return Ok(project);
        }
    }
}
