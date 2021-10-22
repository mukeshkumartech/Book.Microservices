using System;

namespace Entities
{
    public class Book
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public long? ISBN { get; set; }
        public string PublicationDate { get; set; }
    }
}
