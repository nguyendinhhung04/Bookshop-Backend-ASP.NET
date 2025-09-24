namespace bookshop.DataAccessLayer.Models.DAO
{
    public interface IDAO<T> where T : class
    {
        Task<List<T>> GetAll();
    }
}
