using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using APPSisFortasV1.Modelo;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http.Json;

namespace APPSisFortasV1.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiarioBordo : ContentPage
    {
        int idUserLogado;
        int idVecLogado;
        long kmInicialVec = 0;
        DateTime dataDoDia = DateTime.Now;        
        DateTime horarioIni;
        DateTime horarioFin;
        int idEstabLocal;
        int idDiarioLogado = 0;
        DiarioBordoModel diario;
        Veiculo veiculo;
        Establecimento estabelecimento;
        public DiarioBordo(int idLoginUser, int vec)
        {
            InitializeComponent();
            
            idUserLogado = idLoginUser;
            idVecLogado = vec;
            LBDATA.Text = DateTime.Now.ToString("dd / MM / yyyy");
            diario = new DiarioBordoModel();
            veiculo = new Veiculo();
            estabelecimento = new Establecimento();

            VerificaDadosDiarioAsync();
            RetornaDadosVeiculoAsync();
            
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HorarioInicial.Focus();
        }

        public async void VerificaDadosDiarioAsync()
        {
            HttpClient clienteHTTP = new HttpClient();
            var dbDiarioBordo = await clienteHTTP.GetAsync("http://localhost:44306/api/tblDiarioBordoes/GetDiarioByUser?idUser=" + idUserLogado);
            diario = dbDiarioBordo.IsSuccessStatusCode ? JsonConvert.DeserializeObject<DiarioBordoModel>(await dbDiarioBordo.Content.ReadAsStringAsync()) : new DiarioBordoModel();
            idEstabLocal = diario.idEstabelecimento;
            kmInicialVec = diario.kmFinal;
            KmInicial.Text = Convert.ToString(kmInicialVec);
            RetornaDadosEstabelecimentoAsync();
        }

        /*
        public async void VerificaDadosDiarioAsync()
        {
            HttpClient clienteHTTP = new HttpClient();
            string json = await clienteHTTP.GetStringAsync("http://localhost:44306/api/tblDiarioBordoes");
            List<DiarioBordoModel> listDiario = JsonConvert.DeserializeObject<List<DiarioBordoModel>>(json);

            foreach (var userDiario in listDiario)
            {
                if (userDiario.idUser == idUserLogado)
                {
                    idEstabLocal = userDiario.idEstabelecimento;
                    idDiarioLogado = userDiario.idDiarioBordo;
                    kmInicialVec =  userDiario.kmFinal;
                }
            }
        }*/


        public async void RetornaDadosVeiculoAsync()
        {
            HttpClient clienteHTTP = new HttpClient();
            var dbVeiculo = await clienteHTTP.GetAsync("http://localhost:44306/api/tblVeiculoes/GetVeiculosByUser?idVec=" + idVecLogado);
            veiculo = dbVeiculo.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Veiculo>(await dbVeiculo.Content.ReadAsStringAsync()) : new Veiculo();
            LBVEICULONUMBER.Text = Convert.ToString(veiculo.codVecAd);
            LBMODELOAVEICULO.Text = veiculo.modelo;
            LBPLACAVEICULO.Text = veiculo.placa;
        }
        /*
        public async void RetornaDadosVeiculoAsync()
        {
            HttpClient clienteHTTP = new HttpClient();
            string json = await clienteHTTP.GetStringAsync("http://localhost:44306/api/tblVeiculoes");
            List<Veiculo> listVeiculo = JsonConvert.DeserializeObject<List<Veiculo>>(json);

            foreach (var vec in listVeiculo)
            {
                if (vec.idVeiculo == idVecLogado)
                {
                    LBVEICULONUMBER.Text = Convert.ToString(vec.codVecAd);
                    LBMODELOAVEICULO.Text = vec.modelo;
                    LBPLACAVEICULO.Text = vec.placa;
                }
            }
        }*/


        public async void RetornaDadosEstabelecimentoAsync()
        {
            HttpClient clienteHTTP = new HttpClient();
            var dbEstabelecimento = await clienteHTTP.GetAsync("http://localhost:44306/api/tblEstabelecimentoes/GetEstabelecimentoByUser?idEstab=" + idEstabLocal);
            estabelecimento = dbEstabelecimento.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Establecimento>(await dbEstabelecimento.Content.ReadAsStringAsync()) : new Establecimento();
            LBLOTACAO.Text = estabelecimento.nmEstab;
        }
        /*
        public async void RetornaDadosEstabelecimentoAsync()
        {
            HttpClient clienteHTTP = new HttpClient();
            string json = await clienteHTTP.GetStringAsync("http://localhost:44306/api/tblEstabelecimentoes");
            List<Establecimento> listEstab = JsonConvert.DeserializeObject<List<Establecimento>>(json);

            foreach (var estab in listEstab)
            {
                if (estab.idEstabelecimento == idEstabLocal)
                {
                    LBLOTACAO.Text = estab.nmEstab;
                }
            }
        }*/


        private async void AtualizaDiarioBordoAsync(object sender, EventArgs e)
        {
            //TimeSpan.TryParse(HorarioInicial.Text, out horarioIni);
            //TimeSpan.TryParse(HorarioFinal.Text, out horarioFin);
            HttpClient clienteHTTP = new HttpClient();                        
            diario.dataDia = dataDoDia;
            diario.horarioInicial = Convert.ToDateTime(HorarioInicial.Text);
            diario.kmInicial = kmInicialVec;
            diario.horarioFinal = Convert.ToDateTime(HorarioFinal.Text);
            diario.kmFinal = (long)Convert.ToInt32(KmFinal.Text);
            diario.imagemKm = null;

            HttpResponseMessage resposta;
            if (diario.idDiarioBordo >= 1)
            {
                resposta = await clienteHTTP.PutAsJsonAsync("http://localhost:44306/api/tblDiarioBordoes/PuttblDiarioBordo/" + diario.idDiarioBordo, diario);
            }
            else
            {
                resposta = await clienteHTTP.PostAsJsonAsync("http://localhost:44306/api/tblDiarioBordoes", diario);
            }

            if (resposta.IsSuccessStatusCode)
            {
                await DisplayAlert("Atualizar", "Diario de Bordo atualizado com sucesso!", "Ok");
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao tentar atualizar o Diario de Bordo!", "Ok");
            }
        }
        
    }
}