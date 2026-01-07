import { useState } from "react";
import Header from "./components/main/header";
import Sidebar from "./components/main/sidebar";
import DashboardContent from "./components/main/dashboard-content";
import MyTask from "./components/main/my-task";

const testTask = {
  title: "Attend Nischal's Birthday Party",
  fullTitle: "Birthday Celebration for Nischal",
  objective: "Celebrate Nischal's birthday and have fun with friends.",
  description:
    "Buy gifts on the way and pick up cake from the bakery. (6 PM | Fresh Elements).....",
  priority: "Moderate",
  status: "Not Started",
  date: "20/06/2023",
  deadline: "6:00 PM Today",
  image: "images/task_image1.png",
  notes: ["Check bakery opening hours", "Buy wrapping paper"],
};

const Main = () => {
  // 1. Стейт для активного пункту меню
  const [activeId, setActiveId] = useState(1);

  // 2. Функція для вибору контенту
  const renderContent = () => {
    switch (activeId) {
      case 1:
        return <DashboardContent />;
      case 3:
        return <MyTask selectedTask={testTask} />;
      default:
        return (
          <div className="empty-state">
            <h1>Сторінка "{activeId}" у розробці</h1>
          </div>
        );
    }
  };

  return (
    <div className="main-page">
      <Header />
      <div className="main-content">
        <Sidebar activeId={activeId} setActiveId={setActiveId} />

        {renderContent()}
      </div>
    </div>
  );
};

export default Main;
