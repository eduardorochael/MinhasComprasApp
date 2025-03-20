
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
                if (value == null) // Colocar o null diferente de vazio 
                {
                    throw new Exception("Por favor , preencha a Descrição");
                }
                _descricao = value;
            }
                
        }
        public double Quantidade { get; set; }
        public double Preco {  get; set; }

        public double Total { get => Quantidade * Preco; }
    }
}
