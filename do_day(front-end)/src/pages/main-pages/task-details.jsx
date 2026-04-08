import React from "react";

const TaskDetails = ({ task, onGoBack, onDelete, onEdit, onVital }) => {
  if (!task) return null;

  return (
    <div className="task-details">
      <div className="task-details-container">
        <div className="task-details-header">
          <div className="header-left">
            <img src={task.image} alt="Task" className="header-main-img" />
            <div className="header-info">
              <h1 className="task-full-title">{task.title}</h1>
              <div className="meta-info">
                <p>
                  Priority: <span className="blue-text">{task.priority}</span>
                </p>
                <p>
                  Status: <span className="red-text">{task.status}</span>
                </p>
                <p className="date-text">Created on: {task.date}</p>
              </div>
            </div>
          </div>
          <button className="go-back-btn" onClick={onGoBack}>
            Go Back
          </button>
        </div>

        <div className="task-details-content">
          <p className="main-desc-text">
            Buy gifts on the way and pick up cake from the bakery. (6 PM | Fresh
            Elements)
          </p>

          <ol className="ordered-task-list">
            <li>
              A cake, with candles to blow out. (Layer cake, cupcake, flat sheet
              cake)
            </li>
            <li>The birthday song.</li>
            <li>A place to collect gifts.</li>
          </ol>

          <div className="optional-section">
            <h3>Optional:</h3>
            <ul className="unordered-task-list">
              <li>Paper cone-shaped party hats, paper whistles that unroll.</li>
              <li>
                Games, activities (carry an object with your knees, then drop it
                into a milk bottle.)
              </li>
              <li>
                Lunch: sandwich halves, or pizza slices, juice, pretzels, potato
                chips...THEN cake & candles and the song.
              </li>
            </ul>
          </div>
        </div>

        <div className="bottom-actions-row">
          <button
            className="icon-action-btn delete"
            onClick={() => onDelete(task.id)}
          >
            <svg className="icon">
              <use
                xlinkHref={`images/icons/my-task-icons.svg#icon-delete`}
              ></use>
            </svg>
          </button>
          <button className="icon-action-btn edit" onClick={() => onEdit(task)}>
            <svg className="icon">
              <use xlinkHref={`images/icons/my-task-icons.svg#icon-edit`}></use>
            </svg>
          </button>
          <button
            className="icon-action-btn vital"
            onClick={() => onVital(task.id)}
          >
            <img src="images/icons/alert.svg" alt="Vital" />
          </button>
        </div>
      </div>
    </div>
  );
};

export default TaskDetails;
