using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
        /// Az ételek aktuális listája amit a kígyó megehet
        /// </summary>
        private List<GamePoint> Meals;

        /// <summary>
        /// Véletlenszámgenerátor
        /// </summary>
        private Random randomNumberGenerator = new Random();

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
            SetSnakeForStart();
            SetMealsForStart();
            if (GameTimer != null)
            {// Hamár korábban elindítottuk a játékot akkor ezt most megállítjuk
                GameTimer.Stop();
            }
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

        private void SetSnakeForStart()
        {
            Snake = new Snake();
            Snake.Gamepoints = new List<GamePoint>();

            // kígyó fejét generáljuk
            var head = GetRandomGamePoint();

            Snake.Gamepoints.Add(head);            

            // kígyó elhelyezés
            // vízszintesen rajzolunk ha az x 10-nél nagyobb akkor balra ha kissebb akkor jobbra

            ShowSnakeHead(head);

            for (int i = 0; i < ArenaSettings.SnakeCountForStart; i++)
            {
                GamePoint gamePoint;
                if (head.X <= 10)
                { // jobbra nyúlik a kígyó
                    gamePoint = new GamePoint(head.X + i + 1, head.Y);
                }
                else
                { // balra nyúlik
                     gamePoint = new GamePoint(head.X - i - 1, head.Y);
                }

                Snake.Gamepoints.Add(gamePoint);
                ShowSnakeTail(gamePoint);
            }
        }

        private void SetMealsForStart()
        {
           
           Meals = new List<GamePoint>();

            // Ez így már műödik de:
            // 1.Kibányászást függvénybe kell szervezni
            // 2. nem kezeljük ha egy olyan helyre tesszük a csillagot ahol már van
            while (Meals.Count < ArenaSettings.MealsCountForStart)
            {
                GamePoint meal = GetRandomGamePoint();

                // Csak akkor továbmenni ha az étel még nincs a táblán és nem ütközik a kígyóval
                if (!Meals.Any(gamePoint => gamePoint.X == meal.X && gamePoint.Y == meal.Y) && !Snake.Gamepoints.Any(gamePoint => gamePoint.X == meal.X && gamePoint.Y == meal.Y))
                {
                    // A megjelenítést és a hozzáadást csak akkor hagyja végre ha az if true-t ad vissza-t ad vissza
                };

                ShowMeal(meal);

                // hozzáadnia listához
                Meals.Add(meal);

            }

        }

        /// <summary>
        /// Kijelöl a táblán egy véletlenszerű pontot
        /// </summary>
        /// <returns></returns>
        private GamePoint GetRandomGamePoint()
        {
            var x = randomNumberGenerator.Next(1, ArenaSettings.maxX + 1);
            var y = randomNumberGenerator.Next(1, ArenaSettings.maxY + 1);

            var gamePoint = new GamePoint(x: x, y: y);
            return gamePoint;
        }

        private void ShowMeal(GamePoint meal)
        {
            // megjelenítés
            var child = GetGridArenaCell(meal);

            // Children gyűjtemény uielementekből áll ahhoz hogy kibányásszuk a fontawesome vezérlőt elkell kérnünk a változózól
            child.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            child.Foreground = Brushes.Red;
            child.Spin = true;
            child.SpinDuration = 5;
        }

        private void ShowSnakeHead(GamePoint head)
        {
            // megjelenítés
            var child = GetGridArenaCell(head);

            // Children gyűjtemény uielementekből áll ahhoz hogy kibányásszuk a fontawesome vezérlőt elkell kérnünk a változózól
            child.Icon = FontAwesome.WPF.FontAwesomeIcon.Circle;
            child.Foreground = Brushes.Green;
        }

        /// <summary>
        /// A kígyó farkának megjelenítése
        /// </summary>
        /// <param name="head"></param>
        private void ShowSnakeTail(GamePoint tail)
        {
            // megjelenítés
            var child = GetGridArenaCell(tail);

            // Children gyűjtemény uielementekből áll ahhoz hogy kibányásszuk a fontawesome vezérlőt elkell kérnünk a változózól
            child.Icon = FontAwesome.WPF.FontAwesomeIcon.Circle;
            child.Foreground = Brushes.Blue;

        }

        private FontAwesome.WPF.ImageAwesome GetGridArenaCell(GamePoint gamePoint)
        {
            var child = MainWindow.GridArena.Children[(gamePoint.Y - 1) * ArenaSettings.maxX + (gamePoint.X - 1)];

            var cell = (FontAwesome.WPF.ImageAwesome)child;

            return cell;
        }
    }
}
