using System;
using System.Collections.Generic;
using System.Text;
using APPSisFortasV1.Modelo;

namespace APPSisFortasV1.Modelo
{
     public class Usuario
    {

        public int idUser { get; set; }
        public string nmUser { get; set; }
        public string cpf { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public string cep { get; set; }
        public string endUser { get; set; }
        public int numCasa { get; set; }
        public string bairroUser { get; set; }
        public string cidade { get; set; }        
        public string estado { get; set; }
        public string tpSangue { get; set; }
        public string escalaMotorista { get; set; }
        public string tpUser { get; set; }
        public string senhaUser { get; set; }
        
    }
}
