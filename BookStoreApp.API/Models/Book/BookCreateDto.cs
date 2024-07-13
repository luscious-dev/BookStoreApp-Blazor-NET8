using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book
{
	public class BookCreateDto
	{
		public string Title { get; set; }

		[Range(1800, int.MaxValue)]
		public int Year { get; set; }

		[Required]
		public string Isbn { get; set; } = null!;

		[Required]
		[StringLength(250, MinimumLength = 10)]
		public string? Summary { get; set; }

		public string? Image { get; set; }

		public decimal? Price { get; set; }
	}
}
