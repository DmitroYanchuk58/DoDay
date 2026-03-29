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
import TaskDetails from "./components/main/task-details";
import CreateTask from "./components/main/create-task";

import useCategories from "./components/hooks/useCategories";

import { useState } from "react";

const Main = () => {
  const [activeId, setActiveId] = useState(1);
  const [selectedTask, setSelectedTask] = useState(null);
  const [isCreatingCategory, setIsCreatingCategory] = useState(false);
  const { categories, modalConfig, actions } = useCategories();
  const [isInviteModalOpen, setIsInviteModalOpen] = useState(false);
  const [isEditingTask, setIsEditingTask] = useState(false);
  const [isCreatingTask, setIsCreatingTask] = useState(false);
  const [taskToEdit, setTaskToEdit] = useState(null);

  const [user, setUser] = useState(() => {
    const stickyValue = localStorage.getItem("user");
    return stickyValue !== null ? JSON.parse(stickyValue) : null;
  });

  const isDashboardActive = activeId === 1 && !selectedTask;

  const openTaskDetails = (task) => {
    setSelectedTask(task);
  };

  const openEditOverlay = (task) => {
    setTaskToEdit(task);
    setIsEditingTask(true);
  };

  const openCreateOverlay = () => {
    setIsCreatingTask(true);
  };

  const openInviteModal = () => setIsInviteModalOpen(true);
  const closeInviteModal = () => setIsInviteModalOpen(false);

  const pages = {
    1: selectedTask ? (
      <>
        <TaskDetails
          task={selectedTask}
          onGoBack={() => setSelectedTask(null)}
          onEdit={() => setIsEditingTask(true)}
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
    ) : (
      <>
        <DashboardContent
          onInviteClick={openInviteModal}
          onEditClick={openEditOverlay}
          onTaskClick={openTaskDetails}
          onCreateClick={openCreateOverlay}
          user={user}
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
        <CreateTask
          isOpen={isCreatingTask}
          onClose={() => setIsCreatingTask(false)}
          user={user}
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
    5: <AccountInfo user={user} setUser={setUser} />,
    6: <ChangePassword user={user} changeOnDashboard={() => setActiveId(1)} />,
  };

  return (
    <div className="main-page">
      <div className="header-section">
        <Header />
      </div>
      <div className="main-content">
        <div
          className={`sidebar-section ${isDashboardActive ? "dashboard" : ""}`}
        >
          <Sidebar activeId={activeId} setActiveId={setActiveId} user={user} />
        </div>

        <div
          className={`content-section ${isDashboardActive ? "dashboard" : ""}`}
        >
          {pages[activeId] || <div>Сторінка в розробці</div>}
        </div>
      </div>
    </div>
  );
};

export default Main;
