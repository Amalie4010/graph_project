import { useParams, useNavigate } from "react-router-dom";
import useServerPage from "../viewModels/useServerPage";

// Style
import "./CSS/ServerPage.css"

// Components
import Chart from "../components/Chart"
import SideNav from "../components/SideNav"


function ServerStatusPage() {
  const navigate = useNavigate();

  const { id } = useParams();
  const { remove, data, info } = useServerPage(Number(id));

  const sampleData: number[] = data?.sampleData ?? [];
  const xnm: string[] = data?.xnm ?? [];
  
  const playerCount: number = sampleData[sampleData.length-1];

  return (
    <>
      <div id="layout">
        <SideNav />
        <main>
          <div id="title">
            <p>O==============================O</p>
            <p>{info?.address}</p>
            <p>O==============================O</p>
          </div>
          { info?.maxPlayer === 0 ? 
          <p id="error">This server is offline or doesn't exist</p> 
          :
          <div id="info">
            <div>
              <p className="describetion">Player max</p>
              <p className="number">{info?.maxPlayer}</p>
            </div>
            <div>
              <p className="describetion">Players online</p>
              <p className="number">{playerCount}</p>
            </div>
          </div> }
          <Chart sampleData={sampleData} xnm={xnm} />
          <button className="action" onClick={async () => {
            await remove();
            navigate("/");
          }}> 
            Delete
          </button>
        </main>
      </div>

    </>
  )
}

export default ServerStatusPage