import React, { useState } from "react";
import TaskCard from "./task-card";

const MyTask = () => {
  const [tasks, setTasks] = useState([
    {
      id: 1,
      title: "Submit Documents",
      fullTitle: "Document Submission for Project X",
      objective: "To submit required documents for something important",
      description:
        "Review the list of documents required for submission and ensure all necessary documents are ready.",
      priority: "Extreme",
      status: "Not Started",
      date: "20/06/2023",
      deadline: "End of Day",
      image: "/images/task_image1.png",
      notes: ["Ensure documents are authentic", "Maintain confidentiality"],
    },
    {
      id: 2,
      title: "Complete assignments",
      fullTitle: "Final Year Assignments",
      objective: "Pass the final year with high grades",
      description:
        "Complete all research papers and submit them to the professor before the deadline.",
      priority: "Moderate",
      status: "In Progress",
      date: "21/06/2023",
      deadline: "Next Week",
      image: "/images/task_image2.png",
      notes: ["Check for plagiarism", "Format according to APA style"],
    },
    {
      id: 3,
      title: "Complete assignments",
      fullTitle: "Final Year Assignments",
      objective: "Pass the final year with high grades",
      description:
        "Complete all research papers and submit them to the professor before the deadline.",
      priority: "Moderate",
      status: "In Progress",
      date: "21/06/2026",
      deadline: "Next Week",
      image: "/images/task_image2.png",
      notes: ["Check for plagiarism", "Format according to APA style"],
    },
  ]);

  const [selectedTaskId, setSelectedTaskId] = useState(tasks[0].id);

  const selectedTask = tasks.find((t) => t.id === selectedTaskId);

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
                {...task}
                type="compact"
                className={selectedTaskId === task.id ? "selected" : ""}
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

export default MyTask;
