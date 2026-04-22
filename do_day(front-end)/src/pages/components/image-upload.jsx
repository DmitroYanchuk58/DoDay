import React from "react";

const ImageUpload = ({ image, fileInputRef, onFileSelect }) => {
  const handleDrop = (e) => {
    e.preventDefault();
    onFileSelect(e.dataTransfer.files[0]);
  };

  return (
    <div className="edit-form-right">
      <label className="upload-label">Upload Image</label>
      <div
        className="upload-dropzone"
        onDragOver={(e) => e.preventDefault()}
        onDrop={handleDrop}
      >
        <input
          type="file"
          ref={fileInputRef}
          onChange={(e) => onFileSelect(e.target.files[0])}
          style={{ display: "none" }}
          accept="image/*"
        />
        {image ? (
          <img
            src={image}
            alt="Preview"
            className="upload-preview"
            style={{ width: "100%", height: "100%", objectFit: "cover" }}
          />
        ) : (
          <>
            <div className="upload-icon-box">
              <img src="images/icons/upload-image.svg" alt="upload" />
            </div>
            <p>Drag & Drop files here</p>
            <span>or</span>
            <button
              className="btn-browse"
              type="button"
              onClick={() => fileInputRef.current.click()}
            >
              Browse
            </button>
          </>
        )}
      </div>
    </div>
  );
};

export default ImageUpload;
