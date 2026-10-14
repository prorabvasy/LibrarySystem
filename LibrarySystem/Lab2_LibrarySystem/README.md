# Лабораторный практикум №2 — Интерфейсы. Репозиторий. JSON

Решение для .NET 8, разбито на три независимых проекта по частям задания.

## Состав
- `Part1_Interfaces` — IMovable, IDrawable, IShape, I3DShape, ISP, платежи и логгеры.
- `Part2_Repository` — Generic Repository, IBookRepository, EF Core + SQLite, асинхронные CRUD и Unit of Work.
- `Part3_UserApi` — ASP.NET Core REST API пользователей, JSON, EF Core + SQLite, уникальный Login и CRUD.
- `LibrarySystem_Repository.png`, `LibrarySystem_UserApi.png` — диаграммы PNG.

## Запуск
Установите .NET 8 SDK. Из папки соответствующего проекта выполните:

```bash
dotnet run --project Part1_Interfaces/Part1_Interfaces.csproj
dotnet run --project Part2_Repository/Part2_Repository.csproj
dotnet run --project Part3_UserApi/Part3_UserApi.csproj
```

Для проектов 2 и 3 при первом запуске создаются SQLite-файлы `library.db` и `users.db`.

## API: примеры
`POST /user` JSON body:
```json
{"login":"example_user","passHash":"<длинная хешированная строка не менее 32 символов>"}
```
Доступны `GET /user/{id}`, `PUT /user/{id}`, `DELETE /user/{id}`. Для PUT передавайте `login` и при необходимости новый `passHash`. Поле PassHash никогда не возвращается из API.

## Важные примечания
- В задании указано, что пароль передаётся как хеш. API проверяет, что PassHash не выглядит как короткий исходный пароль, но не вычисляет хеш самостоятельно. В реальном проекте следует применять надёжный алгоритм хеширования паролей с солью и безопасное сравнение.
- В примере REST API отсутствует аутентификация/авторизация и HTTPS на уровне приложения; перед промышленным использованием это необходимо настроить.
