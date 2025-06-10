using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace QuizServer.Models.DTOs
{
    public partial class Pregunta:ObservableObject
    {
        [ObservableProperty]
        int time;
        [ObservableProperty]

        string texto;
        [ObservableProperty]

        string[] respuestas = new string[4];
        [ObservableProperty]

        int respuesta;
    }
}
