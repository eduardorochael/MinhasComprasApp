using MinhasComprasApp.Models;

namespace MinhasComprasApp.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0090:Usar 'new(...)'", Justification = "<Pendente>")]
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {

        try
        {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
            Produto produto_anexado = BindingContext as Produto;
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.

#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_discricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

            await App.Db.Update(p);
            await DisplayAlert("Sucesso!", " Registro Atualizado", "OK");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}
