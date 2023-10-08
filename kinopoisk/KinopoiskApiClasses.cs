using System;
using System.Collections.Generic;

namespace KinopoiskAPI.Models
{

    public class Film
    {
        public int kinopoiskId { get; set; }
        public string kinopoiskHDId { get; set; }
        public string imdbId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string nameOriginal { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
        public string coverUrl { get; set; }
        public string logoUrl { get; set; }
        public int reviewsCount { get; set; }
        public double ratingGoodReview { get; set; }
        public int ratingGoodReviewVoteCount { get; set; }
        public double ratingKinopoisk { get; set; }
        public int ratingKinopoiskVoteCount { get; set; }
        public double ratingImdb { get; set; }
        public int ratingImdbVoteCount { get; set; }
        public double ratingFilmCritics { get; set; }
        public int ratingFilmCriticsVoteCount { get; set; }
        public double ratingAwait { get; set; }
        public int ratingAwaitCount { get; set; }
        public double ratingRfCritics { get; set; }
        public int ratingRfCriticsVoteCount { get; set; }
        public string webUrl { get; set; }
        public int year { get; set; }
        public int filmLength { get; set; }
        public string slogan { get; set; }
        public string description { get; set; }
        public string shortDescription { get; set; }
        public string editorAnnotation { get; set; }
        public bool isTicketsAvailable { get; set; }
        public string productionStatus { get; set; }
        public string type { get; set; }
        public string ratingMpaa { get; set; }
        public string ratingAgeLimits { get; set; }
        public bool hasImax { get; set; }
        public bool has3D { get; set; }
        public string lastSync { get; set; }
        public List<Country> countries { get; set; }
        public List<Genre> genres { get; set; }
        public int startYear { get; set; }
        public int endYear { get; set; }
        public bool serial { get; set; }
        public bool shortFilm { get; set; }
        public bool completed { get; set; }
    }

    public class SeasonResponse
    {
        public int total { get; set; }
        public List<Season> items { get; set; }
    }

    public class FactResponse
    {
        public int total { get; set; }
        public List<Fact> items { get; set; }
    }

    public class DistributionResponse
    {
        public int total { get; set; }
        public List<Distribution> items { get; set; }
    }

    public class BoxOfficeResponse
    {
        public int total { get; set; }
        public List<BoxOffice> items { get; set; }
    }

    public class Fact
    {
        public string text { get; set; }
        public string type { get; set; }
        public bool spoiler { get; set; }
    }

    public class BoxOffice
    {
        public string type { get; set; }
        public int amount { get; set; }
        public string currencyCode { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class AwardResponse
    {
        public int total { get; set; }
        public List<Award> items { get; set; }
    }

    public class Award
    {
        public string name { get; set; }
        public bool win { get; set; }
        public string imageUrl { get; set; }
        public string nominationName { get; set; }
        public int year { get; set; }
        public List<AwardPerson> persons { get; set; }
    }

    public class AwardPerson
    {
        public int kinopoiskId { get; set; }
        public string webUrl { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string sex { get; set; }
        public string posterUrl { get; set; }
        public int growth { get; set; }
        public string birthday { get; set; }
        public string death { get; set; }
        public int age { get; set; }
        public string birthplace { get; set; }
        public string deathplace { get; set; }
        public string profession { get; set; }
    }

    public class Distribution
    {
        public string type { get; set; }
        public string subType { get; set; }
        public string date { get; set; }
        public bool reRelease { get; set; }
        public Country country { get; set; }
        public List<Company> companies { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
    }

    public class Season
    {
        public int number { get; set; }
        public List<Episode> episodes { get; set; }
    }

    public class Episode
    {
        public int seasonNumber { get; set; }
        public int episodeNumber { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string synopsis { get; set; }
        public string releaseDate { get; set; }
    }

    public class Country
    {
        public string country { get; set; }
    }

    public class Genre
    {
        public string genre { get; set; }
    }

    public class FiltersResponse
    {
        public List<Genre> genres { get; set; }
        public List<Country> countries { get; set; }
    }

    public class FilmSearchResponse
    {
        public string keyword { get; set; }
        public int pagesCount { get; set; }
        public int searchFilmsCountResult { get; set; }
        public List<FilmSearchResult> films { get; set; }
    }

    public class FilmSearchResult
    {
        public int filmId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string type { get; set; }
        public string year { get; set; }
        public string description { get; set; }
        public string filmLength { get; set; }
        public List<Country> countries { get; set; }
        public List<Genre> genres { get; set; }
        public string rating { get; set; }
        public int ratingVoteCount { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
    }

    public class FilmSearchByFiltersResponse
    {
        public int total { get; set; }
        public int totalPages { get; set; }
        public List<FilmSearchResult> items { get; set; }
    }

    public class FilmSequelsAndPrequelsResponse
    {
        public int filmId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string nameOriginal { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
        public string relationType { get; set; }
    }

    public class RelatedFilmResponse
    {
        public int total { get; set; }
        public List<RelatedFilm> items { get; set; }
    }

    public class RelatedFilm
    {
        public int filmId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string nameOriginal { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
        public string relationType { get; set; }
    }

    public class ReviewResponse
    {
        public int total { get; set; }
        public int totalPages { get; set; }
        public int totalPositiveReviews { get; set; }
        public int totalNegativeReviews { get; set; }
        public int totalNeutralReviews { get; set; }
        public List<Review> items { get; set; }
    }

    public class Review
    {
        public int kinopoiskId { get; set; }
        public string type { get; set; }
        public string date { get; set; }
        public int positiveRating { get; set; }
        public int negativeRating { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }

    public class ExternalSourceResponse
    {
        public int total { get; set; }
        public List<ExternalSource> items { get; set; }
    }

    public class ExternalSource
    {
        public string url { get; set; }
        public string platform { get; set; }
        public string logoUrl { get; set; }
        public int positiveRating { get; set; }
        public int negativeRating { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }

    public class FilmTopResponse
    {
        public int pagesCount { get; set; }
        public List<FilmSearchResult> films { get; set; }
    }

    public class StaffResponse
    {
        public int staffId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string description { get; set; }
        public string posterUrl { get; set; }
        public string professionText { get; set; }
        public string professionKey { get; set; }
    }

    public class PersonResponse
    {
        public int personId { get; set; }
        public string webUrl { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string sex { get; set; }
        public string posterUrl { get; set; }
        public string growth { get; set; }
        public string birthday { get; set; }
        public string death { get; set; }
        public int age { get; set; }
        public string birthplace { get; set; }
        public string deathplace { get; set; }
        public int hasAwards { get; set; }
        public string profession { get; set; }
        public List<Fact> facts { get; set; }
        public List<Spouse> spouses { get; set; }
        public List<PersonFilm> films { get; set; }
    }

    public class Spouse
    {
        public int personId { get; set; }
        public string name { get; set; }
        public bool divorced { get; set; }
        public string divorcedReason { get; set; }
        public string sex { get; set; }
        public int children { get; set; }
        public string webUrl { get; set; }
        public string relation { get; set; }
    }

    public class PersonFilm
    {
        public int filmId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string rating { get; set; }
        public bool general { get; set; }
        public string description { get; set; }
        public string professionKey { get; set; }
    }

    public class PersonByNameResponse
    {
        public int total { get; set; }
        public List<PersonByName> items { get; set; }
    }

    public class PersonByName
    {
        public int kinopoiskId { get; set; }
        public string webUrl { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string sex { get; set; }
        public string posterUrl { get; set; }
    }

    public class ImageResponse
    {
        public List<string> items { get; set; }
    }

    public class PremiereResponse
    {
        public int total { get; set; }
        public List<PremiereResponseItem> items { get; set; }
    }

    public class PremiereResponseItem
    {
        public int kinopoiskId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public int year { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
        public List<Country> countries { get; set; }
        public List<Genre> genres { get; set; }
        public int duration { get; set; }
        public string premiereRu { get; set; }
    }

    public class DigitalReleaseResponse
    {
        public int page { get; set; }
        public int total { get; set; }
        public List<DigitalReleaseItem> releases { get; set; }
    }

    public class DigitalReleaseItem
    {
        public int filmId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public int year { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
        public List<Country> countries { get; set; }
        public List<Genre> genres { get; set; }
        public double rating { get; set; }
        public int ratingVoteCount { get; set; }
        public double expectationsRating { get; set; }
        public int expectationsRatingVoteCount { get; set; }
        public int duration { get; set; }
        public string releaseDate { get; set; }
    }

    public class VideoResponse
    {
        public int total { get; set; }
        public List<VideoResponseItem> items { get; set; }
    }

    public class VideoResponseItem
    {
        public string url { get; set; }
        public string name { get; set; }
        public string site { get; set; }
    }

    public class ApiError
    {
        public string message { get; set; }
    }

    public class FiltersResponse_genres
    {
        public int id { get; set; }
        public string genre { get; set; }
    }

    public class FiltersResponse_countries
    {
        public int id { get; set; }
        public string country { get; set; }
    }

    public class FilmSearchResponse_films
    {
        public int filmId { get; set; }
        public string nameRu { get; set; }
        public string imdbId { get; set; }
        public string nameEn { get; set; }
        public string nameOriginal { get; set; }
        public List<Country> countries { get; set; }
        public List<Genre> genres { get; set; }
        public double ratingKinopoisk { get; set; }
        public double ratingImdb { get; set; }
        public int year { get; set; }
        public string type { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
    }

    public class FilmSearchByFiltersResponse_items
    {
        public int kinopoiskId { get; set; }
        public string imdbId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string nameOriginal { get; set; }
        public List<Country> countries { get; set; }
        public List<Genre> genres { get; set; }
        public double ratingKinopoisk { get; set; }
        public double ratingImdb { get; set; }
        public int year { get; set; }
        public string type { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
    }

    public class RelatedFilmResponse_items
    {
        public int filmId { get; set; }
        public string nameRu { get; set; }
        public string nameEn { get; set; }
        public string nameOriginal { get; set; }
        public string posterUrl { get; set; }
        public string posterUrlPreview { get; set; }
        public string relationType { get; set; }
    }
}
