using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LvivCompany.Bookstore.Entities;

namespace LvivCompany.Bookstore.DataAccess.IRepo
{
    /// <summary>
    /// Represents StatusRepository class
    /// </summary>
    /// <seealso cref="LvivCompany.Bookstore.DataAccess.IRepo.IRepo{LvivCompany.Bookstore.Entities.Status}" />
    public class StatusRepository : IRepo<Status>
    {
      

        private BookStoreContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public StatusRepository(BookStoreContext context)
        {
            this._context = context;
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>The status list</returns>
        public IEnumerable<Status> GetAll()
        {
            return _context.Statuses;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Status Get(long id)
        {
            return _context.Statuses.Find(id);
        }

        /// <summary>
        /// Creates the specified status.
        /// </summary>
        /// <param name="status">The status.</param>
        public void Create(Status status)
        {
            _context.Statuses.Add(status);
        }

        /// <summary>
        /// Updates the specified status.
        /// </summary>
        /// <param name="status">The status.</param>
        public void Update(Status status)
        {
            _context.Entry(status).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(long id)
        {
            Status status = _context.Statuses.Find(id);
            if (status != null)
                _context.Statuses.Remove(status);
        }

        /// <summary>
        /// Deletes the specified status.
        /// </summary>
        /// <param name="status">The status.</param>
        public void Delete(Status status)
        {
            _context.Entry(status).State = EntityState.Deleted;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
