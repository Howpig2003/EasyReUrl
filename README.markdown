# EasyReUrl - A Simple URL Shortener

This demo is a practice project created by the author to learn and explore the ASP.NET MVC architecture.

## Features
- Generates 6-character short URLs (e.g., `https://yourdomain.com/xyzabc`) using a 58-character set (A-Z, a-z, 2-9, excluding 0, 1, l, O, I for readability).
- Redirects short URLs to original URLs.
- Reuses short URLs older than 7 days to handle collisions.
- Stores URL data in a SQLite database.
- Provides a user-friendly web interface with error handling.

## Tech Stack
- ASP.NET Core (.NET 8.0)
- SQLite with Entity Framework Core (v9.0.7)
- Razor view (`Index.cshtml`) with custom CSS (`index.css`)
- MVC architecture

## AI Usage
This project was developed with assistance from Grok, created by xAI, which contributed to code generation (e.g., collision handling logic), debugging (e.g., resolving `SqliteException`), and documentation.

## License
Licensed under the [MIT License](LICENSE).