import React, { useState } from "react";

const ChangePassword = ({ onGoBack }) => {
  const [formData, setFormData] = useState({
    firstName: "Sundar",
    lastName: "Gurung",
    email: "sundargurung360@gmail.com",
    currentPassword: "",
    newPassword: "",
    confirmPassword: "",
  });

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
        <form className="account-form" onSubmit={(e) => e.preventDefault()}>
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
                type="email"
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
