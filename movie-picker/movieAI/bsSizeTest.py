import movieAI
from timeit import default_timer as timer

def measureTime(dataset):
    ai = movieAI.MovieAI(datasetFolder=dataset, debug=False)
    exampleConfig = {"numOfRecomendations": 100,
                     "maxNumOfNeighbours": 15,
                     "minNumOfNeighbours": 10}
    exampleRating1 = movieAI.csvToMovieRatings("example_user_1.csv")
    exampleRating2 = movieAI.csvToMovieRatings("example_user_2.csv")
    finalRating = ai.combineRatings([exampleRating1, exampleRating2])

    start = timer()
    for i in range(0, 100):
        result, prediction = \
            ai.predictRatingForUnseenMovies(finalRating, exampleConfig)
    end = timer()
    return end-start


if __name__ == "__main__":
    print("Największa baza: {}".format(measureTime('ml-latest/')))
    print("Duża baza: {}".format(measureTime("ml-25m/")))
    print("Mała baza: {}".format(measureTime('ml-latest-small/')))
