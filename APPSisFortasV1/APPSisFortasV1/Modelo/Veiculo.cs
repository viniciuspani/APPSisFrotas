using System;
using System.Collections.Generic;
using System.Text;

namespace APPSisFortasV1.Modelo
{
    public class Veiculo
    {
        public int idVeiculo { get; set; }
        public int codVecAd { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string chassi { get; set; }
        public string placa { get; set; }
        public int anoFab { get; set; }
        public string vinculoVec { get; set; }
        public string statusCons { get; set; }
        public Int64 km { get; set; }
        public int emManuntencao { get; set; }
        
    }
}
