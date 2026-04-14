<div align="center">
 <h1>DoDay</h1>
</div>
<div align="center">DoDay is a high-performance, full-stack task management ecosystem designed to eliminate cognitive load by streamlining daily workflows through dynamic categorization and real-time synchronization.</div>

<div align="center">
  <h2>Table of Contents</h2>
</div>
<p>&nbsp;&nbsp;<a href="#features">Features</a> </p>
<p>&nbsp;&nbsp;<a href="#tech-stack">Tech Stack</a></p>
<p>&nbsp;&nbsp;<a href="#getting-started">Getting Started</a></p>
<p>&nbsp;&nbsp;<a href="#architecture">Architecture</a></p>


<div align="center">
  <h2>Tech Stack</h2>
</div>

<div align="center">
  <h3>Backend</h3>
</div>
<ul>
  <li><b>C# (.NET 9.0)</b> – Robust, type-safe language for enterprise-grade logic.</li>
  <li><b>ASP.NET Core Web API</b> – High-performance framework for building RESTful services.</li>
  <li><b>Entity Framework Core</b> – Modern ORM for seamless database interaction and migrations.</li>
  <li><b>Fluent Validation</b> – Strongly-typed validation rules to ensure data integrity.</li>
  <li><b>Swagger (OpenAPI)</b> – Interactive documentation and testing interface for API endpoints.</li>
  <li><b>Serilog & Seq</b> – Structured logging and centralized monitoring for real-time debugging.</li>
</ul>

<div align="center">
  <h3>Frontend</h3>
</div>
<ul>
  <li><b>React</b> – Component-based library for building a dynamic and responsive UI.</li>
  <li><b>JavaScript (ES6+)</b> – Core logic for client-side interactivity and state management.</li>
  <li><b>Axios</b> – Promise-based HTTP client for connecting the frontend to the API.</li>
</ul>

<div align="center">
  <h3>Database & Infrastructure</h3>
</div>
<ul>
  <li><b>Microsoft SQL Server (MSSQL)</b> – Relational database for structured, reliable data storage.</li>
  <li><b>Docker</b> – Containerization to ensure the environment remains consistent across development and production.</li>
</ul>

<div align="center">
  <h3>Quality Assurance</h3>
</div>
<ul>
  <li><b>xUnit</b> – Industry-standard unit testing framework to ensure code reliability.</li>
  <li><b>Moq</b> – Used for mocking dependencies during testing.</li>
</ul>

<div align="center">
<h2>Features</h2>
</div>

1. Identity & Access Management (IAM)

<ul>
<li><b>Stateless Authentication:</b> Implemented secure user sessions utilizing <b>JSON Web Tokens (JWT)</b>, ensuring scalable and decoupled authorization between the React client and ASP.NET backend.</li>
<li><b>Secure Persistence:</b> Engineered a robust registration and login pipeline with password hashing and strict data validation to safeguard user credentials.</li>
</ul>

2. Dynamic Data Orchestration
<ul>
<li><b>Hierarchical Task Management:</b> Users can architect custom workflows by categorizing tasks, reducing cognitive load through logical data segmentation.</li>
<li><b>Asynchronous CRUD Operations:</b> Optimized the user interface for high responsiveness by leveraging asynchronous API calls, ensuring a lag-free experience during task manipulation.</li>
</ul>

3. Enterprise-Grade Observability
<ul>
<li><b>Structured Logging & Diagnostics:</b> Integrated <b>Serilog</b> with a <b>Seq</b> sink to provide real-time telemetry, enabling rapid root-cause analysis and system health monitoring.</li>
<li><b>Automated API Documentation:</b> Utilized <b>Swagger/OpenAPI</b> to maintain a live, interactive contract between the backend and frontend, facilitating seamless integration and manual endpoint testing.</li>
</ul>

4. DevOps & Infrastructure
<ul>
<li><b>Containerized Deployment:</b> Orchestrated the entire ecosystem—including the MSSQL database and .NET API—using <b>Docker</b> to eliminate environment-specific discrepancies and simplify onboarding.</li>
<li><b>Database Integrity:</b> Managed complex relational data structures via <b>Entity Framework Core</b> Migrations, ensuring consistent schema evolution across development environments.</li>
</ul>

<div align="center">
<h2>Architecture</h2>
</div>
<div align="center"><img width="966" height="526" alt="DoDayArchitecture" src="https://github.com/user-attachments/assets/821a4d1d-4a02-483e-b60c-73a2828da8f8" /></div>



