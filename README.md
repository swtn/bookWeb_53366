# 📚 BookWeb – Twój osobisty katalog książek

BookWeb to webowa aplikacja stworzona w technologii **ASP.NET Core Razor Pages**, umożliwiająca wyszukiwanie książek, zarządzanie własną biblioteką oraz przeglądanie bestsellerów New York Timesa.

---

## 🔧 Funkcje aplikacji

- 🔍 Wyszukiwanie książek przy użyciu **Google Books API**
- ✅ Dodawanie książek do list:
  - „Przeczytane”
  - „Do przeczytania”
- 🗑️ Usuwanie książek z list
- 🔐 Rejestracja i logowanie użytkowników
- 📈 Przeglądanie **Top 15 książek NY Times** 
- 🎨 Nowoczesny, responsywny interfejs z użyciem Bootstrap
- 📦 Integracja z dwoma API zewnętrznymi (Google + NYT)

---

## 🛠️ Wymagania

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022 / Rider / VS Code
- Klucz API do:
  - [Google Books API](https://developers.google.com/books/docs/v1/using)
  - [NY Times Books API](https://developer.nytimes.com/docs/books-product/1/overview)

---

## 🚀 Uruchomienie projektu

1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/twoje-repozytorium/bookweb.git
   cd bookweb
   ```
2. Pobierz zależności:
    ```bash
    dotnet restore
    ```
3. Uruchom projekt:
    ```bash 
    dotnet run
    ```

4. Otwórz przeglądarke:
    ```bash
    htttps://localhost:5001
    ```

## 📂 Struktura projektu


- **Pages/** – Razor Pages (Search, Profile, BookDetails, TopBooks)
- **Services/** – Integracje z Google Books i NYT API
- **Models/** – Klasy domenowe książek i użytkownika
- **wwwroot/** – Stylizacja, skrypty i pliki statyczne

## 🤖 Technologie
- ASP.NET Core 6 (Razor Pages)
- Entity Framework Core
- Google Books API
- NY Times Books API
- Bootstrap 5

## 👨‍🎓 Autor
**Daniel Świątek**  
Projekt stworzony na potrzeby zaliczenia przedmiotu *Tworzenie usług sieciowych w architekturze REST*
###### Indeks 53366
