import { useState } from "react";

import Input from "./components/reg-log-components/input";
import Checkbox from "./components/reg-log-components/checkbox";
import Button from "./components/reg-log-components/button";

const Login = ({ onLogin, changeOnRegisterPage }) => {
  const [username, setUsername] = useState("");

  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const result = await AuthService.login(email, password);

      const isSuccess = result.item1;
      const userData = result.item2;

      if (isSuccess && userData) {
        localStorage.setItem("user", JSON.stringify(userData));

        onLogin();
      } else {
        setMessage("Неправильний логін або пароль");
      }
    } catch (err) {
      setMessage("Помилка підключення до сервера");
    }
  };
  return (
    <div className="reg-log login">
      <div className="auth-card">
        <div className="auth-form-container">
          <h2>Sign In</h2>
          <form
            onSubmit={(e) => {
              e.preventDefault();
              handleLogin();
            }}
          >
            <Input
              icon="/images/icons/username.svg"
              alt="user"
              placeholder="Enter Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />

            <Input
              icon="/images/icons/password.svg"
              alt="lock"
              type="password"
              placeholder="Enter Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />

            <Checkbox text="Remember me" />

            <Button text="Login" type="submit"></Button>

            <div className="social-auth-container">
              <div className="social-login-row">
                <span>Or, Login with</span>
                <div className="social-icons">
                  <img
                    src="/images/icons/facebook.svg"
                    alt="Facebook"
                    className="social-icon"
                  />
                  <img
                    src="/images/icons/google.svg"
                    alt="Google"
                    className="social-icon"
                  />
                  <img
                    src="/images/icons/x.svg"
                    alt="X"
                    className="social-icon"
                  />
                </div>
              </div>
            </div>
          </form>
          <div className="auth-footer">
            <span>Don’t have an account? </span>
            <span className="create-link" onClick={changeOnRegisterPage}>
              Create One
            </span>
          </div>
        </div>
        <div className="auth-image">
          <img
            src="/images/login.png"
            alt="Flat vector illustration of a woman standing next to a large smartphone showing a credit card and a green checkmark, symbolizing a completed transaction."
          />
        </div>
      </div>
    </div>
  );
};

export default Login;
