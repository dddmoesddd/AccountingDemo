using FrameWork.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Infrastruture.Persistance
{
    public interface IRepositoryBase<T> where T : IAggregateRoot
    {
        IReadOnlyList<T> GetAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SetDbContext(DbContext context);

    }
}
