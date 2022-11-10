namespace FantasyBasketballLeague.Infrastructure.Data.Constants
{
    public class ValidationConstants
    {
        // Person 
        public const int FirstNameMaxLength = 50;
        public const int FirstNameMinLength = 50;

        public const int LastNameMaxLength = 50;
        public const int LastNameMinLength = 50;

        // Basketball player
        public const int JerseyMaxNumer = 99;
        public const int JerseyMinNumer = 0;

        // Team
        public const int TeamNameMaxLength = 50;
        public const int TeamNameMinLength = 3;

        public const int OpenPositionsDefaultValue = 10;

        // Username
        public const int UsernameMaxLength = 20;
        public const int UsernameMinLength = 5;

        // Email
        public const int EmailMaxLength = 60;
        public const int EmailMinLength = 10;

        // Password
        public const int PasswordMaxLength = 20;
        public const int PasswordMinLength = 5;

        // League
        public const int LeagueNameMaxLength = 50;
        public const int LeagueNameMinLength = 3;
    }
}
