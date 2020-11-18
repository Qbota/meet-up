import {Response, Server} from 'miragejs'
import user from './data/user'
import movies from './data/movies'
import prefs from './data/userMoviePrefs'
import invites from "@/mock/data/invites";
import names from "@/mock/data/names";
import meetings from "@/mock/data/meetings";
import movieGenres from "@/mock/data/movieGenres";
import foodTypes from "@/mock/data/foodTypes";


export function makeServer({environment = 'development'} = {}) {

    return new Server({
        environment,

        routes() {

            this.namespace = 'api/meet-up'
            this.routes = 1000
            this.groups = [
                {
                    name: 'Group 1',
                    description: 'Description',
                    icon: 'fas fa-bicycle',
                    members: ['kuba@test.pl']
                }
            ]
            this.meetingsList = [...meetings]

            this.post('/user/authenticate', (schema, request) => {
                let loginCommand = JSON.parse(request.requestBody)
                if (loginCommand.password === 'test')
                    return new Response(200, {}, user)
                else {
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

            this.get('/userMoviePrefs', () =>{
                return new Response(200, {}, prefs)
            })

            this.get('/movie/genre', () => {
                return new Response(200, {}, movieGenres)
            })

            this.get('/food/type', () => {
                return new Response(200, {}, foodTypes)
            })

            this.post('/group', (schema, request) => {
                let createdGroup = JSON.parse(request.requestBody)
                this.groups.push(createdGroup)
                return new Response(201, {}, {createdGroup})
            })

            this.get('/group', () => {
                return new Response(200, {}, this.groups)
            })

            this.get('/invites', () => {
                return new Response(200, {}, invites)
            })

            this.get('/names', () => {
                return new Response(200, {}, names)
            })

            this.post('/invites', (schema, request) => {
                let invite = JSON.parse(request.requestBody)
                console.log('Received invite response')
                console.log(invite)
                return new Response(201, {} , {})
            })

            this.get('/meeting', () => {
                console.log(this.meetingsList)
                return new Response(200, {}, this.meetingsList)
            })

            this.post('/meeting', (schema, request) => {
                let meeting = JSON.parse(request.requestBody)
                let start = meeting.dates[0]
                meeting.start = Date.parse(start)
                this.meetingsList.push(meeting)
                return new Response(201, {},{})
            })

        }
    })
}
