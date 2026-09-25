namespace MoviesApp.Common
{
    public static class EntityValidation
    {
        public const int MovieIdMinValue = 1;
        public const int MovieIdMaxValue = int.MaxValue;
        public const int MovieTitleMaxLength = 100;
        public const int MovieGenreMinLength = 3;
        public const int MovieGenreMaxLength = 50;
        public const string MovieReleaseDateRegexPattern = @"^\d{4}-\d{2}-\d{2}$"; // YYYY-MM-DD format
        public const int MovieDirectorMinLength = 2;
        public const int MovieDirectorMaxLength = 100;
        public const int MovieDurationMinLength = 1;
        public const int MovieDurationMaxLength = 500;
        public const int MovieDescriptionMinLength = 10;
        public const int MovieDescriptionMaxLength = 2000;
        public const int MovieImageUrlMaxLength = 2048;
    }
}
