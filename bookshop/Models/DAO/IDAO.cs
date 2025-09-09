namespace bookshop.Models.DAO
{
    public interface IDAO<T> where T : class
    {
        List<T> GetAll();
        List<T> GetTempData();
    }
}
