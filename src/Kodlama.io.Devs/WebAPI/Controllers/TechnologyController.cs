using Application.Features.ProgrammingLanguageSubTechs.Models;
using Application.Features.ProgrammingLanguageSubTechs.Queries.GetListProgrammingLanguageSubTech;
using Application.Features.Technologies.Commands.CreatedTechnologies;
using Application.Features.Technologies.Commands.DeleteTechnologies;
using Application.Features.Technologies.Commands.UpdatedTechnologies;
using Application.Features.Technologies.Dtos;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Models;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Querıes.GetByIdProgrammingLanguage;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Querıes.GetByListProgrammingLanguage;
using Kodlama.io.Devs.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rentACar.Application.Features.Models.Queries.GetListModelByDynamic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : BaseController
    {

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand createdTechnologyCommand)
        {
            CreatedTechnologyDto result = await Mediator.Send(createdTechnologyCommand);
            return Created("", result);
        }

        [HttpGet]

        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologiesQuery getListTechnologiesQuery = new GetListTechnologiesQuery { PageRequest = pageRequest };

            TechnologyListModels result = await Mediator.Send(getListTechnologiesQuery);
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListUserProfileByDynamicQuery getListTechnologiesByDynamicQuery = new GetListUserProfileByDynamicQuery { PageRequest = pageRequest, Dynamic = dynamic };

            TechnologyListModels result = await Mediator.Send(getListTechnologiesByDynamicQuery);
            return Ok(result);
        }

        [HttpPost("Delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeleteTechnologyDto deleteTechnologyDto = await Mediator.Send(deleteTechnologyCommand);
            return Created("", deleteTechnologyDto);
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] string name)
        {
            UpdatedTechnologyCommand updateTechnologyCommand = new() { Id = Id, Name = name };
            UpdatedTechnologyDto result = await Mediator.Send(updateTechnologyCommand);
            return Created("", result);
        }

    }
}
