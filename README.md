# Cinemate - Your Cinema Companion

Cinemate is a **.NET MAUI** mobile application designed for movie enthusiasts. The app is **optimized strictly for Android**, specifically for **Pixel 5 - API 34**. It provides users with tools to discover, track, and receive recommendations for movies in an intuitive interface.

## Features
- **News Feed** – Fetches and displays the latest movie industry news via web scraping.
- **Movie Discovery** – Search for movies based on filters such as genre, year, and popularity.
- **Personalized Recommendations** – AI-based suggestions using an external API.
- **Movie Collection** – Add, rate, and categorize movies in a personal watchlist.
- **Offline Storage** – Uses SQLite for storing news and movie details locally.

## Installation & Setup
### Prerequisites
- **.NET 8 SDK**
- **Visual Studio 2022** with .NET MAUI workload
- **Android Emulator (Pixel 5 - API 34 recommended)**
- **Internet connection** (Required for API interactions)

### Running the App
1. Clone the repository:
   ```sh
   git clone https://github.com/annam015/Cinemate.git
   ```
2. Open the project in **Visual Studio 2022**.
3. Select **Android Emulator (Pixel 5 - API 34)**.
4. Build and run the application.

## Notes
- **Internet connection is required** for fetching news and movie recommendations.
- **News loading may take longer on first use** due to web scraping.
- **To add a movie**, ensure there is a `.png` image in the Downloads folder.
- **Three default movies** are preloaded to demonstrate the UI.

For a **video demo**, visit: [Cinemate Demo](https://drive.google.com/file/d/16MZHuW45lOugKM3IA-VUBSCej1Uxpg2b/view).
