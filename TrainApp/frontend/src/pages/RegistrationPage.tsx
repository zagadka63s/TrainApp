import React, { useState } from "react";

const RegistrationPage: React.FC = () => {
  const [userName, setUserName] = useState("");
  const [email, setEmail ] = useState("");
  const [password, setPassword] = useState("");
  const [confirm, setConfirm] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();
    setSuccess("");
    setError("");

    if (password !== confirm) {
      setError("Пароли не совпадают!");
      return;
    }
    if (!userName.trim() || !password.trim()) {
      setError("Заполните все поля!");
      return;
    }

    try {
      const response = await fetch("https://localhost:7105/api/auth/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          userName,
          email,
          password,
          role: "User"
        })
      });

      if (response.ok) {
        setSuccess("Регистрация прошла успешно! Можете войти.");
        setUserName("");
        setPassword("");
        setConfirm("");
      } else {
        const data = await response.json().catch(() => null);
        setError((data && data.message) || "Ошибка при регистрации.");
      }
    } catch (err) {
      setError("Ошибка подключения к серверу.");
    }
  };

  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleRegister}>
        <h2>Регистрация</h2>
        <input
          type="text"
          placeholder="Имя пользователя"
          value={userName}
          onChange={e => setUserName(e.target.value)}
          required
        />
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Пароль"
          value={password}
          onChange={e => setPassword(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Повторите пароль"
          value={confirm}
          onChange={e => setConfirm(e.target.value)}
          required
        />
        {error && <div className="error">{error}</div>}
        {success && <div style={{ color: "green", marginBottom: 6 }}>{success}</div>}
        <button
          type="submit"
          disabled={!userName || !password || !confirm}
        >
          Зарегистрироваться
        </button>
      </form>
    </div>
  );
};

export default RegistrationPage;
