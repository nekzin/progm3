using System;
using System.Collections;
using System.Collections.Generic;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public override string ToString()
    {
        return $"{Title} by {Author} ({Year})";
    }
}

public class Library : IEnumerable<Book>
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public IEnumerator<Book> GetEnumerator()
    {
        return books.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        library.AddBook(new Book("Великий Гэтсби", "Скотт Фиджеральд", 1925));
        library.AddBook(new Book("Война и мир", "Лев Толстой", 1869));
        library.AddBook(new Book("1984", "Джордж Оруэлл", 1949));

        foreach (var book in library)
        {
            Console.WriteLine(book);
        }
    }
}