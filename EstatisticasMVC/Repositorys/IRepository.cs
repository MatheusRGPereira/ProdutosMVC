namespace EstatisticasMVC.Repositorys
{

    public interface IRepository<T>
    {
        void Salvar(T obj);

        List<T> Todos();

        void ApagarPorId(int id);

        T? BuscarPorId(int id);
    }
}
