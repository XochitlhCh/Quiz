using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizServer.Models.DTOs;

namespace QuizClient.Services
{
    public class ClientService
    {
        UdpClient client;
        public ClientService()
        {
            client = new UdpClient();
            client.EnableBroadcast = true;
        }

        public void EnviarRespuesta(string respuesta)
        {
            RespuestaDTO res = new RespuestaDTO()
            {
                Respuesta = respuesta
            };
            var endpoint = new IPEndPoint(IPAddress.Broadcast, 65000);
            var json = JsonSerializer.Serialize(res);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            client.Send(buffer, buffer.Length, endpoint);
        }



    }
}
