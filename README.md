# Habit Tracker API 🚀

A robust ASP.NET Core Web API for tracking daily habits and monitoring personal progress. This project implements a clean architecture using the **Service Pattern**, **DTO Mapping**, and **Entity Framework Core**.

## ✨ Features

- **Habit Management**: Full CRUD operations (Create, Read, Update, Delete) for habits.
- **Archiving System**: Support for archiving habits to keep your active list clean.
- **Smart Check-ins**: Log daily progress with a built-in constraint preventing multiple check-ins for the same habit on the same day.
- **Automatic Data Seeding**: The database is automatically migrated and populated with sample data on startup for immediate testing.
- **RESTful Design**: Proper use of HTTP verbs (GET, POST, PUT, PATCH, DELETE) and status codes (201 Created, 404 NotFound, 409 Conflict).
- **Interactive Documentation**: Integrated Swagger UI with detailed XML comments for every endpoint.

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 9.0 (Web API)
- **Database**: SQLite
- **ORM**: Entity Framework Core
- **Documentation**: Swagger (OpenAPI) & XML Documentation
- **Tools**: `.http` file support for rapid testing

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (Version 8.0 or 9.0)

### Installation & Execution

1. **Clone the repository**:

   ```bash
   git clone [https://github.com/akinmertbur/HabitTrackerApi.git](https://github.com/akinmertbur/HabitTrackerApi.git)
   cd HabitTrackerApi
   ```

2. **Restore Dependencies**:

   ```bash
   dotnet restore
   ```

3. **Run the Application**:

   ```bash
   dotnet run --project HabitTrackerApi
   ```

4. **Access the API**: Open your browser to:
   - Swagger UI: http://localhost:5295/swagger

   - API Root: http://localhost:5295/api (Note: Check the terminal output for the exact port if 5295 is occupied)

### Project Structure

- Entities/: Domain models and database relationship configurations.

- DTOs/: Data Transfer Objects to decouple internal logic from the API layer.

- Services/: Business logic layer (Habit and Check-in services).

- Data/: HabitTrackerDbContext, Fluent API configurations, and DbInitializer.

- Controllers/: API endpoints with documentation attributes.

### API Endpoints Summary

#### Habits

Method,Endpoint,Description

- GET,/api/habits,List all habits (filter by isArchived)
- GET,/api/habits/{id},Get specific habit with its history
- POST,/api/habits,Create a new habit
- PUT,/api/habits/{id},Update habit details
- PATCH,/api/habits/{id}/archive,Archive a specific habit
- DELETE,/api/habits/{id},Remove a habit and all its history

#### Check-ins

Method,Endpoint,Description

- POST,/api/checkins,Log a completion (1 per day limit)
- GET,/api/checkins/habit/{id},Get all progress records for a habit
- DELETE,/api/checkins/{id},Remove a specific check-in record

### Testing

The project includes a HabitTrackerApi.http file. You can use this directly in Visual Studio or VS Code (with REST Client extension) to test all endpoints without needing Postman.
