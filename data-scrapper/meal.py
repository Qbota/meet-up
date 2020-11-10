class Meal:
    def __init__(self, name, category, cuisine, ingredients):
        self.name = name
        self.cuisine = cuisine
        self.ingredients = ingredients
        self.category = self.map_category(category)
    
    def map_category(self, category):
        non_vege_categories = [
            'Pork',
            'Beef',
            'Chicken',
            'Lamb',
            'Rabbit'
        ]
        return 'Meat meal' if category in non_vege_categories else category
       
    def to_dict(self):
        return {
            'name': self.name,
            'cuisine': self.cuisine,
            'category': self.category,
            'ingredients': self.ingredients,
        }
    
    def __str__(self):
        return ('Meal name: {name}\n'
                'cuisine: {cuisine}\n'
                'category: {category}\n'
                'meal ingredients: {ingredients}\n'.format_map(self.to_dict()))
