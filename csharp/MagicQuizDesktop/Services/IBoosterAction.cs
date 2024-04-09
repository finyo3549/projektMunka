using MagicQuizDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicQuizDesktop.Services
{
    public interface IBoosterAction
    {
        void Execute(List<Question> questions, ref int currentQuestionIndex, Action updateUiCallback);
    }
}
