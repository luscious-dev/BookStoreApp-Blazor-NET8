using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book
{
	public class BookDetailsDto
	{
		public string Title { get; set; }

		[Range(1800, int.MaxValue)]
		public int Year { get; set; }
		public string Isbn { get; set; } = null!;

		public string? Summary { get; set; }

		public string? Image { get; set; }

		public decimal? Price { get; set; }

		public int? AuthorId { get; set; }
		public string AuthorName { get; set; }
	}
}
