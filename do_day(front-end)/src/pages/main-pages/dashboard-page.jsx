import { useState, useEffect } from "react";
import { TaskService } from "../../apiClient/TaskService";
import TaskCard from "../components/task-card";
import StatusChart from "../components/status-chart";

const DashboardContent = ({
  onInviteClick,
  onEditClick,
  onTaskClick,
  onCreateClick,
  user,
  tasks,
  refreshTasks,
}) => {
  const completedTasks = (tasks || []).filter(
    (task) =>
      (task.status ?? task.Status)?.toString().toLowerCase() === "completed",
  );

  const todoTasks = tasks.filter(
    (t) => (t.status ?? t.Status)?.toString().toLowerCase() !== "completed",
  );

  const handleDeleteTask = async (id) => {
    try {
      await TaskService.deleteTask(id);
      if (refreshTasks) {
        await refreshTasks();
      }
    } catch (err) {
      console.error("Помилка оновлення:", err);
    }
  };

  const handleFinishingTask = async (task) => {
    try {
      task.status = "Completed";
      await TaskService.updateTask(task);
      if (refreshTasks) {
        await refreshTasks();
      }
    } catch (err) {
      console.error("Помилка оновлення:", err);
    }
  };

  const handleVitalTask = async (task) => {
    try {
      task.priority = "Urgent";
      await TaskService.updateTask(task);
      if (refreshTasks) {
        await refreshTasks();
      }
    } catch (err) {
      console.error("Помилка оновлення:", err);
    }
  };

  return (
    <main className="dashboard-main">
      <section className="welcome-section">
        <div className="welcome-text">
          <h1>Welcome back, {user?.username ?? ""} 👋</h1>
        </div>
        <div className="team-avatars">
          <img src="/images/list_img1.png" alt="team" />
          <img src="/images/list_img2.png" alt="team" />
          <img src="/images/list_img3.png" alt="team" />
          <img src="/images/list_img4.png" alt="team" />
          <div className="more-count">+4</div>
          <button className="invite-btn" onClick={onInviteClick}>
            + Invite
          </button>
        </div>
      </section>

      <div className="dashboard-grid">
        <section className="column todo">
          <div className="column-header">
            <div>
              <svg className="icon-to-do">
                <use xlinkHref="images/icons/dashboard-icons.svg#icon-to-do"></use>
              </svg>
              <h3>To-Do</h3>
            </div>
            <button className="add-task-btn" onClick={() => onCreateClick()}>
              <svg className="icon icon-plus">
                <use xlinkHref="images/icons/dashboard-icons.svg#icon-plus"></use>
              </svg>
              Add task
            </button>
          </div>
          <p className="current-date">
            20 June • <span>Today</span>
          </p>

          <div className="task-list">
            {todoTasks.map((task) => {
              const image =
                task.image && !task.image.startsWith("data:image")
                  ? `data:image/png;base64,${task.image}`
                  : task.image;

              return (
                <TaskCard
                  key={task.id}
                  task={task}
                  onVital={handleVitalTask}
                  onDelete={handleDeleteTask}
                  onFinish={handleFinishingTask}
                  openEditTask={(e, t) => onEditClick(t)}
                />
              );
            })}
          </div>
        </section>

        <aside className="aside">
          <div className="column task-status">
            <div className="column-header">
              <div>
                <svg className="icon-status">
                  <use xlinkHref="images/icons/dashboard-icons.svg#icon-task-status"></use>
                </svg>
                <h3>Task Status</h3>
              </div>
            </div>
            <div className="charts-row">
              <StatusChart percent={20} color="#FF4D4D" label="Not Started" />
              <StatusChart percent={50} color="#0052FF" label="In Progress" />
              <StatusChart percent={30} color="#00CA71" label="Completed" />
            </div>
          </div>

          <div className="column complete-tasks">
            <div className="column-header">
              <div>
                <svg className="icon-tasks">
                  <use xlinkHref="images/icons/dashboard-icons.svg#icon-completed-tasks"></use>
                </svg>
                <h3>Completed Task</h3>
              </div>
            </div>
            <div className="task-list">
              {completedTasks.map((task) => {
                const image =
                  task.image && !task.image.startsWith("data:image")
                    ? `data:image/png;base64,${task.image}`
                    : task.image;

                return (
                  <div
                    key={task.id}
                    onClick={() => onTaskClick(task)}
                    style={{ cursor: "pointer" }}
                  >
                    <TaskCard
                      key={task.id}
                      task={task}
                      onVital={handleVitalTask}
                      onDelete={handleDeleteTask}
                      onFinish={handleFinishingTask}
                      openEditTask={(e, t) => onEditClick(t)}
                    />
                  </div>
                );
              })}
            </div>
          </div>
        </aside>
      </div>
    </main>
  );
};

export default DashboardContent;
