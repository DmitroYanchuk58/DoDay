import { useState } from "react";
import { Link } from "react-router-dom";

import Checkbox from "./components/reg-log-components/checkbox";
import Input from "./components/reg-log-components/input";
import Button from "./components/reg-log-components/button";

const Register = ({ onRegister, changeOnLoginPage }) => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [agree, setAgree] = useState(false);

  return (
    <div className="reg-log registartion">
      <div className="auth-card">
        <div className="auth-image">
          <img
            src="/images/register.png"
            alt="Illustration of a man interacting with floating digital documents and a mobile interface"
          />
        </div>

        <div className="auth-form-container">
          <h2>Sign Up</h2>
          <form
            onSubmit={(e) => {
              e.preventDefault();
              onRegister();
            }}
          >
            <Input
              icon="/images/icons/first_name.svg"
              alt="first name"
              placeholder="Enter First Name"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
            />
            <Input
              icon="/images/icons/last_name.svg"
              alt="last name"
              placeholder="Enter Last Name"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
            />
            <Input
              icon="/images/icons/username.svg"
              alt="username"
              placeholder="Enter Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <Input
              icon="/images/icons/email.svg"
              alt="email"
              placeholder="Enter Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <Input
              icon="/images/icons/password.svg"
              alt="password"
              placeholder="Enter Password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <Input
              icon="/images/icons/white_password.svg"
              alt="confirm password"
              placeholder="Confirm Password"
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
            />

            <Checkbox text="I agree to all terms" />

            <Button text="Register" onClick={onRegister} disabled={!agree} />
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
