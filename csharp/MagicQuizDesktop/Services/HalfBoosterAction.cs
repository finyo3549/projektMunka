using MagicQuizDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MagicQuizDesktop.Services
{
    public class HalfBoosterAction : IBoosterAction
    {
        public void Execute(List<Question> questions, ref int currentQuestionIndex, Action updateUiCallback)
        {
            var currentAnswers = questions[currentQuestionIndex].Answers;
            var incorrectAnswers = currentAnswers.Where(a => !a.IsCorrect).ToList();

            // Vegyük csak a helytelen válaszokat, és válasszunk belőlük véletlenszerűen kettőt eltávolítani
            Random rand = new Random();
            while (incorrectAnswers.Count > 2)
            {
                var toRemoveIndex = rand.Next(incorrectAnswers.Count);
                incorrectAnswers[toRemoveIndex].IsActive = false; // Deaktiváljuk a választ
                incorrectAnswers.RemoveAt(toRemoveIndex);
            }

            // Itt kellene a logikát implementálnod, hogy a UI-on deaktiváljuk azokat a válaszokat
            // Ez általában egy Event vagy egy Callback formájában történik
            updateUiCallback();
            MessageBox.Show("A felező booster effektusa alkalmazva.");
        }
    }
}
