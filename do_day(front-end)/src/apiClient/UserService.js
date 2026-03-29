import apiClient from "./apiClient";

export const UserService = {
  async changePassword(idUser, oldPassword, newPassword) {
    try {
      const response = await apiClient.post("/User/ChangePassword", null, {
        params: {
          oldPassword,
          newPassword,
          idUser,
        },
      });

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },

  async updateUser(userData) {
    try {
      const response = await apiClient.post("/User/UpdateUser", userData);
      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },
};
