using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizServer.Models.DTOs
{
    public class Pregunta
    {
        public int Time { get; set; }
        public string Texto { get; set; }
        public string[] Respuestas { get; set; } = new string[4];
        public string Respuesta { get; set; }
    }
}
