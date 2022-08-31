namespace Final_Project.Services
{
    public interface IGeneralDataService<T>
    {
        public List<T> GetAll();
        public T Create(T Entity);
        public T Update(T Entity);

    }
    public interface ISingleDataService<T>
    {
        public T Find(int id);
        public bool Delete(int id);

    }
    public interface ICompositeDataService<T, M, V>
    {
        public T Find(M firstKey, V secondKey);
        public bool Delete(M firstKey, V secondKey);
    }
}
