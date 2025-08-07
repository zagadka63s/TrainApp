import React, { useEffect, useState } from "react";
import '../styles/profile.css';

interface UserProfile {
  username: string;
  email: string;
  // можешь добавить ещё поля, если отдаёшь их с бэка
}

const ProfilePage: React.FC = () => {
  const [profile, setProfile] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      setLoading(false);
      return;
    }

    fetch("https://localhost:7105/api/auth/me", {
      headers: {
        Authorization: "Bearer " + token
      }
    })
      .then(res => {
        if (!res.ok) throw new Error("Не удалось получить данные профиля");
        return res.json();
      })
      .then(data => {
        setProfile(data);
        setLoading(false);
      })
      .catch(() => setLoading(false));
  }, []);

  if (loading) return <div>Загрузка профиля...</div>;
  if (!profile) return <div>Нет данных о пользователе</div>;

  return (
    <div className="profile-container">
      <h2 className="profile-title">Профиль пользователя</h2>
      <div className="profile-info">
        <p><b>Имя пользователя:</b> {profile.username}</p>
        <p><b>Email:</b> {profile.email}</p>
        {/* добавляй больше полей, если надо */}
      </div>
    </div>
  );
};

export default ProfilePage;
