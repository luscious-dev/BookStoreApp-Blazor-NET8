using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using AutoMapper;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

		public AuthorsController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
		{
			_context = context;
			_mapper = mapper;
			_logger = logger;
		}

		// GET: api/Authors
		[HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorDtos);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            try
            {
				var author = await _context.Authors.FindAsync(id);


				if (author == null)
				{
					return NotFound();
				}

				var authorDto = _mapper.Map<AuthorDto>(author);

				return authorDto;
			}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went wrong");
                return StatusCode(500, "Something went wrong");
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
			try
			{
				if (id != authorDto.Id)
				{
					return BadRequest();
				}

				var author = await _context.Authors.FindAsync(id);

				if (author == null)
					return NotFound();

				_mapper.Map(authorDto, author);
				_context.Entry(author).State = EntityState.Modified;

				try
				{
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AuthorExists(id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something Went wrong");
				return StatusCode(500, "Something went wrong");
			}
			
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorCreateDto author)
        {
			try
			{
				var authorEnity = _mapper.Map<Author>(author);
				_context.Authors.Add(authorEnity);
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetAuthor", new { id = authorEnity.Id }, authorEnity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something Went wrong");
				return StatusCode(500, "Something went wrong");
			}
			
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
			try
			{
				var author = await _context.Authors.FindAsync(id);
				if (author == null)
				{
					return NotFound();
				}

				_context.Authors.Remove(author);
				await _context.SaveChangesAsync();

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Something Went wrong");
				return StatusCode(500, "Something went wrong");
			}
			
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
