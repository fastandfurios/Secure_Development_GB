using Biblio.DAL.Interfaces;
using Biblio.DAL.Models.Book;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Biblio.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _collection;

        public BookRepository(IOptions<BookStoreDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<Book>(options.Value.BooksCollectionName);
        }

        public async Task AddAsync(Book book, CancellationToken token=default)
            => await _collection.InsertOneAsync(book, null, token)
                .ConfigureAwait(true);
        

        public async Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken token=default)
            => await _collection.Find(_ => true)
                .ToListAsync(cancellationToken: token)
                .ConfigureAwait(true);

        public async Task<Book> GetEntityAsync(string id, CancellationToken token=default)
            => await _collection.Find(b => b.Id == id)
                .FirstOrDefaultAsync(cancellationToken: token)
                .ConfigureAwait(true);

        public async Task UpdateAsync(Book updateBook, string id, CancellationToken token=default)
            => await _collection.ReplaceOneAsync(b => b.Id == id, updateBook, cancellationToken: token)
                .ConfigureAwait(true);

        public async Task DeleteAsync(string id, CancellationToken token=default)
            => await _collection.DeleteOneAsync(b => b.Id == id, cancellationToken: token)
                .ConfigureAwait(true);
    }
}
