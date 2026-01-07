import React, { useState } from "react";
import TaskCard from "./task-card";

const VitalTask = () => {
  // Тестові дані для Vital Tasks
  const [tasks] = useState([
    {
      id: 1,
      title: "Walk the dog",
      fullTitle: "Walk the dog",
      objective:
        "Take Luffy and Jiro for a leisurely stroll around the neighborhood.",
      description:
        "Take the dog to the park and bring treats as well. Enjoy the fresh air and give them the exercise and mental stimulation they need for a happy and healthy day. Don't forget to bring along squeaky and fluffy for some extra fun along the way!",
      priority: "Extreme",
      status: "Not Started",
      date: "20/06/2023",
      deadline: "Today",
      image: "images/task_image4.png",
      notes: [
        "Listen to a podcast or audiobook",
        "Practice mindfulness or meditation",
        "Take photos of interesting sights along the way",
        "Practice obedience training with your dog",
        "Chat with neighbors or other dog walkers",
        "Listen to music or an upbeat playlist",
      ],
    },
    {
      id: 2,
      title: "Take grandma to hospital",
      fullTitle: "Hospital Visit with Grandma",
      objective: "Ensure grandma gets her routine checkup safely.",
      description: "Go back home and take grandma to the hosp....",
      priority: "Moderate",
      status: "In Progress",
      date: "20/06/2023",
      deadline: "14:00 PM",
      image: "images/task_image5.png",
      notes: ["Bring medical documents", "Check appointment time"],
    },
  ]);

  const [selectedId, setSelectedId] = useState(tasks[0].id);
  const selectedTask = tasks.find((t) => t.id === selectedId);

  return (
    <div className="task-details-page">
      <div className="task-sidebar card tasks">
        <h3 className="sidebar-title">Vital Tasks</h3>
        <div className="sidebar-list">
          {tasks.map((task) => (
            <div key={task.id} onClick={() => setSelectedId(task.id)}>
              <TaskCard
                {...task}
                type="compact"
                className={selectedId === task.id ? "selected" : ""}
              />
            </div>
          ))}
        </div>
      </div>

      {selectedTask && (
        <div className="details-main card details">
          <div className="details-header">
            <img
              src={selectedTask.image}
              alt="Task large"
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

          <div className="details-actions">
            <button className="action-btn">
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
      )}
    </div>
  );
};

export default VitalTask;
