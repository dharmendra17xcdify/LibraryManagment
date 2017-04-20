using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.API
{
    public class BooksAssigned
    {
        public List<int> Books { get; set; }
        public int UserId { get; set; }
    }
}