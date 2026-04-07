import React, { useState, useEffect } from "react";
import TaskCard from "./task-card";
import { TaskService } from "../../../apiClient/TaskService";

const VitalTask = ({ user, refreshTasks }) => {
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

  const [selectedId, setSelectedId] = useState(tasks[0]?.id);
  const selectedTask = tasks.find((t) => t.id === selectedId);

  return (
    <div className="task-details-page">
      <div className="task-sidebar card tasks">
        <h3 className="sidebar-title">Vital Tasks</h3>
        <div className="sidebar-list">
          {tasks.map((task) => (
            <div key={task.id} onClick={() => setSelectedId(task.id)}>
              <TaskCard
                id={task.id}
                title={task.name}
                description={task.description}
                {...task}
                onDelete={handleDeleteTask}
                type="compact"
                isVitalPage={true}
                className={selectedId === task.id ? "selected" : ""}
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
              alt="Task"
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
            <p>{selectedTask.description}</p>
            {selectedTask.notes && (
              <div className="additional-notes">
                <ol>
                  {selectedTask.notes.map((note, index) => (
                    <li key={index}>{note}</li>
                  ))}
                </ol>
              </div>
            )}
          </div>
        </div>
      ) : (
        <div className="details-main card details empty">
          <p>Select a task to see details or list is empty</p>
        </div>
      )}
    </div>
  );
};

export default VitalTask;
