# Dashboard Searching WCF

This is a Windows Communication Foundation (WCF) web service project developed as an extension to the [Dashboard Searching Web App](https://github.com/zeynep-ates/Dashboard-Searching-Web-App). The purpose of this project is to provide a more adaptable and platform-independent solution for searching telemetry data within Grafana dashboards.

## Background

During my internship, I worked on a dashboard management problem related to satellite telemetry data. Grafana was used to visualize large datasets collected from various satellite components. Each dashboard contains many panels, and telemetry values can appear in multiple panels across multiple dashboards. When a telemetry ID changes or needs to be investigated, manually locating all affected panels is extremely time-consuming.

The original web application allowed users to search JSON dashboard data by telemetry ID or name. This WCF version was created to support:

- Platform independence
- Structured responses as class object lists
- Easier integration with other services or desktop applications

## Features

- Accepts telemetry ID or partial name as input
- Searches through Grafana dashboard JSON files
- Returns all matching panels and dashboard information
- Sends results as a list of C# class objects

## Technologies Used

- WCF (Windows Communication Foundation)
- .NET Framework
- C#
- Grafana HTTP API
- JSON parsing with Newtonsoft.Json

## Security Note

Sensitive information like API keys is not included in this repository. Please define your API key in a secure configuration file such as `App.config` or environment variables before running the application.

## Related Repository

[Dashboard-Searching-Web-App](https://github.com/zeynep-ates/Dashboard-Searching-Web-App)
