import DashboardContent from "../../main-pages/dashboard-content";
import DashboardOverlay from "../../main-pages/dashboard-overlay";
import MyTask from "../../main-pages/my-task";
import VitalTask from "../../main-pages/vitals-tasks";
import TaskCategories from "../../main-pages/categories";
import CreateCategory from "../../main-pages/create-category";
import CategoryModal from "../../main-pages/modal-category";
import ChangeAccountInfoPage from "../../main-pages/change-account-info-page";
import ChangePassword from "../../main-pages/change-password";
import EditTask from "../../main-pages/edit-task";
import TaskDetails from "../../main-pages/task-details";
import CreateTask from "../../main-pages/create-task";

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
        return (
          <ChangeAccountInfoPage user={user} setUser={uiActions.setUser} />
        );
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
