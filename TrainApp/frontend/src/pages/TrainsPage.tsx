import React, { useEffect, useState } from "react";
import '../styles/trains.css';

interface Train {
    id: number;
    number : string;
    name : string;
    type : string;
}

const TrainsPage: React.FC = () => {
    const [trains, setTrains] = useState<Train[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetch("https://localhost:7105/api/trains")
          .then(res => {
            if(!res.ok) throw new Error("Error receiving trains");
            return res.json();
          })
          .then(data => {
            setTrains(data);
            setLoading(false);
          })
          .catch(() => setLoading(false));
    }, []);

    if( loading ) return <div className="trains-loader">Загрузка поездов...</div>;

    return (
        <div className="trains-container">
            <h2 className="trains-title">Список поездов</h2>
            {trains.length === 0 ? (
                <div className = "trains-empty">Нет поездов</div>
            ) : (
            <ul className="trains-list">
                {trains.map(train => (
                    <li key={train.id} className="train-card">
                        <div><b>{train.number}</b> - {train.name}</div>
                        <div>Тип: {train.type}</div>
                    </li>
                ))}
            </ul>
            )}
        </div>
    );
};

export default TrainsPage;