using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Contracts.Projects;
using TaskFlow.Api.Domain.Enums;
using TaskFlow.Api.Services.Projects;
using TaskFlow.Api.Errors.Exceptions;

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
            return CreatedAtRoute("GetProjectById",new { id = project.Id },project);
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(
            [FromQuery] string? status,
            CancellationToken cancellationToken)
        {
            var parsedStatus = ParseProjectStatus(status);

            var projects = await _projectService.ListAsync(
                parsedStatus,
                cancellationToken);

            return Ok(projects);
        }

        [HttpGet("{id:guid}", Name = "GetProjectById")]
        public async Task<IActionResult> GetByIdAsync(Guid id,CancellationToken cancellationToken)
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

        private static ProjectStatus? ParseProjectStatus(string? status)
        {
            if (status is null)
            {
                return null;
            }

            return status switch
            {
                "active" => ProjectStatus.Active,
                "archived" => ProjectStatus.Archived,

                _ => throw new ValidationException(
                    "O filtro status possui um valor inválido.",
                    new Dictionary<string, string[]>
                    {
                        ["status"] =
                        [
                            "O campo status deve ser active ou archived."
                        ]
                    })
            };
        }
    }
}
