import Header from "./components/main/header";
import Sidebar from "./components/main/sidebar";
import DashboardContent from "./components/main/dashboard-content";
import MyTask from "./components/main/my-task";
import VitalTask from "./components/main/vitals-tasks";
import TaskCategories from "./components/main/categories";
import CreateCategory from "./components/main/create-category";
import CategoryModal from "./components/main/modal-category";
import useCategories from "./components/hooks/useCategories";
import { useState } from "react";

const Main = () => {
  const [activeId, setActiveId] = useState(1);
  const [isCreatingCategory, setIsCreatingCategory] = useState(false);
  const { categories, modalConfig, actions } = useCategories();

  const pages = {
    1: <DashboardContent />,
    2: <VitalTask />,
    3: <MyTask />,
    4: isCreatingCategory ? (
      <CreateCategory
        onCreate={(title) => {
          actions.addCategory(title);
          setIsCreatingCategory(false);
        }}
        onCancel={() => setIsCreatingCategory(false)}
      />
    ) : (
      <>
        <TaskCategories
          categories={categories}
          onEditItem={actions.openEdit}
          onDeleteItem={actions.deleteItem} // Тепер це миттєва функція
          onAddItem={actions.openAdd} // Для плюсика в таблиці
          onDeleteCategory={actions.deleteCategory}
          onAddCategoryClick={() => setIsCreatingCategory(true)}
          onGoBack={() => setActiveId(1)}
        />
        <CategoryModal
          {...modalConfig}
          onClose={actions.closeModal}
          onSubmit={actions.handleModalSubmit}
        />
      </>
    ),
  };

  return (
    <div className="main-page">
      <Header />
      <div className="main-content">
        <Sidebar activeId={activeId} setActiveId={setActiveId} />
        {pages[activeId] || <div>Сторінка в розробці</div>}
      </div>
    </div>
  );
};

export default Main;
