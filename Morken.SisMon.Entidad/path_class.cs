using System;

namespace Morken.SisMon.Entidad
{
    public class path_class
    {
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public coord_struct[] Coordenadas { get; set; }

        public path_class(int int_pOrden, string str_pNombre, string str_pCoordenada)
        {
            this.Orden = int_pOrden;
            this.Nombre = str_pNombre;
            string[] array = str_pCoordenada.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            this.Coordenadas = new coord_struct[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                this.Coordenadas[i].orden = i;
                string[] array2 = array[i].Split(new char[] { ',' });
                this.Coordenadas[i].lon = double.Parse(array2[0]);
                this.Coordenadas[i].lat = double.Parse(array2[1]);
                this.Coordenadas[i].unknown = double.Parse(array2[2]);
            }
        }

        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double getDistance(double dbl_pLongitudOrigen, double dbl_pLatitudOrigen,
            double dbl_pLongitudDestino, double dbl_pLatitudDestino)
        {
            int Radius = 6371000; //Radio de la tierra
                                  //Distancia = 6371 * ACOS(COS(Lat1) * COS(Lat2) * COS(Long2 - Long1) + SIN(Lat1) * SIN(Lat2));

            double dbl_LatitudOrigen = dbl_pLatitudOrigen / 1E6;
            double dbl_LatitudDestino = dbl_pLatitudDestino / 1E6;
            double dbl_LongitudOrigen = dbl_pLongitudOrigen / 1E6;
            double dbl_LongitudDestino = dbl_pLongitudDestino / 1E6;

            double dbl_DiferenciaLatitud = ConvertToRadians(dbl_LatitudDestino - dbl_LatitudOrigen);
            double dbl_DiferenciaLongitud = ConvertToRadians(dbl_LongitudDestino - dbl_LongitudOrigen);

            double a =
                Math.Sin(dbl_DiferenciaLatitud / 2) *
                Math.Sin(dbl_DiferenciaLatitud / 2) +
                Math.Cos(ConvertToRadians(dbl_LatitudOrigen)) *
                Math.Cos(ConvertToRadians(dbl_LatitudDestino)) *
                Math.Sin(dbl_DiferenciaLongitud / 2) *
                Math.Sin(dbl_DiferenciaLongitud / 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));
            double d = Radius * c * 1000000;
            return (d);
        }
    }
}
