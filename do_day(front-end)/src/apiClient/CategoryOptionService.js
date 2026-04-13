import apiClient from "./apiClient";

export const CategoryOptionService = {
  async createCategoryOption(categoryOption) {
    try {
      const response = await apiClient.post(
        "/CategoryOption/CreateCategoryOption",
        categoryOption,
      );

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },

  async updateCategoryOption(categoryOption) {
    try {
      const response = await apiClient.put(
        "/CategoryOption/UpdateCategoryOption",
        categoryOption,
      );

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },

  async deleteCategoryOption(id) {
    try {
      const response = await apiClient.delete(
        "/CategoryOption/DeleteCategoryOption",
        { params: { id } },
      );

      return response.data;
    } catch (error) {
      console.error("Error:", error.response?.data || error.message);
      throw error;
    }
  },
};
