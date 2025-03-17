using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CloudKit;
using MinhasComprasApp.Models;
using MinhasComprasApp.Views;
using SQLite;

namespace MinhasComprasApp.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;

    }

    protected async override void OnAppearing()
    {
        List<Produto> tap = await App.Db.GetAll();

        tap.ForEach(i => lista.Add(i));
    }
    

    private  void ToolbarItem_Clicked(object sender, EventArgs e)
    {        
           try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tap = await App.Db.Search(q);

        tap.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O Total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        if (sender is MenuItem menuItem && menuItem.BindingContext is Produto produto)
        {
            bool confirmacao = await DisplayAlert(
                "Confirmação",
                $"Deseja remover o produto {produto.Descricao}?",
                "Sim",
                "Não"
            );

            if (confirmacao)
            {
                await App.Db.Delete(produto); // Certifique-se de que App.Db.Delete retorna Task

                lista.Remove(produto); // Remove da lista local para atualizar a tela
            }
        }
    }

    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection database;

        public DatabaseService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Produto>().Wait();
        }

        public async Task Delete(Produto produto)
        {
            await database.DeleteAsync(produto);
        }
    }

}