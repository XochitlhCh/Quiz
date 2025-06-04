using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizServer.Models.DTOs
{
    public class PreguntaDTO
    {
        public int Time { get; set; }
        public string Pregunta { get; set; }
        public string[] Respuestas { get; set; } = new string[4];
        public string Respuesta { get; set; } 

    }
}
