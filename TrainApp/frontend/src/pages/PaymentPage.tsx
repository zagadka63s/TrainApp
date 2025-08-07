import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";

interface Ticket {
  id: number;
  trainName: string;
  departure: string;
  arrival: string;
  seat: string;
  date: string;
  price?: number;
}

interface User {
  userName: string;
  email: string;
}

const PaymentPage: React.FC = () => {
  const { ticketId } = useParams();
  const navigate = useNavigate();

  const [ticket, setTicket] = useState<Ticket | null>(null);
  const [user, setUser] = useState<User | null>(null);

  const [card, setCard] = useState("");
  const [expiry, setExpiry] = useState("");
  const [cvv, setCvv] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    // Загрузить билет
    fetch(`https://localhost:7105/api/tickets/${ticketId}`)
      .then(res => res.json())
      .then(data => setTicket(data));

    // Примерно: подтянуть юзера
    const userData = JSON.parse(localStorage.getItem("user") || "{}");
    setUser(userData);
  }, [ticketId]);

  const handlePay = (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setLoading(true);

    if (!card || !expiry || !cvv) {
      setError("Заповніть всі поля для оплати");
      setLoading(false);
      return;
    }
    setTimeout(() => {
      setLoading(false);
      navigate("/profile?paid=1");
    }, 1000);
  };

  if (!ticket) return <div>Завантаження квитка...</div>;

  return (
    <div className="payment-container">
      <h2>Оплата квитка</h2>
      <div className="payment-details">
        <div><b>Поїзд:</b> {ticket.trainName}</div>
        <div><b>Маршрут:</b> {ticket.departure} → {ticket.arrival}</div>
        <div><b>Місце:</b> {ticket.seat}</div>
        <div><b>Дата:</b> {ticket.date}</div>
        <div><b>Ціна:</b> {ticket.price} грн</div>
        <div style={{margin: "14px 0 8px 0"}}><b>Ваш профіль:</b></div>
        <div>{user?.userName} ({user?.email})</div>
      </div>
      <form className="payment-form" onSubmit={handlePay}>
        <input
          type="text"
          placeholder="Номер картки"
          maxLength={19}
          value={card}
          onChange={e => setCard(e.target.value.replace(/[^\d\s]/g, ""))}
          required
        />
        <input
          type="text"
          placeholder="Термін дії (MM/YY)"
          maxLength={5}
          value={expiry}
          onChange={e => setExpiry(e.target.value)}
          required
        />
        <input
          type="text"
          placeholder="CVV"
          maxLength={3}
          value={cvv}
          onChange={e => setCvv(e.target.value.replace(/\D/g, ""))}
          required
        />
        {error && <div className="payment-error">{error}</div>}
        <button type="submit" disabled={loading}>
          {loading ? "Оплата..." : "Оплатити"}
        </button>
      </form>
    </div>
  );
};

export default PaymentPage;
