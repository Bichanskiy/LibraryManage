using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class Program
{
    private static void Main(string[] args)
    {
        List<int> ListOfId = new List<int>();
        Library library = new Library();
        Reader reader = new Reader("Alex", 1);

        while(true)
        {
            Console.WriteLine("Добро пожаловать в нашу библиотеку. Выберите действие");
            Console.WriteLine("1. Добавить Книгу в библиотеку");
            Console.WriteLine("2. Регистрация нового читателя");
            Console.WriteLine("3. Выдача книг читателю");
            Console.WriteLine("4. Возврат книги читателем");
            Console.WriteLine("5. Просмотр всех свободных книг");
            Console.WriteLine("6. Просмотр всех книг у читателя");

            string UserChoise = Console.ReadLine();

            switch (UserChoise)
            {
                case "1":
                    AddBookUI(library);
                    break;
                case "2":
                    NewReader(library);
                    break;
                case "3":
                    GivingBook(library, reader);
                    break;
                case "4":
                    GetBackBook(reader);
                    break;
                case "5":
                    ShowBookByStatus(library);
                    break;
                case "6":
                    ShowAllReadersBooks(library);
                    break;
                default:
                    Console.WriteLine("Вы выбрали не то действие");
                    break;
            }
            Console.WriteLine("Нажмите любую кнопку чтобы продолжить");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void AddBookUI(Library library)
    {
        Console.Write("Введите название книги: ");
        string title = Console.ReadLine();
        Console.Write("Введите автора книги: ");
        string author = Console.ReadLine();
        Console.Write("Введите год выпуска: ");
        int year = Convert.ToInt32(Console.ReadLine());
        Book book1 = new Book(title, author, year, true);
        library.AddBook(book1);
        Console.WriteLine("Книга успешно добавлена!");
    }

    static void NewReader(Library library)
    {
        Console.Write("Введите своё имя: ");
        string name = Console.ReadLine();
        Random rand = new Random();
        int newId = rand.Next(0, 100000000);

        Reader reader = new Reader(name, newId);

        library.AddReader(reader);
        

    }

    static void GivingBook(Library library, Reader reader)
    {
        Console.Write("Введите название желаемой книги: ");
        string userChoise = Console.ReadLine();

        foreach (Book book in library.AllBooks)
        {
            if (book.Title == userChoise && book.Status == true)
            {
                reader.AddGetBook(book);
                Console.WriteLine("Книга успешно добавлена!");
                book.Status = false;
                break;
            }

            else
            {
                Console.WriteLine("Книга занята, либо вы неправильно ввели её название");
            }
        }
    }

    static void GetBackBook(Reader reader)
    {
        Console.Write("Какую книгу вы хотите вернуть: ");
        string userChoise = Console.ReadLine();

        foreach (Book book in reader.GetBooks)
        {
            if (book.Title == userChoise)
            {
                reader.GetBooks.Remove(book);
                book.Status = true;
                Console.WriteLine("Вы вернули книгу!");
                break;
            }

            else
            {
                Console.WriteLine("Вы такую книгу не брали или ввели неправилльное название");
            }
        }
    }

    static void ShowBookByStatus(Library library)
    {
        foreach (Book book in library.AllBooks)
        {
            if (book.Status)
            {
                book.DisplayInfo();
            }
        }
    }

    static void ShowAllReadersBooks(Library library)
    {
        Console.Write("Книги какого читателя вам нужно посмотреть(Напишите ID): ");
        int userChoise = Convert.ToInt32(Console.ReadLine());
        int x = 0;

        foreach (Reader reader in library.AllReaders)
        {
            if (reader.Id == userChoise)
            {
                x = 1;
                reader.ShowAllBooks();
                break;
            }
        }
        if (x == 0)
        {
            Console.WriteLine("Такого читателя нет");
        }
    }




class Book
{
    public string Title;
    public string Author;
    public int Year;
    public bool Status; // Если true - в библиотеке, false - на руках у человека

    public Book(string title, string author, int year, bool status)
    {
        Title = title;
        Author = author;
        Year = year;
        Status = status;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Название книги: {Title}, автор: {Author}, год выпуска: {Year}, статус книги: {Status}");
    }


}

class Reader
{
    public string Name;
    public int Id;
    public List<Book> GetBooks;

    public Reader(string name, int id)
    {
        Name = name;
        Id = id;
        GetBooks = new List<Book>();
    }

    public void AddGetBook(Book book)
    {
        GetBooks.Add(book);
    }

    public void ShowAllBooks()
    {
        foreach (Book book in GetBooks)
        {
            book.DisplayInfo();
        }
    }


}

class Library
{
    public List<Book> AllBooks = new List<Book>();
    public List<Reader> AllReaders = new List<Reader>();

    public void AddBook(Book book)
    {
        AllBooks.Add(book);
    }

    public void AddReader(Reader reader)
    {
        AllReaders.Add(reader);
    }


}
    

}