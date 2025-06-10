using QuizServer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizServer.Services
{
    public class QuizServerService
    {
        UdpClient listener;
        Thread? hilo;

        public QuizServerService()
        {
            listener = new(65000);
            listener.EnableBroadcast = true;

            //RecibirRectangulos(); //Hilo principal

            hilo = new(RecibirRespuestas);
            hilo.IsBackground = true;
            hilo.Start();
        
        }


        public event Action<RespuestaDTO,String>? RespuestaRecibida;


        public async Task RecibirUusarios()
        {

        }



        private void RecibirRespuestas()
        {
            while (true)
            {
                IPEndPoint cliente = new(IPAddress.Any, 0);

                //Un buffer es un arreglo binario donde queda
                //la información transferida o por transferir.
                byte[] buffer = listener.Receive(ref cliente);


                var json = Encoding.UTF8.GetString(buffer);

                var res = JsonSerializer.Deserialize<RespuestaDTO>(json);

                if (res != null)
                {
                    RespuestaRecibida?.Invoke(res,cliente.ToString());
                }
            }
        }

        



    }
}
