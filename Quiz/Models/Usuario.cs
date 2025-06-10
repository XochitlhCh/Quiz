using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace QuizServer.Models
{
    public partial class Usuario: ObservableObject
    {
        
        public string EndPoint { get; set; }
        public string Nombre { get; set; }
        [ObservableProperty]
        int puntuacion;
    }
}
