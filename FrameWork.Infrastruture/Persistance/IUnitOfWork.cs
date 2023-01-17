namespace FrameWork.Infrastruture.Persistance
{
    public interface IUnitOfWork
    {
        void Begin();
        Task Commit();
        void Rollback();
    }
}
