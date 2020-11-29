import os
import dload
import pandas as pd
import lenskit.datasets as ds
import csv
from lenskit.algorithms import Recommender
from lenskit.algorithms.user_knn import UserUser
from datetime import datetime


DATASET = 'ml-latest-small/'
DATASET_LINK = "http://files.grouplens.org/datasets/movielens/ml-latest-small.zip"


def setCwdToWhereScriptIs():
    os.chdir(os.path.dirname(os.path.abspath(__file__)))


class MovieAI():

    NON_EXISTING_USER = -1

    def __init__(self, datasetFolder=DATASET, debug=False):
        self._debug = debug
        setCwdToWhereScriptIs()
        self.datasetFolder = datasetFolder
        self.datasetLodaded = self._loadDataset()
        if self.datasetLodaded:
            self.log("Movie dataset loaded succesfully")
        else:
            self.log("Could not load dataset")

    def log(self, text):
        if self._debug:
            print(datetime.now().strftime("%m/%d/%Y, %H:%M:%S") + " | " + text)

    def _readDatasetFile(self):
        self.log("Opening dataset...")
        file = os.path.dirname(os.path.abspath(__file__)) + \
            "\\" + self.datasetFolder
        self.movieDataset = ds.MovieLens(file)

    def _loadDataset(self):
        self._readDatasetFile()
        if not self._checkDatasetExistance():
            if not self._downloadAndCheckDataset():
                return False
        return True

    def _checkDatasetExistance(self):
        try:
            self.movieDataset.ratings
            self.movieDataset.movies
            self.movieDataset.links
            self.movieDataset.tags
            return True
        except FileNotFoundError:
            self.log("Dataset unavailable!")
            return False

    def _downloadAndCheckDataset(self):
        self.log("Downloading dataset...")
        dload.save_unzip(DATASET_LINK, os.getcwd())
        self._readDatasetFile()
        self.log("Checking downloaded dataset...")
        if self._checkDatasetExistance():
            return True
        else:
            return False

    def _validatePredictConfig(self, predictConfigDict):
        try:
            numOfRecom = int(predictConfigDict["numOfRecomendations"])
            maxNumOfNeigh = int(predictConfigDict["maxNumOfNeighbours"])
            minNumOfNeigh = int(predictConfigDict["minNumOfNeighbours"])
            return (numOfRecom, maxNumOfNeigh, minNumOfNeigh)
        except (KeyError, ValueError):
            self.log("Incorrect prediction config data! \
                      Algorithm cannot proceed.")
            return (None, None, None)

    def predictRatingForUnseenMovies(self, userMovieRatings,
                                     predictConfigDict):

        numOfRecom, maxNumOfNeigh, minNumOfNeigh = \
            self._validatePredictConfig(predictConfigDict)

        if None in (numOfRecom, maxNumOfNeigh, minNumOfNeigh):
            return False, None

        userUser = UserUser(maxNumOfNeigh, min_nbrs=minNumOfNeigh)
        algo = Recommender.adapt(userUser)
        algo.fit(self.movieDataset.ratings)

        userRecom = algo.recommend(self.NON_EXISTING_USER,
                                   numOfRecom,
                                   ratings=pd.Series(userMovieRatings))

        return True, userRecom

    def combineRatings(self, ratings):
        '''
        Params:
        * ratings - list of dicts with:
            keys (int) - movieId
            values (float) - movie ratings
        '''
        combinedRatings = {}
        for userDict in ratings:
            for film in userDict:
                r = []
                for _userDict in ratings:
                    for _film, _value in _userDict.items():
                        if film == _film:
                            r.append(_value)
                            break
                if film not in combinedRatings:
                    if len(r) > 0:
                        combinedRatings[film] = sum(r)/len(r)
        return combinedRatings

    def predictCombinedRating(self, ratings, predictConfigDict=None):
        if predictConfigDict is None:
            predictConfigDict = \
                    {"numOfRecomendations": 100,
                     "maxNumOfNeighbours": 15,
                     "minNumOfNeighbours": 10}
        combinedRating = self.combineRatings(ratings)
        predictedRating = self.predictRatingForUnseenMovies(combinedRating,
                                                            predictConfigDict)
        return predictedRating

    def filterPredictedRating(self, predictedRating, genresDict):
        return None

    def generateMovieRecommendation(self, ratingList, genresDict):
        predictedRating = predictCombinedRating(ratingList)




def csvToMovieRatings(filename):
    '''
    Params:
    * filename (str) - filename of csv file with ratings
    Returns:
    * ratingDict (dict) - dict of pairs:
        key (int) - movieId (item)
        value (float) - movie rating
    '''
    ratingDict = {}
    with open(filename, newline='') as csvfile:
        ratings_reader = csv.DictReader(csvfile)
        for row in ratings_reader:
            if ((row['ratings'] != "") and
                (float(row['ratings']) > 0) and
                    (float(row['ratings']) < 6)):
                ratingDict.update({int(row['item']):
                                   float(row['ratings'])})
    return ratingDict


if __name__ == "__main__":
    ai = MovieAI(debug=True)
    exampleConfig = {"numOfRecomendations": 100,
                     "maxNumOfNeighbours": 15,
                     "minNumOfNeighbours": 10}
    exampleRating1 = csvToMovieRatings("example_user_1.csv")
    exampleRating2 = csvToMovieRatings("example_user_2.csv")
    finalRating = ai.combineRatings([exampleRating1, exampleRating2])

    result, prediction = \
        ai.predictRatingForUnseenMovies(finalRating, exampleConfig)
    print(prediction)
