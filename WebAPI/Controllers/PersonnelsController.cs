
using Business.Handlers.Personnels.Commands;
using Business.Handlers.Personnels.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
	/// <summary>
	/// Personnels If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class PersonnelsController : BaseApiController
	{
		///<summary>
		///List Personnels
		///</summary>
		///<remarks>Personnels</remarks>
		///<return>List Personnels</return>
		///<response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Personnel>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getall")]
		public async Task<IActionResult> GetList()
		{
			var result = await Mediator.Send(new GetPersonnelsQuery());
			if (result!=null)
			{
				return Ok(result);
			}
			return BadRequest();
		}

		///<summary>
		///It brings the details according to its id.
		///</summary>
		///<remarks>Personnels</remarks>
		///<return>Personnels List</return>
		///<response code="200"></response>  
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Personnel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getbyid")]
		public async Task<IActionResult> GetById(int name)
		{
			var result = await Mediator.Send(new GetPersonnelQuery { Name = name });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Add Personnel.
		/// </summary>
		/// <param name="createPersonnel"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreatePersonnelCommand createPersonnel)
		{
			var result = await Mediator.Send(createPersonnel);
			if (result!=null)
			{
				return Ok(result);
			}
			return BadRequest();
		}

		/// <summary>
		/// Update Personnel.
		/// </summary>
		/// <param name="updatePersonnel"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdatePersonnelCommand updatePersonnel)
		{
			var result = await Mediator.Send(updatePersonnel);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Delete Personnel.
		/// </summary>
		/// <param name="deletePersonnel"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeletePersonnelCommand deletePersonnel)
		{
			var result = await Mediator.Send(deletePersonnel);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
