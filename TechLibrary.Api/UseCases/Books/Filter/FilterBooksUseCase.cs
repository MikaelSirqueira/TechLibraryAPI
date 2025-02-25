using Microsoft.EntityFrameworkCore;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAcess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TechLibrary.Api.UseCases.Books.Filter;

public class FilterBooksUseCase
{
    private const int PAGE_SIZE = 10;

    public ResponseBooksJson Execute(RequestFilterBooksJson request)
    {
        var dbContext = new TechLibraryDbContext();

        var objBooks = dbContext.Books.AsQueryable();

        if(string.IsNullOrWhiteSpace(request.Title) == false)
        {
            objBooks = objBooks.Where(book => book.Title.Contains(request.Title, StringComparison.CurrentCultureIgnoreCase));
        }        

        var books = objBooks
            .OrderBy(book => book.Title).ThenBy(book => book.Author)
            .Skip(PAGE_SIZE * (request.PageNumber - 1))
            .Take(PAGE_SIZE)
            .ToList();

        var totalBooks = objBooks.Count();

        return new ResponseBooksJson
        {
            Pagination = new ResponsePaginationJson
            {
                PageNumber = request.PageNumber,
                TotalCount = totalBooks
            },
            Books = books.Select(book => new ResponseBookJson
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            }).ToList()
        };
    }
}
