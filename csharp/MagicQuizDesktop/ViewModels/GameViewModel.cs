using MagicQuizDesktop.Commands;
using MagicQuizDesktop.Models;
using MagicQuizDesktop.Services;
using MagicQuizDesktop.View.Pages;
using MagicQuizDesktop.View.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Formats.Asn1.AsnWriter;

namespace MagicQuizDesktop.ViewModels
{
    internal class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChanged(nameof(CurrentUser));
                    // Itt frissítheted a userId-t is, ha szükséges
                }
            }
        }


        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private string _topicName;
        public string TopicName
        {
            get { return _topicName; }
            set
            {
                _topicName = value;
                OnPropertyChanged(nameof(TopicName));
            }
        }

        private string _questionText;
        public string QuestionText
        {
            get { return _questionText; }
            set
            {
                _questionText = value;
                OnPropertyChanged(nameof(QuestionText));
            }
        }

        private Answer _answer1;
        public Answer Answer1
        {
            get { return _answer1; }
            set
            {
                if (_answer1 != value)
                {
                    _answer1 = value;
                    OnPropertyChanged(nameof(Answer1));
                }
            }
        }

        private Answer _answer2;
        public Answer Answer2
        {
            get { return _answer2; }
            set
            {
                if (_answer2 != value)
                {
                    _answer2 = value;
                    OnPropertyChanged(nameof(Answer2));
                }
            }
        }

        private Answer _answer3;
        public Answer Answer3
        {
            get { return _answer3; }
            set
            {
                if (_answer3 != value)
                {
                    _answer3 = value;
                    OnPropertyChanged(nameof(Answer3));
                }
            }
        }

        private Answer _answer4;
        public Answer Answer4
        {
            get { return _answer4; }
            set
            {
                if (_answer4 != value)
                {
                    _answer4 = value;
                    OnPropertyChanged(nameof(Answer4));
                }
            }
        }

        private bool halfBoosterStatus;
        public bool HalfBoosterStatus
        {
            get { return halfBoosterStatus; }
            set
            {
                halfBoosterStatus = value;
                OnPropertyChanged(nameof(HalfBoosterStatus));
            }
        }




        private QuizApiService _apiService = new QuizApiService();
        private List<Question> _questions;
        private List<Topic> _topics;
        private List<Answer> _answers;
        private int _currentQuestionIndex = 0;




        public ICommand UpdateDatasCommand { get; }
        public ICommand StartGameCommand { get; }
        public ICommand AnswerCommand { get; private set; }
        public ICommand CloseWindowCommand { get; }
        public ICommand HalfBoosterCommand { get; }




        public GameViewModel( User user)
        {
            CurrentUser = user; // Itt tároljuk el a felhasználót
            UpdateDatasCommand = new AsyncRelayCommand(UpdateDatas);
            StartGameCommand = new RelayCommand(_ => StartGame());
            AnswerCommand = new RelayCommand(AnswerClicked);
            CloseWindowCommand = new RelayCommand(_ => CloseWindow());
            HalfBoosterCommand = new RelayCommand(_=>ApplyHalfBoosterEffect());
            HalfBoosterStatus = false;
            _answers = new List<Answer>();
        }




        public event EventHandler RequestClose;

        private void CloseWindow()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
        private async Task UpdateDatas()
        {
            _topics = await _apiService.GetTopicsAsync();
            _questions = await _apiService.GetQuestionsWithAnswesAsync();

            if (_topics == null || _questions == null || _questions.Count == 0)
            {
                MessageBox.Show("Hiba történt az adatok frissítése közben.");
            }
            else
            {
                MessageBox.Show($"Adatok frissítve!");
            }
        }


        private void StartGame()
        {
            // Feltételezve, hogy van egy alapértelmezett felhasználói ID

            if (_questions == null || _questions.Count == 0)
            {
                MessageBox.Show("Kérlek, először frissítsd az adatokat!");
                return;
            }

            // Játékállapot inicializálása
            _score = 0;
            Score = _score;
            _currentQuestionIndex = 0;
            HalfBoosterStatus = true;
            DisplayCurrentQuestion();
        }

        private void DisplayCurrentQuestion()
        {
            if (_currentQuestionIndex < _questions.Count)
            {
                foreach (var answer in _answers)
                {
                    answer.IsActive = true;
                }
                _answers.Clear();
                var currentQuestion = _questions[_currentQuestionIndex];
                var topic = _topics.FirstOrDefault(t => t.Id == currentQuestion.TopicId);
                var topicName = topic != null ? topic.TopicName : "Ismeretlen téma";

                TopicName = $"Téma: {topicName}";
                QuestionText = currentQuestion.QuestionText;

                // Feltételezve, hogy van elegendő válasz minden kérdéshez
                Answer1 = currentQuestion.Answers.Count > 0 ? currentQuestion.Answers[0] : new Answer();
                _answers.Add(Answer1);
                Answer2 = currentQuestion.Answers.Count > 1 ? currentQuestion.Answers[1] : new Answer();
                _answers.Add(Answer2);
                Answer3 = currentQuestion.Answers.Count > 2 ? currentQuestion.Answers[2] : new Answer();
                _answers.Add(Answer3);
                Answer4 = currentQuestion.Answers.Count > 3 ? currentQuestion.Answers[3] : new Answer();
                _answers.Add(Answer4);
            }
            else
            {
                MessageBox.Show($"A játék véget ért! Pontszámod: {_score}");
                // Játék végén döntési logika
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var result = MessageBox.Show("Szeretnél még egy játékot játszani?", "Játék vége", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Új játék indítása, ha igen
                        StartGame();
                    }
                    else
                    {
                        // Esemény kiváltása az ablak bezárásához, ha nem
                        RequestClose?.Invoke(this, EventArgs.Empty);
                    }
                });


            }
        }

        private void AnswerClicked(object parameter)
        {
            if (int.TryParse(parameter.ToString(), out int answerIndex))
            {
                // Ellenőrizzük, hogy a válasz helyes-e
                var isCorrect = _questions[_currentQuestionIndex].Answers[answerIndex].IsCorrect;

                if (isCorrect)
                {
                    _score += 100;
                    MessageBox.Show("Helyes válasz!");
                }
                else
                {
                    MessageBox.Show("Helytelen válasz!");
                }

                // Növeljük a jelenlegi kérdés indexét és megjelenítjük a következő kérdést
                _currentQuestionIndex++;
                Score = _score;
                DisplayCurrentQuestion();
            }
            else
            {
                MessageBox.Show($"Nem sikerült értelmezni a választ: {parameter}");
            }
        }

        private void ApplyHalfBoosterEffect()
        {
            var currentAnswers = _questions[_currentQuestionIndex].Answers;
            var incorrectAnswers = currentAnswers.Where(a => !a.IsCorrect).ToList();

            Random rand = new Random();
            while (incorrectAnswers.Count >= 2)
            {
                var toRemove = incorrectAnswers[rand.Next(incorrectAnswers.Count)];
                foreach (var answer in _answers)
                {
                    if (toRemove == answer)
                    {
                        answer.IsActive = false;
                    }
                }
                    incorrectAnswers.Remove(toRemove);          
            }
            HalfBoosterStatus = false;
        }

    }
}
