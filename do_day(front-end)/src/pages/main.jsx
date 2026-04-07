import { useState } from "react";
import { useTasks } from "./components/hooks/useTasks";

import Header from "./components/main/header";
import Sidebar from "./components/main/sidebar";

import useCategories from "./components/hooks/useCategories";
import PageRenderer from "./components/additionalTools/page-renderer";

const Main = () => {
  const [activeId, setActiveId] = useState(1);
  const [selectedTask, setSelectedTask] = useState(null);
  const [user, setUser] = useState(() =>
    JSON.parse(localStorage.getItem("user")),
  );

  const { tasks, refreshTasks } = useTasks(user);
  const { categories, modalConfig, actions: catActions } = useCategories();
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
    openCreate: () => setModals((prev) => ({ ...prev, create: true })),
    closeCreate: () => setModals((prev) => ({ ...prev, create: false })),
    openEdit: (task) => {
      setTaskToEdit(task);
      setModals((prev) => ({ ...prev, edit: true }));
    },
    closeEdit: () => setModals((prev) => ({ ...prev, edit: false })),
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
              actions: catActions,
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
