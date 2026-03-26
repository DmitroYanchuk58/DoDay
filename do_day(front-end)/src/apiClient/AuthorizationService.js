import axios from "axios";

const apiClient = axios.create({
  baseURL: "http://localhost:8080/api",
  headers: {
    "Content-Type": "application/json",
  },
});

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
      console.error(
        "Помилка реєстрації:",
        error.response?.data || error.message,
      );
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
      console.error("Login error:", error.response?.data || error.message);
      throw error;
    }
  },
};
