using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;

namespace Bernhoeft.GRT.Teste.Api.Controllers.v1
{
    /// <summary>
    /// Gerencia os avisos do sistema.
    /// </summary>
    /// <remarks>
    /// Endpoints responsáveis por consultar, criar, atualizar e remover avisos.
    /// </remarks>
    /// <response code="401">Não autenticado.</response>
    /// <response code="403">Não autorizado.</response>
    /// <response code="500">Erro interno no servidor.</response>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Tags("Avisos")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public class AvisosController : RestApiController
    {
        private readonly IMediator _mediator;

        public AvisosController(IMediator mediator)
            => _mediator = mediator;

        /// <summary>
        /// Obtém um aviso pelo identificador.
        /// </summary>
        /// <param name="id">Identificador numérico do aviso.</param>
        /// <returns>Os dados do aviso correspondente ao identificador informado.</returns>
        /// <response code="200">Aviso encontrado com sucesso.</response>
        /// <response code="404">Nenhum aviso foi encontrado para o identificador informado.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AvisoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAvisoByIdQuery(id), cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Cria um novo aviso.
        /// </summary>
        /// <param name="command">Dados do aviso a ser criado.</param>
        /// <returns>Dados do aviso recém-criado.</returns>
        /// <response code="201">Aviso criado com sucesso.</response>
        /// <response code="400">Dados de entrada inválidos.</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(CreateAvisoCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromBody] CreateAvisoCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Atualiza um aviso existente.
        /// </summary>
        /// <param name="id">Identificador do aviso que será atualizado.</param>
        /// <param name="command">Dados que serão atualizados no aviso.</param>
        /// <returns>Dados do aviso após a atualização.</returns>
        /// <response code="200">Aviso atualizado com sucesso.</response>
        /// <response code="400">Dados de entrada inválidos.</response>
        /// <response code="404">Aviso não encontrado para o identificador informado.</response>
        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(AvisoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(
            int id,
            [FromBody] UpdateAvisoMessageCommandValidator command,
            CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Remove um aviso existente.
        /// </summary>
        /// <param name="id">Identificador do aviso que será removido.</param>
        /// <response code="204">Aviso removido com sucesso.</response>
        /// <response code="404">Aviso não encontrado.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RemoveAvisoCommand(id), cancellationToken);
            return NoContent();
        }
    }
}