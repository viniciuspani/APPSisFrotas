using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using APPSisFortasV1.Modelo;
using APPSisFortasV1.Pages;
using APPSisFortasV1;
using System.Net.Http.Json;
using Xamarin.Essentials;



namespace APPSisFortasV1.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckList : ContentPage
    {
        int checkIdUser;//recebe o id do login do usuario feito na pagina login        
        int idVeiculoLogado = 0;// recebe o idVeiculo correspondendo ao vinculado com o usuario

        CheckListModel checkList;

        public CheckList(int idLogin)
        {
            InitializeComponent();
            checkIdUser = idLogin;
            checkList = new CheckListModel();
            VerificaIdCheckListAsync();
        }
        //Recupera informacoes de Id da tabela CheckList e idVeiculo
        public async void VerificaIdCheckListAsync()
        {
            HttpClient clienteHTTP = new HttpClient();
            var dbCheckList = await clienteHTTP.GetAsync("http://localhost:44306/api/tblchecklists/getchecklistbyuser?idUser=" + checkIdUser);
            checkList = dbCheckList.IsSuccessStatusCode ? JsonConvert.DeserializeObject<CheckListModel>(await dbCheckList.Content.ReadAsStringAsync()) : new CheckListModel();
            idVeiculoLogado = checkList.idVeiculo;
        }



        private async void RegistroCompleto(object sender, EventArgs e)
        {

            HttpClient clienteHTTP = new HttpClient();
            checkList.oleoFreio = Convert.ToString(StatusOleoFreio.SelectedItem);
            checkList.aguaRadiador = Convert.ToString(StatusAguaRadiador.SelectedItem);
            checkList.aguaEsguinchador = Convert.ToString(StatusAguaEnguinchador.SelectedItem);
            checkList.combustivel = Convert.ToString(StatusCombustivel.SelectedItem);
            checkList.oleoHidraulico = Convert.ToString(StatusOleoHidraulico.SelectedItem);
            checkList.oleoMotor = Convert.ToString(StatusOleoMotor.SelectedItem);
            checkList.luzAlta = Convert.ToString(StatusLuzAlta.SelectedItem);
            checkList.luzBaixa = Convert.ToString(StatusLuzBaixa.SelectedItem);
            checkList.luzFreio = Convert.ToString(StatusLuzFreio.SelectedItem);
            checkList.luzPlaca = Convert.ToString(StatusLuzPlaca.SelectedItem);
            checkList.faroletes = Convert.ToString(StatusFaroletes.SelectedItem);
            checkList.piscaAlerta = Convert.ToString(StatusPiscaAlerta.SelectedItem);
            checkList.sirene = Convert.ToString(StatusSirene.SelectedItem);
            checkList.giroflex = Convert.ToString(StatusGiroflex.SelectedItem);
            checkList.buzina = Convert.ToString(StatusBuzina.SelectedItem);
            checkList.limpadorParaBrisa = Convert.ToString(StatusLimpaParabrisa.SelectedItem);
            checkList.luzInterna = Convert.ToString(StatusLuzInterna.SelectedItem);
            checkList.luzPainel = Convert.ToString(StatusLuzPainel.SelectedItem);
            checkList.retrovisores = Convert.ToString(StatusRetrovisores.SelectedItem);
            checkList.velocimetro = Convert.ToString(StatusVelocimetro.SelectedItem);
            checkList.temperatura = Convert.ToString(StatusTemperatura.SelectedItem);
            checkList.cintoSeguranca = Convert.ToString(StatusCintoSeguranca.SelectedItem);
            checkList.kitManuntecao = Convert.ToString(StatusKitEstepe.SelectedItem);
            checkList.freioMao = Convert.ToString(StatusFreioMao.SelectedItem);
            checkList.pneuStep = Convert.ToString(StatusPneuEstepe.SelectedItem);
            checkList.pneuDianteiroDireito = Convert.ToString(StatusPneuDiantDireito.SelectedItem);
            checkList.pneuDianteiroEsquerdo = Convert.ToString(StatusPneuDiantEsquerdo.SelectedItem);
            checkList.pneuTraseiroDireito = Convert.ToString(StatusPneuTrasDireito.SelectedItem);
            checkList.pneuTraseiroEsquerdo = Convert.ToString(StatusPneuTrasEsquerdo.SelectedItem);
            checkList.limpezaVeiculo = Convert.ToString(StatusLimpezaVeiculo.SelectedItem);
            checkList.travaPortas = Convert.ToString(StatusTravaPortas.SelectedItem);
            checkList.pedais = Convert.ToString(StatusPedais.SelectedItem);
            checkList.acionadorVidros = Convert.ToString(StatusAcionadorVidro.SelectedItem);
            checkList.maca = Convert.ToString(StatusMaca.SelectedItem);
            checkList.arCondicionadoDianteiro = Convert.ToString(StatusArDianteiro.SelectedItem);
            checkList.arCondicionadoTraseiro = Convert.ToString(StatusArTraseiro.SelectedItem);

            HttpResponseMessage resposta;

            if (checkList.idCheckList >= 1)
            {
                resposta = await clienteHTTP.PutAsJsonAsync("http://localhost:44306/api/tblCheckLists/PuttblCheckList/" + checkList.idCheckList, checkList);
            }
            else
            {

                resposta = await clienteHTTP.PostAsJsonAsync("http://localhost:44306/api/tblCheckLists/", checkList);
            }
            

            if (resposta.IsSuccessStatusCode)
            {
                await DisplayAlert("Atualizar", "Cadastro atualizado com sucesso!", "Ok");
                Navigation.PushAsync(new Pages.DiarioBordo(checkIdUser, idVeiculoLogado));
            }
            else
            {
                await DisplayAlert("Erro", "Falha ao tentar atualizar o check list!", "Ok");
            }
        }
    }
}
