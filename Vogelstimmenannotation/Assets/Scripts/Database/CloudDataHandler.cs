public interface CloudDataHandler<T>
{
    bool CloudDataIsNewer(T saveFile);
    void Save(T saveFile);
    T Load();
}