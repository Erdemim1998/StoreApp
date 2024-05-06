using StoreApp.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }

        void CreateComment(Comment comment);
    }
}
