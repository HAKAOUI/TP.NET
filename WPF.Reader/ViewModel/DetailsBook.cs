using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;
using WPF.Reader.ASP.Server;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly HttpClient http = new() { BaseAddress = new Uri("https://127.0.0.1:44382") };

        public ICommand ReadCommand { get; init; } = new RelayCommand(book => { Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(book); });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public Book CurrentBook { get; set; }

        public DetailsBook(Book book)
        {
            GetBook(book.Id);
        }

        public async void GetBook(int id)
        {
            var currentBook = await new Client(http).ApiBookGetBookAsync(id);
            CurrentBook = currentBook;
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new Book() { Nom = "Book" }) { }
    }
}
