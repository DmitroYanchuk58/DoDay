import React from "react";
import TaskCard from "./task-card";
import StatusChart from "./status-chart";

const DashboardContent = () => {
  return (
    <main className="dashboard-main">
      <section className="welcome-section">
        <div className="welcome-text">
          <h1>Welcome back, Sundar 👋</h1>
        </div>
        <div className="team-avatars">
          <img src="/images/list_img1.png" alt="team" />
          <img src="/images/list_img2.png" alt="team" />
          <img src="/images/list_img3.png" alt="team" />
          <img src="/images/list_img4.png" alt="team" />
          <div className="more-count">+4</div>
          <button className="invite-btn">+ Invite</button>
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
            <button className="add-task-btn">
              <svg class="icon icon-plus">
                <use xlinkHref="images/icons/dashboard-icons.svg#icon-plus"></use>
              </svg>
              Add task
            </button>
          </div>
          <p className="current-date">
            20 June • <span>Today</span>
          </p>

          <TaskCard
            title="Attend Nischal's Birthday Party"
            description="Buy gifts on the way and pick up cake from the bakery. (6 PM | Fresh Elements)....."
            priority="Moderate"
            status="Not Started"
            date="20/06/2023"
            image="images/task_image1.png"
          />
          <TaskCard
            title="Landing Page Design for TravelDays"
            description="Get the work done by EOD and discuss with client before leaving. (4 PM | Meeting Room)"
            priority="Moderate"
            status="In Progress"
            date="20/06/2023"
            image="images/task_image2.png"
          />
          <TaskCard
            title="Presentation on Final Product "
            description="Make sure everything is functioning and all the necessities are properly met. Prepare the team and get the documents ready for..."
            priority="Moderate"
            status="In Progress"
            date="19/06/2023"
            image="images/task_image3.png"
          />
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
            <TaskCard
              title="Walk the dog"
              description="Take the dog to the park and bring treats as well."
              status="Completed"
              date="2 days ago"
              image="images/task_image4.png"
              type="compact"
            />
            <TaskCard
              title="Conduct meeting"
              description="Meet with the client and finalize requirements."
              status="Completed"
              date="2 days ago"
              image="images/task_image3.png"
              type="compact"
            />
          </div>
        </aside>
      </div>
    </main>
  );
};

export default DashboardContent;
