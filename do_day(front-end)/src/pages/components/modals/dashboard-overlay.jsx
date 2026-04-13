import React from "react";

const DashboardOverlay = ({ isOpen, onClose }) => {
  if (!isOpen) return null;

  return (
    <div className="modal-dash-overlay">
      <div className="modal-dash-container">
        <div className="modal-header">
          <div className="title-container">
            <h2 className="modal-title">Send an invite to a new member</h2>
            <div className="title-underline"></div>
          </div>
          <button className="go-back-btn" onClick={onClose}>
            Go Back
          </button>
        </div>

        <div className="modal-border">
          <div className="modal-section invite-section">
            <label className="input-label">Email</label>
            <div className="input-with-button">
              <input
                type="email"
                placeholder="neerajgurung99@gmail.com"
                className="modal-dash-input"
              />
              <button className="btn-primary">Send Invite</button>
            </div>
          </div>

          <div className="modal-section members-section">
            <label className="input-label">Members</label>
            <div className="members-list">
              <MemberItem
                name="Upashna Gurung"
                email="uppaeygrg332@gmail.com"
                role="Can edit"
                avatar="images/member1.jpg"
              />
              <MemberItem
                name="Jeremy Lee"
                email="jerrylee1998@gmail.com"
                role="Can edit"
                avatar="images/member2.jpg"
              />
              <MemberItem
                name="Thomas Park"
                email="parktho123@gmail.com"
                role="Owner"
                avatar="images/member3.jpg"
              />
              <MemberItem
                name="Rachel Takahashi"
                email="takahasirae32@gmail.com"
                role="Can edit"
                avatar="images/member4.jpg"
              />
            </div>
          </div>

          <div className="modal-section link-section">
            <label className="input-label">Project Link</label>
            <div className="input-with-button">
              <input
                type="text"
                value="https://sharelinkhereandthere.com/3456Syy29"
                readOnly
                className="modal-dash-input link-input"
              />
              <button className="btn-primary">Copy Link</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

const MemberItem = ({ name, email, role, avatar }) => (
  <div className="member-item">
    <div className="member-info">
      <img src={avatar} alt={name} className="member-avatar" />
      <div className="member-details">
        <span className="member-name">{name}</span>
        <span className="member-email">{email}</span>
      </div>
    </div>
    <div className="member-role">
      <select className="member-role" defaultValue={role}>
        <option value="Can edit">Can edit</option>
        <option value="Can view">Can view</option>
        <option value="Admin">Admin</option>
        <option value="Owner">Owner</option>
      </select>
    </div>
  </div>
);

export default DashboardOverlay;
