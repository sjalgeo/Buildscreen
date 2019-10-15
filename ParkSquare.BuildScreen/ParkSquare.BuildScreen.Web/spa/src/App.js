import React from "react";
import "./App.css";
import dayjs from "dayjs";
import advancedFormat from "dayjs/plugin/advancedFormat";
import relativeTime from "dayjs/plugin/relativeTime";

dayjs.extend(advancedFormat);
dayjs.extend(relativeTime);

const compare = (a, b) => a.id - b.id;

function App() {
  const [builds, setBuilds] = React.useState([]);

  const fetchResource = () => {
    fetch("https://localhost:5001/api/builds", {
      method: "GET",
      headers: {}
    })
      .then(response => response.json())
      .then(data => {
        data.sort(compare).reverse();

        const success = data.filter(x => x.status === "succeeded");
        const inProgress = data.filter(x => x.status === "inProgress");
        const failed = data.filter(x => x.status === "failed");

        setBuilds([...inProgress, ...failed, ...success]);

        setTimeout(fetchResource, 5000);
      });
  };

  React.useEffect(fetchResource, []);

  return (
    <div>
      {builds.map(build => (
        <div key={build.id} className={`item box wrapper ${build.status}`}>
          {build.totalNumberOfTests > 0 && (
            <div className="ribbon">
              <span>{`${build.passedNumberOfTests}/${build.totalNumberOfTests}`}</span>
            </div>
          )}

          <span id="dateFilter" data-date={build.startBuildDateTime} />
          <div className="aspect-content">
            <a
              href={build.buildReportUrl}
              target="_blank"
              rel="noopener noreferrer"
            >
              <p className="item-title" title={build.buildDefinition}>
                {build.buildDefinition}
              </p>
            </a>
            <p className="item-subtitle" title="{{build.repoName}}">
              {build.repoName}
            </p>
            <div className="itemcontent">
              {build.status !== "inProgress" && build.status !== "notStarted" && (
                <p>
                  Built{" "}
                  <span>{dayjs(build.finishBuildDateTime).fromNow()}</span>
                </p>
              )}

              {build.status === "inProgress" && (
                <p>
                  Started{" "}
                  <span>{dayjs(build.startBuildDateTime).fromNow()}</span>
                </p>
              )}

              <p>
                by <span>{build.requestedByName}</span>
              </p>
              <p className="branch-name">{build.branch}</p>
            </div>
            {/* <div className="avatar">
              <img no-avatar src="https://www.gravatar.com/HASH" alt="avatar" />
            </div> */}
          </div>
        </div>
      ))}
    </div>
  );
}

export default App;
