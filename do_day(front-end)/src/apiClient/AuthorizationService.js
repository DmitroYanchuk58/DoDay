import apiClient from "./apiClient";

export const AuthService = {
  async register(username, password, email, firstName, lastName) {
    try {
      const response = await apiClient.post("/Authorization/Register", {
        username,
        password,
        email,
        firstName,
        lastName,
      });

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },
  async login(email, password) {
    try {
      const response = await apiClient.get("/Authorization/Login", {
        params: {
          email: email,
          password: password,
        },
      });

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },
};
