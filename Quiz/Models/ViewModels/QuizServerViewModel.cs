using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizServer.Models.DTOs;
using QuizServer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizServer.Models.ViewModels
{
    public enum Vistas { Server, Quiz, Questions }

    public partial class QuizServerViewModel:ObservableObject
    {

        public ObservableCollection<Usuario> Usuarios { get; set; } = new();
        [ObservableProperty]
        Dictionary<Pregunta, List<Respuesta>> respuestas = new Dictionary<Pregunta, List<Respuesta>>();
        [ObservableProperty]
        Pregunta preguntaSeleccionada = new Pregunta() { Time = 10, Texto = "¿Cuál es la capital de Francia?", Respuestas = new string[] { "París", "Londres", "Berlín", "Madrid" }, Respuesta = "A" };


        QuizServerService quizServerService = new QuizServerService();




        public QuizServerViewModel()
        {
            preguntaSeleccionada = new Pregunta() { Time = 10, Texto = "¿Cuál es la capital de Francia?", Respuestas = new string[] { "París", "Londres", "Berlín", "Madrid" }, Respuesta = "A" };
            respuestas.Add(preguntaSeleccionada, new List<Respuesta>());
            VerQuizCommand = new RelayCommand(VerQuiz);
            quizServerService.RespuestaRecibida += QuizServerService_RespuestaRecibida; ;
        }

        private void QuizServerService_RespuestaRecibida(RespuestaDTO res, string endpoint)
        {
            var usr = Usuarios.FirstOrDefault(u => u.EndPoint == endpoint);
            if (usr == null)
            {
                usr = new Usuario()
                {
                    EndPoint = endpoint,
                    Nombre = res.Respuesta,
                    Puntuacion = 0
                };
                Usuarios.Add(usr);
            }
            else if (Respuestas[preguntaSeleccionada].FirstOrDefault(x => x.Usuario == usr.EndPoint) == null)
            {
                Respuesta respuesta = new Respuesta()
                {
                    Usuario = usr.EndPoint,
                    RespuestaTexto = res.Respuesta
                };
                respuestas[preguntaSeleccionada].Add(respuesta);
            }
        }
        [RelayCommand]
        void NuevaPregunta()
        {
            Pregunta pregunta = new Pregunta()
            {
                Time = 10,
                Texto = "¿Cuál es la capital de Francia?",
                Respuestas = new string[] { "París", "Londres", "Berlín", "Madrid" },
                Respuesta = "A"
            };
            Respuestas.Add(pregunta,new List<Respuesta>());
        }










        #region Navegacion
        [ObservableProperty]
        Vistas vista  = Vistas.Questions;


        public void VerQuiz()
        {
            Vista = Vistas.Questions;

        }
        public ICommand VerQuizCommand{ get; set; }
        #endregion






    }
}
