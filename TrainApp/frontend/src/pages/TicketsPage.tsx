import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import '../styles/tickets.css';

interface Ticket {
  id: number;
  trainName: string;
  departure: string;
  arrival: string;
  seat: string;
  date: string;
  price?: number;
}

const TicketsPage: React.FC = () => {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Для модального окна
  const [selectedTicket, setSelectedTicket] = useState<Ticket | null>(null);
  const navigate = useNavigate(); // ← добавил useNavigate

  useEffect(() => {
    fetch("https://localhost:7105/api/tickets")
      .then(res => {
        if (!res.ok) throw new Error("Помилка отримання квитків");
        return res.json();
      })
      .then(data => {
        setTickets(data);
        setLoading(false);
      })
      .catch(() => {
        setError("Неможливо завантажити квитки");
        setLoading(false);
      });
  }, []);

  // Закрытие модалки (по крестику или фону)
  const closeModal = () => setSelectedTicket(null);

  if (loading) return <div className="tickets-loader">Завантаження квитків...</div>;
  if (error) return <div className="tickets-empty">{error}</div>;

  return (
    <div className="tickets-container">
      <h2 className="tickets-title">Доступні квитки</h2>
      {tickets.length === 0 ? (
        <div className="tickets-empty">Квитків не знайдено</div>
      ) : (
        <ul className="tickets-list">
          {tickets.map(ticket => (
            <li
              key={ticket.id}
              className="ticket-card"
              tabIndex={0}
              style={{ cursor: "pointer" }}
              onClick={() => setSelectedTicket(ticket)}
            >
              <div>
                <b>{ticket.trainName}</b> — {ticket.departure} → {ticket.arrival}
              </div>
              <div>Місце: {ticket.seat}</div>
              <div>Дата: {ticket.date}</div>
              {ticket.price && <div>Ціна: {ticket.price} грн</div>}
            </li>
          ))}
        </ul>
      )}

      {/* --- Модальное окно --- */}
      {selectedTicket && (
        <div className="modal-backdrop" onClick={closeModal}>
          <div className="modal-window" onClick={e => e.stopPropagation()}>
            <button className="modal-close" onClick={closeModal}>&times;</button>
            <h3>Деталі квитка</h3>
            <div>
              <b>Поїзд:</b> {selectedTicket.trainName}<br/>
              <b>Маршрут:</b> {selectedTicket.departure} → {selectedTicket.arrival}<br/>
              <b>Дата:</b> {selectedTicket.date}<br/>
              <b>Місце:</b> {selectedTicket.seat}<br/>
              {selectedTicket.price && <><b>Ціна:</b> {selectedTicket.price} грн<br/></>}
            </div>
            <button
              className="modal-buy-btn"
              onClick={() => {
                closeModal();
                navigate(`/payment/${selectedTicket.id}`);
              }}
            >
              Купити квиток
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default TicketsPage;
