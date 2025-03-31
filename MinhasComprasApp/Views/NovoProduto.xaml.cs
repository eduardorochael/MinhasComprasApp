using MinhasComprasApp.Models;

namespace MinhasComprasApp.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
				Descricao = txt_discricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text),
				Categoria = txt_categoria.Text,
                Data = dt_datacompra.Date
			};

			await App.Db.Insert(p);
			await DisplayAlert("Sucesso!", " Registro Inserido", "OK");
			await Navigation.PopAsync();

		}catch (Exception ex) 
		{
			await DisplayAlert("Ops", ex.Message, "Ok");
		}
    }
}