using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.User
{
	public class UserDto
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
