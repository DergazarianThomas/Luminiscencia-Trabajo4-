using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backend
{
    public class Habitacion
    {
        string nombre;
        double ancho;
        double largo;
        double altura;
        double ilumSol;
        double indiceK;
        double reflexTecho;
        double reflexPared;
        double reflexSuelo;
        double coefMant;
        double coefUtiliz;
        double flujoLuminoso;
        string nombreLamp;
        double potencia;
        double lumenes;
        double nDeLuminariasOblg;
        double iluminancia;
        double nDeLumianariasTotal;
        double nLampAncho;
        double nLampLargo;
        double sepEntreLampAncho;
        double sepEntreLampLargo;
        double sepEntreLampParedAncho;
        double sepEntreLampParedLargo;
        double potenciaTotal;

        public Habitacion() { }

        public string Nombre { get;set; }
        public double Ancho { get;set; }
        public double Largo { get;set; }
        public double Altura { get;set; }
        public double IlumSol { get; set; }
        public double IndiceK { get;set; }
        public double ReflexTecho { get; set; }
        public double ReflexPared { get; set; }
        public double ReflexSuelo { get; set; }
        public double CoefMant { get; set; }
        public double CoefUtiliz{ get; set; }
        public double FlujoLuminoso{ get; set; }
        public string NombreLamp{ get; set; }
        public double Potencia{ get; set; }
        public double Lumenes{ get; set; }
        public double NDeLuminariasOblg{ get; set; }
        public double Iluminancia{ get; set; }
        public double NDeLuminariasTotal { get; set; }
        public double NLampAncho{ get; set; }
        public double NLampLargo{ get; set; }
        public double SepEntreLampAncho{ get; set; }
        public double SepEntreLampLargo{ get; set; }
        public double SepEntreLampParedAncho{ get; set; }
        public double SepEntreLampParedLargo{ get; set; }
        public double PotenciaTotal { get; set; }

    }
}