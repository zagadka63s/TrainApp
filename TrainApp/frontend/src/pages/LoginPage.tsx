import React, {useState} from "react";
import { useNavigate } from "react-router-dom";

const LoginPage: React.FC = () => {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    const response = await fetch("https://localhost:7105/api/auth/login", {
      method: "POST",
      headers : { "Content-Type": "application/json"},
      body: JSON.stringify ({userName, password})
    });

    setLoading(false);
    if(response.ok) {
      const data = await response.json();
      localStorage.setItem("token", data.token);
      setError("");
      navigate("/profile");
    } else {
      setError("Невірний логін або пароль!");
    }
  };

  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleLogin} autoComplete="on">
        <h2>Вхід до TrainApp</h2>
        <input
          type="text"
          placeholder="Ім'я користувача"
          value={userName}
          autoComplete="username"
          onChange={e => setUserName(e.target.value)}
          disabled={loading}
          required
        />
        <input
        type="password"
        placeholder="Пароль"
        value={password}
        autoComplete="current-password"
        onChange={e => setPassword(e.target.value)}
        disabled={loading}
        required
      />
      {error && <div className="error">{error}</div>}
      <button type ="submit" disabled={loading}>
        {loading ? "Вхід... " : "Увійти"}
      </button>
      <div className="form-link">
        Не маетє аккаунту?{" "}
        <span
          className="form-link-span"
          style={{ color : "397be7", cursor: "pointer"}}
          onClick={() => navigate ("/register")}
        >
          Зареєструватись
        </span>
      </div>
    </form>
  </div>
  );
};

export default LoginPage;