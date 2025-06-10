using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizClient.Services;

namespace QuizClient.Models.ViewModels
{
    public enum Vistas { Home, Game }

    public partial class QuizClientViewModel:ObservableObject
    {
        public ClientService client = new();
        [ObservableProperty]
        string nombreUsuario;
        [ObservableProperty]
        Vistas vista = Vistas.Home;
        [ObservableProperty]
        string id;


        public QuizClientViewModel()
        {
            id = Guid.NewGuid().ToString().Substring(0, 5);
            nombreUsuario = "Usuario";

        }

        [RelayCommand]
        public void Registrarse()
        {
            nombreUsuario= nombreUsuario+id;
            client.EnviarRespuesta(nombreUsuario);
            Vista = Vistas.Game;
        }
        [RelayCommand]
        public void EnviarRespuesta(string respuesta)
        {
            client.EnviarRespuesta(respuesta);
        }



    }
}
