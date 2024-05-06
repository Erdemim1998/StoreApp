using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EfCommentRepository : ICommentRepository
    {
        private readonly StoreContext _context;

        public EfCommentRepository(StoreContext context)
        {
            _context = context;
        }

        public IQueryable<Comment> Comments => _context.Comments;

        public async void CreateComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            _context.SaveChanges();
        }
    }
}
