import DashboardContent from "../main/dashboard-content";
import DashboardOverlay from "../main/dashboard-overlay";
import MyTask from "../main/my-task";
import VitalTask from "../main/vitals-tasks";
import TaskCategories from "../main/categories";
import CreateCategory from "../main/create-category";
import CategoryModal from "../main/modal-category";
import AccountInfo from "../main/account-info";
import ChangePassword from "../main/change-password";
import EditTask from "../main/edit-task";
import TaskDetails from "../main/task-details";
import CreateTask from "../main/create-task";

const PageRenderer = ({
  activeId,
  user,
  tasks,
  refreshTasks,
  selectedTask,
  setSelectedTask,
  uiActions,
  categoryData,
}) => {
  const renderDashboard = () => {
    if (selectedTask) {
      return (
        <TaskDetails
          task={selectedTask}
          onGoBack={() => setSelectedTask(null)}
          onEdit={() => uiActions.openEdit(selectedTask)}
        />
      );
    }

    return (
      <DashboardContent
        user={user}
        tasks={tasks}
        refreshTasks={refreshTasks}
        onInviteClick={uiActions.openInvite}
        onEditClick={uiActions.openEdit}
        onTaskClick={setSelectedTask}
        onCreateClick={uiActions.openCreate}
      />
    );
  };

  const renderPage = () => {
    switch (activeId) {
      case 1:
        return renderDashboard();
      case 2:
        return <VitalTask user={user} refreshTasks={refreshTasks} />;
      case 3:
        return <MyTask user={user} refreshTasks={refreshTasks} />;
      case 4:
        return categoryData.isCreating ? (
          <CreateCategory
            onCreate={(title) => {
              categoryData.actions.addCategory(title);
              categoryData.setIsCreating(false);
            }}
            onCancel={() => categoryData.setIsCreating(false)}
          />
        ) : (
          <TaskCategories
            categories={categoryData.categories}
            onAddCategoryClick={() => categoryData.setIsCreating(true)}
            onGoBack={() => uiActions.setActiveId(1)}
            {...categoryData.actions}
          />
        );
      case 5:
        return <AccountInfo user={user} setUser={uiActions.setUser} />;
      case 6:
        return (
          <ChangePassword
            user={user}
            changeOnDashboard={() => uiActions.setActiveId(1)}
          />
        );
      default:
        return <div>Сторінка в розробці</div>;
    }
  };

  return (
    <>
      {renderPage()}

      <DashboardOverlay
        isOpen={uiActions.modals.invite}
        onClose={uiActions.closeInvite}
      />
      <CreateTask
        isOpen={uiActions.modals.create}
        onClose={uiActions.closeCreate}
        user={user}
        onTaskCreated={refreshTasks}
      />
      <EditTask
        isOpen={uiActions.modals.edit}
        task={uiActions.taskToEdit}
        onClose={uiActions.closeEdit}
        onSave={refreshTasks}
      />
      <CategoryModal
        {...categoryData.modalConfig}
        onClose={categoryData.actions.closeModal}
        onSubmit={categoryData.actions.handleModalSubmit}
      />
    </>
  );
};

export default PageRenderer;
