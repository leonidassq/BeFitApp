
# 💪 BeFitApp – aplikacja ASP.NET MVC

Aplikacja do zarządzania sesjami treningowymi, typami ćwiczeń oraz statystykami użytkownika. Projekt stworzony w technologii ASP.NET MVC z użyciem Entity Framework oraz tożsamości ASP.NET (Identity).

## 📁 Struktura projektu

- **Typy ćwiczeń** – możliwość dodawania, edytowania i usuwania własnych typów ćwiczeń.
- **Sesje treningowe** – każdy użytkownik może tworzyć i przeglądać tylko swoje sesje.
- **Wykonane ćwiczenia** – przypisywanie ćwiczeń do sesji (z obciążeniem, seriami, powtórzeniami).
- **Statystyki** – podsumowanie wykonanych ćwiczeń.
- **Logowanie/rejestracja użytkowników** – z systemem ról (admin/użytkownik).

## 🧪 Jak uruchomić aplikację lokalnie?

### ✅ Wymagania

- Visual Studio 2022 (lub nowszy)
- .NET 7.0 SDK
- Workload: ASP.NET and web development
- SQL Server Express / LocalDB (domyślna konfiguracja)

### ⚙️ Krok po kroku

1. **Sklonuj repozytorium:**

   ```bash
   git clone https://github.com/leonidassq/BeFitApp
   cd BeFitApp
   ```

2. **Otwórz projekt w Visual Studio:**

   - Otwórz plik `BeFitApp.sln`

3. **Ustaw projekt jako startowy:**

   - Prawy klik na `BeFitApp` w Solution Explorer → *Set as Startup Project*

4. **Zastosuj migracje i utwórz bazę danych:**

   - Otwórz **Tools > NuGet Package Manager > Package Manager Console**
   - Wpisz:

     ```powershell
     Update-Database
     ```

### ▶️ Uruchomienie

- Kliknij **Start (F5)** lub zielony ▶️ przycisk.
- Aplikacja otworzy się w przeglądarce (np. `https://localhost:5001/`).

## 👤 Konta testowe

| E-mail             | Hasło        | Rola         |
|--------------------|--------------|--------------|
| `admin@gmail.com`  | `zaq1@WSX`  | Administrator |

## 🛡️ Funkcje zabezpieczeń

- Dostęp do sesji, ćwiczeń i statystyk możliwy tylko po zalogowaniu.
- Każdy użytkownik widzi tylko swoje dane.
- Administrator ma pełny dostęp do typów ćwiczeń.

## 📄 Licencja

Projekt przygotowany na potrzeby zaliczenia przedmiotu — nie do komercyjnego wykorzystania.
