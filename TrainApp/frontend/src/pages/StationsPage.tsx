import React, {useEffect, useState } from "react";
import '../styles/stations.css';

interface Station {
  id: number;
  name: string;
  city: string;
  code: string;
  latitude: number | null;
  longitube : number | null;
}

const StationsPage: React.FC = () => {
  const [stations, setStations] = useState<Station[]>([]);
  const [loading, setLoading]= useState(true);
  const [search, setSearch] = useState("");

  useEffect(() => {
    fetch("https://localhost:7105/api/Stations")
      .then(res => {
        if(!res.ok) throw new Error("Помилка при отриманні станцій");
        return res.json();
      })
      .then(data => {
        setStations(data);
        setLoading(false);
      })
      .catch(() => setLoading(false));
  }, []);

  const filteredStations = stations.filter(station => 
    station.name.toLowerCase().includes(search.toLowerCase ()) ||
    station.city.toLowerCase().includes(search.toLowerCase())
  );

  if (loading) return <div className="stations-loader">Завантаження станцій...</div>;

  return (
    <div className="stations-container">
      <div className="stations-header-block">
        <h2 className="stations-title">Станції</h2>
        <input
          type="text"
          placeholder="Пошук станції або міста"
          value={search}
          onChange={e => setSearch(e.target.value)}
          className="stations-search"
        />
      </div>
      {filteredStations.length === 0 ? (
        <div className="stations-empty">станцій не знайдено</div>
      ) : (
        <ul className="stations-list">
          {filteredStations.map(station => (
            <li key={station.id} className="station-card">
              <div><b>{station.name}</b> ({station.code})</div>
              <div>Місто: {station.city}</div>
              <div>Широта: {station.latitude ?? '-'}</div>
              <div>Довгота: {station.longitube ?? '-'}</div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default StationsPage;