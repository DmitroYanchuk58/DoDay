import { useState } from "react";
import { AuthService } from "../apiClient/AuthorizationService";

import Checkbox from "./components/reg-log-components/checkbox";
import Input from "./components/reg-log-components/input";
import Button from "./components/reg-log-components/button";

const Register = ({ changeOnLoginPage }) => {
  // 1. Усі стейти на верхньому рівні
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [agree, setAgree] = useState(false);
  const [message, setMessage] = useState("");

  const handleRegister = async (e) => {
    if (e) e.preventDefault();

    if (!agree) {
      setMessage("Ви повинні погодитися з умовами.");
      return;
    }

    if (password !== confirmPassword) {
      setMessage("Паролі не збігаються!");
      return;
    }

    try {
      const data = await AuthService.register(
        username,
        password,
        email,
        firstName,
        lastName,
      );

      setMessage("Реєстрація успішна!");
      console.log("Дані від сервера:", data);
    } catch (err) {
      const errorDetail = err.response?.data?.detail;
      setMessage("Помилка: " + errorDetail);
    }
  };

  return (
    <div className="reg-log registartion">
      <div className="auth-card">
        <div className="auth-image">
          <img src="/images/register.png" alt="Registration Illustration" />
        </div>

        <div className="auth-form-container">
          <h2>Sign Up</h2>

          {/* Виводимо повідомлення про статус (успіх або помилка) */}
          {message && <p className="auth-message">{message}</p>}

          <form onSubmit={handleRegister}>
            <Input
              icon="/images/icons/first_name.svg"
              placeholder="Enter First Name"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
            />
            <Input
              icon="/images/icons/last_name.svg"
              placeholder="Enter Last Name"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
            />
            <Input
              icon="/images/icons/username.svg"
              placeholder="Enter Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <Input
              icon="/images/icons/email.svg"
              placeholder="Enter Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <Input
              icon="/images/icons/password.svg"
              placeholder="Enter Password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <Input
              icon="/images/icons/white_password.svg"
              placeholder="Confirm Password"
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />

            <Checkbox
              text="I agree to all terms"
              checked={agree}
              onChange={(e) => setAgree(e.target.checked)}
            />

            <Button text="Register" type="submit" disabled={!agree} />
          </form>

          <div className="social-auth-container">
            <div className="auth-footer">
              <span>Already have an account?</span>
              <span className="create-link" onClick={changeOnLoginPage}>
                Sign In
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
