# Yumify Online Commerce

Yumify Online Commerce is an advanced e-commerce platform designed to handle online purchases with an efficient and scalable architecture. It integrates various modern tools and technologies to deliver seamless user experiences, secure payments, and optimized performance.

# Features
▶️ RESTful API: Provides clean, standardized APIs for client applications. <br>
▶️ JWT Authentication: Secure authentication using JSON Web Tokens. <br>
▶️ Automapper: Simplifies object-to-object mapping. <br>
▶️ Dependency Injection: Ensures clean code and modularity. <br>
▶️ MSSQL Server & Entity Framework Core: Database management using SQL Server with EF Core for object-relational mapping (ORM).<br>
▶️ LINQ: Querying data efficiently using Language Integrated Query. <br>
▶️ Onion Architecture: Clean, maintainable architecture with clear separation of concerns.<br>
▶️ Stripe Payment Integration: Secure online payment processing via Stripe.<br>
▶️ Specification Design Pattern: Manages complex business rules and queries.<br>
▶️ Redis: In-memory caching and database using Redis for improved performance.<br>
▶️ Custom Middleware: Custom middleware for handling requests and responses.<br>

# Technologies
<h3 bold> ASP.NET Core </h3>
<h3 bold> Entity Framework Core </h3>
<h3 bold> MSSQL Server </h3>
<h3 bold> Redis </h3>
<h3 bold> Stripe</h3>
<h3 bold> JWT Authentication</h3>
<h3 bold> LINQ </h3>
<h3 bold> AutoMapper </h3>
<h3 bold> Dependency Injection </h3>


# Project Architecture
The project follows Onion Architecture to maintain a clear separation of concerns. Here are the primary layers:

Core: Contains the domain models, interfaces, and business logic.<br>
Infrastructure: Handles the data access and external services (e.g., Redis, Stripe, and MSSQL).<br>
API: The web API layer, which exposes endpoints and processes incoming requests.<br>
Custom Middleware: Custom middleware is used to handle tasks like logging, request validation, and error handling globally. <br>

Caching with Redis :
The project uses Redis both for in-memory database and caching to improve response time for frequently requested data.<br>

Specification Design Pattern
We use the Specification Design Pattern to handle complex business logic, allowing cleaner and more maintainable query code.<br>

# API Documentation
The API exposes various endpoints for managing products, orders, payments, and user accounts.<br>

You can explore the available endpoints through Swagger by running the project and navigating to /swagger.

# Contributing
Contributions are welcome! Please follow these steps:<br>

Fork the repository.<br>
1- Create a new branch (git checkout -b feature/your-feature-name).<br>
2- Commit your changes (git commit -m 'Add your feature').<br>
3- Push to the branch (git push origin feature/your-feature-name).<br>
4- Open a pull request.<br>
