# GameVault API

A RESTful backend API built with ASP.NET Core 8 for managing player accounts, authentication, match history, and leaderboards.

## Overview

GameVault API is a backend project designed to demonstrate modern .NET backend development practices, including authentication, database management, API design, and layered architecture.

The project allows players to register accounts, authenticate using JWT tokens, view profiles, record match results, track match history, and compete on a leaderboard.

## Tech Stack

* ASP.NET Core 8 Web API
* C#
* Entity Framework Core
* SQL Server
* JWT Authentication
* BCrypt Password Hashing
* Swagger / OpenAPI
* Git & GitHub

## Architecture

The project follows a layered architecture:

Controller → Service → Repository → EF Core → SQL Server

### Patterns Used

* Repository Pattern
* Dependency Injection
* DTO Pattern
* Service Layer Pattern
* Middleware-based Error Handling

## Features

### Authentication

* User Registration
* User Login
* JWT Token Generation
* BCrypt Password Hashing
* Protected Endpoints

### Player Management

* View Own Profile
* View Public Player Profiles
* Update Profile Information

### Match System

* Record Match Results
* Track Player Statistics
* Match History Retrieval

### Leaderboard

* Rank Players by Total Score
* Dynamic Leaderboard Updates

### Validation & Error Handling

* Request Validation
* Consistent API Error Responses
* Global Exception Handling Middleware

## Database

### Players

* Id
* Username
* Email
* PasswordHash
* Level
* TotalScore
* GamesPlayed
* GamesWon
* CreatedAt
* UpdatedAt

### Matches

* Id
* GameMode
* PlayedAt
* DurationSeconds

### MatchParticipants

* MatchId
* PlayerId
* Score
* Result

## API Endpoints

### Authentication

POST /api/auth/register

POST /api/auth/login

### Players

GET /api/players/me

PATCH /api/players/me

GET /api/players/{id}

### Matches

POST /api/matches

GET /api/matches/history/{playerId}

### Leaderboard

GET /api/leaderboard

### Health Check

GET /api/game/health

## Setup

1. Clone repository

2. Configure appsettings.Development.json

3. Update database

```bash
dotnet ef database update
```

4. Run application

```bash
dotnet run
```

5. Open Swagger

```text
https://localhost:<port>/swagger
```

## What I Learned

* ASP.NET Core Web API Development
* Entity Framework Core
* SQL Server Integration
* JWT Authentication
* Repository Pattern
* Dependency Injection
* DTO Design
* REST API Best Practices
* Backend Architecture Fundamentals

## Challenges and Solutions

### 1. Secure Password Storage

**Challenge:** Storing user passwords safely without exposing sensitive information in the database.

**Solution:** Implemented BCrypt password hashing before persistence. Passwords are never stored in plain text, and login verification uses BCrypt hash comparison.

### 2. Stateless Authentication

**Challenge:** Allowing authenticated users to access protected endpoints without repeatedly sending credentials.

**Solution:** Implemented JWT authentication with claims-based identity. User ID and username are embedded inside the token and validated automatically by ASP.NET Core middleware.

### 3. Entity Relationships

**Challenge:** Representing matches involving multiple players while maintaining clean database relationships.

**Solution:** Created a MatchParticipant junction entity to model the many-to-many relationship between Players and Matches using Entity Framework Core.

### 4. Clean API Architecture

**Challenge:** Preventing controllers from becoming tightly coupled to database operations.

**Solution:** Applied Repository Pattern and Service Layer architecture, separating business logic from data access and improving maintainability.

### 5. Input Validation

**Challenge:** Preventing invalid or malformed requests from reaching business logic.

**Solution:** Added DTO validation using Data Annotations and configured consistent API validation responses through ASP.NET Core's validation pipeline.
