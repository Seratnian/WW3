public interface LocalDataHandler<T>
{
    bool LocalDataIsNewer(T data);
    void Save(T data);
    T Load();
}