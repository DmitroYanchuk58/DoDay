const TaskCategories = ({
  categories,
  onDeleteCategory,
  onAddCategoryClick,
  onGoBack,
  onEditItem,
  onDeleteItem,
  onAddItem,
}) => {
  return (
    <div className="task-categories-page">
      <div className="categories-card">
        <div className="categories-main-header">
          <h2>Task Categories</h2>
          <button className="go-back-link" onClick={onGoBack}>
            Go Back
          </button>
        </div>

        <button className="add-category-btn" onClick={onAddCategoryClick}>
          Add Category
        </button>

        <div className="categories">
          {categories.map((category) => (
            <TableSection
              key={category.id}
              title={category.title}
              data={category.items}
              onDeleteTable={() => onDeleteCategory(category.id)}
              onEditItem={(item) => onEditItem(category.id, item)}
              onDeleteItem={(item) => onDeleteItem(category.id, item)}
              onAddItem={() => onAddItem(category.id)}
            />
          ))}
        </div>
      </div>
    </div>
  );
};

const TableSection = ({ title, data, onDeleteItem, onEditItem, onAddItem }) => (
  <div className="category-section">
    <div className="category-header">
      <h3 className="category-title-text">{title}</h3>
      <div className="category-actions">
        <button className="btn-delete" onClick={onAddItem}>
          <img className="icon" src="images/icons/plus.svg"></img>
          Add item
        </button>
      </div>
    </div>

    <div className="table-container">
      <table className="task-table">
        <thead>
          <tr>
            <th className="col-sn">SN</th>
            <th className="col-name">{title}</th>
            <th className="col-action">Action</th>
          </tr>
        </thead>
        <tbody>
          {data.map((item, index) => (
            <tr key={item.id}>
              <td>{index + 1}</td>
              <td className="name-cell">{item.name}</td>
              <td className="action-cell">
                <button className="btn-edit" onClick={() => onEditItem(item)}>
                  <img className="icon" src="images/icons/edit.svg"></img>
                  Edit
                </button>
                <button
                  className="btn-delete"
                  onClick={() => onDeleteItem(item.id)}
                >
                  <img className="icon" src="images/icons/delete.svg"></img>
                  Delete
                </button>
              </td>
            </tr>
          ))}
          {data.length === 0 && (
            <tr>
              <td colSpan="3" style={{ textAlign: "center", color: "#ccc" }}>
                No items yet
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  </div>
);

export default TaskCategories;
