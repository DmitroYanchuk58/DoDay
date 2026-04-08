import { useState, useEffect } from "react";
import { TaskService } from "../../apiClient/TaskService";
import TaskCard from "./task-card";
import StatusChart from "./status-chart";

const DashboardContent = ({
  onInviteClick,
  onEditClick,
  onTaskClick,
  onCreateClick,
  user,
  tasks,
  refreshTasks,
}) => {
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

  const todoTasks = tasks.filter((t) => {
    const status = t.column || t.Column;
    return status === "todo" || !status;
  });

  const completedTasks = tasks.filter((t) => {
    const status = t.column || t.Column;
    return status === "completed";
  });

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
            {todoTasks.map((task) => (
              <TaskCard
                key={task.id || task.Id}
                title={task.name || task.Name}
                description={task.description || task.Description}
                {...task}
                onDelete={handleDeleteTask}
              />
            ))}
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
              {completedTasks.map((task) => (
                <div
                  key={task.id}
                  onClick={() => onTaskClick(task)}
                  style={{ cursor: "pointer" }}
                >
                  <TaskCard
                    key={task.id}
                    {...task}
                    type="compact"
                    onDelete={handleDeleteTask}
                    openEditTask={(e) => {
                      e.stopPropagation();
                      onEditClick(task);
                    }}
                  />
                </div>
              ))}
            </div>
          </div>
        </aside>
      </div>
    </main>
  );
};

export default DashboardContent;
