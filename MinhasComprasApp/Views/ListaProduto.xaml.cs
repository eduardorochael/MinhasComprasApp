using MinhasComprasApp.Models;
using System.Collections.ObjectModel;
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
        try
        { 
            lista.Clear(); // para não deixa duplicado a lista 
            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
           await DisplayAlert("Ops", ex.Message, "OK");
        }
       
    }


    private void ToolbarItem_Clicked(object sender, EventArgs e)
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
        try
        {
            string q = e.NewTextValue;

            lst_produtos.IsRefreshing = true;
            
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));

        }
        catch (Exception ex) 
        {
           await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O Total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try 
        { 
            MenuItem selecionado = sender as MenuItem;

            Produto p = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlert("Tem certeza?", $"Remover {p.Descricao}?","SIM","NÃO");

            if (confirm) 
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }

        }
        catch (Exception ex)
        {
           await DisplayAlert("Ops", ex.Message, "OK");
        }


    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
            Produto p = e.SelectedItem as Produto;
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

            Navigation.PushAsync(new Views.EditarProduto 
            {
                BindingContext = p,
            });

           
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async  void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        } finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }
}