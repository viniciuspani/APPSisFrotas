using System;
using System.Collections.Generic;
using System.Text;

namespace APPSisFortasV1.Modelo
{
    class DiarioBordoModel
    {
        public int idDiarioBordo { get; set; }
        public int idVeiculo { get; set; }
        public int idEstabelecimento { get; set; }
        public int idUser { get; set; }
        public DateTime dataDia { get; set; }
        public DateTime horarioInicial { get; set; }
        public DateTime horarioFinal { get; set; }
        public Int64 kmInicial { get; set; }
        public Int64 kmFinal { get; set; }
        public byte[] imagemKm { get; set; }
    }
}
