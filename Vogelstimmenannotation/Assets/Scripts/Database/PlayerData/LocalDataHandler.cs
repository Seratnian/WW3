public interface LocalDataHandler<T>
{
    void Save(T data);
    T Load();
    void SetPathAndName(string path, string name);
}