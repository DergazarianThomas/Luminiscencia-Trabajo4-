using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Backend
{
    public class Calculos
    {
        // definicion de variables 

        private double resK;
        private double resFlujLum;
        private double resNumLamp;
        private double resIlum;
        private double anchoLamp;
        private double largoLamp;
        private double sepLampLargo;
        private double sepLampAncho;
        private double sepParedLargo;
        private double sepParedAncho;
        private double LampTotal;
        private double PotTotal;

        




        // calculos de IndiceK
        public string IndiceK(string largo, string ancho, string altura)
        {
            Double Largo = Convert.ToDouble(largo);
            Double Ancho = Convert.ToDouble(ancho);
            Double Altura = Convert.ToDouble(altura);


            resK = Largo * Ancho / (Altura * (Largo + Ancho));

            return Convert.ToString(resK);
        }

        // Calculos de Flujo luminoso

        public string FlujoLuminoso(string largo, string ancho, string ilumSol, string mant, string utiliz)
        {

            Double Largo = Convert.ToDouble(largo);
            Double Ancho = Convert.ToDouble(ancho);
            Double IlumSol = Convert.ToDouble(ilumSol);
            Double Mant = Convert.ToDouble(mant);
            Double Utiliz = Convert.ToDouble(utiliz);

            resFlujLum = (Largo * Ancho * IlumSol) / (Utiliz * Mant);

            return Convert.ToString(resFlujLum);

        }

        // Calculo de numero de lamparas

        public string NumLamp(string lumenes)
        {
            Double Lumenes = Convert.ToDouble(lumenes);

            resNumLamp = Math.Ceiling(resFlujLum / Lumenes);

            return Convert.ToString(resNumLamp);
        }

        // Calculo de Iluminancia

        public string Iluminancia(string potencia, string lumenes, string mant, string utiliz, string largo, string ancho)
        {
            Double Largo = Convert.ToDouble(largo);
            Double Ancho = Convert.ToDouble(ancho);
            Double Mant = Convert.ToDouble(mant);
            Double Utiliz = Convert.ToDouble(utiliz);
            Double Lumenes = Convert.ToDouble(lumenes);
            Double Potencia = Convert.ToDouble(potencia);

            resIlum = (resNumLamp * Lumenes * Utiliz * Mant) / (Largo * Ancho);

            return Convert.ToString(resIlum);
        }

        // Calculos varios sobre lamparas

        public string[] LampFinales(string ancho, string largo, string nlamp, string potencia)
        {
            string [] LampFinales = new string[8];

            Double Largo = Convert.ToDouble(largo);
            Double Ancho = Convert.ToDouble(ancho);
            Double NLamp = Convert.ToDouble(nlamp);
            Double Potencia = Convert.ToDouble(potencia);

            anchoLamp = Math.Sqrt((NLamp / Largo) * Ancho);

            largoLamp = Math.Ceiling(anchoLamp * (Largo / Ancho));


            sepLampLargo = Largo / largoLamp;

            sepLampAncho = Ancho / Math.Ceiling(anchoLamp);

            sepParedLargo = sepLampLargo / 2;

            sepParedAncho = sepLampAncho / 2;

            LampTotal = largoLamp * Math.Ceiling(anchoLamp);

            PotTotal = LampTotal * Potencia;
            
            LampFinales[0] = Convert.ToString(Math.Ceiling(anchoLamp)); 

            LampFinales[1] = Convert.ToString(largoLamp); 

            LampFinales[2] = Convert.ToString(sepLampLargo); 

            LampFinales[3] = Convert.ToString(sepLampAncho); 

            LampFinales[4] = Convert.ToString(sepParedLargo);

            LampFinales[5] = Convert.ToString(sepParedAncho);

            LampFinales[6] = Convert.ToString(LampTotal);

            LampFinales[7] = Convert.ToString(PotTotal);

            return LampFinales;


        }



        
       


     
       }


    }







