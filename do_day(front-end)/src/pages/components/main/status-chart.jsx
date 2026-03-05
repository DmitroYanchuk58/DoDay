const StatusChart = ({ percent, color, label }) => {
  const chartStyle = {
    background: `conic-gradient(${color} ${percent * 3.6}deg, #EDF2F7 0deg)`,
  };

  return (
    <div className="status-chart-item">
      <div className="circular-chart" style={chartStyle}>
        <div className="chart-inner">
          <span className="percent-text">{percent}%</span>
        </div>
      </div>
      <span className="chart-label">{label}</span>
    </div>
  );
};

export default StatusChart;
