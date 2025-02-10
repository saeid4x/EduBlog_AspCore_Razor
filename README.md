 

# Educational Blog Platform

Welcome to the Educational Blog Platform, a comprehensive web application built using ASP.NET Core, Razor Pages, Entity Framework Core, and SQL Server. This platform is designed to provide educational content across three main categories: Podcasts, Courses, and Blogs.

## Features

- **Podcasts**: Listen to educational podcasts on various topics.
- **Courses**: Enroll in and manage online courses to enhance your skills.
- **Blogs**: Read insightful articles and stay updated with the latest trends in education.
- **User Management**: Secure user authentication and authorization.
- **Admin Dashboard**: Manage content, users, and settings with ease.
- **Responsive Design**: Access the platform from any device with a responsive and user-friendly interface.

## Technologies Used

- **ASP.NET Core**: For building robust and scalable web applications.
- **Razor Pages**: For server-side rendering of web pages with a clean separation of concerns.
- **Entity Framework Core**: For data access and interactions with the SQL Server database.
- **SQL Server**: As the relational database management system.
- **Bootstrap**: For responsive and modern UI design.

## Getting Started

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/educational-blog-platform.git
   cd educational-blog-platform
   ```

2. **Set Up the Database**:
   - Update the connection string in `appsettings.json` to point to your SQL Server instance.
   - Run the migrations to set up the database schema:
     ```bash
     dotnet ef database update
     ```

3. **Run the Application**:
   ```bash
   dotnet run
   ```

4. **Access the Platform**:
   - Open your browser and navigate to `https://localhost:5001` to explore the platform.
 