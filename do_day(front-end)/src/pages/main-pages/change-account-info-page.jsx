import { useState } from "react";
import { UserService } from "../../apiClient/UserService";
import ProfileHeader from "../components/profile-header";
import AccountForm from "../components/account-form";

const ChangeAccountInfoPage = ({ onGoBack, user, setUser }) => {
  const [formData, setFormData] = useState({
    firstName: user?.firstName || "",
    lastName: user?.lastName || "",
    email: user?.email || "",
    number: user?.number || "",
    position: user?.position || "",
  });

  const [status, setStatus] = useState({ type: "", text: "" });
  const [isLoading, setIsLoading] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    setStatus({ type: "info", text: "Saving..." });

    try {
      await UserService.updateUser({ ...formData, id: user.id });
      const updatedUser = { ...user, ...formData };

      localStorage.setItem("user", JSON.stringify(updatedUser));
      setUser(updatedUser);
      setStatus({ type: "success", text: "Updated successfully!" });
    } catch (err) {
      setStatus({ type: "error", text: err.message || "Error" });
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="account-info-container">
      <ProfileHeader
        firstName={formData.firstName}
        lastName={formData.lastName}
        email={formData.email}
        onGoBack={onGoBack}
      />
      <div className="account-card">
        <AccountForm
          formData={formData}
          onChange={handleChange}
          onSubmit={handleSubmit}
          isLoading={isLoading}
          message={status}
        />
      </div>
    </div>
  );
};

export default ChangeAccountInfoPage;
