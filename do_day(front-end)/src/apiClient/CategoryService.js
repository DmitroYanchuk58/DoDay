import apiClient from "./apiClient";

export const CategoryService = {
  async getCategories(idUser) {
    try {
      const response = await apiClient.get("/Category/GetCategoriesByUser", {
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

  async createCategory(category) {
    try {
      const response = await apiClient.post(
        "/Category/CreateCategory",
        category,
      );

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },

  async deleteCategory(id) {
    try {
      const response = await apiClient.delete("/Category/DeleteCategory", {
        params: { id },
      });

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },

  async updateCategory(category) {
    try {
      const response = await apiClient.put(
        "/Category/UpdateCategory",
        category,
      );

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },
};
