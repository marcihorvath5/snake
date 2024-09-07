using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Snake.Models
{
    /// <summary>
    /// A játék indításáért felelő osztály
    /// </summary>
    public class Arena
    {
        private TimeSpan PlayTime;
        private DispatcherTimer GameTimer;
        private MainWindow MainWindow;

        public Arena(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;
        }

        /// <summary>
        /// A játék indítása
        /// </summary>
        public void Start()
        {
            // Pontszámok nullázása
            PlayTime = TimeSpan.FromSeconds(0);
            GameTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, ItIsTimeToShow, Application.Current.Dispatcher);

        }

        /// <summary>
        /// A játék megállítása
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        private void ItIsTimeToShow(object? sender, EventArgs e)
        {
            //Frissíteni a játékidőt
            PlayTime = PlayTime.Add(TimeSpan.FromMilliseconds(100));

            // Kiírni a képernyőre
            MainWindow.LabelPlayTime.Content = $"Játék idő: {PlayTime.ToString("mm\\:ss")}";
        }
    }
}
