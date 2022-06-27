using System;
using System.Collections.Generic;
using System.Text;

namespace APPSisFortasV1.Modelo
{
    public class AutenticacaoRequest
    {
        public string Cpf { get; set; }
        public string Senha { get; set; }

        public AutenticacaoRequest(string cpf, string senha)
        {
            Cpf = cpf;
            Senha = senha;
        }
    }
}
