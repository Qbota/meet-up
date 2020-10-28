import {Response, Server} from 'miragejs'
import user from './data/user'

export function makeServer({ environment = 'development' } = {}) {

    return new Server({
        environment,

        routes() {

            this.namespace = 'api/meet-up'

            this.post('/user', () => {
                return new Response(201, {}, user)
            })

        }
    })
}
