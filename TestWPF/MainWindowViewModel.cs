using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestWPF
{
    class MainWindowViewModel
    {
        public ObservableCollection<Student> Students { get; } = new ObservableCollection<Student>()
        {
            new Student
            {
                Answers = 0,
                Errors = 0,
                Questions = 3,
                Rating = 1,
                RemoteId = "4 - 8681",
                Right = 100,
                RatingAll = 1,
                Time = "13.27",
                Wins = 100,
            },

            new Student
            {
                Answers = 1,
                Errors = 1,
                Questions = 3,
                Rating = 2,
                RemoteId = "3 - 8681",
                Right = 90,
                RatingAll = 2,
                Time = "16.27",
                Wins = 90,
            },

            new Student
            {
                Answers = 1,
                Errors = 1,
                Questions = 5,
                Rating = 2,
                RemoteId = "2 - 8681",
                Right = 80,
                RatingAll = 2,
                Time = "11.27",
                Wins = 90,
            },

            new Student
            {
                Answers = 2,
                Errors = 2,
                Questions = 5,
                Rating = 3,
                RemoteId = "1 - 8681",
                Right = 80,
                RatingAll = 3,
                Time = "11.27",
                Wins = 75,
            },
        };
        public MainWindowViewModel()
        {
            
        }

        public string TestData { get; set; } = "Тестовые данные!";

        public class Erros
        {
            public string Error1 { get; set; }
            public string Error2 { get; set; }
        }

        public List<Erros> Errors { get; } = new List<Erros>()
        {
            new Erros()
            {
                Error1 = "Овария",
                Error2 = "Будта",
            },

            new Erros()
            {
                Error1 = "Бepeх",
                Error2 = "",
            }
        };
    }
}
