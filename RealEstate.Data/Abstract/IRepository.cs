using System;
using System.Linq.Expressions;

namespace RealEstate.Data.Abstract;

public interface IRepository<T> where T : class
{
    /// <summary>
    /// Gets entity by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task <T> GetAsync(int id);
    /// <summary>
    /// Gets entity by filter.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="showIsDeleted"></param>
    /// <param name="asExpanded"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task <T> GetAsync(
        Expression<Func<T, bool>> predicate,
        bool showIsDeleted = false,
        bool asExpanded = false,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    );
    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync();
    /// <summary>
    /// Gets all entities by filter.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="showIsDeleted"></param>
    /// <param name="asExpanded"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
        bool showIsDeleted = false,
        bool asExpanded = false,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    );
    /// <summary>
    /// Counts entities.
    /// </summary>
    /// <returns></returns>
    Task<int> CountAsync();
    /// <summary>
    /// Counts entities by filter.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    /// <summary>
    /// Checks if any entity exists by filter.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity"></param>
    void Update(T entity);

    /// <summary>
    /// Updates multiple entities.
    /// </summary>
    /// <param name="entities"></param>
    void BatchUpdate(IEnumerable<T> entities);

    /// <summary>
    /// Removes an entity.
    /// </summary>
    /// <param name="entity"></param>
    void Remove(T entity);
    /// <summary>
    /// Sayfalanmış veri getirme
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="showIsDeleted"></param>
    /// <param name="asExpanded"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int skip = 0,
        int take = 10,
        bool showIsDeleted = false,
        bool asExpanded = false,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    );
}
