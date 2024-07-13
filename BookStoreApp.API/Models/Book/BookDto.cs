namespace BookStoreApp.API.Models.Book
{
	public class BookDto: BaseDto
	{
		public string? Title { get; set; }

		public string? Summary { get; set; }

		public string? Image { get; set; }

		public decimal Price { get; set; }
		public string AuthorName { get; set; }
	}
}
