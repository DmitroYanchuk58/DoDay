import { useState } from "react";
import { AuthService } from "../apiClient/AuthorizationService";

import Checkbox from "./components/reg-log-components/checkbox";
import Input from "./components/reg-log-components/input";
import Button from "./components/reg-log-components/button";

const Register = ({ changeOnMainPage, changeOnLoginPage }) => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [agree, setAgree] = useState(false);
  const [message, setMessage] = useState("");

  const registrateUser = async (e) => {
    if (e) e.preventDefault();

    if (password !== confirmPassword) {
      setMessage("Passwords should be same");
      return;
    }

    try {
      const result = await AuthService.register(
        username,
        password,
        email,
        firstName,
        lastName,
      );

      console.log("Server responce:", result);

      const userData = result?.user || result;

      localStorage.setItem("user", JSON.stringify(userData));

      changeOnMainPage();
    } catch (err) {
      const errorDetail =
        err.response?.data?.detail || err.message || "Undefited error";
      setMessage("Error: " + errorDetail);
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

          <form onSubmit={registrateUser}>
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

            {message && <p className="auth-message">{message}</p>}

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
