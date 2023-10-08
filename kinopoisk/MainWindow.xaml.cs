using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace kinopoisk
{
    public partial class MainWindow : Window
    {
        private TopFilmsResponse filmsResponse;
        private int currentPage = 1;

        public MainWindow()
        {
            InitializeComponent();
            LoadTopFilms(currentPage); // Загрузка списка фильмов при открытии окна
            MoviesListView.MouseDoubleClick += MoviesListView_MouseDoubleClick; // Добавляем обработчик события двойного клика
            TypeComboBox.SelectionChanged += TypeComboBox_SelectionChanged;
            YearComboBox.SelectionChanged += YearComboBox_SelectionChanged;
            RatingComboBox.SelectionChanged += RatingComboBox_SelectionChanged;
        }

        private void FilterMovies()
        {
            string selectedType = ((ComboBoxItem)TypeComboBox.SelectedItem).Content.ToString();
            string selectedYear = ((ComboBoxItem)YearComboBox.SelectedItem).Content.ToString();
            string selectedRating = ((ComboBoxItem)RatingComboBox.SelectedItem).Content.ToString();

            List<Movie> filteredMovies = filmsResponse.Films;

            if (selectedType != "Все типы")
            {
                filteredMovies = filteredMovies.Where(movie => movie.Type == selectedType).ToList();
            }

            if (selectedYear != "Все годы")
            {
                filteredMovies = filteredMovies.Where(movie => movie.Year == selectedYear).ToList();
            }

            if (selectedRating != "Все рейтинги")
            {
                double minRating = double.Parse(selectedRating.Split(' ')[0]);
                filteredMovies = filteredMovies.Where(movie => movie.Rating >= minRating).ToList();
            }

            MoviesListView.ItemsSource = filteredMovies;
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterMovies();
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterMovies();
        }

        private void RatingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterMovies();
        }

        private async void LoadTopFilms(int page)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Указываем параметры запроса
                    string type = "TOP_250_BEST_FILMS"; // Тип топа или коллекции

                    // Формируем URL для запроса
                    string apiUrl = $"https://kinopoiskapiunofficial.tech/api/v2.2/films/top?type={type}&page={page}";

                    // Добавляем заголовок с вашим API ключом
                    client.DefaultRequestHeaders.Add("X-API-KEY", "6fc5dcce-4b6e-409c-adcd-21a2498de0d8");

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        filmsResponse = JsonConvert.DeserializeObject<TopFilmsResponse>(json);

                        // Отображение списка фильмов в ListView
                        MoviesListView.ItemsSource = filmsResponse.Films;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить список фильмов.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                }

            }
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadTopFilms(currentPage);
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < filmsResponse.PagesCount)
            {
                currentPage++;
                LoadTopFilms(currentPage);
            }
        }

        private void MoviesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Получите выбранный фильм из списка
            Movie selectedMovie = (Movie)MoviesListView.SelectedItem;

            // Проверка на null, чтобы избежать ошибок
            if (selectedMovie != null)
            {
                // Создайте и отобразите окно с подробным описанием фильма
                MovieDetailsWindow detailsWindow = new MovieDetailsWindow(selectedMovie);
                detailsWindow.ShowDialog();
            }
        }



        public class TopFilmsResponse
        {
            public int PagesCount { get; set; }
            public List<Movie> Films { get; set; }
        }

        public class Movie
        {
            public int FilmId { get; set; }
            public string NameRu { get; set; }
            public string NameEn { get; set; }
            public string Year { get; set; }
            public string FilmLength { get; set; }
            public double Rating { get; set; }
            public int RatingVoteCount { get; set; }
            public string PosterUrl { get; set; }
            public string PosterUrlPreview { get; set; }
            public int Id { get; set; } // Добавьте поле Id
            public string Description { get; set; } // Добавьте поле Description
            public string Type { get; set; } // Добавьте поле Type
        }

    }
}
