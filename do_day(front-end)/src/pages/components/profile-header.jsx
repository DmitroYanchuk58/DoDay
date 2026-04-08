const ProfileHeader = ({ firstName, lastName, email, onGoBack }) => (
  <>
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
        <h3>{`${firstName} ${lastName}`}</h3>
        <p>{email}</p>
      </div>
    </div>
  </>
);

export default ProfileHeader;
