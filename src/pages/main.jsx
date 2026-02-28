import Header from "./components/main/header";
import Sidebar from "./components/main/sidebar";
import DashboardContent from "./components/main/dashboard-content";
import DashboardOverlay from "./components/main/dashboard-overlay";
import MyTask from "./components/main/my-task";
import VitalTask from "./components/main/vitals-tasks";
import TaskCategories from "./components/main/categories";
import CreateCategory from "./components/main/create-category";
import CategoryModal from "./components/main/modal-category";
import AccountInfo from "./components/main/account-info";
import ChangePassword from "./components/main/change-password";
import EditTask from "./components/main/edit-task";

import useCategories from "./components/hooks/useCategories";

import { useState } from "react";

const Main = () => {
  const [activeId, setActiveId] = useState(1);
  const [isCreatingCategory, setIsCreatingCategory] = useState(false);
  const { categories, modalConfig, actions } = useCategories();
  const [isInviteModalOpen, setIsInviteModalOpen] = useState(false);
  const [isEditingTask, setIsEditingTask] = useState(false);
  const [taskToEdit, setTaskToEdit] = useState(null);

  const openEditOverlay = (task) => {
    setTaskToEdit(task);
    setIsEditingTask(true);
  };

  const openInviteModal = () => setIsInviteModalOpen(true);
  const closeInviteModal = () => setIsInviteModalOpen(false);

  const pages = {
    1: (
      <>
        <DashboardContent
          onInviteClick={openInviteModal}
          onEditClick={openEditOverlay}
        />
        <DashboardOverlay
          isOpen={isInviteModalOpen}
          onClose={closeInviteModal}
        />

        <EditTask
          isOpen={isEditingTask}
          task={taskToEdit}
          onClose={() => setIsEditingTask(false)}
          onSave={(updatedData) => {
            console.log("Збережено:", updatedData);
            setIsEditingTask(false);
          }}
        />
      </>
    ),
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
          onDeleteItem={actions.deleteItem}
          onAddItem={actions.openAdd}
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
    5: <AccountInfo />,
    6: <ChangePassword />,
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
