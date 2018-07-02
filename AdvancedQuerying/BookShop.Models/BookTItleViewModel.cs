using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Models
{
    public class BookTItleViewModel
    {
        public BookTItleViewModel(Book book)
        {
            Title = book.Title;
        }

        public string Title { get; set; }
    }
}
