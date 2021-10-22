using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IBookRepository: IBaseRepository<Book>
    {
        Task<IEnumerable<Book>> Get(Book bookData);

        Task<int> Add(Book book);

        Task<int> Update(Book book);

        Task Delete(int id);
    }

}
