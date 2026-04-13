import DashboardContent from "../../main-pages/dashboard-page";
import DashboardOverlay from "../../components/modals/dashboard-overlay";
import MyTask from "../../main-pages/my-task-page";
import VitalTask from "../../main-pages/vitals-tasks-page";
import TaskCategories from "../../main-pages/categories-page";
import CategoryModal from "../../components/modals/category-modal";
import ChangeAccountInfoPage from "../../main-pages/change-account-info-page";
import ChangePassword from "../../main-pages/change-password-page";
import EditTask from "../../components/edit-task";
import TaskDetails from "../../components/task-details";
import CreateTask from "../../components/create-task";

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
        onEditClick={uiActions.openEditTask}
        onTaskClick={setSelectedTask}
        onCreateClick={uiActions.openCreateTask}
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
        return categoryData.modalConfig.isOpen ? (
          <CategoryModal
            config={categoryData.modalConfig}
            onCancel={() => categoryData.actions.closeModal()}
            onSubmit={(value) => categoryData.modalConfig.onSubmit(value)}
          />
        ) : (
          <TaskCategories
            categories={categoryData.categories}
            onGoBack={() => uiActions.setActiveId(1)}
            onAddCategoryClick={() =>
              categoryData.actions.openCreateCategoryModal()
            }
            onAddCategoryOptionClick={(id) =>
              categoryData.actions.openCreateCategoryOptionModal(id)
            }
            onEditCategoryOptionClick={(categoryId, item) =>
              categoryData.actions.openEditCategoryOptionModel(
                categoryId,
                item.id,
                item.name,
              )
            }
            onDeleteCategoryOptionClick={(itemId) =>
              categoryData.actions.deleteCategoryOption(itemId)
            }
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
        onClose={uiActions.closeCreateTask}
        user={user}
        onTaskCreated={refreshTasks}
      />
      <EditTask
        isOpen={uiActions.modals.edit}
        task={uiActions.taskToEdit}
        onClose={uiActions.closeEditTask}
        onSave={refreshTasks}
      />
    </>
  );
};

export default PageRenderer;
