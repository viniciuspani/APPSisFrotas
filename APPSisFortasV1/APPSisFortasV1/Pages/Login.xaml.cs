using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using APPSisFortasV1.Modelo;
using APPSisFortasV1.Pages;
using System.Net.Http;
using APPSisFortasV1;
using System.Net;
using System.Net.Http.Json;



namespace APPSisFortasV1.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        int idUsuarioLogado = 0;
            
        
        public Login()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TXTCPF.Focus();
        }

        private async void VerificarLogin(object sender, EventArgs e)
        {
            HttpClient clienteHTTP = new HttpClient();

            var logado = await clienteHTTP.PostAsJsonAsync("http://localhost:44306/api/tblusuarios/PostAutenticacao", new AutenticacaoRequest(TXTCPF.Text, TXTSENHA.Text));

            if(!logado.IsSuccessStatusCode)
            {
                await DisplayAlert("Erro", "Usuario invalido", "ok");
            } 
            else
            {
                int.TryParse(await logado.Content.ReadAsStringAsync(), out idUsuarioLogado);
                await Navigation.PushAsync(new Pages.CheckList(idUsuarioLogado));
            }

        }
    }
}