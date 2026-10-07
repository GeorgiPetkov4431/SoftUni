namespace BookShelf.Common
{
    public static class EntityValidation
    {
        /* Author Begin */
        public const int AuthorNameMinLength = 2;
        public const int AuthorNameMaxLength = 100;
        public const int AuthorCountryMaxLength = 60;
        /* Author End */

        /* Book Begin */
        public const int BookTitleMinLength = 3;
        public const int BookTitleMaxLength = 500;
        /* Book End */
    }
}
