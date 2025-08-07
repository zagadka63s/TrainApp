import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import AppFooter from "./components/AppFooter";
import HomePage from "./pages/HomePage";
import ProfilePage from "./pages/ProfilePage";
import LoginPage from "./pages/LoginPage";
import RegistrationPage from "./pages/RegistrationPage";
import TicketsPage from "./pages/TicketsPage";
import TrainsPage from "./pages/TrainsPage";
import StationsPage from "./pages/StationsPage";
import PaymentPage from "./pages/PaymentPage";

const App: React.FC = () => (
  <Router>
    <Header />
    <main>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegistrationPage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/tickets" element={<TicketsPage />} />
        <Route path="/stations" element={<StationsPage />} />
        <Route path="/trains" element={<TrainsPage/> } />
        <Route path="/payment/:ticketId" element={<PaymentPage />} />
      </Routes>
    </main>
    <AppFooter />
  </Router>
);

export default App;
