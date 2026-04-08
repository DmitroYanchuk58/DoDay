import FormInput from "./form-input";

const AccountForm = ({ formData, onChange, onSubmit, isLoading, message }) => (
  <form className="account-form" onSubmit={onSubmit}>
    <div className="form-grid">
      <FormInput
        label="First Name"
        name="firstName"
        value={formData.firstName}
        onChange={onChange}
      />
      <FormInput
        label="Last Name"
        name="lastName"
        value={formData.lastName}
        onChange={onChange}
      />
      <FormInput
        label="Email Address"
        name="email"
        type="email"
        value={formData.email}
        onChange={onChange}
      />
      <FormInput
        label="Contact Number"
        name="number"
        value={formData.number}
        onChange={onChange}
      />
      <FormInput
        label="Position"
        name="position"
        value={formData.position}
        onChange={onChange}
        fullWidth
      />

      {message.text && (
        <p className={`status-message ${message.type}`}>{message.text}</p>
      )}
    </div>

    <div className="account-form-actions">
      <button type="submit" className="btn-save" disabled={isLoading}>
        {isLoading ? "Saving..." : "Save Changes"}
      </button>
    </div>
  </form>
);

export default AccountForm;
