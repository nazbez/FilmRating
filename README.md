# Film Rating Application
The Film Rating is a web application that allows users to view films descriptions like genre, year, duration, actors, etc, and rate this films. The system requires user authentication for rating films, and includes an admin interface for managing actors, directors, and films.

## Features
- User Authentication: Only authorized users can rate films. User registration and login functionality are implemented to ensure secure access. Users can sign up or log in using email/password or Google authentication.
- Film Rating: Users can rate films on a scale from 0 to 10.
- Film Descriptions: Users can view detailed information about films, including rating, genre, year, director, duration, budget, and actors.
- Admin Interface: Administrators have access to an exclusive admin interface so that they can add, edit, and remove films, actors, and directors.

## Technologies Used

#### Backend:
- C#: Backend development language.
- ASP.NET Core Web API 7: Web framework for building RESTful APIs.
- Entity Framework Core 7: Object-Relational Mapping (ORM) tool for interacting with the database.

#### Frontend:
- Node.js 18.14.0: JavaScript runtime for building server-side applications.
- Angular 15: Frontend framework for building dynamic and interactive user interfaces.
- TypeScript: Programming language for building scalable and maintainable web applications.

#### Database and Hosting:
- Azure Blob Storage: Storage service for storing film covers.
- Azure App Service: Platform-as-a-Service (PaaS) for hosting the web application.
- Azure SQL: Relational database service for hosting the database.

## Installation and Setup

#### 1. Clone the repository:
```
git clone https://github.com/nazbez/FilmRating.git
```

#### 2. Backend Setup:
1. Install the latest version of [.NET Core SDK](https://dotnet.microsoft.com/download)
2. Open the solution file `FilmRating.sln` in your IDE
3. Restore the NuGet packages by right-clicking on the solution file and selecting "Restore NuGet Packages" or using a command specific to your IDE.
4. Update the database connection string in the `appsettings.json` file with your database configuration.
5. Apply any pending database migrations. You can do this through the command line by navigating to the root directory of the project and running the command:  
   ```
   dotnet ef database update
   ``` 
   Alternatively, you can use IDE-specific tools or commands to apply the migrations, e.g. [Package Manager Console in Visual Studio](https://learn.microsoft.com/en-us/ef/core/cli/powershell#add-migration) 
6. Build the backend project to ensure all dependencies are resolved and the project compiles successfully.

#### 3. Frontend Setup:
1. Open a command prompt and navigate to the `FilmRating\ClientApp` folder.
2. Install the dependencies:
   ```
   npm install
   ```
3. Start the Angular development server:
   ```
   ng serve
   ```

#### 4. Edit the hosts file (Windows):
1. Open the file `C:\Windows\System32\drivers\etc\hosts` for editing. You may need administrator privileges to modify this file.
2. Add the following line to the file:
   ```
   127.0.0.1 film-rating.local.com
   ```

#### 5. Access the application:

1. Open your web browser and go to `https://film-rating.local.com:7057` to access the Film Rating Application.

## Usage
1. Register a new user account or log in with an existing account. You can sign up or log in using email/password or Google authentication.
2. Browse the list of films to view their descriptions. (Note: Unauthorized users can also view film descriptions)
3. Rate films by selecting a rating from 0 to 10.
4. Administrators can log in with their admin credentials to access the admin interface.
5. In the admin interface, administrators can add, edit, and remove films, actors, and directors.

## Contribution 
If you would like to contribute to the Film Rating project, we welcome your contributions! Follow these steps:
1. Fork the repository on GitHub.
2. Create a new branch with a descriptive name for your feature or bug fix.
3. Make your changes and commit them to your branch.
4. Push your branch to your forked repository.
5. Submit a pull request to the main repository, explaining your changes in detail.

Please follow the project's coding style and guidelines when contributing.

### Team 
1. Nazar Bezuhlyi: [GitHub profile](https://github.com/nazbez) 
2. Yulia Biriukova: [GitHub profile](https://github.com/yuliaBiriukova) 
3. dmytryG: [GitHub profile](https://github.com/dmytryG) 

Feel free to reach out to any of the team members if you have any questions or need assistance with the project.

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT)