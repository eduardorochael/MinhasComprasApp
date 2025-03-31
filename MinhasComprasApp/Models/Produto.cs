using SQLite;

namespace MinhasComprasApp.Models
{
    public class Produto
    {
        string _descricao;

        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Descricao {
            get => _descricao;
            set 
            {
                if (string.IsNullOrWhiteSpace(value)) // Verificar a descrição se está vazia 
                {
                    throw new Exception("Por favor , preencha a Descrição");
                }
                _descricao = value;
            }
        }
        public double Quantidade { get; set; }
        public double Preco {  get; set; }
        public double Total { get => Quantidade * Preco; }


        // Regritar Categoria 
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
        public string Categoria { get; set; }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.

        //Registra data da compra 
        public DateTime Data { get; set; }
    }
}
