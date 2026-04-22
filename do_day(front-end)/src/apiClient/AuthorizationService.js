import apiClient from "./apiClient";

export const AuthService = {
  async register(username, password, email, firstName, lastName) {
    const response = await apiClient.post("/Authorization/Register", {
      username,
      password,
      email,
      firstName,
      lastName,
    });

    if (response.data?.isSuccess) {
      localStorage.setItem("user", JSON.stringify(response.data.user));
      localStorage.setItem("token", JSON.stringify(response.data.token));
    }
    return response.data;
  },

  async login(email, password) {
    const response = await apiClient.get("/Authorization/Login", {
      params: { email, password },
    });

    if (response.data?.isSuccess) {
      const { user, token } = response.data;

      localStorage.setItem("user", JSON.stringify(user));
      localStorage.setItem("token", JSON.stringify(token));
    }

    return response.data;
  },
};
