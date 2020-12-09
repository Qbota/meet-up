cd frontend
nohup yarn serve &
cd ../backend/WebApplication
nohup dotnet run &
cd ../../meal-picker
nohup ./run.bat &
cd ../movie-picker/movieAI
nohup ./run.bat &