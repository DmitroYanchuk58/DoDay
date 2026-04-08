import { useState, useEffect } from "react";
import { TaskService } from "../../apiClient/TaskService";
import TaskCard from "./task-card";

const MyTask = ({ user, refreshTasks }) => {
  const [tasks, setTasks] = useState([]);
  const fetchTasks = async () => {
    try {
      const data = await TaskService.getTasks(user.id);
      setTasks(data);
    } catch (err) {
      const errorDetail =
        err.response?.data?.detail || err.message || "Undefined error";
      console.error("Failed to fetch tasks:", errorDetail);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  const [selectedTaskId, setSelectedTaskId] = useState(tasks[0]?.id);

  const selectedTask = tasks.find((t) => t.id === selectedTaskId);

  const handleDeleteTask = async (id) => {
    try {
      await TaskService.deleteTask(id);
      setTasks((prev) => prev.filter((task) => task.id !== id));
      if (refreshTasks) {
        await refreshTasks();
      }
    } catch (err) {
      console.error("Помилка оновлення:", err);
    }
  };

  return (
    <div className="task-details-page">
      <div className="task-sidebar card tasks">
        <h3 className="sidebar-title">My Tasks</h3>
        <div className="sidebar-list">
          {tasks.map((task) => (
            <div
              key={task.id}
              onClick={() => setSelectedTaskId(task.id)}
              style={{ cursor: "pointer" }}
            >
              <TaskCard
                id={task.id}
                title={task.name || task.Name}
                {...task}
                onDelete={handleDeleteTask}
                type="compact"
                className={selectedTaskId === task.id ? "selected" : ""}
              />
            </div>
          ))}
        </div>
      </div>

      {selectedTask ? (
        <div className="details-main card details">
          <div className="details-header">
            <img
              src={selectedTask.image}
              alt="Task large"
              className="details-image"
            />
            <div className="details-title-block">
              <h2>{selectedTask.name}</h2>
              <div className="details-meta">
                <p>
                  Priority:{" "}
                  <span className="red-text">{selectedTask.priority}</span>
                </p>
                <p>
                  Status:{" "}
                  <span className="red-text">{selectedTask.status}</span>
                </p>
                <p className="gray-text">Created on: {selectedTask.date}</p>
              </div>
            </div>
          </div>

          <div className="details-body">
            <p>
              <strong>Task Title:</strong> {selectedTask.fullTitle}
            </p>
            <p>
              <strong>Objective:</strong> {selectedTask.objective}
            </p>
            <p>
              <strong>Task Description:</strong> {selectedTask.description}
            </p>
            {selectedTask.notes && (
              <div className="additional-notes">
                <strong>Additional Notes:</strong>
                <ul>
                  {selectedTask.notes.map((note, index) => (
                    <li key={index}>{note}</li>
                  ))}
                </ul>
              </div>
            )}
            <p>
              <strong>Deadline for Submission:</strong> {selectedTask.deadline}
            </p>
          </div>

          <div className="details-actions">
            <button
              className="action-btn"
              onClick={() => handleDeleteTask(selectedTask.id)}
            >
              <svg className="icon">
                <use
                  xlinkHref={`images/icons/my-task-icons.svg#icon-delete`}
                ></use>
              </svg>
            </button>
            <button className="action-btn">
              <svg className="icon">
                <use
                  xlinkHref={`images/icons/my-task-icons.svg#icon-edit`}
                ></use>
              </svg>
            </button>
          </div>
        </div>
      ) : (
        <div className="details-main card details empty-state">
          <p>Будь ласка, виберіть завдання або список порожній.</p>
        </div>
      )}
    </div>
  );
};

export default MyTask;
