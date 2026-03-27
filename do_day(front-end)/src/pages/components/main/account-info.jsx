import { useState } from "react";
import { UserService } from "../../../apiClient/UserService";

const AccountInfo = ({ onGoBack, user, setUser }) => {
  const [formData, setFormData] = useState({
    firstName: user?.firstName ?? "",
    lastName: user?.lastName ?? "",
    email: user?.email ?? "",
    contactNumber: user?.number ?? "",
    position: user?.position ?? "",
  });

  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("Saving...");

    try {
      const dataToSend = {
        id: user.id,
        firstName: formData.firstName,
        lastName: formData.lastName,
        email: formData.email,
        position: formData.position,
        number: formData.number,
      };

      await UserService.updateUser(dataToSend);

      setMessage("Account information updated successfully!");

      const updatedUser = {
        ...user,
        firstName: formData.firstName || user?.firstName || "",
        lastName: formData.lastName || user?.lastName || "",
        email: formData.email || user?.email || "",
        position: formData.position || user?.position || "",
        number: formData.number || user?.number || "",
      };

      if (!updatedUser.id) {
        console.error("Помилка: Спроба зберегти користувача без ID!");
        return;
      }

      localStorage.setItem("user", JSON.stringify(updatedUser));
      setUser(updatedUser);
    } catch (err) {
      const errorDetail =
        err.response?.data?.detail || err.message || "Update error";
      setMessage("Error: " + errorDetail);
    }
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
        <form className="account-form" onSubmit={handleSubmit}>
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
                value={formData.number}
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
            {message && <p className="auth-message">{message}</p>}
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
