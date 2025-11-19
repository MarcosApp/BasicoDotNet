namespace Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities
{
    public partial class AvisoEntity
    {
        public int Id { get; private set; }
        public bool Ativo { get; set; } = true;
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }

        public AvisoEntity(string titulo, string mensagem)
        {
            Titulo = titulo;
            Mensagem = mensagem;
            Ativo = true;
            DataCriacao = DateTime.UtcNow;
        }
        public void DefinirId(int id)
               => Id = id;

        public void AtualizarMensagem(string novaMensagem)
        {
            Mensagem = novaMensagem;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Desativar()
        {
            Ativo = false;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}