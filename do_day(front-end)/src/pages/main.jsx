import { useState } from "react";
import { useTasks } from "./tools/hooks/useTasks";
import useCategories from "../controllers/category-controller";

import Header from "./components/header";
import Sidebar from "./components/sidebar";

import PageRenderer from "./tools/additionalTools/page-renderer";

const Main = () => {
  const [activeId, setActiveId] = useState(1);
  const [selectedTask, setSelectedTask] = useState(null);
  const [user, setUser] = useState(() =>
    JSON.parse(localStorage.getItem("user")),
  );

  const { tasks, refreshTasks } = useTasks(user);
  const {
    categories,
    modalConfig,
    actions: categoryActions,
  } = useCategories(user.id);
  const [isCreatingCategory, setIsCreatingCategory] = useState(false);

  const [modals, setModals] = useState({
    invite: false,
    edit: false,
    create: false,
  });
  const [taskToEdit, setTaskToEdit] = useState(null);

  const uiActions = {
    setActiveId,
    setUser,
    modals,
    taskToEdit,
    openInvite: () => setModals((prev) => ({ ...prev, invite: true })),
    closeInvite: () => setModals((prev) => ({ ...prev, invite: false })),
    openCreateTask: () => setModals((prev) => ({ ...prev, create: true })),
    closeCreateTask: () => setModals((prev) => ({ ...prev, create: false })),
    openEditTask: (task) => {
      setTaskToEdit(task);
      setModals((prev) => ({ ...prev, edit: true }));
    },
    closeEditTask: () => setModals((prev) => ({ ...prev, edit: false })),
    openCreateCategoryOption: (categoryId) => {
      categoryActions.openAdd(categoryId);
    },
  };

  const isDashboardActive = activeId === 1 && !selectedTask;

  return (
    <div className="main-page">
      <Header />
      <div className="main-content">
        <div
          className={`sidebar-section ${isDashboardActive ? "dashboard" : ""}`}
        >
          <Sidebar activeId={activeId} setActiveId={setActiveId} user={user} />
        </div>

        <div
          className={`content-section ${isDashboardActive ? "dashboard" : ""}`}
        >
          <PageRenderer
            activeId={activeId}
            user={user}
            tasks={tasks}
            refreshTasks={refreshTasks}
            selectedTask={selectedTask}
            setSelectedTask={setSelectedTask}
            uiActions={uiActions}
            categoryData={{
              categories,
              modalConfig,
              actions: categoryActions,
              isCreating: isCreatingCategory,
              setIsCreating: setIsCreatingCategory,
            }}
          />
        </div>
      </div>
    </div>
  );
};

export default Main;
