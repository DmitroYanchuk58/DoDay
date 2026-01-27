import { useState } from "react";

const useCategories = () => {
  // 1. Оголошуємо стани
  const [categories, setCategories] = useState([
    {
      id: "cat_1",
      title: "Task Status",
      items: [
        { id: 1, name: "Completed" },
        { id: 2, name: "In Progress" },
        { id: 3, name: "Not Started" },
      ],
    },
    {
      id: "cat_2",
      title: "Task Priority",
      items: [
        { id: 1, name: "Extreme" },
        { id: 2, name: "Moderate" },
        { id: 3, name: "Low" },
      ],
    },
  ]);

  const [modalConfig, setModalConfig] = useState({ isOpen: false });

  // 2. Оголошуємо допоміжну функцію (вона має бути всередині хука)
  const updateItems = (categoryId, transformFn) => {
    setCategories((prev) =>
      prev.map((cat) =>
        cat.id === categoryId ? { ...cat, items: transformFn(cat.items) } : cat,
      ),
    );
  };

  // 3. Оголошуємо дії (також всередині хука)
  const actions = {
    addCategory: (title) => {
      setCategories((prev) => [
        ...prev,
        { id: `cat_${Date.now()}`, title, items: [] },
      ]);
    },

    deleteCategory: (id) => {
      setCategories((prev) => prev.filter((c) => c.id !== id));
    },

    openAdd: (categoryId) => {
      setModalConfig({
        isOpen: true,
        type: "add-item",
        categoryId,
        item: null,
        titlePrefix: "Add",
        titleMain: "New Item",
        submitLabel: "Create",
        initialValue: "",
        inputLabel: "Enter name for the new item",
      });
    },

    openEdit: (categoryId, item) => {
      setModalConfig({
        isOpen: true,
        type: "edit",
        categoryId,
        item,
        titlePrefix: "Edit",
        titleMain: "Item Name",
        submitLabel: "Update",
        initialValue: item.name,
      });
    },

    deleteItem: (categoryId, itemId) => {
      updateItems(categoryId, (items) => items.filter((i) => i.id !== itemId));
    },

    closeModal: () => setModalConfig({ isOpen: false }),

    handleModalSubmit: (newValue) => {
      const { type, categoryId, item } = modalConfig;

      if (type === "add-item") {
        updateItems(categoryId, (items) => [
          ...items,
          { id: Date.now(), name: newValue },
        ]);
      } else if (type === "edit") {
        updateItems(categoryId, (items) =>
          items.map((i) => (i.id === item.id ? { ...i, name: newValue } : i)),
        );
      }
      setModalConfig({ isOpen: false });
    },
  };

  // 4. Повертаємо все необхідне для використання в компонентах
  return { categories, modalConfig, actions };
};

export default useCategories;
