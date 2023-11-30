# Student Module Manager

Student Module Manager is a web platform designed to help students manage their academic modules. The platform provides students with the ability to log in and view details of their current year modules, track their progress, and manage their module information.

## Features
- **User Authentication:** Secure login system for students to access personalized module information.
- **Module Dashboard:** An intuitive dashboard displaying a list of enrolled modules with details such as module code, title, and study progress.
- **Study Analytics:** Track and display the most studied modules based on user activity.
- **Edit and Delete Modules:** Allow students to edit and delete module details to keep their information up-to-date.
- **Responsive Design:** A user-friendly interface accessible on both desktop and mobile devices.

## Video Demo
https://github.com/Tobyrams/StudentModuleManager/assets/87528122/334dda39-25a9-4497-92e6-abeb3eb51395

## Getting Started

These instructions will help you set up a local development environment for the Student Module Manager project.

### Prerequisites:

1. **Visual Studio 2022 or later:** [Visual Studio website](https://visualstudio.microsoft.com/)

2. **.NET 6 SDK:** [.NET website](https://dotnet.microsoft.com/download/dotnet/6.0).

3. **SQL Server Management Studio (SSMS):** [SQL Server Management Studio download page](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).


### Installation
1. Clone the Poe repository from GitHub.
2. Open the Poe.sln solution in Visual Studio.
3. Update the connection string in **appsettings.json** with your **SQL Server instance name**.
4. Open the **Package Manager Console**.
5. Run the following commands to create the database and tables:
- **Update-Database -Context ApplicationDbContext**
- **Update-Database -Context AuthDbContext**




### Running the Application
- Press F5 to start the application.
- The application will run on port 5000 by default. You can open a web browser and navigate to http://localhost:5000 to access the application.

## Acknowledgements
- [Chart.js](https://www.chartjs.org/) - Library for creating interactive charts.
- [Bootstrap](https://getbootstrap.com/) - Front-end framework for responsive design.

## POE
[Programming 2B](https://github.com/Tobyrams/StudentModuleManager/blob/master/PROG6212POE.pdf)

### Languages and Tools:
![Static Badge](https://img.shields.io/badge/C%23-green?style=for-the-badge&logoColor=blue)
![Static Badge](https://img.shields.io/badge/HTML-orange?style=for-the-badge&logoColor=orange)
![Static Badge](https://img.shields.io/badge/CSS-purple?style=for-the-badge&logoColor=purple)
![Static Badge](https://img.shields.io/badge/JavaScript-yellow?style=for-the-badge&logoColor=yellow)














