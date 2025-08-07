import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";  // <-- добавили useNavigate

interface Station {
  id: number;
  name: string;
  city: string;
}

const HomePage: React.FC = () => {
  const [isLogged, setIsLogged] = useState(false);

  // Для формы поиска
  const [stations, setStations] = useState<Station[]>([]);
  const [from, setFrom] = useState("");
  const [to, setTo] = useState("");
  const [date, setDate] = useState("");

  const navigate = useNavigate(); // <-- добавили

  useEffect(() => {
    const token = localStorage.getItem("token");
    setIsLogged(!!token);

    // Загрузка станций
    fetch("https://localhost:7105/api/Stations")
      .then(res => res.json())
      .then(data => setStations(data))
      .catch(() => setStations([]));
  }, []);

  // Формат для сравнения (название + город)
  const formatStation = (st: Station) => `${st.name} (${st.city})`;

  // Определяем, выбрана ли валидная станция (реально существует)
  const validFrom = stations.some(st => formatStation(st).toLowerCase() === from.toLowerCase());
  const validTo = stations.some(st => formatStation(st).toLowerCase() === to.toLowerCase());

  // -- ВАЖНО: новый handleSearch --
  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    if (validFrom && validTo && date) {
      // Передаем параметры в url
      const params = new URLSearchParams({
        from,
        to,
        date
      }).toString();
      navigate(`/tickets?${params}`);
    }
  };

  return (
    <div className="home-container">
      <h2 className="home-title">
        Ласкаво просимо до <span style={{ color: "#397be7" }}>TrainApp!</span>
      </h2>
      <p className="home-subtitle">
        Швидкий пошук та бронювання квитків на потяги онлайн.
      </p>

      {/* Форма поиска */}
      <div className="search-block">
        <form className="search-form" onSubmit={handleSearch}>
          <input
            type="text"
            placeholder="Звідки"
            name="from"
            list="stations-list-from"
            value={from}
            onChange={e => setFrom(e.target.value)}
            autoComplete="off"
            disabled={stations.length === 0}
          />
          <datalist id="stations-list-from">
            {stations.map(st => (
              <option key={st.id} value={`${st.name} (${st.city})`} />
            ))}
          </datalist>

          <input
            type="text"
            placeholder="Куди"
            name="to"
            list="stations-list-to"
            value={to}
            onChange={e => setTo(e.target.value)}
            autoComplete="off"
            disabled={stations.length === 0 || !from}
          />
          <datalist id="stations-list-to">
            {stations
              .filter(st => formatStation(st) !== from)
              .map(st => (
                <option key={st.id} value={`${st.name} (${st.city})`} />
              ))}
          </datalist>

          <input
            type="date"
            name="date"
            value={date}
            onChange={e => setDate(e.target.value)}
            min={new Date().toISOString().split("T")[0]}
            disabled={!validFrom || !validTo}
          />

          <button type="submit" disabled={!validFrom || !validTo || !date}>
            Знайти потяги
          </button>
        </form>
        <div className="search-hint">* Виберіть станції зі списку</div>
      </div>

      {/* Кнопки входа и регистрации — только если НЕ авторизован */}
      {!isLogged && (
        <div className="home-buttons">
          <Link to="/login"><button>Увійти</button></Link>
          <Link to="/register"><button>Реєстрація</button></Link>
        </div>
      )}

      {/* Табло (Заготовка: “Ближчі поїзди” — потом подключим API) */}
      <div className="board-block">
        <h3 className="board-title">Ближчі поїзди</h3>
        <ul className="board-list">
          <li>045Л — ІнтерСіті <span className="train-route">Київ → Львів</span> <span className="train-time">08:30</span></li>
          <li>091К — Інтерсіті <span className="train-route">Київ → Одеса</span> <span className="train-time">09:10</span></li>
        </ul>
        <div className="board-hint">* Інформація для ознайомлення</div>
      </div>
    </div>
  );
};

export default HomePage;
