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
        public ObservableCollection<PreguntaDTO> Preguntas { get; set; } = new();

        public QuizServerViewModel()
        {
            
        }






    }
}
