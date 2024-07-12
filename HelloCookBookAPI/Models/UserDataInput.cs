/*
 * This class is used as user input when adding or updating data
 */

namespace HelloCookBookAPI.Models
{
    public class UserDataInput
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
