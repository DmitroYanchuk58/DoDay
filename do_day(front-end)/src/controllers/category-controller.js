import { useState, useEffect, useMemo } from "react";
import { CategoryService } from "../apiClient/CategoryService";
import { CategoryOptionService } from "../apiClient/CategoryOptionService";

class CategoryController {
  constructor(idUser, categories, setCategories, setModalConfig) {
    this.idUser = idUser;
    this.categories = categories;
    this.setCategories = setCategories;
    this.setModalConfig = setModalConfig;
  }

  async fetchAll() {
    if (!this.idUser) return;
    try {
      const data = await CategoryService.getCategories(this.idUser);
      const mapped = data.map((cat) => ({
        id: cat.id,
        title: cat.name,
        items: cat.categoryOptions.map((opt) => ({
          id: opt.id,
          name: opt.value,
        })),
      }));
      this.setCategories(mapped);
    } catch (err) {
      console.error("Fetch Error:", err);
    }
  }

  //Category actions

  async createCategory(title) {
    try {
      await CategoryService.createCategory({
        name: title,
        idUser: this.idUser,
      });
      await this.fetchAll();
      this.setModalConfig({ isOpen: false });
    } catch (err) {
      console.error("Add Error:", err);
    }
  }

  async deleteCategory(id) {
    try {
      await CategoryService.deleteCategory(id);
      this.setCategories((prev) => prev.filter((c) => c.id !== id));
    } catch (err) {
      console.error("Delete Error:", err);
    }
  }

  async createCategoryOption(idCategory, value) {
    try {
      await CategoryOptionService.createCategoryOption({ idCategory, value });
      await this.fetchAll();
      this.setModalConfig({ isOpen: false });
    } catch (err) {
      console.error("Create Category Option Error:", err);
    }
  }

  async updateCategoryOption(id, value, idCategory) {
    try {
      await CategoryOptionService.updateCategoryOption({
        idCategory,
        value,
        id,
      });
      await this.fetchAll();
      this.setModalConfig({ isOpen: false });
    } catch (err) {
      console.error("Create Category Option Error:", err);
    }
  }

  async deleteCategoryOption(id) {
    try {
      await CategoryOptionService.deleteCategoryOption(id);
      await this.fetchAll();
      this.setModalConfig({ isOpen: false });
    } catch (err) {
      console.error("Create Category Option Error:", err);
    }
  }

  openCreateCategoryModal() {
    this.setModalConfig({
      isOpen: true,
      type: "add-item",
      titleMain: "New Category",
      submitLabel: "Create",
      initialValue: "",
      onSubmit: (value) => this.createCategory(value),
    });
  }

  openCreateCategoryOptionModal(categoryId) {
    this.setModalConfig({
      isOpen: true,
      type: "add-item",
      categoryId: categoryId,
      titleMain: "New Category Option",
      submitLabel: "Create",
      initialValue: "",
      inputLabel: "Option Name",
      onSubmit: (value) => this.createCategoryOption(categoryId, value),
    });
  }

  openEditCategoryOptionModel(id, idCategory, oldValue) {
    this.setModalConfig({
      isOpen: true,
      type: "edit-item",
      categoryId: idCategory,
      categoryOptionId: id,
      titleMain: "Edit Category Option",
      submitLabel: "Edit",
      initialValue: oldValue,
      inputLabel: "Option Name",
      onSubmit: (value) => this.updateCategoryOption(idCategory, value, id),
    });
  }

  closeModal() {
    this.setModalConfig({ isOpen: false });
  }
}

const useCategories = (idUser) => {
  const [categories, setCategories] = useState([]);
  const [modalConfig, setModalConfig] = useState({ isOpen: false });

  const controller = useMemo(
    () =>
      new CategoryController(idUser, categories, setCategories, setModalConfig),
    [idUser, categories],
  );

  useEffect(() => {
    controller.fetchAll();
  }, [idUser]);

  return {
    categories,
    modalConfig,
    actions: controller,
  };
};

export default useCategories;
