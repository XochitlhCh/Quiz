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
        UdpClient client;

        public QuizServerService()
        {
            listener = new(65000);
            client = new();
            listener.EnableBroadcast = true;

            //RecibirRectangulos(); //Hilo principal

            Thread hilo = new(RecibirRespuestas);
            hilo.IsBackground = true;
            hilo.Start();
        }
        public event Action<RespuestaDTO,String>? RespuestaRecibida;

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
                    RespuestaRecibida?.Invoke(res,client.Client.RemoteEndPoint.ToString());
                }
            }
        }
    }
}
