import { Line } from "react-chartjs-2";
import { Chart as ChartJS, CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend, Filler } from "chart.js";

import "./CSS/Chart.css"
import type { ServerData } from "../models/ServerData";
ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend, Filler);

const Chart = ({ sampleData, xnm }: ServerData) => {

  const graphMax: number = Math.ceil((Math.max(...sampleData)+Math.max(...sampleData)/10)/100)*100;
  const graphMin: number = Math.floor((Math.min(...sampleData)-Math.min(...sampleData)/10)/100)*100;

  const canvasData = {
    datasets: [
      {
        label: "Home",
        borderColor: "sienna",
        pointRadius: 0,
        fill: true,
        backgroundColor: "sienna", // no?
        lineTension: 0.4,
        data: sampleData,
        borderWidth: 1,
      },
    ],
  };

  const options = {
    maintainAspectRatio: false,
    scales: {
      x: {
        grid: {
          display: false,
        },
        labels: xnm,
      },
      y: {
        min: graphMin,
        max: graphMax,
      },
    },
    responsive: true,
    plugins: {
      legend: {
        display: false,
      },
    }
  };

  return (
    <>
    <div className="chart-container">
      <Line id="home" options={options} data={canvasData} />
    </div>
    </>
  );
};

export default Chart;