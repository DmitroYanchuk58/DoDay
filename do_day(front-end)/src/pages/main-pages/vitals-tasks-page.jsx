import React, { useState, useEffect } from "react";
import TaskCard from "../components/task-card";
import { TaskService } from "../../apiClient/TaskService";

const VitalTask = ({ user, refreshTasks, onEditClick, tasks }) => {
  const vitalTasks = (tasks || []).filter(
    (task) =>
      (task.priority ?? task.Priority)?.toString().toLowerCase() === "urgent",
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
      task.priority = "Low";
      await TaskService.updateTask(task);
      if (refreshTasks) {
        await refreshTasks();
      }
    } catch (err) {
      console.error("Помилка оновлення:", err);
    }
  };

  const [selectedId, setSelectedId] = useState(vitalTasks[0]?.id);
  const selectedTask = vitalTasks.find((t) => t.id === selectedId);

  return (
    <div className="task-details-page">
      <div className="task-sidebar card vitalTasks">
        <h3 className="sidebar-title">Vital Tasks</h3>
        <div className="sidebar-list">
          {vitalTasks.map((task) => (
            <div key={task.id} onClick={() => setSelectedId(task.id)}>
              <TaskCard
                key={task.id}
                task={task}
                onVital={handleVitalTask}
                onDelete={handleDeleteTask}
                onFinish={handleFinishingTask}
                openEditTask={(e, t) => onEditClick(t)}
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
                <p className="gray-text">
                  Created on: {selectedTask.dateCreated.split("T")[0]}
                </p>
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
