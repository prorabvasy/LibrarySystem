# LibrarySystem

Учебный пример информационной системы библиотеки на C# / .NET 8 с многослойной архитектурой.

## Состав решения
- `LibrarySystem.Domain` — доменные сущности.
- `LibrarySystem.DAL` — интерфейс репозитория и демонстрационная реализация в памяти.
- `LibrarySystem.BLL` — поиск книг, выдача и возврат.
- `LibrarySystem.ConsoleUI` — консольное меню.
- `database/schema.sql` — логическая схема Microsoft SQL Server на 8 сущностей.
- `../LibrarySystem_ER.png`, `../LibrarySystem_Architecture.png` — диаграммы для отчёта.

## Запуск
Установить .NET 8 SDK, открыть `LibrarySystem.sln` в Visual Studio 2022 либо выполнить в корне проекта:

```bash
dotnet run --project src/LibrarySystem.ConsoleUI/LibrarySystem.ConsoleUI.csproj
```

Данные демонстрационной программы хранятся в памяти и сбрасываются при перезапуске. SQL-скрипт показывает проектируемую схему SQL Server; подключение к реальной БД в этом учебном каркасе ещё не реализовано.

## Функции
1. Просмотр книг.
2. Поиск по названию или ISBN.
3. Выдача книги читателю с проверкой доступных экземпляров.
4. Просмотр выдач.
5. Регистрация возврата.
