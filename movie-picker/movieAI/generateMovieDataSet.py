# This file combines The Open Movie Database with Movie Lens Database
import lenskit.datasets as ds
import os
import tmdbsimple as tmdb
from datetime import datetime
import apiKey
import json
from pymongo import MongoClient

TMDB_API_KEY = apiKey.TMDB_API_KEY
FILE_NAME = "movieDataset.json"

def log(text):
    print(datetime.now().strftime("%m/%d/%Y, %H:%M:%S") + " | " + text)


def generateLinks():
    db = ds.MovieLens('ml-latest-small/')
    links = db.links
    links.drop("imdbId", 1)
    return links


def combineDataset(mDb, devTest=False):
    # https://developers.themoviedb.org/3/movies/get-movie-details
    tmdb.API_KEY = TMDB_API_KEY
    mDb["genres"] = ""
    mDb["title"] = ""
    mDb["posterPath"] = ""
    mDb["releaseDate"] = ""
    mDb["voteAverage"] = ""
    mDb["voteCount"] = ""

    start = datetime.now()
    length = len(mDb.index)
    for index, row in mDb.iterrows():
        try:
            movie = tmdb.Movies(row["tmdbId"])
            _ = movie.info()
            mDb.at[index, "genres"] = str([i['name'] for i in movie.genres])
            mDb.at[index, "title"] = str(movie.title)
            # https://image.tmdb.org/t/p/$SIZE/ $SIZE in [original,w500,...]
            mDb.at[index, "posterPath"] = str(movie.poster_path)
            mDb.at[index, "releaseDate"] = str(movie.release_date)
            mDb.at[index, "voteAverage"] = str(movie.vote_average)
            mDb.at[index, "voteCount"] = str(movie.vote_count)
            log("{}/{} Processing movie {}".format(index,
                                                   length,
                                                   str(movie.title)))
        except Exception as e:
            log("Exception {}! Skipping this movie.".format(e))
        if index >= 100 and devTest:
            break
    end = datetime.now()

    log("Script started at {}".format(start.strftime("%m/%d/%Y, %H:%M:%S")))
    log("Script ended at {}".format(end.strftime("%m/%d/%Y, %H:%M:%S")))
    log("It took {} minutes".format(round((end-start).total_seconds() / 60.0, 2)))
    return mDb, round((end-start).total_seconds())


def loadToMongo(file):
    myClient = MongoClient('mongodb://localhost:27017/')
    myDb = myClient["movieDatabase"]
    myCol = myDb["movieCollection"]
    movieDict = {}
    with open(file) as f:
        data = json.load(f)
        for key, value in data.items():
            for _key, _value in value.items():
                if _key not in movieDict.keys():
                    movieDict[_key] = {}
                movieDict[_key][key] = _value
    for key, value in movieDict.items():
        elementToMongo = {}
        elementToMongo['id'] = key
        for _key, _value in value.items():
            elementToMongo[_key] = _value
        myCol.insert_one(elementToMongo)


if __name__ == "__main__":
    os.chdir(os.path.dirname(os.path.abspath(__file__)))
    if not os.path.isfile(FILE_NAME):
        result, _ = combineDataset(generateLinks())
        result.to_json(FILE_NAME)
    loadToMongo(FILE_NAME)
