import apiClient from "./apiClient";

export const TaskService = {
  async createTask(idUser, name, description) {
    try {
      const response = await apiClient.post(
        "/Task/CreateTask",
        { name, description },
        {
          params: {
            idUser,
          },
        },
      );

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },

  async getTask(idTask) {
    try {
      const response = await apiClient.post(
        "/Task/GetTask",
        {},
        {
          params: {
            idTask,
          },
        },
      );

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },

  async getTasks(idUser) {
    try {
      const response = await apiClient.get("/Task/GetTasks", {
        params: {
          idUser,
        },
      });

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },
};
