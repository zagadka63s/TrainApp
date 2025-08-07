import React, { useEffect, useState } from "react";
import { NavLink, Link, useNavigate } from "react-router-dom";

function parseJwt(token: string): any | null {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(window.atob(base64).split('').map(
      c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
    ).join(''));
    return JSON.parse(jsonPayload);
  } catch {
    return null;
  }
}

const Header: React.FC = () => {
  const [userName, setUserName] = useState<string | null>(null);
  const navigate = useNavigate();

  // Проверяем токен при загрузке (или при изменении)
  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      setUserName(null);
      return;
    }
    const payload = parseJwt(token);
    setUserName(payload?.unique_name || payload?.UserName || null); // зависит от того, как выдаёт backend
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("token");
    setUserName(null);
    navigate("/login");
  };

  return (
    <header className="main-header">
      <nav className="main-nav">
        <Link className="logo" to="/">TrainApp</Link>
        <div className="nav-links">
          <NavLink to="/" end>Головна</NavLink>
          <NavLink to="/tickets">Квитки</NavLink>
          <NavLink to="/trains">Поїзди</NavLink>
          <NavLink to="/stations">Станції</NavLink>
          {!userName ? (
            <NavLink to="/login">Увійти</NavLink>
          ) : (
            <div style={{ display: "flex", alignItems: "center", gap: 12 }}>
              <span
                style={{
                  fontWeight: 600,
                  color: "#294478",
                  marginRight: "8px",
                  cursor: "pointer",
                  background: "#e9f0fa",
                  borderRadius: "7px",
                  padding: "7px 12px"
                }}
                onClick={() => navigate("/profile")}
                title="Перейти до профілю"
              >
                {userName}
              </span>
              <button
                onClick={handleLogout}
                style={{
                  border: "none",
                  background: "#f7f8fc",
                  color: "#b43b2b",
                  borderRadius: 7,
                  fontWeight: 500,
                  padding: "7px 16px",
                  cursor: "pointer",
                  transition: "background 0.17s"
                }}
                onMouseOver={e => (e.currentTarget.style.background = "#ffeaea")}
                onMouseOut={e => (e.currentTarget.style.background = "#f7f8fc")}
              >
                Вийти
              </button>
            </div>
          )}
        </div>
      </nav>
    </header>
  );
};

export default Header;
