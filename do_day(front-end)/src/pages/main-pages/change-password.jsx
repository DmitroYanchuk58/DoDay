import { useState } from "react";
import { UserService } from "../../apiClient/UserService";

const ChangePassword = ({ onGoBack, user, changeOnDashboard }) => {
  const [formData, setFormData] = useState({
    firstName: user?.firstName ?? "",
    lastName: user?.lastName ?? "",
    email: user?.email ?? "",
    currentPassword: "",
    newPassword: "",
    confirmPassword: "",
  });

  const [message, setMessage] = useState();

  const isNullOrWhiteSpace = (str) => {
    return !str || str.trim().length === 0;
  };

  const toChangePassword = async (e) => {
    if (e) e.preventDefault();

    if (isNullOrWhiteSpace(formData.currentPassword)) {
      setMessage("Current password shouldn't be empty");
      return;
    }

    if (isNullOrWhiteSpace(formData.newPassword)) {
      setMessage("Old password shouldn't be empty");
      return;
    }

    if (formData.newPassword !== formData.confirmPassword) {
      setMessage("Passwords should be same");
      return;
    }

    try {
      const result = await UserService.changePassword(
        user.id,
        formData.currentPassword,
        formData.newPassword,
      );
      setMessage("Your password was changed successfuly");
    } catch (err) {
      const errorDetail =
        err.response?.data?.detail || err.message || "Undefited error";
      setMessage("Error: " + errorDetail);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  return (
    <div className="account-info-container">
      <div className="account-header">
        <div className="title-with-underline">
          <span className="main-title">Change Password</span>
        </div>
        <button className="go-back-link" onClick={onGoBack}>
          Go Back
        </button>
      </div>
      <div className="profile-summary">
        <img
          src="images/profile_image.jpg"
          alt="Profile"
          className="profile-avatar-large"
        />
        <div className="profile-text">
          <h3>{`${formData.firstName} ${formData.lastName}`}</h3>
          <p>{formData.email}</p>
        </div>
      </div>

      <div className="account-card">
        <form className="account-form" onSubmit={toChangePassword}>
          <div className="form-grid">
            <div className="form-group">
              <label>Current Password</label>
              <input
                type="text"
                name="currentPassword"
                value={formData.currentPassword}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              <label>New Password</label>
              <input
                type="text"
                name="newPassword"
                value={formData.newPassword}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              <label>Confirm Password</label>
              <input
                type="text"
                name="confirmPassword"
                value={formData.confirmPassword}
                onChange={handleChange}
              />
            </div>
            {message && <p className="auth-message">{message}</p>}
          </div>

          <div className="account-form-actions">
            <button type="submit" className="btn-save">
              Update Password
            </button>
            <button type="button" className="btn-cancel" onClick={onGoBack}>
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default ChangePassword;
