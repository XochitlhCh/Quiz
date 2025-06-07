using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuizServer.Models
{
    public class Usuario
    {
        public EndPoint EndPoint { get; set; }
        public string Nombre { get; set; }
        public int Puntuacion { get; set; }
    }
}
