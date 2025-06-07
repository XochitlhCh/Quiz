using QuizServer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizServer.Models.ViewModels
{
    public class QuizServerViewModel
    {
        public ObservableCollection<Pregunta> Preguntas { get; set; } = new();



        public QuizServerViewModel()
        {
            // Initialize with some sample data
            Preguntas.Add(new Pregunta
            {
                Time = 30,
                Texto = "What is the capital of France?",
                Respuestas = new[] { "Paris", "London", "Berlin", "Madrid" },
                Respuesta = "Paris"
            });
            Preguntas.Add(new Pregunta
            {
                Time = 20,
                Texto = "What is 2 + 2?",
                Respuestas = new[] { "3", "4", "5", "6" },
                Respuesta = "4"
            });
        }

        public void AddPregunta(Pregunta pregunta)
        {
            if (pregunta != null)
            {
                Preguntas.Add(pregunta);
            }
        }

        public void RemovePregunta(Pregunta pregunta)
        {
            if (pregunta != null && Preguntas.Contains(pregunta))
            {
                Preguntas.Remove(pregunta);
            }
        }

        public void ClearPreguntas()
        {
            Preguntas.Clear();
        }










    }
}
