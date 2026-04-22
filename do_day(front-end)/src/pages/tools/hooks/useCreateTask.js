import { useState, useRef } from "react";
import { TaskService } from "../../../apiClient/TaskService";

export const useCreateTask = (user, onClose, onTaskCreated) => {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [priority, setPriority] = useState("Low");
  const [finishDate, setFinishDate] = useState(
    new Date().toISOString().split("T")[0],
  );
  const [image, setImage] = useState(null);
  const [message, setMessage] = useState("");
  const fileInputRef = useRef(null);

  const handleFile = (file) => {
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => setImage(reader.result);
      reader.readAsDataURL(file);
    }
  };

  const createTask = async () => {
    if (!name || !description) {
      setMessage("Title and Description fields should be filled");
      return;
    }

    let cleanImage = image;
    if (cleanImage?.includes("base64,")) {
      cleanImage = cleanImage.split("base64,")[1];
    }

    try {
      await TaskService.createTask(
        user.id,
        name,
        description,
        priority,
        finishDate,
        cleanImage,
      );
      setName("");
      setDescription("");
      setImage(null);
      onClose();
      if (onTaskCreated) await onTaskCreated();
    } catch (err) {
      console.error("Upload error:", err);
      setMessage("Error creating task.");
    }
  };

  return {
    state: { name, description, priority, finishDate, image, message },
    setters: { setName, setDescription, setPriority, setFinishDate, setImage },
    fileInputRef,
    handleFile,
    createTask,
  };
};
