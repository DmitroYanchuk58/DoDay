import { useState, useEffect } from "react";
import { TaskService } from "../../../apiClient/TaskService";

export const useTasks = (user) => {
  const [tasks, setTasks] = useState([]);

  const refreshTasks = async () => {
    if (!user?.id) return;
    try {
      const data = await TaskService.getTasks(user.id);
      setTasks(data);
    } catch (err) {
      console.error("Error refreshing tasks:", err);
    }
  };

  useEffect(() => {
    refreshTasks();
  }, [user?.id]);

  return { tasks, refreshTasks };
};
