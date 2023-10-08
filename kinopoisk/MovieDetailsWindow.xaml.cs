using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using KinopoiskAPI.Models;
using Newtonsoft.Json;

namespace kinopoisk
{
    public partial class MovieDetailsWindow : Window
    {
        private MainWindow.Movie movie;
        private List<ReviewInfo> reviews;

        public MovieDetailsWindow(MainWindow.Movie selectedMovie)
        {
            InitializeComponent();
            movie = selectedMovie;

            // Устанавливаем значения элементов управления из объекта фильма
            TitleTextBlock.Text = movie.NameRu;
            YearTextBlock.Text = movie.Year;
            DescriptionTextBlock.Text = movie.Description;

            // Устанавливаем изображение
            if (!string.IsNullOrEmpty(movie.PosterUrl))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(movie.PosterUrl));
                PosterImage.Source = bitmapImage;
            }

            // Загружаем видеоролики и изображения фильма
            LoadVideos(movie.FilmId);
            LoadSimilarMovies(movie.FilmId);
            LoadImages(movie.FilmId);
            LoadReviews(movie.FilmId);


            // Загрузка информации о персонах (актерах, режиссерах и т.д.) по ID фильма
            LoadStaff(movie.FilmId);


        }




        private async Task<List<PersonInfo>> GetCreatorsAsync(int filmId)
        {
            string apiUrl = $"https://kinopoiskapiunofficial.tech/api/v1/staff?filmId={filmId}&type=CREW";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", "6fc5dcce-4b6e-409c-adcd-21a2498de0d8");

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<PersonInfo> creators = JsonConvert.DeserializeObject<List<PersonInfo>>(json);

                    return creators;
                }
                else
                {
                    Console.WriteLine($"Ошибка при запросе данных о создателях: {response.StatusCode}");
                    return null;
                }

            }
        }



        private async void LoadStaff(int filmId)
        {
            List<PersonInfo> staff = await GetStaffAsync(filmId);

            if (staff != null && staff.Count > 0)
            {
                // Создайте список StaffResponse и заполните его данными из staff
                List<StaffResponse> staffList = staff.Select(person => new StaffResponse
                {
                    NameRu = person.NameRu,
                    NameEn = person.NameEn,
                    ProfessionText = person.ProfessionText
                }).ToList();

                // Привяжите staffList к вашему ListView
                StaffListView.ItemsSource = staffList;
            }
            else
            {
                StaffListView.ItemsSource = null; // Очистите ListView, если данных нет
               //StaffTextBlock.Text = "Информация о персонах фильма отсутствует.";
            }
        }

        private async Task<List<PersonInfo>> GetStaffAsync(int filmId)
        {
            string apiUrl = $"https://kinopoiskapiunofficial.tech/api/v1/staff?filmId={filmId}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", "6fc5dcce-4b6e-409c-adcd-21a2498de0d8");

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<PersonInfo> staff = JsonConvert.DeserializeObject<List<PersonInfo>>(json);

                    return staff;
                }
                else
                {
                    Console.WriteLine($"Ошибка при запросе данных о персонах: {response.StatusCode}");
                    return null;
                }
            }
        }


        private async void LoadVideos(int filmId)
        {
            List<VideoInfo> videos = await GetVideosAsync(filmId);

            if (videos != null)
            {
                // Отображаем видеоролики в списке
                VideosListView.ItemsSource = videos;

                // Добавьте отладочный вывод, чтобы убедиться, что данные получены
                foreach (var video in videos)
                {
                    Console.WriteLine($"Video Name: {video.Name}, Site: {video.Site}, Url: {video.Url}");
                }
            }
            else
            {
                Console.WriteLine("Список видеороликов пуст.");
            }
        }

        private async void LoadReviews(int filmId)
        {
            reviews = await GetReviewsAsync(filmId); // Присвоение рецензий переменной reviews

            if (reviews != null)
            {
                ReviewsListView.ItemsSource = reviews;
                Console.WriteLine($"Загружено {reviews.Count} рецензий."); // Добавьте это
            }
            else
            {
                Console.WriteLine("Список рецензий пуст или возникла ошибка при загрузке.");
            }
        }

        private async void LoadImages(int filmId)
        {
            List<ImageInfo> images = await GetImagesAsync(filmId);

            if (images != null)
            {
                // Отображаем изображения в списке
                ImagesListView.ItemsSource = images;

                // Добавьте отладочный вывод, чтобы убедиться, что данные получены
                foreach (var image in images)
                {
                    Console.WriteLine($"Image Type: {image.Type}, Url: {image.Url}");
                }
            }
            else
            {
                Console.WriteLine("Список изображений пуст.");
            }
        }

        private async void LoadSimilarMovies(int filmId)
        {
            List<MovieInfo> similarMovies = await GetSimilarMoviesAsync(filmId);

            if (similarMovies != null)
            {
                // Отобразить список похожих фильмов
                SimilarMoviesListView.ItemsSource = similarMovies;
            }
            else
            {
                Console.WriteLine("Список похожих фильмов пуст.");
            }
        }

        private async void LoadExternalSources(int filmId)
        {
            List<ExternalSourceInfo> externalSources = await GetExternalSourcesAsync(filmId);

            if (externalSources != null)
            {
                // Отобразить список внешних источников
                ExternalSourcesListView.ItemsSource = externalSources;
            }
            else
            {
                Console.WriteLine("Список внешних источников пуст.");
            }
        }

        public async Task<List<VideoInfo>> GetVideosAsync(int filmId)
        {
            string apiUrl = $"https://kinopoiskapiunofficial.tech/api/v2.2/films/{filmId}/videos";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", "6fc5dcce-4b6e-409c-adcd-21a2498de0d8");

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    VideoResponse videoResponse = JsonConvert.DeserializeObject<VideoResponse>(json);

                    return videoResponse.Videos;
                }
                else
                {
                    throw new Exception("Не удалось получить видео фильма.");
                }
            }
        }

        public async Task<List<ImageInfo>> GetImagesAsync(int filmId)
        {
            string apiUrl = $"https://kinopoiskapiunofficial.tech/api/v2.2/films/{filmId}/images";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-KEY", "6fc5dcce-4b6e-409c-adcd-21a2498de0d8");

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    ImageResponse imageResponse = JsonConvert.DeserializeObject<ImageResponse>(json);

                    return imageResponse.Images;
                }
                else
                {
                    throw new Exception("Не удалось получить изображения фильма.");
                }
            }
        }

        public async Task<List<MovieInfo>> GetSimilarMoviesAsync(int filmId)
        {
            try
            {
                string apiUrl = $"https://kinopoiskapiunofficial.tech/api/v2.2/films/{filmId}/similars";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-KEY", "6fc5dcce-4b6e-409c-adcd-21a2498de0d8");

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        SimilarMoviesResponse similarMoviesResponse = JsonConvert.DeserializeObject<SimilarMoviesResponse>(json);

                        return similarMoviesResponse.Items;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при запросе похожих фильмов: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ExternalSourceInfo>> GetExternalSourcesAsync(int filmId)
        {
            try
            {
                string apiUrl = $"https://kinopoiskapiunofficial.tech/api/v2.2/films/{filmId}/external_sources?page=1";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-KEY", "6fc5dcce-4b6e-409c-adcd-21a2498de0d8");

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        ExternalSourcesResponse externalSourcesResponse = JsonConvert.DeserializeObject<ExternalSourcesResponse>(json);

                        return externalSourcesResponse.Items;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при запросе внешних источников: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null;
            }
        }



        public async Task<List<ReviewInfo>> GetReviewsAsync(int filmId, int page = 1, string order = "DATE_DESC")
        {
            try
            {
                string apiUrl = $"https://kinopoiskapiunofficial.tech/api/v2.2/films/{filmId}/reviews?page={page}&order={order}";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-API-KEY", "6fc5dcce-4b6e-409c-adcd-21a2498de0d8");

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        ReviewResponse reviewResponse = JsonConvert.DeserializeObject<ReviewResponse>(json);

                        return reviewResponse.Items;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при запросе рецензий: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null;
            }
        }


        // Добавьте класс для хранения информации о персонах
        public class PersonResponse
        {
            public int Total { get; set; }
            public List<PersonInfo> Items { get; set; }
        }

        public class PersonInfo
        {
            public int StaffId { get; set; }        // Уникальный идентификатор персоны
            public string NameRu { get; set; }      // Имя на русском языке
            public string NameEn { get; set; }      // Имя на английском языке
            public string Description { get; set; }  // Описание персоны
            public string PosterUrl { get; set; }   // URL постера (фото) персоны
            public string ProfessionText { get; set; } // Текстовое описание профессии персоны (например, "Режиссеры", "Актеры" и т.д.)
            public string ProfessionKey { get; set; }  // Ключ профессии персоны (например, "DIRECTOR", "ACTOR" и т.д.)
        }



        public class VideoResponse
        {
            public List<VideoInfo> Videos { get; set; }
        }

        public class VideoInfo
        {
            public string Url { get; set; }
            public string Name { get; set; }
            public string Site { get; set; }
        }

        public class ImageResponse
        {
            public List<ImageInfo> Images { get; set; }
        }

        public class ImageInfo
        {
            public string Url { get; set; }
            public string Type { get; set; }
        }

        public class SimilarMoviesResponse
        {
            public List<MovieInfo> Items { get; set; }
        }

        public class MovieInfo
        {
            public int FilmId { get; set; }
            public string NameRu { get; set; }
            public string NameEn { get; set; }
            public string NameOriginal { get; set; }
            public string PosterUrl { get; set; }
            public string PosterUrlPreview { get; set; }
            public string RelationType { get; set; }
        }

        public class ExternalSourcesResponse
        {
            public List<ExternalSourceInfo> Items { get; set; }
        }

        public class ExternalSourceInfo
        {
            public string Url { get; set; }
            public string Platform { get; set; }
            public string LogoUrl { get; set; }
            public int PositiveRating { get; set; }
            public int NegativeRating { get; set; }
            public string Author { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
        public class ReviewResponse
        {
            public int Total { get; set; }
            public int TotalPages { get; set; }
            public int TotalPositiveReviews { get; set; }
            public int TotalNegativeReviews { get; set; }
            public int TotalNeutralReviews { get; set; }
            public List<ReviewInfo> Items { get; set; }
        }

        public class ReviewInfo
        {
            public int KinopoiskId { get; set; }
            public string Type { get; set; }
            public DateTime Date { get; set; }
            public int PositiveRating { get; set; }
            public int NegativeRating { get; set; }
            public string Author { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
        public class StaffResponse
        {
            public string NameRu { get; set; }
            public string NameEn { get; set; }
            public string ProfessionText { get; set; }
        }
    }
}