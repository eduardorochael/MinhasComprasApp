using System.Collections.ObjectModel;
using MinhasComprasApp.Models;

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
}