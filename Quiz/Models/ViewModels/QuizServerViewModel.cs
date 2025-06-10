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
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace QuizServer.Models.ViewModels
{
    public enum Vistas { Server, Quiz, Questions, Espera, Juego, Asiertos, Resultados }

    public partial class QuizServerViewModel:ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Usuario> usuarios  = new();
        [ObservableProperty]
        Dictionary<Pregunta, List<Respuesta>> respuestas = new Dictionary<Pregunta, List<Respuesta>>();
        [ObservableProperty]
        public Pregunta preguntaSeleccionada = new Pregunta() { Time = 10, Texto = "¿Cuál es la capital de Francia?", Respuestas = new string[] { "París", "Londres", "Berlín", "Madrid" }, Respuesta = 0 };
        [ObservableProperty]
        bool showAnswers = false;
        [ObservableProperty]
        bool showAnswer = false;
        [ObservableProperty]
        ObservableCollection<Usuario> usuariosRespondieron = new ObservableCollection<Usuario>();



        QuizServerService quizServerService = new QuizServerService();



        [ObservableProperty]
        private string tiempoTexto;


        public QuizServerViewModel()
        {
            preguntaSeleccionada = new Pregunta() { Time = 10, Texto = "¿Cuál es la capital de Francia?", Respuestas = new string[] { "París", "Londres", "Berlín", "Madrid" }, Respuesta = 0 };
            respuestas.Add(preguntaSeleccionada, new List<Respuesta>());
            VerQuizCommand = new RelayCommand(VerQuiz);
            quizServerService.RespuestaRecibida += QuizServerService_RespuestaRecibida;



            TiempoTexto = "00:00";
           


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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Usuarios.Add(usr);
                });
            }
            else if (Respuestas[preguntaSeleccionada].FirstOrDefault(x => x.Usuario == usr.EndPoint) == null)
            {
                Respuesta respuesta = new Respuesta()
                {
                    Usuario = usr.EndPoint,
                    RespuestaTexto = res.Respuesta
                };
                Respuestas[preguntaSeleccionada].Add(respuesta);
                

                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (preguntaSeleccionada.Respuesta == int.Parse( res.Respuesta))
                    {
                        usr.Puntuacion++;
                    }
                    UsuariosRespondieron.Add(usr);
                    

                });
            }
        }




        // switch
        //{
        //    0 => "A",
        //    1 => "B",
        //    2 => "C",
        //    3 => "D",
        //    _ => "A"
        //}
        [RelayCommand]
        public void NuevaPregunta()
        {
            string respuesta = preguntaSeleccionada.Respuestas[0];
            PreguntaSeleccionada.Respuestas = preguntaSeleccionada.Respuestas.OrderBy(x => Guid.NewGuid()).ToArray();
            Pregunta pregunta = new Pregunta()
            {
                Time = preguntaSeleccionada.Time,
                Texto = preguntaSeleccionada.Texto,
                Respuestas = preguntaSeleccionada.Respuestas,
                Respuesta = Array.IndexOf(preguntaSeleccionada.Respuestas, respuesta)
            };
            Respuestas.Add(pregunta,new List<Respuesta>());
            PreguntaSeleccionada.Texto = "";
            PreguntaSeleccionada.Time = 10;
            PreguntaSeleccionada.Respuestas = new string[] { "", "", "", "" };
            PreguntaSeleccionada.Respuesta = 0;
            
        }





        [RelayCommand]
        public void EsperarJugadores()
        {
            quizServerService.RecibirUusarios();
            Vista = Vistas.Espera;
        }

        [RelayCommand]
        public async void Iniciar()
        {
            foreach (var question in Respuestas)
            {
                PreguntaSeleccionada = question.Key;
                Vista = Vistas.Juego;
                for (int i = 5; i >= 0; i--)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        TiempoTexto = TimeSpan.FromSeconds(i).ToString(@"mm\:ss");
                    });
                    await Task.Delay(1000);
                }

                ShowAnswers = true;
                for (int i = preguntaSeleccionada.Time; i >= 0; i--)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        TiempoTexto = TimeSpan.FromSeconds(i).ToString(@"mm\:ss");
                    });
                    await Task.Delay(1000);
                }
                ShowAnswer = true;
                for (int i = 5; i >= 0; i--)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        TiempoTexto = TimeSpan.FromSeconds(i).ToString(@"mm\:ss");
                    });
                    await Task.Delay(1000);
                }

                ShowAnswers = false;
                ShowAnswer = false;


                Application.Current.Dispatcher.Invoke(() =>
                {
                    var ordenados = usuarios.OrderByDescending(u => u.Puntuacion).ToList();
                    Usuarios.Clear();
                    foreach (var usuario in ordenados)
                    {
                        Usuarios.Add(usuario);
                    }
                });
                
                Vista = Vistas.Asiertos;
                UsuariosRespondieron.Clear();
                for (int i = 5; i >= 0; i--)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        TiempoTexto = TimeSpan.FromSeconds(i).ToString(@"mm\:ss");
                    });
                    await Task.Delay(1000);
                }
                
               



            }
            Vista = Vistas.Asiertos;

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
