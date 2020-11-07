import json
import requests
import pymongo

from movie import Movie

urls = json.loads(open('movies_urls/fantasy.json','r').read())
myclient = pymongo.MongoClient('mongodb://localhost:27017/')
my_db = myclient['test']
my_col = my_db['Films']

for url in urls:
    movie_id = url['id'][7:-1]
    print(movie_id)
    res = requests.get('http://www.omdbapi.com/?i={}&apikey=f6abe5ea'.format(movie_id), \
                        headers={'X-RapidAPI-Key': 'PROVIDE API KEY'})
    body = res.json()

    movie = Movie(
        title = body['Title'],
        year = body['Year'],
        duration = body['Runtime'],
        genres = body['Genre'],
        director = body['Director'],
        actors = body['Actors'],
        poster_url = body['Poster'] if 'Poster' in body else None,
        imdb_rating = body['imdbRating']
    )
    print(movie)
    movies = my_col.find({'title': movie.title})

    if movies.count() == 0:
        my_col.insert_one(movie.to_dict())
    elif movies.count() >= 2:
        my_col.remove({'title': movie.title})

    
