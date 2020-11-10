class Movie():
    def __init__(self, title, year, duration, genres,
                director, actors, poster_url, imdb_rating):
        self.title = title
        self.year = int(year)
        self.duration = duration
        self.genres = [genre.rstrip().lstrip() for genre in genres.split(',')]
        self.director = director
        self.actors = [actor.rstrip().lstrip() for actor in actors.split(',')]
        self.poster_url = poster_url
        self.imdb_rating = self.handle_rating(imdb_rating)
    
    def handle_rating(self, rating):
        if 'N' in rating:
            return 0.0
        digit_one, digit_two = rating.split('.')
        return int(digit_one) + float(digit_two)/10

    def to_dict(self):
        return {
            'title': self.title,
            'rating': self.imdb_rating,
            'duration': self.duration,
            'genres': self.genres,
            'year': self.year,
            'actors': self.actors,
            'director': self.director,
            'poster_url': self.poster_url
        }
    
    def __str__(self):
        return ('Title: {title}\n'
                'rating: {rating}\n'
                'duration: {duration}\n'
                'year: {year}\n'
                'genres: {genres}\n'
                'director: {director}\n'
                'poster: {poster_url}\n'
                'actors: {actors}\n'.format_map(self.to_dict()))