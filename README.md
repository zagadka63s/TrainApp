TrainApp
Онлайн-пошук, бронювання та «купівля» (демо) залізничних квитків.
Бекенд: ASP.NET Core Web API (.NET 8, EF Core).
Фронтенд: React + TypeScript + Vite.

Можливості

Пошук доступних місць/рейсів
Список поїздів і станцій
Реєстрація/логін, JWT-аутентифікація
Створення квитка (резерв/оплата демо), перевірка зайнятості місця

Архитектура

/TrainApp               # backend (ASP.NET Core Web API)
  /Controllers
  /Services
  /Data                 # ApplicationDbContext (EF Core)
  /Entities             # сутності (Train, Route, Coach, Seat, Ticket, User ...)
  /DTOs                 # Dto на відповіді/запити
  appsettings.json
/frontend               # React + TS + Vite
  /src
    /api                # http-клієнти/ендпоінти
    /pages              # сторінки (Home, Tickets, Payment, Profile ...)
    /features           # бізнес-логіка, хуки
    /styles             # css


Технології

Backend
.NET 8, ASP.NET Core
Entity Framework Core (Migrations)
JWT Auth (Bearer)
Swagger/Swashbuckle


Аутентифікація (JWT)

Реєстрація: POST /api/auth/register

Логін: POST /api/auth/login → повертає { token, user: { id, username, email } }

«Хто я»: GET /api/auth/me (потрібен заголовок Authorization: Bearer <token>)

Фронт зберігає token і user в localStorage, на захищені запити відправляє Authorization: Bearer.

Квитки

Отримати все (адмін): GET /api/tickets

Отримати за Id: GET /api/tickets/{id}

Створити (покупка/резерв): POST /api/tickets

Видалити: DELETE /api/tickets/{id}

Квитки користувача: GET /api/tickets/user/{userId}
