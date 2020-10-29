import lenskit.datasets as ds
import pandas as pd
from datetime import datetime
import dload
import os

DATABASE = 'ml-latest-small/'
DATABASE_LINK = "http://files.grouplens.org/datasets/movielens/ml-latest-small.zip"


def setCwdToWhereScriptIs():
    os.chdir(os.path.dirname(os.path.abspath(__file__)))


class MovieAI():

    def __init__(self,databaseFolder = DATABASE, debug = False):
        self._debug = debug
        setCwdToWhereScriptIs()
        self.databaseFolder = databaseFolder
        self.databaseLodaded = self._loadDatabase()
        if self.databaseLodaded:
            self.log("Movie database loaded succesfully")
        else:
            self.log("Could not load database")

    
    def log(self,text):
        if self._debug:
            print(datetime.now().strftime("%m/%d/%Y, %H:%M:%S") + "\t|\t" + text)

    def _readDatabaseFile(self):
        file = os.path.dirname(os.path.abspath(__file__)) + "\\" + self.databaseFolder
        self.movieDatabase = ds.MovieLens(file)

    def _loadDatabase(self):
        self._readDatabaseFile()
        if not self._checkDatabaseExistance():
            if not self._downloadAndCheckDatabase():
                return False
        return True

    def _checkDatabaseExistance(self):
        try:
            self.movieDatabase.ratings
            self.movieDatabase.movies
            self.movieDatabase.links
            self.movieDatabase.tags
            return True
        except FileNotFoundError:
            return False

    def _downloadAndCheckDatabase(self):
        try:
            dload.save_unzip(DATABASE_LINK,os.getcwd())
            self._readDatabaseFile()
            if self._checkDatabaseExistance():
                return True
            else:
                return False
        except:
            return False


if __name__ == "__main__":
    ai = MovieAI(debug=True)