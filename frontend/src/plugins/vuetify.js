import Vue from 'vue';
import Vuetify from 'vuetify/lib';

Vue.use(Vuetify);

export default new Vuetify({
    theme: {
        themes: {
            light: {
                primary: '#00a2e8',
                secondary: '#3f48cc',
                accent: '#99d9ea',
                error: '#FF5252',
                info: '#2196F3',
                success: '#4CAF50',
                warning: '#FFC107',
            }
        }
    }
});
