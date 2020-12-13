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
                Right = 3,
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
                Right = 2,
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
                Right = 4,
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
                RemoteId = "4 - 8681",
                Right = 3,
                RatingAll = 3,
                Time = "11.27",
                Wins = 75,
            },
        };
        public MainWindowViewModel()
        {
            //Students.Add(new Student
            //{
            //    Answers = 0,
            //    Errors = 0,
            //    Questions = 3,
            //    Rating = 1,
            //    RemoteId = "4 - 8681",
            //    Right = 3,
            //    RatingAll = 1,
            //    Time = "13.27",
            //    Wins = 100,
            //});

            //Students.Add(new Student
            //{
            //    Answers = 1,
            //    Errors = 1,
            //    Questions = 3,
            //    Rating = 2,
            //    RemoteId = "3 - 8681",
            //    Right = 2,
            //    RatingAll = 2,
            //    Time = "16.27",
            //    Wins = 90,
            //});

            //Students.Add(new Student
            //{
            //    Answers = 1,
            //    Errors = 1,
            //    Questions = 5,
            //    Rating = 2,
            //    RemoteId = "2 - 8681",
            //    Right = 4,
            //    RatingAll = 2,
            //    Time = "11.27",
            //    Wins = 90,
            //});

            //Students.Add(new Student
            //{
            //    Answers = 2,
            //    Errors = 2,
            //    Questions = 5,
            //    Rating = 3,
            //    RemoteId = "4 - 8681",
            //    Right = 3,
            //    RatingAll = 3,
            //    Time = "11.27",
            //    Wins = 75,
            //});
           // GetStd();
        }

        public void GetStd()
        {
            for (int i = 0; i < 5; i++)
            {
                Students.Add(new Student 
                { 
                    Answers = i,
                    Errors = i,
                    Questions = i,
                    Rating = i,
                    RatingAll = i,
                    RemoteId = $"{i}",
                    Right = i,
                    Time = $"{i}",
                    Wins = i,
                });
            }
        }
    }
}
