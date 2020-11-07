# This file combines The Open Movie Database with Movie Lens Database
import lenskit.datasets as ds
import os
import tmdbsimple as tmdb
from datetime import datetime
import apiKey

TMDB_API_KEY = apiKey.TMDB_API_KEY


def log(text):
    print(datetime.now().strftime("%m/%d/%Y, %H:%M:%S") + " | " + text)


def generateLinks():
    db = ds.MovieLens('ml-latest-small/')
    links = db.links
    links.drop("imdbId", 1)
    return links


def combineDataset(mDb):
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
    end = datetime.now()

    log("Script started at {}".format(start.strftime("%m/%d/%Y, %H:%M:%S")))
    log("Script ended at {}".format(end.strftime("%m/%d/%Y, %H:%M:%S")))
    log("It took {} minutes".format(round((end-start).total_seconds() / 60.0, 2)))
    return mDb


if __name__ == "__main__":
    os.chdir(os.path.dirname(os.path.abspath(__file__)))
    result = combineDataset(generateLinks())
    result.to_json("movieDataset.json")
