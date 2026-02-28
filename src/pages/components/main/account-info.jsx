import React, { useState } from "react";

const AccountInfo = ({ onGoBack }) => {
  const [formData, setFormData] = useState({
    firstName: "Sundar",
    lastName: "Gurung",
    email: "sundargurung360@gmail.com",
    contactNumber: "",
    position: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  return (
    <div className="account-info-container">
      <div className="account-header">
        <div className="title-with-underline">
          <span className="main-title">Account Information</span>
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
              <label>First Name</label>
              <input
                type="text"
                name="firstName"
                value={formData.firstName}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              <label>Last Name</label>
              <input
                type="text"
                name="lastName"
                value={formData.lastName}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              <label>Email Address</label>
              <input
                type="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              <label>Contact Number</label>
              <input
                type="text"
                name="contactNumber"
                value={formData.contactNumber}
                onChange={handleChange}
              />
            </div>

            <div className="form-group full-width">
              <label>Position</label>
              <input
                type="text"
                name="position"
                value={formData.position}
                onChange={handleChange}
              />
            </div>
          </div>

          <div className="account-form-actions">
            <button type="submit" className="btn-save">
              Save Changes
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

export default AccountInfo;
