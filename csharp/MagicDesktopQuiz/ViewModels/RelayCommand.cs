using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MagicQuizDesktop.ViewModels
{
    internal class RelayCommand : ICommand
    {
        // Az akció, amit a parancs végrehajtásakor kell futtatni.
        private Action<object> execute;

        // Egy opcionális feltétel, amely meghatározza, hogy a parancs végrehajtható-e.
        private Func<object, bool> canExecute;

        /// <summary>
        /// Esemény, amely akkor váltódik ki, amikor változik a parancs végrehajthatóságának állapota.
        /// A CommandManager.RequerySuggested eseményre feliratkozva biztosítja, hogy a CanExecute metódus
        /// automatikusan újraértékelődjön bizonyos UI események hatására, pl. billentyűzet vagy egér interakciók.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Konstruktor a RelayCommand létrehozásához.
        /// </summary>
        /// <param name="execute">Az akció, amit a parancs végrehajt.</param>
        /// <param name="canExecute">Egy opcionális feltétel, ami megmondja, hogy a parancs végrehajtható-e.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Meghatározza, hogy a parancs végrehajtható-e az adott paraméterrel.
        /// </summary>
        /// <param name="parameter">A parancshoz kapcsolódó paraméter.</param>
        /// <returns>Igaz, ha a parancs végrehajtható, egyébként hamis.</returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        /// Végrehajtja a parancshoz kötött akciót.
        /// </summary>
        /// <param name="parameter">A parancshoz kapcsolódó paraméter.</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
