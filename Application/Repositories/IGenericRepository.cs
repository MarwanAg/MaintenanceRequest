namespace Application.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public IQueryable<T> GetAll();

        public T GetById(Guid Id);
        public Task<T> GetByIdAsync(Guid Id);

        public void Insert(T input);
        public Task InsertAsync(T input);
        public void InsertRange(List<T> input);
        public Task InsertRangeAsync(List<T> input);

        public void Update(T input);
        public Task UpdateAsync(T input);

        public void Delete(T input);

        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}
