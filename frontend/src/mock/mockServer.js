import {Response, Server} from 'miragejs'
import user from './data/user'
import movies from './data/movies'

export function makeServer({ environment = 'development' } = {}) {

    return new Server({
        environment,

        routes() {

            this.namespace = 'api/meet-up'
            this.routes = 1000

            this.post('/user/authenticate', (schema, request) => {
                let loginCommand = JSON.parse(request.requestBody)
                if(loginCommand.password === 'test')
                    return new Response(200, {}, user)
                else{
                    return new Response(401)
                }
            })

            this.post('/user', () => {
                return new Response(201, {}, user)
            })

            this.get('/movies', () => {
                console.log(movies)
                return new Response(200, {}, movies)
            })

        }
    })
}
