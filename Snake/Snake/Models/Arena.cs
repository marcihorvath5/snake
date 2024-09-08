using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Snake.Models
{
    /// <summary>
    /// A játék indításáért felelő osztály
    /// </summary>
    public class Arena
    {
        /// <summary>
        /// A játék ideje
        /// </summary>
        private TimeSpan PlayTime;
        /// <summary>
        /// A játék óra jelét adó időzítő
        /// </summary>
        private DispatcherTimer GameTimer;
        /// <summary>
        /// Képernyő amin a játék fut    
        /// </summary>
        private MainWindow MainWindow;
        /// <summary>
        /// Az aktuális játék pontszáma
        /// </summary>
        private int Points;
        /// <summary>
        /// Megevett ételek száma
        /// </summary>
        private int EatenMealsCount;
        /// <summary>
        /// Az aktuális játékban szereplő kígyó
        /// </summary>
        private Snake Snake;

        /// <summary>
        /// Arena konstruktor létrehozáskor megkapja a megjelenítő ablakot
        /// </summary>
        public Arena(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;

            SetNewGameCounters();
            ShowGameCounters();
        }

        /// <summary>
        /// A játék indítása
        /// </summary>
        public void Start()
        {
            SetNewGameCounters();
            GameTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, ItIsTimeToShow, Application.Current.Dispatcher);

        }

        /// <summary>
        /// A játék megállítása
        /// </summary>
        public void Stop()
        {
            GameTimer.Stop();
        }

        /// <summary>
        /// Ha leütjük valamelyik key gombot itt megérkezik
        /// És a key parameter jelzi hogy melyik
        /// </summary>
        internal void Keydown(Key key)
        {
            // A leütött billentyű jelzi merre kéne mennie a kígyónak
            switch (key)
            {
                case Key.Left:
                    Snake.Directon = SnakeDirections.Left;
                    break;
                case Key.Up:
                    Snake.Directon = SnakeDirections.Up;
                    break;
                case Key.Right:
                    Snake.Directon = SnakeDirections.Right;
                    break;
                case Key.Down:
                    Snake.Directon = SnakeDirections.Down;
                    break;
                default:
                    throw new Exception($"Erre a gombra  nem vagyunk felkészülve {key}");
            }
        }

        /// <summary>
        /// Ha a játéknak eljön a következő órajele akkor ezt a függvényt hívjuk meg 
        /// </summary>
        private void ItIsTimeToShow(object? sender, EventArgs e)
        {
            //Frissíteni a játékidőt
            PlayTime = PlayTime.Add(TimeSpan.FromMilliseconds(100));

            ShowGameCounters();
        }

        /// <summary>
        /// Megjeleníti a játék számlálóit
        /// </summary>
        private void ShowGameCounters()
        {
            // Kiírni a képernyőre
            MainWindow.LabelPlayTime.Content = $"Játék idő: {PlayTime.ToString("mm\\:ss")}";
            MainWindow.LabelPoints.Content = $"Pontszám: {Points}";
            MainWindow.LabelEatenMealCount.Content = $"Megevett ételek: {EatenMealsCount}";
            MainWindow.LabelSnakeLength.Content = $"Kígyó hossza: {Snake.Length}";
            MainWindow.LabelKeyDown.Content = $"{Snake.Directon}";
        }

        /// <summary>
        /// Beállítja a játék alapállapotának megfelelő számolókat
        /// </summary>
        private void SetNewGameCounters()
        {
            // Pontszámok nullázása
            PlayTime = TimeSpan.FromSeconds(0);
            Points = 0;
            EatenMealsCount = 0;
            Snake = new Snake();
        }
    }
}
