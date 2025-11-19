using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Bernhoeft.GRT.Teste.IntegrationTests.Common;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Aviso
{
    public class AvisoControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AvisoControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenNoticeExists()
        {
            // arrange: o seed cria 2 avisos, então o ID 1 deve existir
            var response = await _client.GetAsync("/api/v1/avisos/1");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await response.Content.ReadFromJsonAsync<AvisoResponse>();
            body.Should().NotBeNull();
            body!.Id.Should().Be(1);
            body.Ativo.Should().BeTrue();
            body.Titulo.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetById_ShouldReturnNotFoundRequest_WhenIdIsInvalid(int invalidId)
        {
            // act
            var response = await _client.GetAsync($"/api/v1/avisos/{invalidId}");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenNoticeDoesNotExist()
        {
            // arrange: id bem alto que não existe
            var response = await _client.GetAsync("/api/v1/avisos/9999");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_ShouldCreateNotice_WhenRequestIsValid()
        {
            // arrange
            var request = new
            {
                titulo = "Aviso de teste",
                mensagem = "Mensagem de teste"
            };

            // act
            var response = await _client.PostAsJsonAsync("/api/v1/avisos", request);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = await response.Content.ReadFromJsonAsync<AvisoResponse>();
            body.Should().NotBeNull();
            body!.Id.Should().BeGreaterThan(0);
            body.Titulo.Should().Be("Aviso de teste");
            body.Mensagem.Should().Be("Mensagem de teste");
            body.Ativo.Should().BeTrue();
            body.DataCriacao.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenTitleOrMessageIsEmpty()
        {
            // arrange
            var request = new
            {
                titulo = "",
                mensagem = ""
            };

            // act
            var response = await _client.PostAsJsonAsync("/api/v1/avisos", request);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_ShouldUpdateOnlyMessage_WhenRequestIsValid()
        {
            // arrange: cria um aviso novo
            var createRequest = new
            {
                titulo = "Aviso original",
                mensagem = "Mensagem original"
            };

            var createResponse = await _client.PostAsJsonAsync("/api/v1/avisos", createRequest);
            var created = await createResponse.Content.ReadFromJsonAsync<AvisoResponse>();
            created.Should().NotBeNull();

            var updateRequest = new
            {
                id = created.Id,
                mensagem = "Mensagem atualizada"
            };

            // act
            var putResponse = await _client.PutAsJsonAsync($"/api/v1/avisos/{created!.Id}", updateRequest);

            // assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updated = await putResponse.Content.ReadFromJsonAsync<AvisoResponse>();
            updated.Should().NotBeNull();
            updated!.Id.Should().Be(created.Id);
            updated.Titulo.Should().Be("Aviso original");
            updated.Mensagem.Should().Be("Mensagem atualizada");
            updated.DataAtualizacao.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldSoftDeleteNotice()
        {
            // arrange: cria um aviso para deletar
            var createRequest = new
            {
                titulo = "Aviso para deletar",
                mensagem = "Mensagem qualquer"
            };

            var createResponse = await _client.PostAsJsonAsync("/api/v1/avisos", createRequest);
            var created = await createResponse.Content.ReadFromJsonAsync<AvisoResponse>();
            created.Should().NotBeNull();

            // act: delete
            var deleteResponse = await _client.DeleteAsync($"/api/v1/avisos/{created!.Id}");

            // assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // garante que não é mais retornado pelo GET
            var getResponse = await _client.GetAsync($"/api/v1/avisos/{created.Id}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }


}
