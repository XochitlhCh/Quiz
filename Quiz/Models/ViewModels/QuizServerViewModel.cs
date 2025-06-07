using GalaSoft.MvvmLight.Command;
using QuizServer.Models.DTOs;
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
    public enum Vistas { Server, Quiz }

    public class QuizServerViewModel:INotifyPropertyChanged
    {

        public ObservableCollection<PreguntaDTO> Preguntas { get; set; } = new();

        public QuizServerViewModel()
        {
            VerQuizCommand = new RelayCommand(VerQuiz);   
        }


        #region Navegacion
        public Vistas Vista { get; set; } = Vistas.Server;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void VerQuiz()
        {
            Vista = Vistas.Quiz;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

        }
        public ICommand VerQuizCommand{ get; set; }
        #endregion






    }
}
