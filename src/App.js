import { useState } from "react";
import { Routes, Route, Navigate, useNavigate } from "react-router-dom";
import "./css/main.css";
import RegisterPage from "./pages/register";
import LoginPage from "./pages/login";
import MainPage from "./pages/main";

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const navigate = useNavigate();

  const goToRegister = () => navigate("/register");
  const goToLogin = () => navigate("/login");
  const loginUser = () => setIsAuthenticated(true);

  return (
    <div className="main">
      <Routes>
        <Route
          path="/"
          element={
            isAuthenticated ? <MainPage /> : <Navigate to="/login" replace />
          }
        />

        <Route
          path="/register"
          element={
            isAuthenticated ? (
              <Navigate to="/" replace />
            ) : (
              <RegisterPage
                onRegister={() => setIsAuthenticated(true)}
                changeOnLoginPage={goToLogin}
              />
            )
          }
        />

        <Route
          path="/login"
          element={
            isAuthenticated ? (
              <Navigate to="/" replace />
            ) : (
              <LoginPage
                onLogin={loginUser}
                changeOnRegisterPage={goToRegister}
              />
            )
          }
        />
      </Routes>
    </div>
  );
}

export default App;
