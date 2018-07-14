using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

namespace TestSGA2018
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Presione enter para ingresar un dato");
                Console.ReadKey();
                HttpClient httpClient = new HttpClient();
                //POST - INSERTAR
                //string jsonPuesto = "{'Descripcion': 'Gerente de Mercadeo'}";
                //httpClient.PostAsync("http://localhost:21730/api/PuestosApi",new StringContent(jsonPuesto,Encoding.UTF8,"application/json"));
                //PUT - MODIFICAR
                //string jsonPuesto = "{'PuestoId':'3','Descripcion': 'Gerente Administrativo'}";
                //httpClient.PutAsync("http://localhost:21730/api/PuestosApi/3",new StringContent(jsonPuesto,Encoding.UTF8,"application/json"));
                //DELETE - ELIMINAR
                //httpClient.DeleteAsync("http://localhost:21730/api/PuestosApi/4");
                Console.WriteLine("Registro Realizado!!");
                Console.ReadKey();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
