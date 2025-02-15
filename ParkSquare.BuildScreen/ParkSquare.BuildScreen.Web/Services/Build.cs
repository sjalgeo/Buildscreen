﻿using System;

namespace ParkSquare.BuildScreen.Web.Services
{
    public class Build
    {
        public string Id { get; set; }

        public string TeamProject { get; set; }

        public string BuildDefinition { get; set; }

        public string Status { get; set; }

        public string RequestedByName { get; set; }

        public DateTime StartBuildDateTime { get; set; }

        public DateTime FinishBuildDateTime { get; set; }

        public int TotalNumberOfTests { get; set; }

        public int PassedNumberOfTests { get; set; }

        public string BuildReportUrl { get; set; } 

        public string RequestedByPictureUrl { get; set; }

        public string Branch { get; set; }

        public string RepoName { get; set; }
    }
}