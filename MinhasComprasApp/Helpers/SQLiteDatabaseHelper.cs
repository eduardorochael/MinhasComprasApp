using MinhasComprasApp.Models;
using MinhasComprasApp.Views;
using SQLite;

namespace MinhasComprasApp.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {

            //Garantir que data do cadastro seja preenchida 
            if (p.Data == DateTime.MinValue)
            {                                            //Modificação utilizando IF 
                p.Data = DateTime.MinValue;
            }

            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=? ,Data=? ,Categoria=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id ,p.Data ,p.Categoria
            );
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }

      
        // Novo método para buscar produtos por categoria
        public Task<List<Produto>> GetByCategory(string categoria)
        {
            string sql = "SELECT * FROM Produto WHERE Categoria = ?";
            return _conn.QueryAsync<Produto>(sql, categoria);
        }

        // Novo método para buscar produtos por período
        public Task<List<Produto>> GetByDateRange(DateTime inicio, DateTime fim)
        {
            string sql = "SELECT * FROM Produto WHERE Data BETWEEN ? AND ?";
            return _conn.QueryAsync<Produto>(sql, inicio, fim);
        }
        internal void Delete(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
