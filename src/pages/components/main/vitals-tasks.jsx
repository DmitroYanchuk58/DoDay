import React, { useState } from "react";
import TaskCard from "./task-card";

const VitalTask = () => {
  const [tasks, setTasks] = useState([
    {
      id: 1,
      title: "Walk the dog",
      description: "Take the dog to the park and bring treats as well...",
      priority: "Extreme",
      status: "Not Started",
      date: "20/06/2023",
      image: "images/task_image4.png",
      notes: ["Listen to a podcast", "Practice mindfulness"],
    },
    {
      id: 2,
      title: "Take grandma to hospital",
      description: "Go back home and take grandma to the hosp....",
      priority: "Moderate",
      status: "In Progress",
      date: "20/06/2023",
      image: "images/task_image5.png",
      notes: ["Bring medical documents"],
    },
  ]);

  const [selectedId, setSelectedId] = useState(tasks[0]?.id);
  const selectedTask = tasks.find((t) => t.id === selectedId);

  const deleteTask = (id) => {
    setTasks((prevTasks) => prevTasks.filter((task) => task.id !== id));

    if (selectedId === id) {
      const remainingTasks = tasks.filter((t) => t.id !== id);
      setSelectedId(remainingTasks[0]?.id);
    }
  };

  return (
    <div className="task-details-page">
      <div className="task-sidebar card tasks">
        <h3 className="sidebar-title">Vital Tasks</h3>
        <div className="sidebar-list">
          {tasks.map((task) => (
            <div key={task.id} onClick={() => setSelectedId(task.id)}>
              <TaskCard
                id={task.id}
                {...task}
                onDelete={deleteTask}
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
              <h2>{selectedTask.title}</h2>
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
