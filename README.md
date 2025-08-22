
<div align="center">
   <img src="https://img.icons8.com/color/96/ai.png" alt="AI Icon" width="80"/>
   <h1>LLM-Advisor</h1>
   <p>
      <img src="https://img.shields.io/badge/.NET-9.0-blueviolet?logo=dotnet"/>
      <img src="https://img.shields.io/badge/PostgreSQL-Database-blue?logo=postgresql"/>
      <img src="https://img.shields.io/badge/Swagger-API-green?logo=swagger"/>
      <img src="https://img.shields.io/badge/JWT-Auth-orange?logo=jsonwebtokens"/>
      <img src="https://img.shields.io/badge/Serilog-Logging-teal?logo=serilog"/>
   </p>
   <p><b>Modular .NET 9.0 web app for academic program recommendations and student guidance</b></p>
</div>

---

## 🚀 Features

<ul>
   <li>🔒 <b>User Authentication</b>: Secure registration and login using JWT</li>
   <li>🎓 <b>Program Recommendation</b>: Tailored academic program suggestions</li>
   <li>💬 <b>Conversation Service</b>: Interactive guidance and Q&A for students</li>
   <li>📝 <b>Feedback System</b>: Collect and manage user feedback</li>
   <li>📖 <b>Swagger API Documentation</b>: Explore and test API endpoints</li>
   <li>📊 <b>Serilog Logging</b>: Structured logging for diagnostics and monitoring</li>
</ul>

## 🗂️ Project Structure

<ul>
   <li><img src="https://img.icons8.com/ios-filled/20/domain.png"/> <code>Domain/</code> - Core domain models and enums</li>
   <li><img src="https://img.icons8.com/ios-filled/20/application-window.png"/> <code>Application/</code> - Business logic and service layer</li>
   <li><img src="https://img.icons8.com/ios-filled/20/database.png"/> <code>Infrastructure/</code> - Data access, repository pattern, and database context</li>
   <li><img src="https://img.icons8.com/ios-filled/20/web.png"/> <code>Presentation/</code> - ASP.NET Core Web API, controllers, configuration, and startup</li>
</ul>

## 🛠️ Technologies Used

<p>
   <img src="https://img.icons8.com/color/24/dotnet.png"/> .NET 9.0 &nbsp; | &nbsp;
   <img src="https://img.icons8.com/color/24/asp.png"/> ASP.NET Core &nbsp; | &nbsp;
   <img src="https://img.icons8.com/color/24/postgreesql.png"/> PostgreSQL &nbsp; | &nbsp;
   <img src="https://img.icons8.com/color/24/json-web-token.png"/> JWT &nbsp; | &nbsp;
   <img src="https://img.icons8.com/color/24/serilog.png"/> Serilog &nbsp; | &nbsp;
   <img src="https://img.icons8.com/color/24/swagger.png"/> Swagger
</p>

## ⚡ Getting Started

### Prerequisites

- <img src="https://img.icons8.com/color/24/dotnet.png"/> [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- <img src="https://img.icons8.com/color/24/postgreesql.png"/> [PostgreSQL](https://www.postgresql.org/download/)

### Setup

1. <img src="https://img.icons8.com/ios-glyphs/20/git.png"/> <b>Clone the repository</b>
    ```sh
    git clone https://github.com/Timmi-tech/LLM-Advisor.git
    ```
2. <img src="https://img.icons8.com/ios-filled/20/settings.png"/> <b>Configure the database</b>
    - Update the PostgreSQL connection string in <code>Presentation/appsettings.json</code>
3. <img src="https://img.icons8.com/ios-filled/20/database.png"/> <b>Apply migrations</b>
    ```sh
    dotnet ef database update --project Infrastructure
    ```
4. <img src="https://img.icons8.com/ios-filled/20/play.png"/> <b>Run the application</b>
    ```sh
    dotnet run --project Presentation
    ```
5. <img src="https://img.icons8.com/color/24/swagger.png"/> <b>Access Swagger UI</b>
    - Visit [http://localhost:5043/swagger](http://localhost:5043/swagger) in your browser

## 💡 Usage

- Use the API endpoints for authentication, recommendations, conversations, and feedback
- Explore endpoints and models via Swagger UI

## 🤝 Contributing

Pull requests are welcome! For major changes, please open an issue first to discuss what you would like to change.

